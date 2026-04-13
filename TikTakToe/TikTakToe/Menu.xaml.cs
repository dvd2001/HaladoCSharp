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
        private Style cbDarkStyle;
        private Style cbLightStyle;
        private Style btnDarkStyle;
        private Style btnLightStyle;
        private Style txtbDarkStyle;
        private Style txtbLightStyle;
        private Style lblDarkStyle;
        private Style lblLightStyle;
        private Style cbxDarkStyle;
        private Style cbxLightStyle;

        public Menu()
        {
            isInit = true;
            InitializeComponent();
            isInit = false;
            DarkStyleSetUp();
            LightStyleSetup();
            SetupStyle();
        }
        public void DarkStyleSetUp()
        {
            cbDarkStyle = new Style(typeof(ComboBox));
            cbDarkStyle.Setters.Add(new Setter(ComboBox.BackgroundProperty, System.Windows.Media.Brushes.Black));
            cbDarkStyle.Setters.Add(new Setter(ComboBox.ForegroundProperty, System.Windows.Media.Brushes.White));
            btnDarkStyle = new Style(typeof(Button));
            btnDarkStyle.Setters.Add(new Setter(Button.BackgroundProperty, System.Windows.Media.Brushes.DarkSlateGray));
            btnDarkStyle.Setters.Add(new Setter(Button.ForegroundProperty, System.Windows.Media.Brushes.White));
            btnDarkStyle.Resources.Add(typeof(Border), new Style(typeof(Border))
            {
                Setters =
                {
                    new Setter(Border.CornerRadiusProperty, new CornerRadius(10))
                }
            });
            btnDarkStyle.Setters.Add(new Setter(Button.BorderThicknessProperty, new Thickness(0)));
            btnDarkStyle.Setters.Add(new Setter(Button.PaddingProperty, new Thickness(10)));
            btnDarkStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(10)));
            btnDarkStyle.Setters.Add(new Setter(Button.FontSizeProperty, 30.0));
            txtbDarkStyle = new Style(typeof(TextBox));
            txtbDarkStyle.Setters.Add(new Setter(TextBox.ForegroundProperty, System.Windows.Media.Brushes.White));
            lblDarkStyle = new Style(typeof(Label));
            lblDarkStyle.Setters.Add(new Setter(Label.ForegroundProperty, System.Windows.Media.Brushes.White));
            cbxDarkStyle = new Style(typeof(CheckBox));
            cbxDarkStyle.Setters.Add(new Setter(CheckBox.ForegroundProperty, System.Windows.Media.Brushes.White));
        }

        public void LightStyleSetup()
        {
            cbLightStyle = new Style(typeof(ComboBox));
            cbLightStyle.Setters.Add(new Setter(ComboBox.BackgroundProperty, System.Windows.Media.Brushes.White));
            cbLightStyle.Setters.Add(new Setter(ComboBox.ForegroundProperty, System.Windows.Media.Brushes.Black));
            btnLightStyle = new Style(typeof(Button));
            btnLightStyle.Setters.Add(new Setter(Button.BackgroundProperty, System.Windows.Media.Brushes.LightGray));
            btnLightStyle.Setters.Add(new Setter(Button.ForegroundProperty, System.Windows.Media.Brushes.Black));
            btnLightStyle.Resources.Add(typeof(Border), new Style(typeof(Border))
            {
                Setters =
                {
                    new Setter(Border.CornerRadiusProperty, new CornerRadius(10))
                }
            });
            btnLightStyle.Setters.Add(new Setter(Button.BorderThicknessProperty, new Thickness(0)));
            btnLightStyle.Setters.Add(new Setter(Button.PaddingProperty, new Thickness(10)));
            btnLightStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(10)));
            btnLightStyle.Setters.Add(new Setter(Button.FontSizeProperty, 30.0));
            txtbLightStyle = new Style(typeof(TextBox));
            txtbLightStyle.Setters.Add(new Setter(TextBox.ForegroundProperty, System.Windows.Media.Brushes.Black));
            lblLightStyle = new Style(typeof(Label));
            lblLightStyle.Setters.Add(new Setter(Label.ForegroundProperty, System.Windows.Media.Brushes.Black));
            cbxLightStyle = new Style(typeof(CheckBox));
            cbxLightStyle.Setters.Add(new Setter(CheckBox.ForegroundProperty, System.Windows.Media.Brushes.Black));
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
                        { typeof(ComboBox), cbDarkStyle },
                        { typeof(Button), btnDarkStyle },
                        { typeof(TextBox), txtbDarkStyle },
                        { typeof(Label), lblDarkStyle },
                        { typeof(CheckBox), cbxDarkStyle }
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
                            { typeof(ComboBox), cbLightStyle },
                            { typeof(Button), btnLightStyle },
                            { typeof(TextBox), txtbLightStyle },
                            { typeof(Label), lblLightStyle },
                            { typeof(CheckBox), cbxLightStyle }
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
            darkMode.Resources.Add(typeof(ComboBox), cbDarkStyle);
            darkMode.Resources.Add(typeof(Button), btnDarkStyle);
            darkMode.Resources.Add(typeof(TextBox), txtbDarkStyle);
            darkMode.Resources.Add(typeof(Label), lblDarkStyle);
            darkMode.Resources.Add(typeof(CheckBox), cbxDarkStyle);
            darkMode.Setters.Add(new Setter(Window.BackgroundProperty, System.Windows.Media.Brushes.Black));
            this.Style = darkMode;
        }
        private void DarkMode_Unchecked(object sender, RoutedEventArgs e)
        {
            isDarkMode = false;
            Style lightMode = new Style(typeof(Window));
            lightMode.Resources.Add(typeof(ComboBox), cbLightStyle);
            lightMode.Resources.Add(typeof(Button), btnLightStyle);
            lightMode.Resources.Add(typeof(TextBox), txtbLightStyle);
            lightMode.Resources.Add(typeof(Label), lblLightStyle);
            lightMode.Resources.Add(typeof(CheckBox), cbxLightStyle);
            lightMode.Setters.Add(new Setter(Window.BackgroundProperty, System.Windows.Media.Brushes.White));
            this.Style = lightMode;
        }
    }
}
