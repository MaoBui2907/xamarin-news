using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;

using XamarinNews.Models;
using XamarinNews.Views;
using XamarinNews.Services;
namespace XamarinNews.ViewModels
{
    public class PostListViewModel : BaseViewModel
    {
        public ObservableCollection<Post> posts { get; set; }
        public bool hasMore { get; set; }
        public Command FetchPostListCommand { get; set; }
        public Command CheckMorePostCommand { get; set; }
        PostManager postManager { get; set; }

        public PostListViewModel(List<string> t, int page)
        {
            Title = "Hell";
            postManager = new PostManager(new RestService());
            FetchPostListCommand = new Command(async () => await FetchPostList(t[3], page));
            CheckMorePostCommand = new Command(async () => await CheckMorePost(t[3], page));
        }
        
        async Task CheckMorePost(string path, int _p)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                hasMore = await postManager.CheckMorePost(path, _p);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task FetchPostList(string path, int _p)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                posts.Clear();
                var items = await postManager.FetchPostsAsync(path, _p);
                foreach (var item in items)
                {
                    posts.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
