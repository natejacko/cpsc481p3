using System;
using System.Collections.Generic;
using System.Globalization;
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
        public List<AnItemControl> selectedItems;
        public bool allowModify { get; private set; } = false;
        public List<object> itemDataBase;

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

            PopulateFromDB(); //Creates all food subcategories based on a fake database
            foreach(UIElement child in SubCategoryView.Children) //Set all food subcategories to hidden
            {
                child.Visibility = Visibility.Hidden;
            }

            selectedItems = new List<AnItemControl>();
        }

        private void PopulateFromDB()
        {
            //We can imagine these lists are actually rows pulled from a database
            //Number of names and prices must match up
            List<string> drinkNames = new List<string>()
                { "Apple Juice", "Banana Smoothie", "Cream Soda", "Fire Ant Shot" };
            List<float> drinkPrices = new List<float>()
                { 2.99f, 4.99f, 3.99f, 9.99f };

            List<string> burgerNames = new List<string>()
                { "Burger1", "Burger2", "Burger3", "Burger4", "Burger5", "Burger6", "Burger7", "Burger8", "Burger9", "Burger10", "Burger11", "Burger12", "Burger13", };
            List<float> burgerPrices = new List<float>()
                { 2.99f, 4.99f, 3.99f, 9.99f, 2.99f, 4.99f, 3.99f, 9.99f, 2.99f, 4.99f, 9.99f, 2.99f, 4.99f};

            for (int i = 0; i < drinkNames.Count; i++)
            {
                AddItemControl anItem = new AddItemControl(drinkNames[i], drinkPrices[i], this);
                this.DrinksView.Children.Add(anItem);
            }
            for (int i = 0; i < burgerNames.Count; i++)
            {
                AddItemControl anItem = new AddItemControl(burgerNames[i], burgerPrices[i], this);
                this.BurgersView.Children.Add(anItem);
            }
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

            //Debug: Should reset the item viewer. (remove all children)
            //Should reset all Menu item note boxes. (remove all children, repopulate them)
            //Note: The above requires that tables keep track of their own item viewers.
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
                    //DEBUG: If modified, must re-send to kitchen.
                }
            }
        }

        private void Table1_Click(object sender, RoutedEventArgs e)
        {
            HeaderTableNo.Text = "Table #1";
        }

        private void Table2_Click(object sender, RoutedEventArgs e)
        {
            HeaderTableNo.Text = "Table #2";
        }

        private void Table3_Click(object sender, RoutedEventArgs e)
        {
            HeaderTableNo.Text = "Table #3";
        }

        private void Table4_Click(object sender, RoutedEventArgs e)
        {
            HeaderTableNo.Text = "Table #4";
        }

        private void Table5_Click(object sender, RoutedEventArgs e)
        {
            HeaderTableNo.Text = "Table #5";
        }

        private void Table6_Click(object sender, RoutedEventArgs e)
        {
            HeaderTableNo.Text = "Table #6";
        }

        private void Table7_Click(object sender, RoutedEventArgs e)
        {
            HeaderTableNo.Text = "Table #7";
        }

        private void Table8_Click(object sender, RoutedEventArgs e)
        {
            HeaderTableNo.Text = "Table #8";
        }

        private void Table9_Click(object sender, RoutedEventArgs e)
        {
            HeaderTableNo.Text = "Table #9";
        }

        private void Table10_Click(object sender, RoutedEventArgs e)
        {
            HeaderTableNo.Text = "Table #10";
        }

        private void View1_Click(object sender, RoutedEventArgs e)
        {
            PatioViewer.Visibility = Visibility.Hidden;
            Viewer1.Visibility = Visibility.Visible;
            View1.Background = new SolidColorBrush(Color.FromRgb(133, 20, 20));
            View2.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        private void View2_Click(object sender, RoutedEventArgs e)
        {
            Viewer1.Visibility = Visibility.Hidden;
            PatioViewer.Visibility = Visibility.Visible;
            View2.Background = new SolidColorBrush(Color.FromRgb(133, 20, 20));
            View1.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        private void DrinksButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement child in SubCategoryView.Children)
            {
                child.Visibility = Visibility.Hidden;
            }
            DrinksView.Visibility = Visibility.Visible;
        }

        private void BurgersButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement child in SubCategoryView.Children)
            {
                child.Visibility = Visibility.Hidden;
            }
            BurgersView.Visibility = Visibility.Visible;
        }

        private void AddCustomItem_Click(object sender, RoutedEventArgs e)
        {
            string description = this.CustomDescription.Text;
            string tempPrice = this.CustomPrice.Text;
            float price = float.Parse(tempPrice, CultureInfo.InvariantCulture.NumberFormat); //DEBUG: Needs error checking
            AnItemControl anItem = new AnItemControl(description, price, selectedItems, this);
            this.Items.Children.Add(anItem);
            this.Scroller.ScrollToEnd();
        }

        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement child in SubCategoryView.Children)
            {
                child.Visibility = Visibility.Hidden;
            }
            CustomView.Visibility = Visibility.Visible;
        }
    }
}
