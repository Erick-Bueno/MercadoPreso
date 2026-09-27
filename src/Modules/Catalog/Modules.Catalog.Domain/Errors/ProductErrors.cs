using System.Net;
using Common.Domain.Enums;
using Common.Domain.Errors;

namespace Modules.Catalog.Domain.Errors;


public sealed record ProductErrors
{
    public static readonly DomainError ProductNameIsRequired = new(HttpStatusCode.BadRequest, "O Nome do produto é obrigatório", ErrorType.Validation);
    public static readonly DomainError CannotAddTheSameImageToTheProduct = new(HttpStatusCode.BadRequest, "Não é possivel adicionar a mesma imagem a um produto", ErrorType.Validation);
    public static readonly DomainError ProductMustHaveAtLeastOneImage = new(HttpStatusCode.BadRequest, "O produto deve possuir pelo menos uma imagem", ErrorType.Validation);
    public static readonly DomainError ProductNotExists = new(HttpStatusCode.BadRequest, "Produto não existe", ErrorType.Validation);
    public static readonly DomainError ProductDoesNotHavePromotion = new(HttpStatusCode.BadRequest, "Produto não possui promoção", ErrorType.Validation);
    public static readonly DomainError ProductAlreadyHasAnActivePromotion = new(HttpStatusCode.BadRequest, "O produto ja possui uma promoção ativa", ErrorType.Validation);

}