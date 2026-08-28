using System.Collections.ObjectModel;
using Common.Domain;
using Modules.Catalog.Domain.Errors;

namespace Modules.Catalog.Domain.Products;

public class Product : Entity
{
    private const int MINIMUN_IMAGE_QUANTITY = 1;

    public string Name { get; private set; } = string.Empty;
    public Price Price { get; private set; }
    public Guid? PromotionId { get; private set; }
    private Collection<Image> Images { get; set; }

    private Product(string name, Price price, Collection<Image> images)
    {
        Name = name;
        Price = price;
        Images = images;
    }

    public static DomainResult<Product> Create(string name, Price price, Collection<Image> images)
    {

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            return ProductErrors.ProductNameIsRequired;
        }
        if(images.Count < MINIMUN_IMAGE_QUANTITY)
        {
            return ProductErrors.ProductMustHaveAtLeastOneImage;
        }
        return new Product(name, price, images);
    }

    public DomainResult<Image> AddImage(Image image)
    {
        if (Images.Contains(image))
        {
            return ProductErrors.CannotAddTheSameImageToTheProduct;
        }
        Images.Add(image);
        return image;
    }
}
