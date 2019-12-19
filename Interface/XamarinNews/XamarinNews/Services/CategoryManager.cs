using System.Collections.Generic;
using System.Threading.Tasks;

using XamarinNews.Models;

namespace XamarinNews.Services
{
    public class CategoryManager
    {
        IRestService restService;

        public CategoryManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<Category>> FetchCategoriesAsync()
        {
            return restService.FetchCategoriesAsync();
        }
    }
}
