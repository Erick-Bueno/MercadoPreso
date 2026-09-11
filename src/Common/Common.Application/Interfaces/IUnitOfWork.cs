namespace Common.Application.Interfaces;

public interface IUnitOfWork
{
    public Task SaveChanges();
}