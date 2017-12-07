using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using ProductShop.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ProductsShop.Client.Core
{
    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            Console.WriteLine("Initializing database...");
            var dbInitializer = (IDbInitializer)serviceProvider.GetService(typeof(IDbInitializer));
            //dbInitializer.Reset();
            Console.WriteLine("Database initialized!");
            //var context = (ProductShopContext)serviceProvider.GetService(typeof(ProductShopContext));
            //Console.WriteLine("Seeding data...");
            //SeedDbContextWithDataFromJson(context);
            //Console.WriteLine("Data successfully inserted!");
            //ExportProductsInRangeToJson(context);
            //ExportSuccessfullySoldProductsToJson(context);
            //ExportCategoryProductsInfoToJson(context);
            //ExportUsersAndProductsToJson(context);
            dbInitializer.Reset();
            Console.WriteLine("Database initialized!");
            var context = (ProductShopContext)serviceProvider.GetService(typeof(ProductShopContext));
            Console.WriteLine("Seeding data...");
            SeedDbContextWithDataFromXml(context);
            Console.WriteLine("Data successfully inserted!");
            ExportProductsInRangeToXml(context);
            ExportSoldProductsToXml(context);
            ExportCategoriesAggregatedInfoToXml(context);
            ExportUsersAndProductsToXml(context);
        }

        private static void ImportCategoryProductsFromJson(ProductShopContext context, Random rnd, Product[] products, Category[] categories)
        {
            for (int i = 0; i < products.Length; i++)
            {
                var randomCategoryId = rnd.Next(1, categories.Length + 1);
                var categoryProd = new CategoryProduct
                {
                    CategoryId = randomCategoryId,
                    ProductId = products[i].Id
                };
                context.CategoryProducts.Add(categoryProd);
            }
            context.SaveChanges();
        }

        private static Category[] ImportCategoriesFromJson(ProductShopContext context)
        {
            var categories = JsonImport<Category>("Files/categories.json");
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return categories;
        }

        private static Product[] ImportProductsFromJson(ProductShopContext context, User[] users, Random rnd)
        {
            var products = JsonImport<Product>("Files/products.json");
            for (int i = 0; i < products.Length; i++)
            {
                var randomId = rnd.Next(0, users.Length + 1);
                var secondRandomId = rnd.Next(1, users.Length + 1);
                if (randomId == 0)
                {
                    products[i].BuyerId = null;
                }
                else
                {
                    products[i].BuyerId = randomId;
                }
                products[i].SellerId = secondRandomId;
            }
            context.Products.AddRange(products);
            context.SaveChanges();
            return products;
        }

        private static User[] ImportUsersFromJson(ProductShopContext context)
        {
            var users = JsonImport<User>("Files/users.json");
            context.Users.AddRange(users);
            context.SaveChanges();
            return users;
        }

        private static void ExportUsersAndProductsToJson(ProductShopContext context)
        {
            var reqUsers = new
            {
                usersCount = context.Users.Count(u => u.ProductsForSale.Any(pfs => pfs.BuyerId != null)),
                users = context.Users
                                        .Where(u => u.ProductsForSale.Any(pfs => pfs.BuyerId != null))
                                        .Select(u => new
                                        {
                                            firstName = u.FirstName,
                                            lastName = u.LastName,
                                            age = u.Age,
                                            soldProducts = new
                                            {
                                                count = u.ProductsForSale.Count(pfs => pfs.BuyerId != null),
                                                products = u.ProductsForSale.Where(pfs => pfs.BuyerId != null).Select(p => new
                                                {
                                                    p.Name,
                                                    p.Price
                                                })
                                            }
                                        })
                                        .OrderByDescending(x => x.soldProducts.count)
                                        .ThenBy(x => x.lastName)
                                        .ToArray()
            };
            var jsonString = JsonConvert.SerializeObject(reqUsers, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            File.WriteAllText("Files/UsersAndProducts.json", jsonString);
        }

        private static void ExportCategoryProductsInfoToJson(ProductShopContext context)
        {
            var reqCategories = context.Categories
                                        .Select(c => new
                                        {
                                            category = c.Name,
                                            productsCount = c.Products.Count,
                                            averagePrice = c.Products.Average(p => p.Product.Price),
                                            totalRevenue = c.Products.Sum(p => p.Product.Price)
                                        })
                                        .OrderBy(c => c.category)
                                        .ToArray();
            var jsonString = JsonConvert.SerializeObject(reqCategories, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            File.WriteAllText("Files/CategoriesByProductsCount.json", jsonString);
        }

        private static void ExportSuccessfullySoldProductsToJson(ProductShopContext context)
        {
            var reqUsers = context.Users
                                        .Where(u => u.ProductsForSale.Any(pfs => pfs.BuyerId != null))
                                        .Select(u => new
                                        {
                                            u.FirstName,
                                            u.LastName,
                                            soldProducts = u.ProductsForSale.Select(sp => new
                                            {
                                                sp.Name,
                                                sp.Price,
                                                buyerFirstName = sp.Buyer.FirstName,
                                                buyerLastName = sp.Buyer.LastName
                                            })
                                        })
                                        .OrderBy(u => u.FirstName)
                                        .ThenBy(u => u.LastName)
                                        .ToArray();
            var jsonString = JsonConvert.SerializeObject(reqUsers, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            File.WriteAllText("Files/SuccessfullySoldProducts.json", jsonString);
        }

        private static void ExportProductsInRangeToJson(ProductShopContext context)
        {
            var reqProducts = context.Products
                            .Select(p => new
                            {
                                p.Name,
                                p.Price,
                                seller = (p.Seller.FirstName == null ? "" : p.Seller.FirstName + " ") + p.Seller.LastName
                            })
                            .Where(p => p.Price >= 500 && p.Price <= 1000)
                            .OrderBy(p => p.Price)
                            .ToArray();
            var jsonString = JsonConvert.SerializeObject(reqProducts, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            File.WriteAllText("Files/ProductsInRange.json", jsonString);
        }

        private static void SeedDbContextWithDataFromJson(ProductShopContext context)
        {
            User[] users = ImportUsersFromJson(context);
            var rnd = new Random();
            Product[] products = ImportProductsFromJson(context, users, rnd);
            Category[] categories = ImportCategoriesFromJson(context);
            ImportCategoryProductsFromJson(context, rnd, products, categories);
        }

        private static T[] JsonImport<T>(string path)
        {
            string jsonString = File.ReadAllText(path);

            var objects = JsonConvert.DeserializeObject<T[]>(jsonString);
            return objects;
        }

        private static void SeedDbContextWithDataFromXml(ProductShopContext context)
        {
            var users = ParseUsersFromXml("Files/users.xml");
            context.AddRange(users);
            context.SaveChanges();

            var rnd = new Random();
            var products = ParseProductsFromXml("Files/products.xml");
            for (int i = 0; i < products.Count; i++)
            {
                var randomId = rnd.Next(0, users.Count + 1);
                var secondRandomId = rnd.Next(1, users.Count + 1);
                if (randomId == 0)
                {
                    products[i].BuyerId = null;
                }
                else
                {
                    products[i].BuyerId = randomId;
                }
                products[i].SellerId = secondRandomId;
            }
            context.AddRange(products);
            context.SaveChanges();

            var categories = ParseCategoryFromXml("Files/categories.xml");
            context.Categories.AddRange(categories);
            context.SaveChanges();

            for (int i = 0; i < products.Count; i++)
            {
                var randomCategoryId = rnd.Next(1, categories.Count + 1);
                var categoryProd = new CategoryProduct
                {
                    CategoryId = randomCategoryId,
                    ProductId = products[i].Id
                };
                context.CategoryProducts.Add(categoryProd);
            }
            context.SaveChanges();
        }

        private static List<User> ParseUsersFromXml(string path)
        {
            string xmlString = File.ReadAllText(path);

            var xDoc = XDocument.Parse(xmlString);

            var elements = xDoc.Root.Elements();

            var users = new List<User>();

            foreach (var element in elements)
            {
                var firstName = element.Attribute("firstName")?.Value;
                var lastName = element.Attribute("lastName").Value;

                int? age = null;
                if (element.Attribute("age") != null)
                {
                    age = int.Parse(element.Attribute("age").Value);
                }

                var user = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };

                users.Add(user);
            }

            return users;
        }

        private static List<Product> ParseProductsFromXml(string path)
        {
            string xmlString = File.ReadAllText(path);

            var xDoc = XDocument.Parse(xmlString);

            var elements = xDoc.Root.Elements();

            var products = new List<Product>();

            foreach (var element in elements)
            {
                var name = element.Element("name").Value;
                var price = 0m;
                try
                {
                    price = decimal.Parse(element.Element("price").Value);
                }
                catch (Exception)
                {
                    price = decimal.Parse(element.Element("price").Value.Replace('.', ','));
                }

                var prod = new Product()
                {
                    Name = name,
                    Price = price
                };

                products.Add(prod);
            }

            return products;
        }

        private static List<Category> ParseCategoryFromXml(string path)
        {
            string xmlString = File.ReadAllText(path);

            var xDoc = XDocument.Parse(xmlString);

            var elements = xDoc.Root.Elements();

            var categories = new List<Category>();

            foreach (var element in elements)
            {
                var name = element.Element("name").Value;

                var cat = new Category()
                {
                    Name = name
                };

                categories.Add(cat);
            }

            return categories;
        }

        private static void ExportUsersAndProductsToXml(ProductShopContext context)
        {
            var reqUsers = context.Users
                                            .Where(u => u.ProductsForSale.Where(pfs => pfs.BuyerId != null).Count() != 0)
                                             .Select(u => new
                                             {
                                                 u.FirstName,
                                                 u.LastName,
                                                 u.Age,
                                                 SoldProducts = u.ProductsForSale.Select(pfs => new
                                                 {
                                                     pfs.Name,
                                                     pfs.Price
                                                 })
                                             })
                                            .OrderByDescending(u => u.SoldProducts.Count())
                                            .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));
            xDoc.Root.SetAttributeValue("count", reqUsers.Length);
            foreach (var user in reqUsers)
            {
                var userElement = new XElement("user");
                userElement.SetAttributeValue("first-name", user.FirstName);
                userElement.SetAttributeValue("last-name", user.LastName);
                userElement.SetAttributeValue("age", user.Age);
                var userProductsElement = new XElement("sold-products");
                userProductsElement.SetAttributeValue("count", user.SoldProducts.Count());
                foreach (var item in user.SoldProducts)
                {
                    var productElement = new XElement("product");
                    productElement.SetAttributeValue("name", item.Name);
                    productElement.SetAttributeValue("price", item.Price);
                    userProductsElement.Add(productElement);
                }
                userElement.Add(userProductsElement);
                xDoc.Root.Add(userElement);
            }
            xDoc.Save("Files/UsersAndProducts.xml");
        }

        private static void ExportCategoriesAggregatedInfoToXml(ProductShopContext context)
        {
            var reqCategories = context.Categories
                                                   .Select(c => new
                                                   {
                                                       category = c.Name,
                                                       productsCount = c.Products.Count,
                                                       averagePrice = c.Products.Average(p => p.Product.Price),
                                                       totalRevenue = c.Products.Sum(p => p.Product.Price)
                                                   })
                                                   .OrderByDescending(c => c.productsCount)
                                                   .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("categories"));
            foreach (var cat in reqCategories)
            {
                var categoryElement = new XElement("category");
                categoryElement.SetAttributeValue("name", cat.category);
                categoryElement.Add(new XElement("products-count", cat.productsCount));
                categoryElement.Add(new XElement("average-price", cat.averagePrice));
                categoryElement.Add(new XElement("total-revenue", cat.totalRevenue));

                xDoc.Root.Add(categoryElement);
            }
            xDoc.Save("Files/CategoriesByProductCount.xml");
        }

        private static void ExportSoldProductsToXml(ProductShopContext context)
        {
            var reqUsers = context.Users
                                            .Where(u => u.ProductsForSale.Count != 0)
                                            .Select(u => new
                                            {
                                                u.FirstName,
                                                u.LastName,
                                                soldProducts = u.ProductsForSale.Select(pfs => new
                                                {
                                                    pfs.Name,
                                                    pfs.Price
                                                })
                                            })
                                            .OrderBy(u => u.FirstName)
                                            .ThenBy(u => u.LastName)
                                            .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));
            foreach (var user in reqUsers)
            {
                var userElement = new XElement("user");
                userElement.SetAttributeValue("first-name", user.FirstName);
                userElement.SetAttributeValue("last-name", user.LastName);

                var soldProductsElement = new XElement("sold-products");
                foreach (var sp in user.soldProducts)
                {
                    soldProductsElement.Add(new XElement("product", new XElement("name", sp.Name), new XElement("price", sp.Price)));
                }

                userElement.Add(soldProductsElement);

                xDoc.Root.Add(userElement);
            }
            xDoc.Save("Files/SoldProducts.xml");
        }

        private static void ExportProductsInRangeToXml(ProductShopContext context)
        {
            var reqProducts = context.Products
                                            .Where(p => p.BuyerId != null)
                                            .Select(p => new
                                            {
                                                p.Name,
                                                p.Price,
                                                BuyerFullName = (p.Buyer.FirstName == null ? "" : p.Buyer.FirstName + " ") + p.Buyer.LastName
                                            })
                                            .Where(p => p.Price >= 1000 && p.Price <= 2000)
                                            .OrderBy(p => p.Price)
                                            .ToArray();
            var xDoc = new XDocument();
            xDoc.Add(xDoc.Declaration);
            xDoc.Add(new XElement("products"));
            foreach (var prod in reqProducts)
            {
                var newElement = new XElement("product");
                newElement.SetAttributeValue("name", prod.Name);
                newElement.SetAttributeValue("price", prod.Price);
                newElement.SetAttributeValue("buyer", prod.BuyerFullName);
                xDoc.Root.Add(newElement);
            }
            xDoc.Save("Files/ProductsInRange.xml");
        }
    }
}
