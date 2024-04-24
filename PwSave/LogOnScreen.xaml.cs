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
using PwSaveController;

namespace PwSave
{
    /// <summary>
    /// Interaktionslogik für LogOnScreen.xaml
    /// </summary>
    public partial class LogOnScreen : Window
    {
        private IMainController controller;

        public LogOnScreen(IMainController con)
        {
            this.controller = con;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbPasswort.Focus();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
#if DEBUG
            this.DialogResult = true;
#else
            if (string.IsNullOrWhiteSpace(tbPasswort.Password))
            {
                MessageBox.Show("Bitte Passwort eingeben. \n", "Obacht!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!controller.ChkPassword(tbPasswort.Password.Trim()))
            {
                MessageBox.Show("Falsches Passwort. \n", "Obacht!", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            //Console.WriteLine($"Passwort: {tbPasswort.Password.Trim()}");
            this.DialogResult = true;
#endif
        }

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        
    }
}
