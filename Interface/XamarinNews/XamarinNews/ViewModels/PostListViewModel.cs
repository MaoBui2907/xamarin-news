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
        public ObservableCollection<PostMeta> Posts { get; set; }
        public string _path { get; set; }
        public int _page { get; set; }
        public bool hasMore { get; set; }
        public bool hasPrev { get; set; }
        public Command FetchPostListCommand { get; set; }
        public Command CheckMorePostCommand { get; set; }
        PostManager postManager { get; set; }
        public PostListViewModel(List<string> t, int page)
        {
            Title = t[1];
            _path = t[3];
            _page = page;
            hasPrev = false;
            postManager = new PostManager(new RestService());
            Posts = new ObservableCollection<PostMeta>();
        }

        public async Task CheckMorePost()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                hasMore = await postManager.CheckMorePost(_path, _page);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            Console.WriteLine("shit");
            Console.WriteLine(hasMore);
        }

        public async Task FetchPostList()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Posts.Clear();
                Posts = new ObservableCollection<PostMeta>(await postManager.FetchPostsAsync(_path, _page));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            Console.WriteLine(Posts.Count);
        }
    }
}
