using System;
using System.Collections.Generic;
using System.Text;

namespace Bunker
{
    //Хранит в себе один показатель, который может быть скрытым или
    //открытым. Агрегация с классами Card и Game(?)
    class Position
    {
        //Позиция может быть открытой и закрытой (закрыта по умолчанию)
        public bool Open { get; set; } = false;

        //Имя позиции в интерфейсе
        public string TypeDataPrint { get; set; }

        //Хранит в себе значение (главное свойство в классе)
        public string Data { get; set; }

        //Уровень скрытности будет контролировать скрытность позиций на разных раундах
        //Чем выше скрытность, тем позже будет открыта позиция
        public int Levelhide { get; set; }

        public Position()
        {
        }
               
        public Position(string file, int levelhide = 0)
        {
            Levelhide = levelhide;
            Refresh(file); //Data получает значение
        }

        //Обновление позиции (возможно при нажатии соответствующей кнопки)
        public void Refresh(string file)
        {
            Data = Handler.RandomStrFile(file);
            TypeDataPrint = Handler.DiscoverPositionType(file);
        }
    }

    
    class Catastrophe : Position
    {
        public string Description { get; set; }

        public string Card1 { get; set; }

        public string Card2 { get; set; }

        //Если равен 0 - ничего не изображено, 1 - первое условие, 2 - все условия
        public int FlagZeroToOne { get; set; }
        public Catastrophe()
        {
            (Data, Description) = Handler.RandomCatastrophe();
            Card1 = Handler.RandomStrFile("card2.txt");
            Card2Assign(Card1);
        }

        private void Card2Assign(string card1)
        {
            Card2 = Handler.RandomStrFile("card2.txt");
            if(Card2==card1) { Card2Assign(card1); }
        }
    }
}
