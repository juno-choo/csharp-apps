using System;

namespace DigitalProductInventoryApp
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string CategoryDescription { get; set; }
    }
    public interface IPrimaryProperties
    {
        int Id { get; set; }
        string Title { get; set; }
    }

    public abstract class ProductBase : IPrimaryProperties
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }

    public class Movie : ProductBase
    {
        public string Director { get; set; }
        public string Producer { get; set; }
    }

    public class DigitalBook : ProductBase
    {
        public string Author { get; set; }
        public string ISBN { get; set; }
    }

    public class Music : ProductBase
    {
        public string Artist { get; set; }
        public string Label { get; set; }
    }
    public abstract class CategoryBase : IPrimaryProperties
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class MovieCategory : CategoryBase
    {
        public MovieCategory()
        {
            Description = "Category for Movies";
        }
    }

    public class DigitalBookCategory : CategoryBase
    {
        public DigitalBookCategory()
        {
            Description = "Category for Digital Books";
        }
    }

    public class MusicCategory : CategoryBase
    {
        public MusicCategory()
        {
            Description = "Category for Music";
        }
    }

    public static class FactoryPattern<T, U> where T : class, U, new()
                                             where U : class
    {
        public static U GetInstance()
        {
            U objT;
            objT = new T();
            return objT;
        }
    }
}