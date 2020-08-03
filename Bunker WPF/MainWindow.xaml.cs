﻿using System;
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

        //Список со всеми кнопками
        public List<Button> AllButtons { get; set; }

        //Список с кнопками, отключаемыми в конце игры
        public List<Button> SubButtons { get; set; }

        //В данной хранится экземпляр запущенной игры
        private Game CurrentGame { get; set; }

        //Словари, с помощью которого можно получить переменную по её текстовому значению
        public Dictionary<string, TextBox> TextBoxDict { get; set; }

        public Dictionary<string, TextBlock> TextBlockDict { get; set; }

        public Dictionary<string, Button> ButtonDeleteDict { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            BNext_Round.IsEnabled = false;
            BNext_Talking.IsEnabled = false;
            BVoting.IsEnabled = false;
            BNew_Condition.IsEnabled = false;
            
        }

        //Присвоение всех словарей для доступа к XAML по именам
        private void AssignDict()
        {
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
            
            //Очищается чат
            GameProgress.Text = "";

            //Все кнопки, которые могли быть отключены - включаются
            BNext_Round.IsEnabled = true;
            BNext_Talking.IsEnabled = true;
            BVoting.IsEnabled = true;
            BNew_Condition.IsEnabled = true;

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

            AssignDict();
            Discharge();

            CurrentGame = game1.StartGame();

            AssignNames();
        }

        public void New_Round(object sender, RoutedEventArgs e)
        {
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

        }

        private void Voting_On(object sender, RoutedEventArgs e)
        {
            CurrentGame.Voting(CurrentGame.PlayersToVote);
        }

        private void DeletePlayer(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            int i = Int32.Parse(btn.Name.Substring(btn.Name.Length - 2));
            GameProgress.Text = i.ToString();
            foreach (var player in Player.PlayersList)
            {
                if(player.Quanity == i)
                {
                    player.DeletePlayer();
                    string number = i.ToString();
                    GameProgress.Text = number;

                    TextBlockDict["BlockPlayer" + number].Text = "";
                    break;
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
