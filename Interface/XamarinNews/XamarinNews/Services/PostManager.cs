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
        public Task<List<PostMeta>> FetchPostsAsync(string path, int page)
        {
            return restService.FetchPostsAsync(path, page);
        }

        public Task<Post> GetPostAsync(int ind)
        {
            return restService.GetPostAsync(ind);
        }

        public Task<bool> CheckMorePost(string path, int page)
        {
            return restService.CheckMorePostAsync(path, page);
        }
    }
}
