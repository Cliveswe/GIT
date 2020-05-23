using InvoiceMaker.ButtonControls;
using Microsoft.Win32;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel.ButtonControls
{
    class LoadImageButton : InvoiceButton
    {
        private string filePath;
        private OpenFileDialog openFileDialog;
        //image file extensions
        private readonly string imageExtensions = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";


        /// <summary>
        /// Gets or sets an image file to been shown.
        /// </summary>
        /// <value>
        /// The show image file.
        /// </value>
        public ImageSource ShowImageFile
        {
            get {
                return _showImageFile;
            }
            set {
                _showImageFile = value;
                OnPropertyChanged("ShowImageFile");
            }
        }
        private ImageSource _showImageFile;
        /// <summary>
        /// Get the file of the loaded image.
        /// </summary>
        /// <returns>String</returns>
        public string GetImageFilePath()
        {
            return filePath;
        }
        public override bool ButtonCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Sets the OpensFileDialog.
        /// </summary>
        private void OpenFileDialogSetUp()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = imageExtensions;
        }
        public override void ButtonExecute(object parameter)
        {
            OpenFileDialogSetUp();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                //new bitmap to present the image
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(filePath);
                bi3.EndInit();
                ShowImageFile = bi3;
            }
            
        }

        public LoadImageButton()
        {
            ButtonContent = "Load image";
            filePath = string.Empty;
        }
    }
}
