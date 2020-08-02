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

        //Список со всеми кнопками
        public List<Button> AllButtons { get; set; }

        //Список с кнопками, отключаемыми в конце игры
        public List<Button> SubButtons { get; set; }

        //В данной хранится экземпляр запущенной игры
        private Game CurrentGame { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            BNext_Round.IsEnabled = false;
            BNext_Talking.IsEnabled = false;
            BVoting.IsEnabled = false;
        }
        

        private void New_Game(object sender, RoutedEventArgs e)
        {
            //Все кнопки, которые могли быть отключены - включаются
            BNext_Round.IsEnabled = true;
            BNext_Talking.IsEnabled = true;
            BVoting.IsEnabled = true;

            int number;
            int start;
            int end;
            if (Int32.TryParse(HowManyPlayers.Text, out number) && Int32.TryParse(PlayersForEndingGame.Text, out number))
            {
                start = Int32.Parse(PlayersForEndingGame.Text);
                end = Int32.Parse(HowManyPlayers.Text);
            }
            else
            {
                start = 8;
                end = 4;
            }
            Game game1 = new Game(this, start, end);
            CurrentGame = game1.StartGame();

            
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
                if (Player.PlayersList[i - 1] != null)
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

      
    }

}
