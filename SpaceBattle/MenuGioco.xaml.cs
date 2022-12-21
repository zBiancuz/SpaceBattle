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
using System.Windows.Shapes;

namespace SpaceBattle
{
    /// <summary>
    /// Logica di interazione per MenuGioco.xaml
    /// </summary>
    public partial class MenuGioco : Window
    {
        public MenuGioco()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadGame(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage S = new SettingsPage();
            S.ShowDialog();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            HelpPage H = new HelpPage();
            H.ShowDialog();
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}
