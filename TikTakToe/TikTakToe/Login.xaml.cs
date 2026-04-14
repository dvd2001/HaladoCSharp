using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TikTakToe
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private bool isDarkMode;
        private Style txtbDark;
        private Style txtbLight;
        private Style btnDark;
        private Style btnLight;
        private Style cbxDark;
        private Style cbxLight;
        private Style lblDark;
        private Style lblLight;
        private Style txtDark;
        private Style txtLight;
        public Login()
        {
            InitializeComponent();
            btnLogin.IsEnabled = false;
            DarkModeSetter();
            LightModeSetter();
            SetupStyle();
        }

        private void SetupStyle()
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            var value = key?.GetValue("AppsUseLightTheme");
            if (!(value is bool))
            {
                cbxDarkMode.IsChecked = true;
                isDarkMode = true;
                this.Style = new Style(typeof(Window))
                {
                    Resources =
                    {
                        { typeof(TextBox), txtbDark },
                        { typeof(Button), btnDark },
                        { typeof(CheckBox), cbxDark },
                        { typeof(Label), lblDark },
                        { typeof(TextBlock), txtDark }
                    },
                    Setters =
                    {
                        new Setter(Window.BackgroundProperty, Brushes.Black)
                    }
                };
            }
        }

        private void DarkModeSetter()
        {
            txtbDark = new Style(typeof(TextBox));
            txtbDark.Setters.Add(new Setter(TextBox.BackgroundProperty, Brushes.DarkSlateGray));
            txtbDark.Setters.Add(new Setter(TextBox.ForegroundProperty, Brushes.White));
            btnDark = new Style(typeof(Button));
            btnDark.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.DarkSlateGray));
            btnDark.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            btnDark.Resources.Add((typeof(Border)), new Style(typeof(Border))
            {
                Setters =
                {
                    new Setter(Border.CornerRadiusProperty, new CornerRadius(10)),
                }
            });
            btnDark.Triggers.Add(new Trigger
            {
                Property = Button.IsEnabledProperty,
                Value = false,
                Setters =
                {
                    new Setter(Button.OpacityProperty, 0.5)
                }
            });
            btnDark.Setters.Add(new Setter(Button.FontSizeProperty, 30.0));
            btnDark.Setters.Add(new Setter(Button.MaxWidthProperty, 200.0));
            cbxDark = new Style(typeof(CheckBox));
            cbxDark.Setters.Add(new Setter(CheckBox.ForegroundProperty, Brushes.White));
            lblDark = new Style(typeof(Label));
            lblDark.Setters.Add(new Setter(Label.ForegroundProperty, Brushes.White));
            txtDark = new Style(typeof(TextBlock));
            txtDark.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brushes.White));

        }
        private void LightModeSetter()
        {
            txtbLight = new Style(typeof(TextBox));
            txtbLight.Setters.Add(new Setter(TextBox.BackgroundProperty, Brushes.White));
            txtbLight.Setters.Add(new Setter(TextBox.ForegroundProperty, Brushes.Black));
            btnLight = new Style(typeof(Button));
            btnLight.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.LightGray));
            btnLight.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
            btnLight.Resources.Add((typeof(Border)), new Style(typeof(Border))
            {
                Setters =
                {
                    new Setter(Border.CornerRadiusProperty, new CornerRadius(10)),
                }
            });
            btnLight.Setters.Add(new Setter(Button.FontSizeProperty, 30.0));
            cbxLight = new Style(typeof(CheckBox));
            cbxLight.Setters.Add(new Setter(CheckBox.ForegroundProperty, Brushes.Black));
            lblLight = new Style(typeof(Label));
            lblLight.Setters.Add(new Setter(Label.ForegroundProperty, Brushes.Black));
            txtLight = new Style(typeof(TextBlock));
            txtLight.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brushes.Black));
        }

        private void DarkMode_Checked(object sender, RoutedEventArgs e)
        {
            isDarkMode = true;
            this.Style = new Style(typeof(Window))
            {
                Resources =
                {
                    { typeof(TextBox), txtbDark },
                    { typeof(Button), btnDark },
                    { typeof(CheckBox), cbxDark },
                    { typeof(Label), lblDark },
                    { typeof(TextBlock), txtDark }
                },
                Setters =
                {
                    new Setter(Window.BackgroundProperty, Brushes.Black)
                }
            };
        }
        private void DarkMode_Unchecked(object sender, RoutedEventArgs e)
        {
            isDarkMode = false;
            this.Style = new Style(typeof(Window))
            {
                Resources =
                {
                    { typeof(TextBox), txtbLight },
                    { typeof(Button), btnLight },
                    { typeof(CheckBox), cbxLight },
                    { typeof(Label), lblLight },
                    { typeof(TextBlock), txtLight }
                },
                Setters =
                {
                    new Setter(Window.BackgroundProperty, Brushes.White)
                }
            };
        }
        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            btnLogin.IsEnabled = !string.IsNullOrWhiteSpace(txtUsername.Text);
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            Menu menu = new Menu(username, isDarkMode);
            menu.Show();
            this.Close();
        }
    }
}
