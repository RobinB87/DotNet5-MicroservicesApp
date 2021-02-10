using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ApiCollection.Interfaces
{
    public interface ICatalogApi
    {
        Task<IEnumerable<Catalog>> GetCatalog();
        Task<Catalog> GetCatalog(string id);
        Task<IEnumerable<Catalog>> GetCatalogByName(string name);
        Task<IEnumerable<Catalog>> GetCatalogByCategory(string category);
        Task<Catalog> CreateCatalog(Catalog catalog);
    }
}