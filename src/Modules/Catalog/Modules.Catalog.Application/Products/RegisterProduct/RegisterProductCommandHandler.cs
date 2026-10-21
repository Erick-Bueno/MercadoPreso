using Common.Application.Interfaces;
using Common.Domain;
using Common.Domain.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Application.Products.Interfaces;
using Modules.Catalog.Domain.Products;
using Modules.Catalog.Domain.Products.Events;

namespace Modules.Catalog.Application.Products.RegisterProduct;

public class RegisterProductCommandHandler(
    IProductRepository productRepository,
    [FromKeyedServices("catalog")] IUnitOfWork unitOfWork
) : ICommandHandler<RegisterProductCommand, RegisterProductResponse>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RegisterProductResponse>> Handle(
        RegisterProductCommand command,
        CancellationToken cancellationToken
    ) =>
        Price
            .Create(command.Price)
            .Bind(price => Product.Create(command.Name, price))
            .Tap(_productRepository.Create)
            .Tap(async _ => await _unitOfWork.SaveChanges(cancellationToken))
            .Map(product => new RegisterProductResponse(product.Id));
}
