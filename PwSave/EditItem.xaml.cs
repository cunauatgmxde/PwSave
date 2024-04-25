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
using PwSaveData;

namespace PwSave
{
    /// <summary>
    /// Interaktionslogik für EditItem.xaml
    /// </summary>
    public partial class EditItem : Window
    {
        private IMainController controller;
        private bool saveChanges = false;

        public EditItem(IMainController con, PwSammlungRow item)
        {
            this.controller = con;
            InitializeComponent();
            if (item.Id < 1)
                this.Title = "Neuer Eintrag";

            //item.Anbieter = "Test";
            //item.Benutzername = "Wurst";
            saveChanges = false;
            SetButtons();
        }

        private void SetButtons()
        {
            btnSpeichern.IsEnabled = true;// saveChanges;
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            saveChanges = true;
        }
    }
}
