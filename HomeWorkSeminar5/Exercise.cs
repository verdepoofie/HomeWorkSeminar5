using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkSeminar5
{
    internal class Exercise
    {
        private struct Record
        {
            public Record()
            {
            }
            public int ID { get; set; } = -1;
            public DateTime CreationDateTime { get; set; } = DateTime.MinValue;
            public string FullName { get; set; } = "";
            public int Age => DateTime.Today.Year - DateOfBirth.Year;
            public int Height { get; set; } = 0;
            public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
            public string PlaceOfBirth { get; set; } = "";
        }
        private string fileName = "catalog.txt";
        private void DisplayFile()
        {
            Console.WriteLine("Имеющиеся записи:\n");
            string[] wholeFile = File.ReadAllLines(fileName);
            foreach(string record in wholeFile)
            {
                foreach(string recordElement in record.Split("#"))
                    Console.Write(recordElement + "\t");
                Console.WriteLine();
            }
        }
        private void AddRecord()
        {
            try
            {
                string[] file = File.ReadAllLines(fileName);
                Record record = new Record();
                if(file.Length > 0)
                    record.ID = int.Parse(file[file.Length - 1].Split('#')[0]) + 1;
                else
                    record.ID = 0;
                record.CreationDateTime = DateTime.Now;
                Console.WriteLine("Введите Ф.И.О.");
                record.FullName = (string)GetInput(nameof(record.FullName));
                Console.WriteLine("Введите дату рождения в формате день.месяц.год");
                record.DateOfBirth = (DateTime)GetInput(nameof(record.DateOfBirth));
                Console.WriteLine("Введите место рождения");
                record.PlaceOfBirth = (string)GetInput(nameof(record.PlaceOfBirth));
                Console.WriteLine("Введите рост в см");
                record.Height = (int)GetInput(nameof(record.Height));
                File.AppendAllText(fileName,
                    record.ID.ToString() + "#" +
                    record.CreationDateTime.ToString("G") + "#" +
                    record.FullName + "#" +
                    record.Age.ToString() + "#" +
                    record.Height.ToString() + "#" +
                    record.DateOfBirth.ToString("yyyy.MM.dd") + "#" +
                    record.PlaceOfBirth + "\n");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Произошла ошибка:\n" + ex.Message);
            }
        }
        private object GetInput(string inputType)
        {
            while (true)
            {
                var input = Console.ReadLine();
                switch (inputType)
                {
                    case "FullName":
                        if (!string.IsNullOrEmpty(input) && input.Split(' ').Length == 3)
                            return input;
                        break;
                    case "Height":
                        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int height) && height > 0)
                            return height;
                        break;
                    case "DateOfBirth":
                        if(!string.IsNullOrEmpty(input) && DateTime.TryParse(input, out DateTime date) && date < DateTime.Today)
                            return date;
                        break;
                    case "PlaceOfBirth":
                        if (!string.IsNullOrEmpty(input))
                            return input;
                        break;
                    default:
                        return new object();
                }
                Console.WriteLine("Не верный ввод");
            }
        }
        public void Execute()
        {
            Console.WriteLine("Задание, справочник «Сотрудники»");
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            
            while (true)
            {
                Console.WriteLine("Введите 1 для вывода справочника на экран, введите 2 для добавления записи в справочник, Q для выхода");
                var answer = Console.ReadKey();
                Console.WriteLine();
                if (answer.Key != ConsoleKey.Q)
                {
                    switch (answer.Key)
                    {
                        case ConsoleKey.D1:
                            DisplayFile();
                            break;
                        case ConsoleKey.D2:
                            AddRecord();
                            break;
                    }
                }
                else
                    break;
            }
        }
    }
}
