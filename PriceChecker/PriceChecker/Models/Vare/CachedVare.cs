using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PriceChecker.Models
{
    public class CachedVare
    {
        [PrimaryKey, AutoIncrement]
        public new int Id { get; set; }
        public new string Navn { get; set; }
        public new double Pris { get; set; }
        public new string Url { get; set; }
        public new double MaxPris { get; set; }
        public new double MinPris { get; set; }

        private DBManager dbm;
        public CachedVare()
        {
            dbm = new DBManager();
            StartUp();
        }

        public async Task StartUp()
        {
            await dbm.OpretTabel(this);
        }

        public async Task Save()
        {
            await dbm.Insert(this);
        }
        public async Task Delete()
        {
            await dbm.Delete(this);
        }
        public async Task WipeAll()
        {
            await dbm.WipeTabelCachedVareAsync();
        }
        public async Task Update()
        {
            await dbm.Update(this);
        }
        public async Task<List<CachedVare>> GetAll()
        {
            var list = await dbm.GetStoredCachedVareAsync();
            return list;
        }

        public async Task<bool> DuplicateCheck()
        {
            return await dbm.CheckDuplicateAsync(this);
        }


    }
}
