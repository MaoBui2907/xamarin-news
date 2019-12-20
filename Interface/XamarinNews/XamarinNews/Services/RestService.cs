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

        public List<Category> categories { get; private set; }
        public List<Post> posts { get; private set; }
        public Post post { get; private set; }
        public bool status { get; private set; }
        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Category>> FetchCategoriesAsync()
        {
            categories = new List<Category>();
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

        public async Task<List<Post>> FetchPostsAsync(string path, int page)
        {
            posts = new List<Post>();
            var uri = new Uri(string.Format(Constants.PostUrl, "/fetch/", path, "/", page));
            try
            {
                var respone = await _client.GetAsync(uri);
                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsStringAsync();
                    posts = JsonConvert.DeserializeObject<List<Post>>(content);
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
            status = false;
            var uri = new Uri(string.Format(Constants.PostUrl, "/status/", path, "/", page));
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

        public async Task<Post> GetPostAsync(string ind)
        {
            post = new Post();
            var uri = new Uri(string.Format(Constants.PostUrl, "/get/", ind));
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
