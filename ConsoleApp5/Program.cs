using ConsoleApp5.Dbcontext;
using ConsoleApp5.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp5
{
    class Program { 


   
        static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                bool keepRunning = true;
                while (keepRunning)
                {
                    Console.WriteLine("1. Get All Products");
                    Console.WriteLine("2. Get Product by ID");
                    Console.WriteLine("3. Insert Product");
                    Console.WriteLine("4. Delete Product");
                    Console.WriteLine("5. Update Product");
                    Console.WriteLine("6. Exit");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            GetAllProducts(context);
                            break;
                        case "2":
                            GetProductById(context);
                            break;
                        case "3":
                            InsertProduct(context);
                            break;
                        case "4":
                            DeleteProduct(context);
                            break;
                        case "5":
                            UpdateProduct(context);
                            break;
                        case "6":
                            keepRunning = false;
                            break;
                    }
                }
            }
        }

        static void GetAllProducts(AppDbContext context)
        {
            var products = context.Products.Include(p => p.Category).ToList();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Category: {product.Category.CategoryName}");
            }
        }

        static void GetProductById(AppDbContext context)
        {
            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine());
            var product = context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Category: {product.Category.CategoryName}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void InsertProduct(AppDbContext context)
        {
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Product Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            var product = new Product
            {
                Name = name,
                Price = price,
                CategoryId = categoryId
            };

            context.Products.Add(product);
            context.SaveChanges();
            Console.WriteLine("Product added successfully.");
        }

        static void DeleteProduct(AppDbContext context)
        {
            Console.Write("Enter Product ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            var product = context.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void UpdateProduct(AppDbContext context)
        {
            Console.Write("Enter Product ID to update: ");
            int id = int.Parse(Console.ReadLine());
            var product = context.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                Console.Write("Enter new Product Name: ");
                product.Name = Console.ReadLine();

                Console.Write("Enter new Product Price: ");
                product.Price = decimal.Parse(Console.ReadLine());

                Console.Write("Enter new Category ID: ");
                product.CategoryId = int.Parse(Console.ReadLine());

                context.SaveChanges();
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
    }


}
