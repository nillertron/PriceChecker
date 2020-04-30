using System;
using System.Threading.Tasks;

namespace Webscraper
{
    class Program
    {
        static void Main(string[] args)
        {
            GetASite();
            Console.ReadLine();
        }

        async static Task GetASite()
        {
            //var url = "https://www.ebay.com/sch/i.html?_nkw=xbox+one&_in_kw=1&_ex_kw=&_sacat=0&LH_Complete=1&_udlo=&_udhi=&_samilow=&_samihi=&_sadis=15&_stpos=&_sargn=-1%26saslc%3D1&_salic=1&_sop=12&_dmd=1&_ipg=50&_fosrp=1";
            var url = "https://www.dba.dk/soeg/?soeg=iphone";
            var scraper = new Scraper();
            await scraper.GetVareListe(url,"dba");
        }
    }
}
