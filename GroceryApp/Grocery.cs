using GroceryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryApp
{
    public class Grocery
    { 
        public static List<Product> GetAllProducts(GroceryAppContext db)
        {
            List<Product>? products;

           
            products = db.Products.ToList();

            return products;
        }


        public static void AddProducts(GroceryAppContext db,Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        

        public static void AddCategory(GroceryAppContext db, Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static Product? GetProductById(GroceryAppContext db,int id)
        {
            Product? product = db.Products.Find(id);
            return product;
        }

        public static void EditProductById(GroceryAppContext db,int id)
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static List<Product> SearchProduct(GroceryAppContext db,int category_id)
        {
            List<Product> product = db.Products.Where(e =>e.CategoryId==category_id).ToList();
            return product;
        }

        public static List<Product> SearchProductByNameAndPrice(GroceryAppContext db, string product_name,decimal price)
        {
            List<Product> product = db.Products.Where(p => (p.ProductName == product_name && p.UnitPrice <= price)).ToList();
            return product;
        }


    }
}
