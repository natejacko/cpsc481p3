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
        public List<ItemListControl> tableItemLists;
        public int selectedTable;

        public MainWindow()
        {
            InitializeComponent();
            Map.Visibility = Visibility.Hidden;
            ViewOrder.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Hidden;
            Pay.Visibility = Visibility.Hidden;
            ItemListGrid.Visibility = Visibility.Hidden;
            HeaderFooter.Visibility = Visibility.Hidden;
            LoginScreen.Visibility = Visibility.Visible;
            ModBlock.Visibility = Visibility.Hidden;

            PopulateFromDB(); //Creates all food subcategories based on a fake database
            foreach(UIElement child in SubCategoryView.Children) //Set all food subcategories to hidden
            {
                child.Visibility = Visibility.Hidden;
            }

            selectedItems = new List<AnItemControl>();
            tableItemLists = new List<ItemListControl>();

            PopulateTableItemLists(); //Creates an ItemList viewer for each individual table
        }

        private void PopulateTableItemLists()
        {
            for(int i = 0; i < 10; i++) //currently 10 tables
            {
                ItemListControl anItemList = new ItemListControl();
                anItemList.Visibility = Visibility.Hidden;
                tableItemLists.Add(anItemList);
                ItemListGrid.Children.Add(anItemList);
            }
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
            ItemListGrid.Visibility = Visibility.Hidden;
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
                HeaderUserName.Text = Username.Text;
                Username.Text = "";
                Password.Password = "";
            }
            Floor1Selector_Click(sender, e); //Initial state of floor map
            selectedTable = 0; //0 is not a table
            tableItemLists[0].Visibility = Visibility.Visible;

            ModBlock.Visibility = Visibility.Visible;
            foreach (UIElement child in ModBlock.Children)
            {
                child.Visibility = Visibility.Hidden;
            }
            SemiFootBlock.Visibility = Visibility.Visible;
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
            ItemListGrid.Visibility = Visibility.Visible;
        }

        private void MenuButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResetButtons();
            MenuButton.Background = new SolidColorBrush(Color.FromRgb(135, 40, 40));
            HeaderScreenName.Text = "Add Items to Order";
            this.HideAllScreensAfterLogin();
            Menu.Visibility = Visibility.Visible;
            ItemListGrid.Visibility = Visibility.Visible;
            
            //Reset all text fields for adding items
            foreach(object aView in SubCategoryView.Children)
            {
                if(aView is WrapPanel)
                {
                    WrapPanel tempView = (WrapPanel)aView;
                    foreach(AddItemControl anItem in tempView.Children)
                    {
                        anItem.AddItemNotes.Text = "";
                    }
                }
            }
            CustomDescription.Text = "";
            CustomPrice.Text = "";
        }

        private void PayButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResetButtons();
            PayButton.Background = new SolidColorBrush(Color.FromRgb(135, 40, 40));
            HeaderScreenName.Text = "Pay for Order";
            this.HideAllScreensAfterLogin();
            Pay.Visibility = Visibility.Visible;
            ItemListGrid.Visibility = Visibility.Visible;
            float totalPrice = 0.00f;
            float selectedPrice = 0.00f;
            foreach(object child in tableItemLists[selectedTable - 1].Items.Children)
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
            this.ResetButtons();
            Map.Visibility = Visibility.Hidden;
            ViewOrder.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Hidden;
            Pay.Visibility = Visibility.Hidden;
            ItemListGrid.Visibility = Visibility.Hidden;
            HeaderFooter.Visibility = Visibility.Hidden;
            LoginScreen.Visibility = Visibility.Visible;

            //Replay the login screen animation
            Storyboard sb = this.FindResource("LoginAnimation") as Storyboard;
            sb.Begin();

            //DEBUG LIST
            //ITEM CONTROL/PRICE ENTERING
            //Do error checking on price and update it.
            //If modified, must re-send to kitchen.
            //NEED TO ADD:
            //Search/filter
            //Common custom selections
        }

        public void UpdateSelected()
        {
            float selectedPrice = 0.00f;
            foreach (object child in tableItemLists[selectedTable - 1].Items.Children)
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
            tableItemLists[selectedTable - 1].Items.Children.Clear();
        }

        private void PaySelectedButton_Click(object sender, RoutedEventArgs e)
        {
            decimal totalPrice = Decimal.Parse(TotalRemaining.Text, System.Globalization.NumberStyles.Currency);
            totalPrice -= Decimal.Parse(TotalSelected.Text, System.Globalization.NumberStyles.Currency);
            TotalRemaining.Text = totalPrice.ToString("c2");
            TotalSelected.Text = "$0.00";
            List<AnItemControl> itemControlToRemove = new List<AnItemControl>();
            foreach(object child in tableItemLists[selectedTable - 1].Items.Children)
            {
                if(child is AnItemControl)
                {
                    if ((child as AnItemControl).selected)
                        itemControlToRemove.Add((child as AnItemControl));
                }
            }
            foreach(AnItemControl item in itemControlToRemove)
            {
                tableItemLists[selectedTable - 1].Items.Children.Remove(item);
            }
        }

        private void SendAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (AnItemControl anItem in tableItemLists[selectedTable - 1].Items.Children)
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
                tableItemLists[selectedTable - 1].Items.Children.Remove(anItem);
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
                foreach (AnItemControl anItem in tableItemLists[selectedTable - 1].Items.Children)
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
                foreach (AnItemControl anItem in tableItemLists[selectedTable - 1].Items.Children)
                {
                    anItem.ItemDescription.IsReadOnly = true;
                    anItem.ItemPrice.IsReadOnly = true;
                    anItem.Selector.Visibility = Visibility.Visible;
                }
            }
        }

        private void Table1_Click(object sender, RoutedEventArgs e)
        {
            TableClicked(sender, 1);
        }

        private void Table2_Click(object sender, RoutedEventArgs e)
        {
            TableClicked(sender, 2);
        }

        private void Table3_Click(object sender, RoutedEventArgs e)
        {
            TableClicked(sender, 3);
        }

        private void Table4_Click(object sender, RoutedEventArgs e)
        {
            TableClicked(sender, 4);
        }

        private void Table5_Click(object sender, RoutedEventArgs e)
        {
            TableClicked(sender, 5);
        }

        private void Table6_Click(object sender, RoutedEventArgs e)
        {
            TableClicked(sender, 6);
        }

        private void Table7_Click(object sender, RoutedEventArgs e)
        {
            TableClicked(sender, 7);
        }

        private void Table8_Click(object sender, RoutedEventArgs e)
        {
            TableClicked(sender, 8);
        }

        private void Table9_Click(object sender, RoutedEventArgs e)
        {
            TableClicked(sender, 9);
        }

        private void Table10_Click(object sender, RoutedEventArgs e)
        {
            TableClicked(sender, 10);
        }

        private void TableClicked(object thisTable, int num)
        {
            Button table = (Button)thisTable;
            HeaderTableNo.Text = String.Format("Table #{0:D}", num);
            selectedTable = num;

            ModBlock.Visibility = Visibility.Hidden;
            foreach (UIElement child in ModBlock.Children)
            {
                child.Visibility = Visibility.Visible;
            }

            //Change all other table colors to default, and this one to highlighted
            foreach (UIElement child in PatioViewer.Children)
            {
                Type childType = child.GetType();
                if (childType.Equals(table.GetType())) //If its a button
                {
                    Button aTable = (Button)child;
                    aTable.Background = new SolidColorBrush(Color.FromRgb(50, 50, 50));
                }
            }
            foreach (UIElement child in Floor1Viewer.Children)
            {
                Type childType = child.GetType();
                if (childType.Equals(table.GetType())) //If its a button
                {
                    Button aTable = (Button)child;
                    aTable.Background = new SolidColorBrush(Color.FromRgb(50, 50, 50));
                }
            }
            table.Background = new SolidColorBrush(Color.FromRgb(160, 160, 120));

            //Hide all other tableItemLists and make this one visible
            foreach (UIElement child in ItemListGrid.Children)
            {
                child.Visibility = Visibility.Hidden;
            }
            tableItemLists[num - 1].Visibility = Visibility.Visible;
        }

        private void Floor1Selector_Click(object sender, RoutedEventArgs e)
        {
            PatioViewer.Visibility = Visibility.Hidden;
            Floor1Viewer.Visibility = Visibility.Visible;
            Floor1Selector.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 255, 20));
            PatioSelector.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
        }

        private void PatioSelector_Click(object sender, RoutedEventArgs e)
        {
            Floor1Viewer.Visibility = Visibility.Hidden;
            PatioViewer.Visibility = Visibility.Visible;
            PatioSelector.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 255, 20));
            Floor1Selector.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
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
            tableItemLists[selectedTable - 1].Items.Children.Add(anItem);
            tableItemLists[selectedTable - 1].Scroller.ScrollToEnd();
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
