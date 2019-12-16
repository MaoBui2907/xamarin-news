using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamarinNews.Database;
using SQLite;
using System.IO;
using XamarinNews.Droid.Database;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDb))]
namespace XamarinNews.Droid.Database
{
     class SQLiteDb : ISQLiteDb
    {
       public SQLiteAsyncConnection GetConnection()
        {
            var documentsPATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPATH, "MySQLite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}