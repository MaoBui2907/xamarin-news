using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using XamarinNews.Models;
using XamarinNews.Views;
using XamarinNews.Services;
using Xamarin.Forms;
using System.Diagnostics;

namespace XamarinNews.ViewModels
{
    class PostViewModel : BaseViewModel
    {
        public Post p { get; set; }
        public int id { get; set; }
        public bool summar { get; set; }
        PostManager postManager { get; set; }
        public Command GetPostCommand { get; set; }
        public PostViewModel(PostMeta _p)
        {
            Title = "Nội dung tóm tắt";
            p = new Post();
            summar = true;
            id = _p.Id;
            postManager = new PostManager(new RestService());
            //GetPostCommand = new Command(async () => await GetPost());
        }

        public async Task GetPost()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                p = await postManager.GetPostAsync(id, App.SummaRate);
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
