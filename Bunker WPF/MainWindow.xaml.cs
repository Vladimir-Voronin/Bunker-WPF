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
        private int VoteQuantity { get; set; }

        //Словари, с помощью которого можно получить переменную по её текстовому значению
        public Dictionary<string, TextBox> TextBoxDict { get; set; }

        public Dictionary<string, TextBlock> TextBlockDict { get; set; }

        public Dictionary<string, Button> ButtonDeleteDict { get; set; }

        public Dictionary<string, Button>  ButtonVoteDict { get; set; }

        public Dictionary<string, Button> ButtonSubDict { get; set; }

        public Dictionary<string, Button> ButtonExposeDict { get; set; }

        public Dictionary<string, TextBlock> BlockVoteQuantityDict { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AssignDict();
            ChangeEnableSub(false);
            ChangeEnable(ButtonVoteDict, false);
            ChangeEnable(ButtonExposeDict, false);
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
            List<string> quant = new List<string>();

            List<string> quantwithzero = new List<string>();
            foreach (var player in Player.PlayersList)
            {
                quant.Add(player.Quanity.ToString());
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

        public void ChangeEnableSub(bool isenab)
        {
            foreach (var key in ButtonSubDict.Keys)
            {
                ButtonSubDict[key].IsEnabled = isenab;
            }
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

            //Все кнопки, которые могли быть отключены - включаются
            ChangeEnableSub(true);
            ChangeEnable(ButtonVoteDict, false);

            int number;
            int start;
            int end;
            int liveadd;
            if (Int32.TryParse(HowManyPlayers.Text, out number) && Int32.TryParse(PlayersForEndingGame.Text, out number))
            {
                start = Int32.Parse(PlayersForEndingGame.Text);
                end = Int32.Parse(HowManyPlayers.Text);
                liveadd = Int32.Parse(LiveAddBox.Text);
            }
            else
            {
                start = 8;
                end = 4;
                liveadd = 0;
            }
            Game game1 = new Game(this, start, end, liveadd);

            Discharge();

            CurrentGame = game1.StartGame();

            AssignNames();
        }

        public void New_Round(object sender, RoutedEventArgs e)
        {
            ChangeEnableSub(true);
            ChangeEnable(ButtonExposeDict, true);
            ChangeEnable(ButtonVoteDict, false);
            VoteQuantity = 0;
            ClearBlocks(BlockVoteQuantityDict);
            CurrentGame.ResetVotes();
            CurrentGame.StartNewRound();
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
                CurrentGame.Talking(false);
            }
        }

        private void Voting_On(object sender, RoutedEventArgs e)
        {
            ChangeEnable(ButtonVoteDict, true);
            ChangeEnable(ButtonExposeDict, false);
            CurrentGame.Voting(CurrentGame.PlayersToVote);
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
                //Обработка и исключение, либо переход в раунд выживания
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
                    string number = i.ToString();
                    TextBlockDict["BlockPlayer" + number].Text = "";
                    break;
                }
            }
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
            }
        }
    }

}
