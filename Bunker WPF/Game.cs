using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Bunker_WPF;

namespace Bunker
{
    class Game
    {
        //Общие настройки
        public readonly int LevelAdd = 0;     //Константа, отвечающая за минимальный рубеж открытия позиций в начале игры
        public readonly int TimeTalkAlive = 0; //Константа, отвечающая за количество времени, дающееся на разговор живым игрокам (если равно 0, то время неограничено)
        public readonly int TimeToSurvive = 0; //Константа, отвечающая за количество времени, дающееся на разговор при схватке (если равно 0, то время неограничено)
        public readonly int PlayersToEnd = 4;
        public readonly int PlayersToStart = 8;
        //Общие условия игры
        public Catastrophe Cataclysm { get; set; } = new Catastrophe();

        //Делегаты

        public event Message.GameInfo GameIn;


        //Изменяющиеся поля и свойства

        //Количество игроков, оставшихся в отряде
        //public int AlivePlayers { get; set; }

        public int RoundNumber { get; set; }

        //Флаг-состояния раздачи карт
        public bool dealingcards = false;

        //Список игроков, выставленных на голосование
        public List<Player> PlayersToVote { get; set; }

        //Содержит объект MainWindow для доступа к полям и методам, обрабатывающим интерфейс
        private MainWindow Main { get; set; }

        //Конструктор (добавляет нужные методы в делегаты, определяет количество игроков для конца игры, изменяет правила)
        public Game(MainWindow main, bool MessageOn = true)
        {
            Message.FlagGameInfo = MessageOn;
            if (Message.FlagGameInfo == true) { GameIn += PrintMes; }
            Main = main;
        }

        public Game(MainWindow main, int playerstostart, int playerstoend, int leveladd = 0, bool MessageOn = true) : this(main)
        {
            LevelAdd = leveladd;
            PlayersToStart = playerstostart;
            PlayersToEnd = playerstoend;
        }

        public Game(int playerstostart, int playerstoend, int timetalkalive, int timetosurvive,  MainWindow main, int leveladd = 0, bool MessageOn = true) : this(main,  playerstostart, playerstoend, leveladd)
        {
            TimeTalkAlive = timetalkalive;
            TimeToSurvive = timetosurvive;
        }

        //Метод для делегата/события (прогресс игры)
        public void PrintMes(string mes)
        {
            if (Message.FlagGameInfo == true) { Main.GameProgress.Text += mes + "\n"; }
        }


        //Отдельный метод создан для будущего расширения во front-end
        public void CreatePlayers(int many)
        {
            for (int i = 0; i < many - 1; i++)
            {
                Player player = new Player();
            }

        }

        //Раздача карт (до начала игры)
        public void DealCards()
        {
            foreach (var player in Player.PlayersList)
            {
                player.CreateOrRefreshCard();
            }
            dealingcards = true;
            GameIn?.Invoke("Все игроки получили карты");
        }

        public Game StartGame()
        {
            
            GameIn?.Invoke("Игра началась");
            CreatePlayers(PlayersToStart);

            Main.BlockNameCatastrophe.Text = Cataclysm.Data;
            Main.BlockNameDescription.Text = Cataclysm.Description;
            if (dealingcards == false) { DealCards(); }

            CardReload(true);
            
            return this;
        }

        //Один проход по всем игрокам
        public void StartNewRound()
        {
            Main.GameProgress.Text = ""; //очищаем чат
            RoundNumber++;
            GameIn?.Invoke($"Начался раунд {RoundNumber}");
            //Таймер (подключить отдельно для возможности игры с таймером и без)
            foreach (var player in Player.PlayersList)
            {
                if (player.IsAlive)
                {
                    Talking(player);
                }
            }
            GameIn?.Invoke("Начинается голосование");
            Voting(Player.PlayersList);

            
            //Как реализовать удаление игрока???

            //Обработка результатов голосования (передается список игроков, над которыми будет работать алгоритм)
            List<Player> tosurvive = ToSurvive(Player.PlayersList);
            DeleteOrSurviveRound(tosurvive);

            if (Player.PlayersList.Count <= PlayersToEnd)
            {
                GameIn?.Invoke("Игра окончена");
                GameIn?.Invoke("В отряде остались игроки:");
                foreach (var player in Player.PlayersList)
                {
                    GameIn?.Invoke(player.Name);
                    
                }
                for (int i = Player.PlayersList.Count - 1; i > 0; i--)
                {
                    Player.PlayersList[i].DeletePlayer();
                }
                Main.BNext_Round.IsEnabled = false;
                Main.BNext_Talking.IsEnabled = false;
                Main.BVoting.IsEnabled = false;

            }
            if( Player.PlayersList.Count > 0) { CardReload(); }
            
        }

        //Мнение одного игрока
        public void Talking(Player player)
        {
            if (this.TimeTalkAlive != 0)
            {
                //Таймер
            }
            else
            {
                //по указанию ведущего (по кнопке)
            }
        }

        //Алгоритм определяет за кого проголосовало больше всего игроков
        public List<Player> ToSurvive(List<Player> Players)
        {
            List<Player> tosurvive = new List<Player>();
            int current = 0;
            int max = 0;
            foreach (var player in Players)
            {
                current = player.Vote;
                if (current > max)
                {
                    max = current;
                    tosurvive.Clear();
                    tosurvive.Add(player);
                }
                else if (current == max)
                {
                    tosurvive.Add(player);
                }
            }

            GameIn?.Invoke($"Максимальное количество голосов: {max}.");
            return tosurvive;
        }


        //Метод просматривает количество голосов, если есть лидер, то исключает его
        //Если таких игроков несколько, то проводит второй круг между игроками борющимися за выживание
        public void DeleteOrSurviveRound(List<Player> tosurvive)
        {
            if (tosurvive.Count == 1)
            {
                GameIn?.Invoke($"Игрок {tosurvive[0].Name} покидает игру");
                tosurvive[0].DeletePlayer();
                tosurvive.Remove(tosurvive[0]);
            }
            else
            {
                foreach (var player in tosurvive)
                {
                    Talking(player);
                }
                Voting(tosurvive);
                List<Player> result = ToSurvive(tosurvive);
                foreach (var player in result)
                {
                    GameIn?.Invoke($"Игрок {player.Name} покидает игру");
                    player.DeletePlayer();
                }
            }
            
        }

        //Голосование за исключение (слишком много вариантов реализации)
        public void Voting(List<Player> Players)
        {

            Random rand = new Random();
            foreach (var player in Players)
            {
                player.Vote = rand.Next(0, 4);
                Console.WriteLine($"{player.Name}: {player.Vote}");
            }
        }

        public void CardReload(bool startgame = false)
        {
            foreach (var player in Player.PlayersList)
            {
                if (startgame) { player.IsAlive = true; }

                if (RoundNumber == 0 || RoundNumber == 1)
                {
                    //Открытие всех позиций, соотвтествующих уровню LevelAdd
                    foreach (Position pos in player.PlayerCard.allpositions)
                    {
                        if (pos.Levelhide <= LevelAdd) { pos.Open = true; }
                    }
                }
                else
                {
                    //Открытие всех позиций, соотвтествующих уровню LevelAdd + от раунда
                    foreach (Position pos in player.PlayerCard.allpositions)
                    {
                        if (pos.Levelhide <= LevelAdd + RoundNumber - 1) { pos.Open = true; }
                    }
                }
            }
        }

        
    }
}
