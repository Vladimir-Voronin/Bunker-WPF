using System;
using System.Collections.Generic;
using System.Text;

namespace Bunker
{
    class Card
    {
        //Все 12 позиций с присвоением рандомного значения по умолчанию (в один момент сделал вместе со свойствами поля, 
        //возможно стоит убрать, стало слишком громоздко, но пока оставлю)
        public Position Gender { get; set; } = new Position("gender.txt", 0);
        public Position Old { get; set; } = new Position("old.txt", 0);
        public Position Profession { get; set; } = new Position("profession.txt", 0);
        public Position Сhildbearing { get; set; } = new Position("childbearing.txt", 1);
        public Position Health { get; set; } = new Position("health.txt", 0);
        public Position Phobia { get; set; } = new Position("phobia.txt", 0);
        public Position Hobby { get; set; } = new Position("hobby.txt", 0);
        public Position Character { get; set; } = new Position("character.txt", 0);
        public Position Additionally { get; set; } = new Position("additionally.txt", 0);
        public Position Baggage { get; set; } = new Position("baggage.txt", 0);
        public Position Card1 { get; set; } = new Position("card1.txt", 0);
        public Position Card2 { get; set; } = new Position("card2.txt", 0);

        //Возможно список всех позиций будет полезным
        public List<Position> allpositions = new List<Position>(12) { };
      
        public Card()
        {
            allpositions.Add(Gender);
            allpositions.Add(Old);
            allpositions.Add(Profession);
            allpositions.Add(Сhildbearing);
            allpositions.Add(Health);
            allpositions.Add(Phobia);
            allpositions.Add(Hobby);
            allpositions.Add(Character);
            allpositions.Add(Additionally);
            allpositions.Add(Baggage);
            allpositions.Add(Card1);
            allpositions.Add(Card2);
        }


    }
}
