using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace XamarinNews.Database
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();


    }
}
