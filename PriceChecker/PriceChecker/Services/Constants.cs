using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PriceChecker
{
    public static class Constants
    {
        public const string DbName = "SqliteDB";
        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;

        public static string DbPath { get { var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); return Path.Combine(basePath, DbName); } }
    }
}
