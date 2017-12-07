using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StartUp
{
    public static void Main()
    {
        using (var db = new BookShopContext())
        {
            DbInitializer.ResetDatabase(db);

            //var command = int.Parse(Console.ReadLine());

            Console.WriteLine(RemoveBooks(db) + " books were deleted");
        }
    }

    //Task 1
    public static string GetBooksByAgeRestriction(BookShopContext context, string command)
    {
        var cmd = command[0].ToString().ToUpper() + string.Join("", command.Skip(1)).ToLower();
        var enumVal = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), cmd);
        var bookNames = context.Books
                            .Where(b => b.AgeRestriction == enumVal)
                            .Select(b => b.Title)
                            .OrderBy(b => b);
        return string.Join(Environment.NewLine, bookNames);
    }

    //Task 2
    public static string GetGoldenBooks(BookShopContext context)
    {
        var enumVal = (EditionType)Enum.Parse(typeof(EditionType), "Gold");
        var bookNames = context.Books
                            .Where(b => b.EditionType == enumVal)
                            .Where(b => b.Copies < 5000)
                            .OrderBy(b => b.BookId)
                            .Select(b => b.Title);
        return string.Join(Environment.NewLine, bookNames);
    }

    //Task 3
    public static string GetBooksByPrice(BookShopContext context)
    {
        var enumVal = (EditionType)Enum.Parse(typeof(EditionType), "Gold");
        var books = context.Books
                            .Select(b => new
                            {
                                b.Title,
                                b.Price
                            })
                            .Where(b => b.Price > 40)
                            .OrderByDescending(b => b.Price)
                            .ThenBy(b => b.Title);
        var sb = new StringBuilder();
        foreach (var book in books)
        {
            sb.AppendLine($"{book.Title} - ${book.Price:f2}");
        }
        return sb.ToString();
    }

    //Task 4
    public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
    {
        var enumVal = (EditionType)Enum.Parse(typeof(EditionType), "Gold");
        var books = context.Books
                            .OrderBy(b => b.BookId)
                            .Select(b => new
                            {
                                b.Title,
                                ReleaseDate = (DateTime)b.ReleaseDate
                            })
                            .Where(b => b.ReleaseDate.Year != year);
        var sb = new StringBuilder();
        foreach (var book in books)
        {
            sb.AppendLine($"{book.Title}");
        }
        return sb.ToString();
    }

    //Task 5
    public static string GetBooksByCategory(BookShopContext context, string input)
    {
        var categories = input.Split();
        var books = new List<Book>();
        foreach (var category in categories)
        {
            var cat = context.Categories
                        .FirstOrDefault(c => c.Name.ToLower() == category.ToLower());
            books.AddRange(cat.CategoryBooks.Select(cb => cb.Book).ToList());
                        
        }
        var sb = new StringBuilder();
        foreach (var book in books.OrderBy(b => b.Title))
        {
            sb.AppendLine($"{book.Title}");
        }
        return sb.ToString();
    }

    //Task 6
    public static string GetBooksReleasedBefore(BookShopContext context, string date)
    {
        var dateParams = date.Split('-');
        var dt = new DateTime(int.Parse(dateParams[2]), int.Parse(dateParams[1]), int.Parse(dateParams[0]));
        var books = context.Books
                            .Where(b => b.ReleaseDate != null && b.ReleaseDate < dt)
                            .OrderByDescending(b => b.ReleaseDate)
                            .Select(b => new
                             {
                                 b.Title,
                                 b.EditionType,
                                 b.Price
                             });
        var sb = new StringBuilder();
        foreach (var book in books)
        {
            sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
        }
        return sb.ToString();
    }

    //Task 7
    public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
    {
        var suffixLength = input.Length;
        var authors = context.Authors
                            .Where(a => a.FirstName.Substring(a.FirstName.Length - suffixLength) == input)
                            .Select(a => new
                            {
                                a.FirstName,
                                a.LastName
                            })
                            .OrderBy(a => a.FirstName)
                            .ThenBy(a => a.LastName);
        
        var sb = new StringBuilder();
        foreach (var author in authors)
        {
            sb.AppendLine($"{author.FirstName} {author.LastName}");
        }
        return sb.ToString().Trim();
    }

    //Task 8
    public static string GetBookTitlesContaining(BookShopContext context, string input)
    {
        var books = context.Books
                            .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                            .Select(b => new
                            {
                                b.Title
                            })
                            .OrderBy(t => t.Title);

        var sb = new StringBuilder();
        foreach (var book in books)
        {
            sb.AppendLine($"{book.Title}");
        }
        return sb.ToString().Trim();
    }

    //Task 9
    public static string GetBooksByAuthor(BookShopContext context, string input)
    {
        var suffixLength = input.Length;
        var books = context.Books
                            .Include(b => b.Author)
                            .Where(b => b.Author.LastName.Substring(0, suffixLength).ToLower() == input.ToLower())
                            .OrderBy(a => a.BookId);

        var sb = new StringBuilder();
        foreach (var book in books)
        {
            sb.AppendLine($"{book.Title} ({book.Author.FirstName} {book.Author.LastName})");
        }
        return sb.ToString().Trim();
    }

    //Task 10
    public static int CountBooks(BookShopContext context, int lengthCheck)
    {
        var books = context.Books
                            .Where(b => b.Title.Length > lengthCheck)
                            .ToList();

        return books.Count;
    }

    //Task 11
    public static string CountCopiesByAuthor(BookShopContext context)
    {
        var authorBooks = context.Authors
                            .Include(a => a.Books)
                            .Select(a => new
                            {
                                a.FirstName,
                                a.LastName,
                                TotalCopies = a.Books.Sum(b => b.Copies)
                            })
                            .OrderByDescending(a => a.TotalCopies);

        var sb = new StringBuilder();
        foreach (var book in authorBooks)
        {
            sb.AppendLine($"{book.FirstName} {book.LastName} - {book.TotalCopies}");
        }
        return sb.ToString().Trim();
    }

    //Task 12
    public static string GetTotalProfitByCategory(BookShopContext context)
    {
        var categoryBooks = context.Categories
                            .Include(c => c.CategoryBooks)
                            .Select(c => new
                            {
                                c.Name,
                                TotalProfit = c.CategoryBooks.Sum(cb => cb.Book.Price * cb.Book.Copies)
                            })
                            .OrderByDescending(c => c.TotalProfit)
                            .ThenBy(c => c.Name);

        var sb = new StringBuilder();
        foreach (var cat in categoryBooks)
        {
            sb.AppendLine($"{cat.Name} ${cat.TotalProfit:f2}");
        }
        return sb.ToString().Trim();
    }

    //Task 13
    public static string GetMostRecentBooks(BookShopContext context)
    {
        var categoryBooks = context.Categories
                            .Include(c => c.CategoryBooks)
                            .OrderByDescending(c => c.CategoryBooks.Count)
                            .ThenBy(c => c.Name)
                            .Select(c => new
                            {
                                c.Name,
                                MostRecentBooks = c.CategoryBooks
                                    .OrderByDescending(cb => cb.Book.ReleaseDate)
                                    .Take(3)
                                    .Select(cb => cb.Book)
                            });

        var sb = new StringBuilder();
        foreach (var cat in categoryBooks)
        {
            sb.AppendLine($"--{cat.Name}");
            foreach (var book in cat.MostRecentBooks)
            {
                sb.AppendLine($"{book.Title} ({((DateTime)book.ReleaseDate).Year})");
            }
        }
        return sb.ToString().Trim();
    }

    //Task 14
    public static void IncreasePrices(BookShopContext context)
    {
        var books = context.Books
                            .Where(b => ((DateTime)b.ReleaseDate).Year < 2010);

        foreach (var book in books)
        {
            book.Price += 5;
            context.SaveChanges();
        }
    }

    //Task 15
    public static int RemoveBooks(BookShopContext context)
    {
        var booksToDelete = context.Books
                            .Where(b => b.Copies < 4200)
                            .ToList();

        context.Books.RemoveRange(booksToDelete);
        context.SaveChanges();
        return booksToDelete.Count;
    }
}