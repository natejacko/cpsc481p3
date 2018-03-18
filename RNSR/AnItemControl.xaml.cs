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
        bool selected = false;
        public AnItemControl(bool status, string description, string price)
        {
            InitializeComponent();
            this.ItemDescription.Text = description;
            this.ItemPrice.Text = price;
            this.ItemNo.Visibility = Visibility.Hidden;
            this.ItemYes.Visibility = Visibility.Hidden;
            if (status)
            {
                this.ItemYes.Visibility = Visibility.Visible;
            }
            else
            {
                this.ItemNo.Visibility = Visibility.Visible;
            }
        }

        private void Selector_Click(object sender, RoutedEventArgs e)
        {
            if (selected)
            {
                this.ItemDescription.Background = new SolidColorBrush(Color.FromRgb(131, 131, 131));
                selected = false;
                
            }
            else
            {
                this.ItemDescription.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                selected = true;
            }
        }
        

    }
}
