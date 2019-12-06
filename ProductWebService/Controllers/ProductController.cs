using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using ProductWebService.Models;

namespace ProductWebService.Controllers
{
    public class ProductController : ApiController
    {
        List<Product> AllInventory = new List<Product>();
        private SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=ProductDB;Integrated Security=true ;");
        private SqlDataAdapter adapter;
        DataTable dt = new DataTable();

        //1-Get request to return all products in DB
        public IEnumerable<Product> Get()
        {
            var query = "SELECT *  FROM [ProductDB].[dbo].[Products]";
            List<Product> Lproducts = new List<Product>(dt.Rows.Count);
            adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(query, conn)
            };
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow x in dt.Rows)
                {
                    Lproducts.Add(new ReadProduct(x));
                }
            }
            return Lproducts;
        }

        //2-Get Product by certain ProductID
        public IEnumerable<Product> Get(String id)
        {
            
            var query = "SELECT ProductName,ProductID,ProductPrice, Inventory,SalePercent, Category FROM [ProductDB].[dbo].[Products] Where ProductID=" + id;
            List<Product> Lproducts = new List<Product>(dt.Rows.Count);
            adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(query, conn)
            };
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow y in dt.Rows)
                {
                    Lproducts.Add(new ReadProduct(y));
                }
            }
            return Lproducts;
        }

        //3-POST method to create new product
        [HttpPost]
        public void AddProduct([FromBody]Product product)
        {
            
            
            string sqlstr = "INSERT INTO [ProductDB].[dbo].[Products] (ProductName,ProductID,ProductPrice,Inventory,SalePercent,Category) values (@ProductName,@ProductID,@ProductPrice,@Inventory,@SalePercent,@Category)";
            SqlCommand sqlCmd = new SqlCommand(sqlstr,conn);

            sqlCmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            sqlCmd.Parameters.AddWithValue("@ProductID", product.ProductID);
            sqlCmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
            sqlCmd.Parameters.AddWithValue("@Inventory", product.Inventory);
            sqlCmd.Parameters.AddWithValue("@SalePercent", product.SalePercent);
            sqlCmd.Parameters.AddWithValue("@Category", product.Category);
            conn.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            conn.Close();

        }


        //4-Delete method to delete a certain product from DB through certain ProductID
        //[ActionName("DeleteProd")]
        public void Delete(String id)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete FROM [ProductDB].[dbo].[Products] Where ProductID=" + id +"";
            sqlCmd.Connection = conn;
            conn.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
