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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RNSR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Map.Visibility = Visibility.Hidden;
            ViewOrder.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Hidden;
            Pay.Visibility = Visibility.Hidden;
            ItemList.Visibility = Visibility.Hidden;
            HeaderFooter.Visibility = Visibility.Hidden;
            LoginScreen.Visibility = Visibility.Visible;
        }

        private void ResetButtons()
        {
            MapButton.Background = new SolidColorBrush(Color.FromRgb(122, 121, 121));
            ViewOrderButton.Background = new SolidColorBrush(Color.FromRgb(122, 121, 121));
            MenuButton.Background = new SolidColorBrush(Color.FromRgb(122, 121, 121));
            PayButton.Background = new SolidColorBrush(Color.FromRgb(122, 121, 121));
            MoreButton.Background = new SolidColorBrush(Color.FromRgb(122, 121, 121));
        }

        private void HideAllScreensAfterLogin()
        {
            Map.Visibility = Visibility.Hidden;
            ViewOrder.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Hidden;
            Pay.Visibility = Visibility.Hidden;
            ItemList.Visibility = Visibility.Hidden;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (Password.Password.ToString() == "" || Username.Text.ToString() == "")
            {
                Storyboard sb = this.FindResource("ErrorLoginMessage") as Storyboard;
                sb.Begin();
                Password.Password = "";
            }
            else
            {
                LoginScreen.Visibility = Visibility.Hidden;
                Map.Visibility = Visibility.Visible;
                HeaderFooter.Visibility = Visibility.Visible;
                MapButton.Background = new SolidColorBrush(Color.FromRgb(135, 40, 40));
                HeaderScreenName.Text = "Floor Map";
            }
        }

        private void MapButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResetButtons();
            MapButton.Background = new SolidColorBrush(Color.FromRgb(135, 40, 40));
            HeaderScreenName.Text = "Floor Map";
            this.HideAllScreensAfterLogin();
            Map.Visibility = Visibility.Visible;
        }

        private void ViewOrderButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResetButtons();
            ViewOrderButton.Background = new SolidColorBrush(Color.FromRgb(135, 40, 40));
            HeaderScreenName.Text = "View Order";
            this.HideAllScreensAfterLogin();
            ViewOrder.Visibility = Visibility.Visible;
            ItemList.Visibility = Visibility.Visible;
        }

        private void MenuButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResetButtons();
            MenuButton.Background = new SolidColorBrush(Color.FromRgb(135, 40, 40));
            HeaderScreenName.Text = "Add Items to Order";
            this.HideAllScreensAfterLogin();
            Menu.Visibility = Visibility.Visible;
            ItemList.Visibility = Visibility.Visible;
        }

        private void PayButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResetButtons();
            PayButton.Background = new SolidColorBrush(Color.FromRgb(135, 40, 40));
            HeaderScreenName.Text = "Pay for Order";
            this.HideAllScreensAfterLogin();
            Pay.Visibility = Visibility.Visible;
            ItemList.Visibility = Visibility.Visible;
        }

        private void MoreButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResetButtons();
            MoreButton.Background = new SolidColorBrush(Color.FromRgb(135, 40, 40));
            HeaderScreenName.Text = "More Options";
            this.HideAllScreensAfterLogin();
            More.Visibility = Visibility.Visible;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Map.Visibility = Visibility.Hidden;
            ViewOrder.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Hidden;
            Pay.Visibility = Visibility.Hidden;
            ItemList.Visibility = Visibility.Hidden;
            HeaderFooter.Visibility = Visibility.Hidden;
            LoginScreen.Visibility = Visibility.Visible;
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            //This is a button to be removed by the final build. It simply adds a default item to the ItemViewer for testing.

            bool status = false;
            string description = "Bestest burger with dank amounts of ketchup";
            string price = "$19.99";

            AnItemControl anItem = new AnItemControl(status, description, price);
            this.Items.Children.Add(anItem);
            this.Scroller.ScrollToEnd();
        }

        private void TestButton2_Click(object sender, RoutedEventArgs e)
        {
            //This is a button to be removed by the final build. It simply adds a default item to the ItemViewer for testing.

            bool status = true;
            string description = "Some lesser burger";
            string price = "$9.99";

            AnItemControl anItem = new AnItemControl(status, description, price);
            this.Items.Children.Add(anItem);
            this.Scroller.ScrollToEnd();
        }
    }
}