using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Bunker
{
    //Статичный класс, содержащий в себе методы для обработки различных позиций
    static class Handler
    {
        //Метод возвращает рандомную строчку из текстового файла
        public static string RandomStrFile(string file)
        {
            Random rand = new Random();
            string[] strs = File.ReadAllLines($@"D:\Bunker WPF\Bunker WPF\data\{file}" , Encoding.UTF8);
            string s = strs[rand.Next(strs.Length)];
            return s;
        }

        //Выбор рандомного файла из папки, возвращает название файла и его содержание (кортежом)
        public static (string, string) RandomCatastrophe()
        {
            var rand = new Random();
            var files = Directory.GetFiles(@"D:\Bunker WPF\Bunker WPF\Catastrophe", "*.txt");
            var file = files[rand.Next(files.Length)];
            string s = file.Substring(0, file.Length - 4);
            s = s.Replace(@"D:\Bunker WPF\Bunker WPF\Catastrophe\", "");
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
