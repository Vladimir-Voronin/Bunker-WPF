using System;
using System.IO;
using System.Collections.Generic;
using System.Text;


namespace Bunker
{
    //Статичный класс, содержащий в себе методы для обработки различных позиций
    static class Handler
    {

        static Random Rand { get; set; } = new Random();

        //Метод возвращает рандомную строчку из текстового файла
        public static string RandomStrFile(string file)
        {
            string direct = Directory.GetCurrentDirectory();
            string path;
            path = Directory.GetParent(direct).ToString();
            path = Directory.GetParent(path).ToString();
            path = path + @"\data\" + file;
            string[] strs = File.ReadAllLines(path, Encoding.UTF8);
            string s = strs[Rand.Next(strs.Length)];
            return s;
        }

        //Выбор рандомного файла из папки, возвращает название файла и его содержание (кортежом)
        public static (string, string) RandomCatastrophe()
        {
            
            string direct = Directory.GetCurrentDirectory();
            string path;
            path = Directory.GetParent(direct).ToString();
            path = Directory.GetParent(path).ToString();
            path = path + @"\Catastrophe\";
            var files = Directory.GetFiles(path, "*.txt");
            var file = files[Rand.Next(files.Length)];
            var filename = Path.GetFileName(file);
            string s = filename.Substring(0, filename.Length - 4);
            return (s, File.ReadAllText(file, Encoding.UTF8));
        }

        public static string DiscoverPositionType(string file)
        {
           
            if(file.ToString() == "gender.txt")
            {
                return "Пол: ";
            }
            if (file.ToString() == "old.txt")
            {
                return "Возраст: ";
            }
            if (file.ToString() == "profession.txt")
            {
                return "Проффесия: ";
            }
            if (file.ToString() == "childbearing.txt")
            {
                return "Деторождение: ";
            }
            if (file.ToString() == "health.txt")
            {
                return "Здоровье: ";
            }
            if (file.ToString() == "phobia.txt")
            {
                return "Фобия: ";
            }
            if (file.ToString() == "hobby.txt")
            {
                return "Хобби: ";
            }
            if (file.ToString() == "character.txt")
            {
                return "Характер: ";
            }
            if (file.ToString() == "additionally.txt")
            {
                return "Доп. Информация: ";
            }
            if (file.ToString() == "baggage.txt")
            {
                return "Багаж: ";
            }
            if (file.ToString() == "card1.txt")
            {
                return "Карта 1: ";
            }
            if (file.ToString() == "card2.txt")
            {
                return "Карта 2: ";
            }

            return "";
          
        }
    }

}
