using System.Text;

namespace HomeWorkSeminar5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Exercise exercise = new Exercise();
            Console.WriteLine("Домашнее задание 5");
            exercise.Execute();
            Console.ReadLine();
        }
    }
}
