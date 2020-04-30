using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceChecker.Models
{
    public class DBManager
    {
        private static SQLiteAsyncConnection conn;
        public DBManager()
        {
            if(conn == null)
            conn = new SQLiteAsyncConnection(Constants.DbPath, Constants.Flags);

        }

        public async Task OpretTabel<T>(T data)
        {
            await conn.CreateTableAsync(data.GetType());
        }

        //public async Task<List<T>> GetAll<T>(T Data) where T:new()
        //{
        //    var type = Data.GetType();
        //    var liste = new List<type>();
        //    var list = await conn.Table<T>().ToListAsync();
        //    return list;
        //}

        public async Task<List<StoredSearch>> GetStoredSearchesAsync()
        {
            var list = await conn.Table<StoredSearch>().ToListAsync();
            return list;
        }
        public async Task<List<Settings>> GetStoredSettingsAsync()
        {
            var list = await conn.Table<Settings>().ToListAsync();
            return list;
        }

        public async Task WipeTabelStoredSearchAsync()
        {
            await conn.DeleteAllAsync<StoredSearch>();
        }
        public async Task WipeTabelCachedVareAsync()
        {
            await conn.DeleteAllAsync<CachedVare>();
        }

        public async Task<List<CachedVare>> GetStoredCachedVareAsync()
        {
            var list = await conn.Table<CachedVare>().ToListAsync();
            return list;
        }
        public async Task<bool> CheckDuplicateAsync(CachedVare v)
        {
            var list = await conn.Table<CachedVare>().ToListAsync();
            var tjek = list.Where(x => x.Url == v.Url).FirstOrDefault();
            if (tjek == null)
                return false;
            else
                return true;
        }

        //Generiske metoder
        public async Task Insert<T>(T Data)
        {
            await conn.InsertAsync(Data);
        }

        public async Task Delete<T>(T Data)
        {
            await conn.DeleteAsync(Data);
        }
        public async Task Update<T>(T Data)
        {
            await conn.UpdateAsync(Data);
        }

    }
}
