using System.Text.Json;
using Common.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Infrastructure.TransactionOutbox;

// Interceptor executado durante o SaveChanges/SaveChangesAsync,
// antes da operação de persistência das alterações no banco.
public class OutboxInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        // Obtém o DbContext responsável pelo SaveChangesAsync que está sendo executado.
        var context = eventData.Context;

        if (context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        //obter todas as entidades registradas no dbset do context especifico e filtrar pelo numero eventos 
        var entities = context.ChangeTracker
         .Entries<IEvent>()
         .Where(entity => entity.Entity.GetDomainEvents().Count != 0)
         .ToList();


        // Para cada entidade com Domain Events:
        // 1. copia os eventos para memória;
        // 2. limpa os eventos da entidade para evitar processá-los novamente;
        // 3. transforma cada Domain Event em uma mensagem da Outbox.
        var messages = entities.SelectMany(entity =>
        {
            var events = entity.Entity.GetDomainEvents().ToArray();
            entity.Entity.ClearEvents();
            return events.Select(domainEvent =>

                Outbox.Create(domainEvent.GetType().FullName!, JsonSerializer.Serialize(domainEvent, domainEvent.GetType()))
                );
        });

        context.Set<Outbox>().AddRange(messages);
        return base.SavingChangesAsync(eventData, result, cancellationToken);

    }
}