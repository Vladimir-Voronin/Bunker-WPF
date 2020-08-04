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

        //Максимальное количество голосов, используется при равенстве макс голосов
        public int MaxVote { get; set; }

        //проход раунда выживания - если true, значит пройден
        public bool IsNow { get; set; } = false;

        //Флаг-состояния раздачи карт
        public bool dealingcards = false;

        //Количество игроков, окончивших свою речь
        public int PlayersEndTalk { get; set; }

        //Список игроков, выставленных на голосование
        public List<Player> PlayersToVote { get; set; }

        //Содержит объект MainWindow для доступа к полям и методам, обрабатывающим интерфейс
        public MainWindow Main { get; set; }

        public enum Rules
        {
            Managed,
            Auto
        };

        public Rules Rule { get; set; }

        //Конструктор (добавляет нужные методы в делегаты, определяет количество игроков для конца игры, изменяет правила)
        public Game(MainWindow main, bool MessageOn = true, Rules rule = 0)
        {
            Rule = rule;
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
            for (int i = 0; i < many; i++)
            {
                Player player = new Player(true);
            }

        }

        //Раздача карт (до начала игры)
        //public void DealCards()
        //{

        //    dealingcards = true;
        //    GameIn?.Invoke("Все игроки получили карты");
        //}

        public Game StartGame()
        {
            GameIn?.Invoke("Игра началась");
            CreatePlayers(PlayersToStart);

            Main.BlockNameCatastrophe.Text = Cataclysm.Data;
            Main.BlockNameDescription.Text = Cataclysm.Description;

            CardReload(true);
            PrintCard();

            return this;
        }

        //Один проход по всем игрокам (surviveround = true раунд выживания)
        public void StartNewRound(List<Player> playerlist,bool surviveround = false)
        {
            Main.ChangeEnableSub(false, true, false);
            Main.ChangeEnable(Main.ButtonVoteDict, false);
            Main.VoteQuantity = 0;
            Main.ClearBlocks(Main.BlockVoteQuantityDict);
            PlayersEndTalk = 0;
            Main.GameProgress.Text = ""; //очищаем чат
            Main.BVoting.Content = "Начать голосование";

            if (surviveround == false)
            {
                foreach (var player in playerlist)
                {
                    player.OnVote = false;
                }
                Main.ChangeEnable(Main.ButtonExposeDict, true);
                ResetVotes();
                Main.IsSurvive = false;
                RoundNumber++;
                GameIn?.Invoke($"Начался раунд {RoundNumber}");
            }
            
            Main.BNext_Talking.Content = $"Ход игрока {playerlist[PlayersEndTalk].Name}";
            //Таймер (подключить отдельно для возможности игры с таймером и без)
            
            CheckEndGame();

            if (surviveround == false)
            {
                if (playerlist.Count > 0)
                {
                    CardReload();
                    PrintCard();
                }
            }
        }

        //Проверка на конец игры
        public void CheckEndGame()
        {
            if (Player.PlayersList.Count <= PlayersToEnd)
            {
                GameIn?.Invoke("Игра окончена");
                GameIn?.Invoke("В отряде остались игроки:");
                foreach (var player in Player.PlayersList)
                {
                    GameIn?.Invoke(player.Name);
                }
                Player.DeleteAllPlayers();
                Main.ChangeEnableSub(false, false, false);
            }
        }

        //Мнение одного игрока, onvote - true, если идет борьба за выживания между несколькими игроками
        //с одинаковым количеством голосов
        public void Talking(bool onvote)
        {
            if (this.TimeTalkAlive != 0)
            {
                //Таймер
            }
            //Для всех игроков
            if(onvote == false)
            {
                if (PlayersEndTalk < Player.PlayersList.Count)
                {
                    if (PlayersEndTalk + 1 < Player.PlayersList.Count)
                    {
                        Main.BNext_Talking.Content = $"Ход игрока {Player.PlayersList[PlayersEndTalk + 1].Name}";
                    }
                    else if (PlayersEndTalk + 1 == Player.PlayersList.Count)
                    {
                        Main.BNext_Talking.IsEnabled = false;
                        Main.BNext_Talking.Content = $"Ход следующего игрока";
                        Main.ChangeEnableSub(false, false, true);
                        Main.BVoting.IsEnabled = true;
                    }
                    PlayersEndTalk++;
                }
            }
            //Раунд выживаемых
            else 
            {
                List<Player> survivors = new List<Player>();
                foreach (var player in Player.PlayersList)
                {
                    if(player.OnVote == true)
                    {
                        survivors.Add(player);
                    }
                }

                ResetVotes();
                
                if (PlayersEndTalk < survivors.Count)
                {
                    if (PlayersEndTalk + 1 < survivors.Count)
                    {
                        Main.BNext_Talking.Content = $"Ход игрока {survivors[PlayersEndTalk + 1].Name}";
                    }
                    if (PlayersEndTalk + 1 == survivors.Count)
                    {
                        Main.BNext_Talking.IsEnabled = false;
                        Main.BNext_Talking.Content = "Ход следующего игрока";
                        Main.ChangeEnableSub(false, false, true);
                        //Main.BVoting.Content = "Начать голосование";
                        Main.BVoting.IsEnabled = true;
                    }
                    PlayersEndTalk++;
                }
            }
        }

        //Алгоритм определяет за кого проголосовало больше всего игроков
        public List<Player> ToSurvive(List<Player> Players)
        {
            List<Player> tosurvive = new List<Player>();
            int current = 0;
            MaxVote = 0;
            foreach (var player in Players)
            {
                current = player.Vote;
                if (current > MaxVote)
                {
                    MaxVote = current;
                    tosurvive.Clear();
                    tosurvive.Add(player);
                }
                else if (current == MaxVote)
                {
                    tosurvive.Add(player);
                }
            }
            GameIn?.Invoke($"Максимальное количество голосов: {MaxVote}");
            return tosurvive;
        }

        //Метод просматривает количество голосов, если есть лидер, то исключает его
        //Если таких игроков несколько, то проводит второй круг между игроками борющимися за выживание
        public void DeleteOrSurviveRound(List<Player> tosurvive)
        {
            if(tosurvive.Count == 0)
            {

            }
            else if (tosurvive.Count == 1)
            {
                DeletePlayerByVotes(tosurvive);
            }
            else if(IsNow == false)
            {
                foreach (var player in tosurvive)
                {
                    if(player.Vote != MaxVote)
                    {
                        tosurvive.Remove(player);
                    }
                }
                Main.IsSurvive = true;
                StartNewRound(tosurvive, Main.IsSurvive);
                IsNow = true;
                Main.BVoting.Content = "Начать голосование";
            }
            else if (IsNow == true)
            {
                if(tosurvive[0].Vote != 0)
                {
                    foreach (var player in tosurvive)
                    {
                        player.DeletePlayer();
                        GameIn?.Invoke($"Игрок {player.Name} покидает игру");
                        Main.TextBlockDict["BlockPlayer" + player.Quanity.ToString()].Text = "";
                        MaxVote = 0;
                    }
                }
                Main.BNext_Talking.Content = "Ход следующего игрока";
                Main.IsSurvive = false;
                Main.ChangeEnableSub(true, false, false);
            }
        }

        public void DeletePlayerByVotes(List<Player> tosurvive)
        {
            GameIn?.Invoke($"Игрок {tosurvive[0].Name} покидает игру");
            tosurvive[0].DeletePlayer();
            Main.TextBlockDict["BlockPlayer" + tosurvive[0].Quanity.ToString()].Text = "";
            Main.ChangeEnableSub(true, false, false);
            MaxVote = 0;
        }
        //Сброс всех голосов до 0
        public void ResetVotes()
        {
            foreach (var player in Player.PlayersList)
            {
                player.Vote = 0;
            }
        }

        //Голосование за исключение (слишком много вариантов реализации)
        public void Voting(List<Player> Players)
        {
            Main.IsSurvive = false;
            List<Player> tosurvive = ToSurvive(Players);
            DeleteOrSurviveRound(tosurvive);
        }

        public void CardReload(bool startgame = false)
        {
            foreach (var player in Player.PlayersList)
            {
                if (startgame) { player.IsAlive = true; }

                if(RoundNumber == 0 || RoundNumber == 1)
                {
                    foreach (Position pos in player.PlayerCard.allpositions)
                    {
                        if (pos.Levelhide <= LevelAdd) { pos.Open = true; }
                    }
                }
                else
                {
                    foreach (Position pos in player.PlayerCard.allpositions)
                    {
                        if (pos.Levelhide <= LevelAdd + RoundNumber - 1) { pos.Open = true; }
                    }
                }
                //Открытие всех позиций, соотвтествующих уровню LevelAdd
                
            }
        }

        public void PrintCard()
        {
            foreach (Player player in Player.PlayersList)
            {
                string name = "BlockPlayer" + player.Quanity.ToString();
                TextBlock block = Main.TextBlockDict[name];

                block.Text = ""; //очищаем

                foreach (Position pos in player.PlayerCard.allpositions)
                {
                    if (pos.Open)
                    {
                        block.Text += pos.TypeDataPrint + pos.Data + "\n";
                    }
                }
            }
        }
    }
}
