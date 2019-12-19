using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using XamarinNews.Models;

namespace XamarinNews.Services
{
    public class PostManager
    {
        IRestService restService;

        public PostManager(IRestService service)
        {
            restService = service;
        }
        public Task<List<Post>> FetchPostsAsync(string path, int page)
        {
            return restService.FetchPostsAsync(path, page);
        }

        public Task<Post> GetPostAsync(string ind)
        {
            return restService.GetPostAsync(ind);
        }
    }
}
