using Xamarin.Forms;

namespace XamarinNews.Services
{
    public static class Constants
    {
        // The iOS simulator can connect to localhost. However, Android emulators must use the 10.0.2.2 special alias to your host loopback interface.
        public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "http://40.119.210.85:1998/" : "http://40.119.210.85:1998/";
        public static string CategoryUrl = BaseAddress + "api/category";
        public static string PostUrl = BaseAddress + "api/news/{0}/{1}/{2}";
        public static string GetPostUrl = BaseAddress + "api/news/get/{0}";
    }
}
