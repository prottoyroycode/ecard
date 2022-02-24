using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Catalog
{


    /// <summary>
    /// Properties comming from rockvill api  https://ezvoucher.rockvillegroup.com/ezvoucher/catalogs?limit=10&offset=0
    /// </summary>
    public class CatalogueResponse
    {
        public bool success { get; set; }
        public int responseCode { get; set; }
        public string description { get; set; }
        public Data data { get; set; }
    }


    public class Data
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }

        public List<Catalogue> results { get; set; }
        public List<string> favorites { get; set; }

    }
    public class Catalogue
    {

        public string sku { get; set; }
        public string title { get; set; }
        public string upc { get; set; }
        public string description { get; set; }
        public Currency currency { get; set; }
        public List<Categories> categories { get; set; }
        public Regions regions { get; set; }
        public showing_price showing_price { get; set; }
        public string image { get; set; }
        public decimal min_price { get; set; }
        public string min_price_str { get; set; }
        public decimal max_price { get; set; }
        public string price_fix { get; set; }
        public bool pre_order { get; set; }
       
    }

    public class Currency
    {
        public string id { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string symbol { get; set; }
        public string code { get; set; }
    }

    public class Categories
    {
        public string name { get; set; }
    }

    public class Regions
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class showing_price
    {
        public string id { get; set; }
        public string price { get; set; }

        public showing_currency showing_currency { get; set; }
    }

    public class showing_currency
    {
        public string country { get; set; }
        public string currency { get; set; }
        public string symbol { get; set; }
        public string code { get; set; }
    }


}
