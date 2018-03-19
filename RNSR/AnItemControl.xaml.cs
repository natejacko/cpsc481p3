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
            this.ItemNo.Visibility = Visibility.Visible;
            this.ItemYes.Visibility = Visibility.Hidden;
            if (window.allowModify)
            {
                this.ItemDescription.IsReadOnly = false;
                this.ItemPrice.IsReadOnly = false;
            }
            else
            {
                this.ItemDescription.IsReadOnly = true;
                this.ItemPrice.IsReadOnly = true;
            }
        }

        public void Deselect()
        {
            this.ItemDescription.Background = new SolidColorBrush(Color.FromRgb(214, 214, 214));
            this.selected = false;
            this.selectedItems.Remove(this);
            this.window.UpdateSelected();
        }

        private void Selector_Click(object sender, RoutedEventArgs e)
        {
            if (this.selected) //If it WAS selected, make it no longer
            {
                this.ItemDescription.Background = new SolidColorBrush(Color.FromRgb(214, 214, 214));
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
            this.ItemNo.Visibility = Visibility.Hidden;
            this.ItemYes.Visibility = Visibility.Visible;
        }
    }
}
