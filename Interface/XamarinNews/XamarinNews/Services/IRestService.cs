using System.Collections.Generic;
using System.Threading.Tasks;

using XamarinNews.Models;

namespace XamarinNews.Services
{
    public interface IRestService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Post>> GetPostsAsync();

    }
}
