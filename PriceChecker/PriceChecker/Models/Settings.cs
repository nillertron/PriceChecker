using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriceChecker.Models
{
    public class Settings
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public bool ScraperActive { get; set; }
        public int IntervalHours { get; set; }

        private DBManager dbm;
        public Settings()
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
        public async Task Update()
        {
            await dbm.Update(this);
        }
        public async Task<List<Settings>> GetAll()
        {
            var list = await dbm.GetStoredSettingsAsync();
            return list;
        }
    }
}
