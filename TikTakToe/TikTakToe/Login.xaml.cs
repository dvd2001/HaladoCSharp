using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace TikTakToe
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private bool isDarkMode;
        public Login()
        {
            InitializeComponent();
            DarkModeSetup();
            btnLogin.IsEnabled = false;
        }
        private void DarkModeSetup()
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            var value = key?.GetValue("AppsUseLightTheme");
            if (value != null && value.Equals(0))
            {
                isDarkMode = true;
                cbxDarkMode.IsChecked = true;
            }
            else isDarkMode = false;
            ((App)Application.Current).ThemeToggle(isDarkMode);
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
