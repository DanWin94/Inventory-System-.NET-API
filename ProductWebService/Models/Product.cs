using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ProductWebService.Models
{
    public class Product
    {
      
            public String ProductName { get; set; }
            public String ProductID { get; set; }
            public double ProductPrice { get; set; }
            public int Inventory { get; set; }
            public int SalePercent { get; set; }
            public String Category { get; set; }
        }

        public class ReadProduct : Product
        {
            public ReadProduct(DataRow row)
            {
                ProductName = row["ProductName"].ToString();
                ProductID = row["ProductID"].ToString();
                ProductPrice = Convert.ToDouble(row["ProductPrice"]);
                Inventory = Convert.ToInt32(row["Inventory"]);
                SalePercent = Convert.ToInt32(row["salePercent"]);
                Category = row["Category"].ToString();
            }
        }
    
}