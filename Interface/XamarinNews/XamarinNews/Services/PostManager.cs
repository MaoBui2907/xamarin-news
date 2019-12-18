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

        public Task<List<Post>> GetPostsAsync()
        {
            return restService.GetPostsAsync();
        }
    }
}
