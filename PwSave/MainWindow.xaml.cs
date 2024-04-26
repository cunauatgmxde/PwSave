using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Input;
using PwSaveController;
using PwSaveData;
using gnCommon;
using QRCoder;

namespace PwSave
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMainController controller;
        private bool saveChanges = false;

        public MainWindow()
        {
            ServiceLocator.Register<IMessageService>(new MessageService());
            var messageService = ServiceLocator.GetService<IMessageService>();
            controller = new MainController(messageService);
            InitializeComponent();
            //PW
            var dlg = new LogOnScreen(controller);
            if (dlg.ShowDialog() != true)
            {
                Close();
            }

            KategorienLaden();
            lvBilder.ItemsSource = controller.GetPwSammlungRowList();
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var srv = controller.GetMessageService();
            //srv.ShowError("YES");
            SetButtons();
        }

        private void KategorienLaden()
        {
            var dict = new List<string>(){"", "Clemens (privat)", "Clemens (Arbeit)", "Charly"};
            cmbBenutzer.ItemsSource = dict;
            if (dict.Count > 0)
            {
                cmbBenutzer.SelectedIndex = 0;
            }
        }

        private void SetButtons()
        {
            btnSpeichern.IsEnabled = saveChanges;
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            var liste = (List<PwSammlungRow>)lvBilder.ItemsSource;
            if(controller.SavePwSammlungRowList(liste))
                saveChanges = false;
            SetButtons();
        }

        private void btnBeenden_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (PwSammlungRow)lvBilder.SelectedItem;
            if(item == null)
                return;

            controller.MakeRowBackup(item);
            if (!EintragBearbeiten(item))
            {
                controller.ResetRowFromBackup(item);
                return;
            }

            lvBilder.Items.Refresh();
        }

        private bool EintragBearbeiten(PwSammlungRow item)
        {
            var dlg = new EditItem(controller, item);
            bool dlgRet = (bool)dlg.ShowDialog();
            if (dlgRet)
            {
                saveChanges = true;
                SetButtons();
            }
            return dlgRet;
        }

        private void btnNeu_Click(object sender, RoutedEventArgs e)
        {
            PwSammlungRow item = controller.GetNewRowItem();
            var liste = (List<PwSammlungRow>)lvBilder.ItemsSource;
            if (EintragBearbeiten(item))
            {
                lvBilder.ItemsSource = null;
                liste.Add(item);
                lvBilder.ItemsSource = liste;
            }
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLos_Click(object sender, RoutedEventArgs e)
        {
        }

        #region QR Code

        private void btnThumbs_Click(object sender, RoutedEventArgs e)
        {
            // Link für WiFi
            //  https://github.com/codebude/QRCoder/wiki/Advanced-usage---Payload-generators#321-wifi
            string data = "http://www.gonau.de/wp/";
            string codeName = "goNau";
            // Generate the QR code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            // Specify the folder path to save the QR code image
            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "QRCodes");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Save the QR code as a PNG image file inside the specified folder
            string fileName = System.IO.Path.Combine(folderPath, codeName + "_" + "QRCode.png");
            qrCodeImage.Save(fileName, ImageFormat.Png);

            // Display the QR code image using an image viewer application
            //DisplayQRCodeImage(fileName);
            
            MessageBox.Show($"CREATE: {fileName}");
        }
        
        #endregion
        
    }
}
