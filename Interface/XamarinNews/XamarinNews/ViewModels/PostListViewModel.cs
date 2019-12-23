using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;

using XamarinNews.Models;
using XamarinNews.Services;

namespace XamarinNews.ViewModels
{
    public class PostListViewModel : BaseViewModel
    {
        public ObservableCollection<Post> posts { get; set; }
        public bool hasMore { get; set; }
        public Command<List<string>> FetchPostListCommand { get; set; }
        public Command<List<string>> CheckMorePostCommand { get; set; }
        PostManager postManager { get; set; }
        public PostListViewModel(List<string> t, int page)
        {
            Title = t[1];
            postManager = new PostManager(new RestService());
            posts = new ObservableCollection<Post>();
            FetchPostListCommand = new Command<List<string>>(FetchPostList);
            CheckMorePostCommand = new Command<List<string>>(CheckMorePost);
            FetchPostListCommand.Execute(new List<string> { t[3], page.ToString() });
            CheckMorePostCommand.Execute(new List<string> { t[3], page.ToString() });
        }

        async void CheckMorePost(List<string> _c)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            string path = _c[0];
            int _p = int.Parse(_c[1]);

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

        async void FetchPostList(List<string> _c)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            string path = _c[0];
            int _p = int.Parse(_c[1]);

            try
            {
                posts.Clear();
                List<Post> items = await postManager.FetchPostsAsync(path, _p);
                posts = new ObservableCollection<Post>(items);
                Console.WriteLine(posts.Count);
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
