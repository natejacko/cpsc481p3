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
    /// Interaction logic for AnItemControl.xaml
    /// </summary>
    public partial class AnItemControl : UserControl
    {
        public bool selected { get; private set;  }  = false;
        public float price = 0.00f;
        private List<AnItemControl> selectedItems;
        private MainWindow window;

        public AnItemControl(string description, float price, List<AnItemControl> selectedItems, MainWindow window)
        {
            InitializeComponent();
            this.price = price;
            this.window = window;
            this.selectedItems = selectedItems;
            this.ItemDescription.Text = description;
            this.ItemPrice.Text = String.Format("{0:C2}", price); //Defaults to regional format: $0.00
            this.SentItem.Visibility = Visibility.Hidden;
            this.ItemDescription.IsReadOnly = true;
            this.ItemPrice.IsReadOnly = true;
        }

        public void Deselect()
        {
            this.ItemDescription.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            this.selected = false;
            this.selectedItems.Remove(this);
            this.window.UpdateSelected();
        }

        private void Selector_Click(object sender, RoutedEventArgs e)
        {
            if (this.selected) //If it WAS selected, make it no longer
            {
                this.ItemDescription.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                this.selected = false;
                this.selectedItems.Remove(this);
                this.window.UpdateSelected();
            }
            else //If it was NOT selected, make it so
            {
                this.ItemDescription.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                this.selected = true;
                this.selectedItems.Add(this);
                this.window.UpdateSelected();
            }
        }

        public void SendToKitchen()
        {
            this.SentItem.Visibility = Visibility.Visible;
            this.ModButton.IsEnabled = false;
        }

        private void ModButton_Click(object sender, RoutedEventArgs e)
        {
            int count = selectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                AnItemControl anItem = selectedItems.First();
                anItem.Deselect();
                selectedItems.Remove(anItem);
            }
            selectedItems.Add(this);

            this.ModButton.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
            window.AnItemModifying(sender, e, this);
        }
    }
}
