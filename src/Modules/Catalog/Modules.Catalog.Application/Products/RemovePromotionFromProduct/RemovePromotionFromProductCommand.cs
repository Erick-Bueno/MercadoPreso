using System.Windows.Input;
using Common.Application.Interfaces;
using Common.Domain;

namespace Modules.Catalog.Application.Products.RemovePromotionFromProduct;

public record RemovePromotionFromProductCommand(Guid ProductId, Guid PromotionId) : ICommand<Unit>;