using System;
using System.Linq;
using System.Windows;

namespace TikTakToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private int size;
        private int win;
        private bool isMulti;

        public Menu()
        {
            InitializeComponent();
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            size = int.Parse(cbSizeSelector.SelectionBoxItem.ToString().Split('x')[0]);
            win = int.Parse(cbWinSelector.SelectionBoxItem.ToString());
            if (win>size)
            {
                MessageBox.Show("Invalid game parameters. The win condition cannot be greater than the size if the board.");
                return;
            }
            isMulti = false;
            Game game = new Game(size, win, isMulti);
            game.Show();
            game.Closed += (s, args) => this.Show();
            this.Hide();
        }

        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {
            
            size = int.Parse(cbSizeSelector.SelectionBoxItem.ToString().Split('x')[0]);
            win = int.Parse(cbWinSelector.SelectionBoxItem.ToString());
            if(win>size)
            {
                MessageBox.Show("Invalid game parameters. The win condition cannot be greater than the size of the board.");
                return;
            }
            isMulti = true;
            Game game = new Game(size, win, isMulti);
            game.Show();
            game.Closed += (s, args) => this.Show();
            this.Hide();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
