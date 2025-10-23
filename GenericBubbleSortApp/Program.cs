using System;

namespace GenericBubbleSortApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // int[] arr = new int[] { 5, 2, 9, 1, 5, 6 };
            string[] arr = new string[] { "George", "Anna", "Zoe", "Mark", "John" };
            // Console.WriteLine("Original array: " + string.Join(", ", arr));

            /* Employee[] empArr = new Employee[]
            {
                new Employee { Name = "John", Id = 3 },
                new Employee { Name = "Alice", Id = 1 },
                new Employee { Name = "Bob", Id = 2 },
                new Employee { Name = "Diana", Id = 5 },
                new Employee { Name = "Charlie", Id = 4 }
            }; */

            // SortArray<int> sorter = new SortArray<int>();
            // SortArray<Employee> sorter = new SortArray<Employee>();
            SortArray<string> sorter = new SortArray<string>();

            sorter.BubbleSort(arr);
            Console.WriteLine("Sorted string array:");
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

        }
    }

    public class Employee : IComparable<Employee>
    {
        public string Name { get; set; }
        public int Id { get; set; }

        // public int CompareTo(object obj)
        // {
        //     if (obj == null) return 1;

        //     Employee otherEmployee = obj as Employee;
        //     if (otherEmployee != null)
        //         return this.Name.CompareTo(otherEmployee.Name);
        //     else
        //         throw new ArgumentException("Object is not an Employee");
        // }

        public int CompareTo(Employee other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return $"Name: {Name}, Id: {Id}";
        }
    }

    public class SortArray<T> where T : IComparable<T>
    {
        public void BubbleSort(T[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j].CompareTo(arr[j + 1]) > 0)
                    {
                        // Swap arr[j] and arr[j+1]
                        Swap(arr, j);
                    }
                }
            }
        }

        private void Swap(T[] arr, int j)
        {
            T temp = arr[j];
            arr[j] = arr[j + 1];
            arr[j + 1] = temp;
        }
    }
}