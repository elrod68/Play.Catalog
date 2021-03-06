using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play.Contracts
{
    public record CatalogItemCreated(Guid ItemId, string Name, string Description);
    public record CatalogItemUpdated(Guid ItemId, string Name, string Description);
    public record CatalogItemDeleted(Guid ItemId);
}
