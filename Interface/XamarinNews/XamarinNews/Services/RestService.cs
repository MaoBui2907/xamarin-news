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

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            categories = new List<Category>();
            var uri = new Uri(string.Format(Constants.CategoryUrl, string.Empty));
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

        public async Task<List<Post>> GetPostsAsync()
        {
            posts = new List<Post>();
            var uri = new Uri(string.Format(Constants.PostUrl, string.Empty));
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
    }
}
