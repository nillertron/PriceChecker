using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PriceChecker.Models
{
   public class StoredSearch
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string SearchWord { get; set; }
        public double MaxPrice { get; set; }

        public double MinPrice { get; set; }
        private DBManager dbm;
        public StoredSearch()
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
        public async Task<List<StoredSearch>> GetAll()
        {
            var list = await dbm.GetStoredSearchesAsync();
            return list;
        }
    }
}
