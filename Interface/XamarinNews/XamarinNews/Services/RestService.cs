using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

using XamarinNews.Models;

namespace XamarinNews.Services
{
    public class RestService : IRestService
    {
        HttpClient _client;
        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Category>> FetchCategoriesAsync()
        {
            List<Category> categories = new List<Category>();
            var uri = new Uri(string.Format(Constants.CategoryUrl, "/fetch"));
            try
            {
                var respone = await _client.GetAsync(uri);
                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return categories;
        }

        public async Task<List<PostMeta>> FetchPostsAsync(string path, int page)
        {
            List<PostMeta> posts = new List<PostMeta>();
            var uri = new Uri(string.Format(Constants.PostUrl, "fetch", path, page));
            try
            {
                var respone = await _client.GetAsync(uri);
                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsStringAsync();
                    posts = JsonConvert.DeserializeObject<List<PostMeta>>(content);
                }
            } 
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return posts;
        }

        public async Task<bool> CheckMorePostAsync(string path, int page)
        {
            bool status = false;
            var uri = new Uri(string.Format(Constants.PostUrl, "status", path, page));
            try
            {
                var respone = await _client.GetAsync(uri);
                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsStringAsync();
                    status = JsonConvert.DeserializeObject<bool>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return status;
        }

        public async Task<Post> GetPostAsync(int ind, int rate)
        {
            Post post = new Post();
            var uri = new Uri(string.Format(Constants.GetPostUrl, ind, rate));
            try
            {
                var respone = await _client.GetAsync(uri);
                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsStringAsync();
                    post = JsonConvert.DeserializeObject<Post>(content);
                }
            } 
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return post;
        }
    }
}
