using System;

namespace _03.Mankind
{
    public class StartUp
    {
        public static void Main()
        {
            var inputStudent = Console.ReadLine().Split();
            var inputWorker = Console.ReadLine().Split();
            try
            {
                var output1 = new Student(inputStudent[0], inputStudent[1], inputStudent[2]).ToString();
                var output2 = new Worker(inputWorker[0], inputWorker[1], decimal.Parse(inputWorker[2]), decimal.Parse(inputWorker[3])).ToString();
                Console.WriteLine(output1);
                Console.WriteLine(output2);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    }
}
