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
        private List<AnItemControl> selectedItems;
        public bool allowModify { get; private set; } = false;

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
            ModBlock.Visibility = Visibility.Hidden;

            selectedItems = new List<AnItemControl>();
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
                Username.Text = "";
                Password.Password = "";
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
            float totalPrice = 0.00f;
            float selectedPrice = 0.00f;
            foreach(object child in Items.Children)
            {
                if (child is AnItemControl)
                {
                    totalPrice += (child as AnItemControl).price;
                    if ((child as AnItemControl).selected)
                        selectedPrice += (child as AnItemControl).price;
                }
            }
            TotalRemaining.Text = totalPrice.ToString("c2");
            TotalSelected.Text = selectedPrice.ToString("c2");
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

        //This is a button to be removed by the final build. It simply adds a default item to the ItemViewer for testing.
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            string description = "Bestest burger with dank amounts of ketchup";
            float price = 19.99f;
            AnItemControl anItem = new AnItemControl(description, price, selectedItems, this);
            this.Items.Children.Add(anItem);
            this.Scroller.ScrollToEnd();
        }

        //This is a button to be removed by the final build. It simply adds a default item to the ItemViewer for testing.
        private void TestButton2_Click(object sender, RoutedEventArgs e)
        {
            string description = "Some lesser burger";
            float price = 9.49f;
            AnItemControl anItem = new AnItemControl(description, price, selectedItems, this);
            this.Items.Children.Add(anItem);
            this.Scroller.ScrollToEnd();
        }

        //This is a button to be removed by the final build. It simply displays some item info
        private void TestGetSelected_Click(object sender, RoutedEventArgs e)
        {
            float totalPrice = 0;
            foreach (AnItemControl anItem in selectedItems)
            {
                totalPrice += anItem.price;
            }
            this.TestTotalPrice.Text = String.Format("Total Price of Selected: {0:C2}", totalPrice);
        }

        public void UpdateSelected()
        {
            float selectedPrice = 0.00f;
            foreach (object child in Items.Children)
            {
                if (child is AnItemControl)
                {
                    if ((child as AnItemControl).selected)
                        selectedPrice += (child as AnItemControl).price;
                }
            }
            TotalSelected.Text = selectedPrice.ToString("c2");
        }

        private void PayAllButton_Click(object sender, RoutedEventArgs e)
        {
            TotalRemaining.Text = "$0.00";
            this.Items.Children.Clear();
        }

        private void PaySelectedButton_Click(object sender, RoutedEventArgs e)
        {
            decimal totalPrice = Decimal.Parse(TotalRemaining.Text, System.Globalization.NumberStyles.Currency);
            totalPrice -= Decimal.Parse(TotalSelected.Text, System.Globalization.NumberStyles.Currency);
            TotalRemaining.Text = totalPrice.ToString("c2");
            TotalSelected.Text = "$0.00";
            List<AnItemControl> itemControlToRemove = new List<AnItemControl>();
            foreach(object child in Items.Children)
            {
                if(child is AnItemControl)
                {
                    if ((child as AnItemControl).selected)
                        itemControlToRemove.Add((child as AnItemControl));
                }
            }
            foreach(AnItemControl item in itemControlToRemove)
            {
                Items.Children.Remove(item);
            }
        }

        private void SendAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (AnItemControl anItem in Items.Children)
            {
                anItem.ItemNo.Visibility = Visibility.Hidden;
                anItem.ItemYes.Visibility = Visibility.Visible;
            }
        }

        private void SendSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (AnItemControl anItem in selectedItems)
            {
                anItem.ItemNo.Visibility = Visibility.Hidden;
                anItem.ItemYes.Visibility = Visibility.Visible;
            }
        }

        private void RemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (AnItemControl anItem in selectedItems)
            {
                Items.Children.Remove(anItem);
            }
            selectedItems.Clear();
        }

        private void ModifySelected_Click(object sender, RoutedEventArgs e)
        {
            this.allowModify = !this.allowModify; //Toggle the bool
            if (this.allowModify)
            {
                while(selectedItems.Count > 0)
                {
                    AnItemControl anItem = selectedItems.First();
                    anItem.Deselect();
                    selectedItems.Remove(anItem);
                }
                selectedItems.Clear();
                ModifySelected.Background = new SolidColorBrush(Color.FromRgb(0, 200, 0));
                ToggleViewer.Fill = new SolidColorBrush(Color.FromRgb(0, 200, 0));
                ModBlock.Visibility = Visibility.Visible;
                foreach (AnItemControl anItem in Items.Children)
                {
                    anItem.ItemDescription.IsReadOnly = false;
                    anItem.ItemPrice.IsReadOnly = false;
                    anItem.Selector.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                ModifySelected.Background = new SolidColorBrush(Color.FromRgb(244, 152, 43));
                ToggleViewer.Fill = new SolidColorBrush(Color.FromRgb(0, 153, 178));
                ModBlock.Visibility = Visibility.Hidden;
                foreach (AnItemControl anItem in Items.Children)
                {
                    anItem.ItemDescription.IsReadOnly = true;
                    anItem.ItemPrice.IsReadOnly = true;
                    anItem.Selector.Visibility = Visibility.Visible;
                    //DEBUG: Do error checking on price and update it.
                }
            }
        }
    }
}
