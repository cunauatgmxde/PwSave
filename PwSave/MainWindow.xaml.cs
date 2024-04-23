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
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {

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
        }

        private void btnNeu_Click(object sender, RoutedEventArgs e)
        {
            //var item = new ArtRow()
            //{
            //    ImageList = new List<ArtPictureView>(),
            //    Kuenstler = "",
            //    Name = "",
            //    Kurzbeschreibung = "",
            //    Kurzinfo = ""
            //};
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLos_Click(object sender, RoutedEventArgs e)
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

        private void btnThumbs_Click(object sender, RoutedEventArgs e)
        {
        }

        
    }
}
