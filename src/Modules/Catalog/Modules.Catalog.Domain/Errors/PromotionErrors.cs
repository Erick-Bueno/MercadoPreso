using System.Net;
using Common.Domain.Enums;
using Common.Domain.Errors;

namespace Modules.Catalog.Domain.Errors;


public sealed record PromotionErrors
{
    public static readonly DomainError ProductLimitReached = new(HttpStatusCode.BadRequest ,"Limite de produtos atingido", ErrorType.Validation);
    public static readonly DomainError EndDateCannotBeBeforeStartDate = new(HttpStatusCode.BadRequest, "A data final não pode ser antes da data inicial", ErrorType.Validation);
    public static readonly DomainError InvalidPercentage = new (HttpStatusCode.BadRequest, "Percentual inválido", ErrorType.Validation);
    public static readonly DomainError InvalidTitle = new (HttpStatusCode.BadRequest, "Titulo de promoção inválido", ErrorType.Validation);
    public static readonly DomainError PromotionNotExists = new(HttpStatusCode.BadRequest, "Promoção não existe", ErrorType.Validation);
    public static readonly DomainError InvalidPromotion = new(HttpStatusCode.BadRequest, "Promoção inválida", ErrorType.Validation);
    public static readonly DomainError IsDisabled = new(HttpStatusCode.BadRequest, "Promoção desativada", ErrorType.Validation);
}