using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodFinder2.Models
{
    public class Product
    {
        [JsonProperty("image")]
        public string ImageURL { get; set; }
        //[JsonProperty("superDepartment")]
        //public string SuperDepartment { get; set; }
        //[JsonProperty("tpnb")]
        //public long TPNB { get; set; }
        [JsonProperty("ContentsMeasureType")]
        public string ContentsMeasureType { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("UnitOfSale")]
        public int UnitOfSale { get; set; }
        //[JsonProperty("description")]
        //public IEnumerator<string> LstDescription { get; set; }
        //public string Description { get; set; }
        //[JsonProperty("AverageSellingUnitWeight")]
        //public decimal AverageSellingUnitWeight { get; set; }
        [JsonProperty("UnitQuantity")]
        public string UnitQuantity { get; set; }
        [JsonProperty("id")]
        public long ID { get; set; }
        //[JsonProperty("ContentsQuantity")]
        //public int ContentsQuantity { get; set; }
        [JsonProperty("department")]
        public string Department { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        //[JsonProperty("unitprice")]
        //public decimal UnitPrice { get; set; }

    }
}