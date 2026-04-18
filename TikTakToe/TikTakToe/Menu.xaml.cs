using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TikTakToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private string username;
        private int size;
        private int win;
        private int lineThickness;
        private bool isMulti;
        private bool isInit;
        private bool isDarkMode;

        public Menu(string username, bool isDarkMode)
        {
            this.username = username;
            this.isDarkMode = isDarkMode;
            isInit = true;
            InitializeComponent();
            isInit = false;
            SetupSelections();
            ((App)Application.Current).ThemeToggle(isDarkMode);
        }
        private void SetupSelections()
        {
            if (File.Exists($"{username}.txt"))
            {
                string[] settings = File.ReadAllLines($"{username}.txt");
                cbSizeSelector.SelectedIndex = int.Parse(settings[0].Split('x')[0]) - 3;
                cbWinSelector.SelectedIndex = int.Parse(settings[1]) - 3;
                cbLineThickness.SelectedIndex = int.Parse(settings[2]) - 1;
                cbxDarkMode.IsChecked = isDarkMode = bool.Parse(settings[3]);
            }
        }
        
        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            size = int.Parse(cbSizeSelector.SelectionBoxItem.ToString().Split('x')[0]);
            win = int.Parse(cbWinSelector.SelectionBoxItem.ToString());
            lineThickness = int.Parse(cbLineThickness.SelectionBoxItem.ToString());
            isMulti = false;
            Game game = new Game(size, win, lineThickness, isMulti, isDarkMode);
            game.Show();
            game.Closed += (s, args) => this.Show();
            this.Hide();
        }

        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {

            size = int.Parse(cbSizeSelector.SelectionBoxItem.ToString().Split('x')[0]);
            win = int.Parse(cbWinSelector.SelectionBoxItem.ToString());
            lineThickness = int.Parse(cbLineThickness.SelectionBoxItem.ToString());
            isMulti = true;
            Game game = new Game(size, win, lineThickness, isMulti, isDarkMode);
            game.Show();
            game.Closed += (s, args) => this.Show();
            this.Hide();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            string[] settings = new string[]
            {
                cbSizeSelector.SelectionBoxItem.ToString(),
                cbWinSelector.SelectionBoxItem.ToString(),
                cbLineThickness.SelectionBoxItem.ToString(),
                cbxDarkMode.IsChecked.ToString()
            };
            File.WriteAllLines($"{username}.txt", settings);
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
        private void DarkMode_Checked(object sender, RoutedEventArgs e)
        {
            isDarkMode = true;
            ((App)Application.Current).ThemeToggle(isDarkMode);
        }
        private void DarkMode_Unchecked(object sender, RoutedEventArgs e)
        {
            isDarkMode = false;
            ((App)Application.Current).ThemeToggle(isDarkMode);
        }
    }
}
