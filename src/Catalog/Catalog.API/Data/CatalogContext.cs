using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext()
        {

        }

        public IMongoCollection<Product> Products { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
