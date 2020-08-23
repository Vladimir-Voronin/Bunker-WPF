using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bunker;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Bunker_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //хранится экземпляр запущенной игры
        private Game CurrentGame { get; set; }

        //Общее количество голосов, отданное в раунде
        public int VoteQuantity { get; set; }

        //становится true в раундах на выживание
        public bool IsSurvive { get; set; }

        //Словари, с помощью которого можно получить переменную по её текстовому значению
        public Dictionary<string, TextBox> TextBoxDict { get; set; }

        public Dictionary<string, TextBlock> TextBlockDict { get; set; }

        public Dictionary<string, Button> ButtonDeleteDict { get; set; }

        public Dictionary<string, Button>  ButtonVoteDict { get; set; }

        public Dictionary<string, Button> ButtonSubDict { get; set; }

        public Dictionary<string, Button> ButtonExposeDict { get; set; }

        public Dictionary<string, TextBlock> BlockVoteQuantityDict { get; set; }


        public DispatcherTimer timer;

        public static readonly DependencyProperty TickCounterProperty = DependencyProperty.Register(
            "TickCounter", typeof(int), typeof(MainWindow), new PropertyMetadata(default(int)));

        public MainWindow()
        {
            InitializeComponent();
            AssignDict();
            ChangeEnableSub(false, false, false);
            ChangeEnable(ButtonVoteDict, false);
            ChangeEnable(ButtonExposeDict, false);
            ChangeEnable(ButtonDeleteDict, false);
            BNew_Condition.IsEnabled = false;
            TimerBlock.Visibility = Visibility.Hidden;
        }

        public int TickCounter
        {
            get { return (int)GetValue(TickCounterProperty); }
            set { SetValue(TickCounterProperty, value); }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (--TickCounter <= 0)
            {
                var timer = (DispatcherTimer)sender;
                timer.Stop();
            }
        }

        //Присвоение всех словарей для доступа к XAML по именам
        private void AssignDict()
        {
            ButtonSubDict = new Dictionary<string, Button>
            {
                { "BNext_Round", BNext_Round },
                { "BNext_Talking", BNext_Talking },
                { "BVoting", BVoting },
                { "BNew_Condition", BNew_Condition },
            };

               TextBoxDict = new Dictionary<string, TextBox>
            {
                { "BoxNamePlayer01", BoxNamePlayer01 },
                { "BoxNamePlayer02", BoxNamePlayer02 },
                { "BoxNamePlayer03", BoxNamePlayer03 },
                { "BoxNamePlayer04", BoxNamePlayer04 },
                { "BoxNamePlayer05", BoxNamePlayer05 },
                { "BoxNamePlayer06", BoxNamePlayer06 },
                { "BoxNamePlayer07", BoxNamePlayer07 },
                { "BoxNamePlayer08", BoxNamePlayer08 },
                { "BoxNamePlayer09", BoxNamePlayer09 },
                { "BoxNamePlayer10", BoxNamePlayer10 },
                { "BoxNamePlayer11", BoxNamePlayer11 },
                { "BoxNamePlayer12", BoxNamePlayer12 },
            };

            TextBlockDict = new Dictionary<string, TextBlock>
            {
                { "BlockPlayer1", BlockPlayer1 },
                { "BlockPlayer2", BlockPlayer2 },
                { "BlockPlayer3", BlockPlayer3 },
                { "BlockPlayer4", BlockPlayer4 },
                { "BlockPlayer5", BlockPlayer5 },
                { "BlockPlayer6", BlockPlayer6 },
                { "BlockPlayer7", BlockPlayer7 },
                { "BlockPlayer8", BlockPlayer8 },
                { "BlockPlayer9", BlockPlayer9 },
                { "BlockPlayer10", BlockPlayer10 },
                { "BlockPlayer11", BlockPlayer11 },
                { "BlockPlayer12", BlockPlayer12 },
            };

            ButtonDeleteDict = new Dictionary<string, Button>
            {
                { "BDelete01", BDelete01 },
                { "BDelete02", BDelete02 },
                { "BDelete03", BDelete03 },
                { "BDelete04", BDelete04 },
                { "BDelete05", BDelete05 },
                { "BDelete06", BDelete06 },
                { "BDelete07", BDelete07 },
                { "BDelete08", BDelete08 },
                { "BDelete09", BDelete09 },
                { "BDelete10", BDelete10 },
                { "BDelete11", BDelete11 },
                { "BDelete12", BDelete12 },
            };

            ButtonVoteDict = new Dictionary<string, Button>
            {
                { "BVote01", BVote01 },
                { "BVote02", BVote02 },
                { "BVote03", BVote03 },
                { "BVote04", BVote04 },
                { "BVote05", BVote05 },
                { "BVote06", BVote06 },
                { "BVote07", BVote07 },
                { "BVote08", BVote08 },
                { "BVote09", BVote09 },
                { "BVote10", BVote10 },
                { "BVote11", BVote11 },
                { "BVote12", BVote12 },
            };

            ButtonExposeDict = new Dictionary<string, Button>
            {
                { "BExpose01", BExpose01 },
                { "BExpose02", BExpose02 },
                { "BExpose03", BExpose03 },
                { "BExpose04", BExpose04 },
                { "BExpose05", BExpose05 },
                { "BExpose06", BExpose06 },
                { "BExpose07", BExpose07 },
                { "BExpose08", BExpose08 },
                { "BExpose09", BExpose09 },
                { "BExpose10", BExpose10 },
                { "BExpose11", BExpose11 },
                { "BExpose12", BExpose12 },
            };

            BlockVoteQuantityDict = new Dictionary<string, TextBlock>
            {
                { "BlockVoteQuantity1", BlockVoteQuantity1 },
                { "BlockVoteQuantity2", BlockVoteQuantity2 },
                { "BlockVoteQuantity3", BlockVoteQuantity3 },
                { "BlockVoteQuantity4", BlockVoteQuantity4 },
                { "BlockVoteQuantity5", BlockVoteQuantity5 },
                { "BlockVoteQuantity6", BlockVoteQuantity6 },
                { "BlockVoteQuantity7", BlockVoteQuantity7 },
                { "BlockVoteQuantity8", BlockVoteQuantity8 },
                { "BlockVoteQuantity9", BlockVoteQuantity9 },
                { "BlockVoteQuantity10", BlockVoteQuantity10 },
                { "BlockVoteQuantity11", BlockVoteQuantity11 },
                { "BlockVoteQuantity12", BlockVoteQuantity12 },
            };
        }

        //Метод включает доступ к кнопкам живых игроков
        public void ChangeEnable(Dictionary<string,Button> dict, bool isenab)
        {
            List<string> quantwithzero = new List<string>();
            foreach (var player in Player.PlayersList)
            {
                if(player.Quanity < 10)
                {
                    quantwithzero.Add("0" + player.Quanity.ToString());
                }
                else
                {
                    quantwithzero.Add(player.Quanity.ToString());
                }
            }

            foreach (var key in dict.Keys)
            {
                if (quantwithzero.Contains(key.Substring(key.Length - 2)))
                {
                    dict[key].IsEnabled = isenab;
                }
                else
                {
                    dict[key].IsEnabled = false;
                }
            }
        }

        public void ChangeEnableSub(bool next_round, bool next_talking, bool voting)
        {
            BNext_Round.IsEnabled = next_round;
            BNext_Talking.IsEnabled = next_talking;
            BVoting.IsEnabled = voting;
        }
        public void ClearBlocks(Dictionary<string,TextBlock> dict)
        {
            foreach (var key in dict.Keys)
            {
                dict[key].Text = "";
            }
        }
        //Присваивает имена, после нажатия кнопки "Начать игру"
        private void AssignNames()
        {
            

            foreach (var box in TextBoxDict.Keys)
            {
                int i = Int32.Parse(box.Substring(box.Length - 2));
                try
                {
                    if (TextBoxDict[box].Text != "")
                    {
                        
                        Player.PlayersList[i - 1].Name = TextBoxDict[box].Text;
                    }
                }
                catch (Exception)
                {
 
                }
            }
        }

        //Очищает все TextBlock (при начале новой игры)
        private void Discharge()
        {
            foreach (var block in TextBlockDict.Keys)
            {
                TextBlockDict[block].Text = "";
            }
        }

        private void New_Game(object sender, RoutedEventArgs e)
        {
            //Если список содержит игроков с прошлой игры, то они удаляются
            Player.DeleteAllPlayers();
            
            //Очищается блоки текста
            GameProgress.Text = "";
            AdditionallyСondition.Text = "";
            ClearBlocks(BlockVoteQuantityDict);
            BVoting.Content = "Начать голосование";

            
            //Все кнопки, которые могли быть отключены - включаются
            ChangeEnableSub(true, false, false);
            ChangeEnable(ButtonVoteDict, false);
            BNew_Condition.IsEnabled = true;

            //Считывание настроек
            int start = TryAndParsing(PlayersForEndingGame, 8);
            int end = TryAndParsing(HowManyPlayers, 4);
            int liveadd = TryAndParsing(LiveAddBox, 0);
            int timetoalive = TryAndParsing(TimeBoxAlive, 0);
            int timetosurvive = TryAndParsing(TimeBoxSurvive, 0);

            Game game1 = new Game(this, start, end, liveadd, timetoalive, timetosurvive);

            Discharge();

            CurrentGame = game1.StartGame();

            //Сбрасывает таймер
            if (CurrentGame.TimeTalkAlive != 0)
            {
                //Инициализируем таймер
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1d);
                timer.Tick += new EventHandler(Timer_Tick);
                TickCounter = 0;
                timer.Stop();
                TimerBlock.Visibility = Visibility.Hidden;
            }

            ChangeEnable(ButtonDeleteDict, true);
            AssignNames();
        }

        //Метод для считки настроек
        public int TryAndParsing(TextBox block, int def)
        {
            int numb;
            if(Int32.TryParse(block.Text, out numb))
            {
                return Int32.Parse(block.Text);
            }
            else
            {
                return def;
            }
        }

        public void HideTimer()
        {
            if (CurrentGame.TimeTalkAlive != 0)
            {
                TickCounter = 0;
                timer.Stop();
                TimerBlock.Visibility = Visibility.Hidden;
            }
        }

        public void ViseTmer(int time)
        {
            TickCounter = time;
            TimerBlock.Visibility = Visibility.Visible;
            timer.Start();
        }

        public void New_Round(object sender, RoutedEventArgs e)
        {
            CurrentGame.StartNewRound(Player.PlayersList);
        }

        //Используется для присваивания имени игроку
        public void ChangedBoxName(object sender, RoutedEventArgs e)
        {
            string tag = ((TextBox)e.OriginalSource).Name;
            int i = Int32.Parse(tag.Substring(tag.Length - 2));
            try
            {
                {
                    Player.PlayersList[i - 1].Name = ((TextBox)e.OriginalSource).Text;
                }
            }
            catch (Exception)
            {

            }
        }

        private void Next_Player_Talking(object sender, RoutedEventArgs e)
        {
            if(CurrentGame.RoundNumber != 0)
            {
                CurrentGame.Talking(IsSurvive);
            }
        }

        private void Voting_On(object sender, RoutedEventArgs e)
        {
            //Все варианты развития голосования
            if (BVoting.Content.ToString() == "Начать голосование")
            {
                HideTimer();
                int i = 0;
                foreach (var player in Player.PlayersList)
                {
                    if (player.OnVote && player.Vote == CurrentGame.MaxVote) { i++; }
                }

                if (i==0)
                {
                    CurrentGame.PrintMes("Никто не был выставлен на голосование, переход к следующему раунду");
                    ChangeEnableSub(true, false, false);
                }
                else
                {
                    //Доступ к голосованию только по выставленным игрокам
                    ChangeEnable(ButtonVoteDict, true);
                    foreach (var player in Player.PlayersList)
                    {
                        if (!player.OnVote || player.Vote != CurrentGame.MaxVote)
                        {
                            if (player.Quanity < 10)
                            {
                                ButtonVoteDict["BVote" + "0" + player.Quanity.ToString()].IsEnabled = false;
                            }
                            else
                            {
                                ButtonVoteDict["BVote" + player.Quanity.ToString()].IsEnabled = false;
                            }
                        }
                    }

                    

                    List<Player> tosurvive = new List<Player>();
                    foreach (var player in Player.PlayersList)
                    {
                        if (player.OnVote && player.Vote == CurrentGame.MaxVote) { tosurvive.Add(player); }
                    }

                    //Сброс голосов, перед следующим голосованием
                    CurrentGame.ResetVotes();

                    if (tosurvive.Count == 1) { CurrentGame.DeletePlayerByVotes(tosurvive); }
                    ChangeEnable(ButtonExposeDict, false);
                }
            }
            else if(IsSurvive == true)
            {
                ChangeEnable(ButtonExposeDict, false);
                bool now = CurrentGame.IsNow;
                CurrentGame.Voting(Player.PlayersList);
                if(now == true)
                {
                    ChangeEnableSub(true, false, false);
                }
                else
                {
                    ChangeEnableSub(false, true, false);
                }
            }
            else if(IsSurvive == false)
            {
                List<Player> tosurvive = CurrentGame.ToSurvive(Player.PlayersList);
                CurrentGame.DeleteOrSurviveRound(tosurvive);
                ChangeEnableSub(true, false, false);
            }
        }

        public void VotePlayer(object sender, RoutedEventArgs e)
        {
            if(VoteQuantity < Player.PlayersList.Count)
            {
                Button btn = (Button)e.OriginalSource;
                int i = Int32.Parse(btn.Name.Substring(btn.Name.Length - 2));
                foreach (var player in Player.PlayersList)
                {
                    if (player.Quanity == i)
                    {
                        player.Vote++;
                        BlockVoteQuantityDict["BlockVoteQuantity" + i.ToString()].Text = player.Vote.ToString();
                        VoteQuantity++;
                        break;
                    }
                }
            }
            if(VoteQuantity == Player.PlayersList.Count)
            {
                if(CurrentGame.ToSurvive(Player.PlayersList).Count > 1)
                {
                    IsSurvive = true;
                }
                BVoting.Content = "Закончить голосование";
            }
        }

        private void DeletePlayer(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            int i = Int32.Parse(btn.Name.Substring(btn.Name.Length - 2));
            foreach (var player in Player.PlayersList)
            {
                if(player.Quanity == i)
                {
                    player.DeletePlayer();
                    TextBlockDict["BlockPlayer" + player.Quanity.ToString()].Text = "";
                    break;
                }
            }
            CurrentGame.CheckEndGame();
        }

        private void ExposePlayer(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            int i = Int32.Parse(btn.Name.Substring(btn.Name.Length - 2));
            foreach (var player in Player.PlayersList)
            {
                if (player.Quanity == i)
                {
                    if(player.OnVote == false)
                    {
                        player.OnVote = true;
                        CurrentGame.PrintMes($"Игрок {player.Name} выставлен");
                        btn.IsEnabled = false;
                        break;
                    }
                }
            }
        }

        private void New_Condition(object sender, RoutedEventArgs e)
        {
            if (CurrentGame.Cataclysm.FlagZeroToOne == 0)
            {
                AdditionallyСondition.Text += CurrentGame.Cataclysm.Card1 + "\n\n";
                CurrentGame.Cataclysm.FlagZeroToOne++;
            }
            else if(CurrentGame.Cataclysm.FlagZeroToOne == 1)
            {
                AdditionallyСondition.Text += CurrentGame.Cataclysm.Card2;
                CurrentGame.Cataclysm.FlagZeroToOne++;
                BNew_Condition.IsEnabled = false;
            }
        }
    }

}
