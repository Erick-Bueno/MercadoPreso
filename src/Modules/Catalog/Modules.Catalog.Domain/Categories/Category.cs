using System.IO.Pipelines;
using Common.Domain;
using Modules.Catalog.Domain.Errors;

namespace Modules.Catalog.Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    public string Name { get; private set; }
    public CategoryId? ParentId { get; private set; }
    public bool Active { get; private set; }
    private Category(CategoryId id, string name, CategoryId? parentId) : base(id)
    {
        Name = name;
        ParentId = parentId;
        Active = true;
    }

    public static Result<Category> Create(string name, CategoryId? parentId)
    {
        if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            return CategoryErrors.CategoryNameIsRequired;
        }
        return new Category(CategoryId.Create(), name, parentId);
        
    }

    public void Deactivate()
    {
        Active = false;
    }
    public void Activate()
    {
        Active = true;
    }
}

public record CategoryId(Guid Value)
{
    public static CategoryId Create() => new(Guid.CreateVersion7());
};