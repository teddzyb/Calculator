using Calculator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Calculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Calculator_Nav(object sender, EventArgs e)
        {
            this.FindByName<View>("MainCalculator").IsVisible = true;
            this.FindByName<View>("MidtermCalculator").IsVisible = false;
            this.FindByName<View>("SettingsPage").IsVisible = false;

            ImageButton btn = (ImageButton)sender;
            btn.Source = "calculator_icon_active.png";

            this.FindByName<ImageButton>("Midterm_Btn").Source = "midterm_icon_grey";
            this.FindByName<ImageButton>("Settings_Btn").Source = "setting_icon_grey";

            if (App.Current.MainPage != null)
            {
                var mp = App.Current.MainPage as NavigationPage;
                mp.BarBackgroundColor = ColorConverters.FromHex("#f8f9fa");
            }
        }

        private void Midterm_Nav(object sender, EventArgs e)
        {
            this.FindByName<View>("MainCalculator").IsVisible = false;
            this.FindByName<View>("MidtermCalculator").IsVisible = true;
            this.FindByName<View>("SettingsPage").IsVisible = false;

            ImageButton btn = (ImageButton)sender;
            btn.Source = "midterm_icon_active.png";

            this.FindByName<ImageButton>("Calculator_Btn").Source = "calculator_icon_grey";
            this.FindByName<ImageButton>("Settings_Btn").Source = "setting_icon_grey";

            if (App.Current.MainPage != null)
            {
                var mp = App.Current.MainPage as NavigationPage;
                mp.BarBackgroundColor = ColorConverters.FromHex("#222222");
            }
        }

        private void Setting_Nav(object sender, EventArgs e)
        {
            this.FindByName<View>("MainCalculator").IsVisible = false;
            this.FindByName<View>("MidtermCalculator").IsVisible = false;
            this.FindByName<View>("SettingsPage").IsVisible = true;

            ImageButton btn = (ImageButton)sender;
            btn.Source = "setting_icon_active.png";

            this.FindByName<ImageButton>("Calculator_Btn").Source = "calculator_icon_grey";
            this.FindByName<ImageButton>("Midterm_Btn").Source = "midterm_icon_grey";

            if (App.Current.MainPage != null)
            {
                var mp = App.Current.MainPage as NavigationPage;
                mp.BarBackgroundColor = ColorConverters.FromHex("#f8f9fa");
            }
        }
    }
}