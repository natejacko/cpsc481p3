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
        private bool selected = false;
        public float price = 0.00f;
        private List<AnItemControl> selectedItems;

        public AnItemControl(string description, float price, List<AnItemControl> selectedItems)
        {
            InitializeComponent();
            this.price = price;
            this.selectedItems = selectedItems;
            this.ItemDescription.Text = description;
            this.ItemPrice.Text = String.Format("{0:C2}", price); //Defaults to regional format: $0.00
            this.ItemNo.Visibility = Visibility.Visible;
            this.ItemYes.Visibility = Visibility.Hidden;
        }

        private void Selector_Click(object sender, RoutedEventArgs e)
        {
            if (this.selected) //If it WAS selected, make it no longer
            {
                this.ItemDescription.Background = new SolidColorBrush(Color.FromRgb(131, 131, 131));
                this.selected = false;
                this.selectedItems.Remove(this);
            }
            else //If it was NOT selected, make it so
            {
                this.ItemDescription.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                this.selected = true;
                this.selectedItems.Add(this);
            }
        }

        public void sendToKitchen()
        {
            this.ItemNo.Visibility = Visibility.Hidden;
            this.ItemYes.Visibility = Visibility.Visible;
        }
    }
}
