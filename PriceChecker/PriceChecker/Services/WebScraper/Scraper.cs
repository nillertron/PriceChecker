using HtmlAgilityPack;
using PriceChecker.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Webscraper
{
    class Scraper
    {
        public async Task<IList> GetVareListe(string url, string site, CancellationTokenSource cts)
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
            string html = string.Empty;

            try
            {
                html = await client.GetStringAsync(url);
            }
            catch (Exception e)
            {

            }
            if (site == "dba")
            {

                var list = await FetchDBAVare(html, cts.Token);

                return list;

            }
            else if (site == "ebay")
            {
                var list = await FetchEbayVare(html, cts.Token);
                return list;
            }
            else if (site == "guloggratis")
            {

                    var list = await FetchGulOgGratisVare(html, cts.Token);

                return list;
            }
            return null;

        }
        public async Task<List<GulOgGratisVare>> FetchGulOgGratisVare(string html, CancellationToken cts)
        {
            var returnList = new List<GulOgGratisVare>();

            await Task.Run(async () =>
            {
                var document = new HtmlDocument();
                document.LoadHtml(html);
                var htmlList = document.DocumentNode.Descendants("div").Where(o => o.GetAttributeValue("class", "").Equals("css-othkjz-ContentSpace-ContentSpace ekm5km11")).ToList();

                var linklist = htmlList[0].Descendants("a").GroupBy(o => o.GetAttributeValue("href", "")).ToList();
                var productList = htmlList[0].Descendants("a").Where(o => o.GetAttributeValue("class", "").Equals("_30CmEGfUpAWIG9LQd_JWwn css-13vvkp0-StyledLink e1xkrxbr3")).ToList();
                var count = 0;
                var cancelLoop = false;
                    productList.ForEach(o =>
                    {

                    if (cts.IsCancellationRequested)
                        cancelLoop = true;
                        if (!cancelLoop)
                        {
                            var url = "https://www.guloggratis.dk" + linklist[count].Key;
                            var vare = o.Descendants("h2").Where(x => x.GetAttributeValue("class", "").Contains("_3G0U8VDIR4I04rmWYnNIxv")).FirstOrDefault().InnerText;
                            var tempPris = o.Descendants("p").Where(p => p.GetAttributeValue("class", "").Contains("gR4KW2Pe_H2Krgzwgw5YZ")).FirstOrDefault().InnerText;
                            var pris = Regex.Match(tempPris, @"\d+.\d+").Value;
                            var money = 0.00;
                            if (pris.Length > 0)
                                money = Double.Parse(pris);
                            returnList.Add(new GulOgGratisVare { Navn = vare, Pris = money, Url = url });
                            count++;
                        }
                    });


            });

            return returnList;
        }

        private async Task<List<Dbavare>> FetchDBAVare(string html, CancellationToken cts)
        {
            var returnListe = new List<Dbavare>();

            await Task.Run(async () =>
            {
                var document = new HtmlDocument();
                document.LoadHtml(html);
                var HtmlList = document.DocumentNode.Descendants("td").Where(o => o.GetAttributeValue("class", "").Equals("mainContent")).ToList();
                var itemListe = new List<string>();
                HtmlList.ForEach(o => { itemListe.Add(o.Descendants("script").Where(x => x.GetAttributeValue("type", "").Equals("application/ld+json")).FirstOrDefault().InnerText); });
                var navn = "";
                string tempPris = "";
                string pris = "";
                double money = 0;
                string url = "";
                bool cancelLoop = false;

                    itemListe.ForEach(o =>
                    {
                        //cts.ThrowIfCancellationRequested();
                        if (cts.IsCancellationRequested)
                            cancelLoop = true;
                        if (!cancelLoop)
                        {
                            navn = o.Substring(o.IndexOf("name\": \"") + 8);
                            navn = navn.Substring(0, navn.IndexOf("\","));
                            url = o.Substring(o.IndexOf("url\": \"") + 7);
                            url = url.Substring(0, url.IndexOf("\","));
                            tempPris = o.Substring(o.IndexOf("price\": \"") + 8);
                            pris = Regex.Match(tempPris, @"\d+.\d+").Value;
                            if (pris.Length > 0)
                                money = Double.Parse(pris);
                            returnListe.Add(new Dbavare { Navn = navn, Pris = money, Url = url });
                        }
                    });
               
            });



            return returnListe;

        }


        public async Task<List<Ebayvare>> FetchEbayVare(string html, CancellationToken cts)
        {
            var returnList = new List<Ebayvare>();

            await Task.Run(async () =>
           {
               var document = new HtmlDocument();
               document.LoadHtml(html);
               var ProductList = document.DocumentNode.Descendants("ul").Where(o => o.GetAttributeValue("id", "").Contains("ListViewInner")).ToList();
               var ProductListItems = ProductList[0].Descendants("li").Where(o => o.GetAttributeValue("id", "").Contains("item")).ToList();

               foreach (var productlistitem in ProductListItems)
               {
                   //Item id
                   var id = productlistitem.GetAttributeValue("listingid", "");
                   //item navn
                   var titel = productlistitem.Descendants("h3").Where(o => o.GetAttributeValue("class", "").Equals("lvtitle")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t');
                   //Item price
                   var b4price = Regex.Match(
                    productlistitem.Descendants("li").Where(o => o.GetAttributeValue("class", "").Equals("lvprice prc")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t'), @"\d+.\d+");
                   var money = 0.00;
                   Double.TryParse(b4price.ToString(), out money);
                   //url
                   var url = productlistitem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
                   returnList.Add(new Ebayvare { Navn = titel, Pris = money, Url = url });
                   Console.WriteLine();

               }

           });

            return returnList;
        }
    }
}
