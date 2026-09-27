using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Modules.Catalog.Endpoints;
public sealed class CatalogGroup : Group
{
    public CatalogGroup()
    {
        Configure("catalog", ep => ep.Description(x => x.WithTags("Catalog")));
    }
}