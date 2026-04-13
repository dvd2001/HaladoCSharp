using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using TikTakToe;

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
        private bool isDarkMode;
        private ThemeSwitch themeSwitch = new ThemeSwitch();

        public Menu()
        {
            isInit = true;
            InitializeComponent();
            isInit = false;
            themeSwitch.DarkStyleSetUp();
            themeSwitch.LightStyleSetup();
            SetupStyle();
        }
        private void SetupStyle()
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            var value = key?.GetValue("AppsUseLightTheme");
            if (!(value is bool))
            {
                cbxDarkMode.IsChecked = true;
                this.Style = new Style(typeof(Window))
                {
                    Resources =
                    {
                        { typeof(ComboBox), themeSwitch.cbDarkStyle },
                        { typeof(Button), themeSwitch.btnDarkStyle },
                        { typeof(TextBox), themeSwitch.txtbDarkStyle },
                        { typeof(Label), themeSwitch.lblDarkStyle },
                        { typeof(CheckBox), themeSwitch.cbxDarkStyle }
                    },
                    Setters =
                    {
                        new Setter(Window.BackgroundProperty, System.Windows.Media.Brushes.Black)
                    }
                };
            }
            else
            {
                this.Style = new Style(typeof(Window))
                {
                    Resources =
                        {
                            { typeof(ComboBox), themeSwitch.cbLightStyle },
                            { typeof(Button), themeSwitch.btnLightStyle },
                            { typeof(TextBox), themeSwitch.txtbLightStyle },
                            { typeof(Label), themeSwitch.lblLightStyle },
                            { typeof(CheckBox), themeSwitch.cbxLightStyle }
                        },
                    Setters =
                        {
                            new Setter(Window.BackgroundProperty, System.Windows.Media.Brushes.White)
                        }
                };
            }
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            size = int.Parse(cbSizeSelector.SelectionBoxItem.ToString().Split('x')[0]);
            win = int.Parse(cbWinSelector.SelectionBoxItem.ToString());
            isMulti = false;
            Game game = new Game(size, win, isMulti, isDarkMode);
            game.Show();
            game.Closed += (s, args) => this.Show();
            this.Hide();
        }

        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {

            size = int.Parse(cbSizeSelector.SelectionBoxItem.ToString().Split('x')[0]);
            win = int.Parse(cbWinSelector.SelectionBoxItem.ToString());
            isMulti = true;
            Game game = new Game(size, win, isMulti, isDarkMode);
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
        private void DarkMode_Checked(object sender, RoutedEventArgs e)
        {
            isDarkMode = true;
            Style darkMode = new Style(typeof(Window));
            darkMode.Resources.Add(typeof(ComboBox), themeSwitch.cbDarkStyle);
            darkMode.Resources.Add(typeof(Button), themeSwitch.btnDarkStyle);
            darkMode.Resources.Add(typeof(TextBox), themeSwitch.txtbDarkStyle);
            darkMode.Resources.Add(typeof(Label), themeSwitch.lblDarkStyle);
            darkMode.Resources.Add(typeof(CheckBox), themeSwitch.cbxDarkStyle);
            darkMode.Setters.Add(new Setter(Window.BackgroundProperty, System.Windows.Media.Brushes.Black));
            this.Style = darkMode;
        }
        private void DarkMode_Unchecked(object sender, RoutedEventArgs e)
        {
            isDarkMode = false;
            Style lightMode = new Style(typeof(Window));
            lightMode.Resources.Add(typeof(ComboBox), themeSwitch.cbLightStyle);
            lightMode.Resources.Add(typeof(Button), themeSwitch.btnLightStyle);
            lightMode.Resources.Add(typeof(TextBox), themeSwitch.txtbLightStyle);
            lightMode.Resources.Add(typeof(Label), themeSwitch.lblLightStyle);
            lightMode.Resources.Add(typeof(CheckBox), themeSwitch.cbxLightStyle);
            lightMode.Setters.Add(new Setter(Window.BackgroundProperty, System.Windows.Media.Brushes.White));
            this.Style = lightMode;
        }
    }
}
