using System;
using System.Collections.Generic;
using System.Text;

namespace Bunker
{
    class Player
    {
        //Имя игрока, по умолчанию будет survivorI (где I - номер экземпляра)
        public string Name { get; set; }

        //Количество экземпляров
        public static int Number { get; set; }

        //Количество голосов за игрока (расширение во front-end)
        public int Vote { get; set; }

        //Находится ли игрок в игре
        public bool IsAlive { get; set; } = false;

        //Карта игрока
        public Card PlayerCard { get; set; }

        //Список всех созданных игроков, в будущем удобно будет проходить по всем игрокам циклом
        public static List<Player> PlayersList = new List<Player>();

        //Игрок по умолчанию создается без карточки
        public Player(bool createcard = false)
        {
            Name = "Survivor" + ++Number; //Префиксный инкремент, чтобы номер 1 игрока был равен 1, а не 0
            if (createcard)
            {
                PlayerCard = new Card();
            }
            PlayersList.Add(this);
            
        }

        public void CreateOrRefreshCard()
        {
            PlayerCard = new Card();
        }

        //Удаление игрока (не забыть уменьшить Number и удалить игрока из PlayerList)
        //Не путать с удалением карты
        public void DeletePlayer()
        {
            PlayerCard = null;
            this.IsAlive = false;
            Number--;
            Name = null;
            PlayersList.Remove(this);
        }

        //Удаление карты игрока
        public void DeletePlayerCard()
        {
            PlayerCard = null;
        }

        public static void DeleteAllPlayers()
        {
            if(Player.PlayersList.Count > 0)
            {
                for (int i = Player.PlayersList.Count; i > 0; i--)
                {
                    Player.PlayersList[i - 1].DeletePlayer();
                }
            }   
        }
    }
}
