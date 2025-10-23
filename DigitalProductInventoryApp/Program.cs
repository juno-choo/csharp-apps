using System;

namespace DigitalProductInventoryApp
{
    class Program
    {
        static void Main(string[] args)
        {


        }
    }

    public interface IPrimaryProperties
    {
        int Id { get; set; }
        string Title { get; set; }
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