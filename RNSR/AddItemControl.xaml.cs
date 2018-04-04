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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RNSR
{
    /// <summary>
    /// Interaction logic for AddItemControl.xaml
    /// </summary>
    public partial class AddItemControl : UserControl
    {
        public string name { get; private set; } = " ";
        public float price { get; private set; } = 0.00f;
        private MainWindow window;

        public AddItemControl(string name, float price, MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            this.price = price;
            this.name = name;
            this.AddItemName.Text = name;
            this.AddItemPrice.Text = String.Format("{0:C2}", price); //Defaults to regional format: $0.00
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            string description = this.name + " (" + this.AddItemNotes.Text + ")";
            AnItemControl anItem = new AnItemControl(description, this.price, this.window.selectedItems, this.window);
            this.window.tableItemLists[this.window.selectedTable - 1].Items.Children.Add(anItem);
            this.window.tableItemLists[this.window.selectedTable - 1].Scroller.ScrollToEnd();
        }

        private void KeyBoardTrigger_MouseEnter(object sender, MouseEventArgs e)
        {
            window.ScrollerKeyboard.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            window.ScrollerKeyboard.ScrollToEnd();
        }

        private void AddItemNotes_GotFocus(object sender, RoutedEventArgs e)
        {
            window.ScrollerKeyboard.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            window.ScrollerKeyboard.ScrollToEnd();
            window.KeyBoardImage.Visibility = Visibility.Visible;
        }
    }
}
