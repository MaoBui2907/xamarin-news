using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinNews.Database;
using Foundation;
using UIKit;
using SQLite;
using System.IO;
namespace XamarinNews.iOS.Database
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPATH, "MySQLite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}