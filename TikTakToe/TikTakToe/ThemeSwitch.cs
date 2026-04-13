using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TikTakToe
{
    internal class ThemeSwitch
    {
        public Style cbDarkStyle;
        public Style cbLightStyle;
        public Style btnDarkStyle;
        public Style btnLightStyle;
        public Style txtbDarkStyle;
        public Style txtbLightStyle;
        public Style lblDarkStyle;
        public Style lblLightStyle;
        public Style cbxDarkStyle;
        public Style cbxLightStyle;

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
    }
}
