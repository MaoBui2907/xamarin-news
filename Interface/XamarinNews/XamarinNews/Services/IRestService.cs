using System.Collections.Generic;
using System.Threading.Tasks;

using XamarinNews.Models;

namespace XamarinNews.Services
{
    public interface IRestService
    {
        Task<List<Category>> FetchCategoriesAsync();
        Task<List<Post>> FetchPostsAsync(string path);
        Task<Post> GetPostAsync(string ind);

    }
}
