using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericsBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            Salaries salaries = new Salaries();
            // ArrayList salaryList = salaries.GetSalaries();
            List<float> salaryList = salaries.GetSalaries();

            float salary = salaryList[0];

            salary += salary * 0.02f;

            System.Console.WriteLine($"Updated salary: {salary}");
        }
    }

    public class Salaries
    {
        // ArrayList _salaries = new ArrayList();
        List<float> _salaries = new List<float>();

        public Salaries()
        {
            _salaries.Add(1200.23f);
            _salaries.Add(2300.45f);
            _salaries.Add(3400.67f);
        }

        // public ArrayList GetSalaries()
        // {
        //     return _salaries;
        // }

        public List<float> GetSalaries()
        {
            return _salaries;
        }
    }

}