using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        private bool isInit;

        public Menu()
        {
            isInit = true;
            InitializeComponent();
            isInit = false;
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            size = int.Parse(cbSizeSelector.SelectionBoxItem.ToString().Split('x')[0]);
            win = int.Parse(cbWinSelector.SelectionBoxItem.ToString());
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

        private void WinCondition_Changed(object sender, RoutedEventArgs e)
        {
            if (!isInit)
            {
                ComboBoxItem winSelector = cbWinSelector.SelectedValue as ComboBoxItem;
                ComboBoxItem boardSelector = cbSizeSelector.SelectedValue as ComboBoxItem;
                int boardSize = int.Parse(boardSelector.Content.ToString().Split('x')[0]);
                int winBy = int.Parse(winSelector.Content.ToString());
                Style style = new Style(typeof(ComboBox));
                if (boardSize < winBy)
                {
                    MessageBox.Show("The win condition cannot be greater then the size of the board!");
                    btnSinglePlayer.IsEnabled = false;
                    btnMultiPlayer.IsEnabled = false;
                }
                else
                {
                    btnSinglePlayer.IsEnabled = true;
                    btnMultiPlayer.IsEnabled = true;
                }
            }
        }
    }
}
