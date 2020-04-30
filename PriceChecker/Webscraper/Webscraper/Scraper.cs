using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Webscraper
{
    class Scraper
    {
        public async Task<IList> GetVareListe(string url, string site)
        {
            var client = new HttpClient();
            var html = await client.GetStringAsync(url);
            if(site == "dba")
            {
                var list =  await FetchDBAVare(html);
                return list;

            }
            return null;

        }


        public async Task<List<DbaVare>> FetchDBAVare(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var HtmlList = document.DocumentNode.Descendants("td").Where(o => o.GetAttributeValue("class", "").Equals("mainContent")).ToList();
            var itemListe = new List<string>();
            var returnListe = new List<DbaVare>();
            HtmlList.ForEach(o => { itemListe.Add(o.Descendants("script").Where(x => x.GetAttributeValue("type","").Equals("application/ld+json")).FirstOrDefault().InnerText);  });
            var navn = "";
            string tempPris = "";
            string pris = "";
            double money = 0;
            string url = "";
            itemListe.ForEach(o => { Console.WriteLine(o); });

            itemListe.ForEach(o => 
            {
                navn = o.Substring(o.IndexOf("name\": \"") + 8);
                navn = navn.Substring(0, navn.IndexOf("\","));
                url = o.Substring(o.IndexOf("url\": \"") + 7);
                url = url.Substring(0, url.IndexOf("\","));
                tempPris = o.Substring(o.IndexOf("price\": \"") + 8);
                pris = Regex.Match(tempPris, @"\d+.\d+").Value;
                if (pris.Length > 0)
                    money = Double.Parse(pris);
                returnListe.Add(new DbaVare { Navn = navn, Pris = money, Url = url });

            });


            return returnListe;

        }


        //public async Task MakeHtmlDocument(string html)
        //{
        //    var document = new HtmlDocument();
        //    document.LoadHtml(html);
        //    var ProductList = document.DocumentNode.Descendants("ul").Where(o => o.GetAttributeValue("id", "").Equals("ListViewInner")).ToList();
        //    var ProductListItems = ProductList[0].Descendants("li").Where(o => o.GetAttributeValue("id", "").Contains("item")).ToList();

        //    foreach (var productlistitem in ProductListItems)
        //    {
        //        //Item id
        //        Console.WriteLine(productlistitem.GetAttributeValue("listingid",""));
        //        //item navn
        //        Console.WriteLine(productlistitem.Descendants("h3").Where(o => o.GetAttributeValue("class", "").Equals("lvtitle")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t'));
        //        //Item price
        //        Console.WriteLine(Regex.Match(
        //            productlistitem.Descendants("li").Where(o => o.GetAttributeValue("class", "").Equals("lvprice prc")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t'), @"\d+.\d+"));
        //        Console.WriteLine(productlistitem.Descendants("a").FirstOrDefault().GetAttributeValue("href", ""));
        //        Console.WriteLine(productlistitem.GetAttributeValue("a href",""));
        //        Console.WriteLine();

        //    }
        //    Console.WriteLine();
        //}
    }
}
