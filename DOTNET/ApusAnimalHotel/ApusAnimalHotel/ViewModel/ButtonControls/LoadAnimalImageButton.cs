using Microsoft.Win32;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.ButtonControls
{
    /// <summary>
    /// Using OpenFileDialog to locate an image to display using ImageSource.
    /// </summary>
    /// <seealso cref="ApusAnimalHotel.ViewModel.ButtonControls.AnimalButton" />
    class LoadAnimalImageButton : AnimalButton
    {
        private string filePath;
        private OpenFileDialog openFileDialog;
        private string openFileDialogTitle;
        //image file extensions
        private readonly string imageExtensions = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";

        /// <summary>
        /// Can the button be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Always true.</returns>
        public override bool ButtonCanExecute(object parameter)
        {
            return true;
        }

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
        /// Sets the OpensFileDialog.
        /// </summary>
        private void OpenFileDialogSetUp()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = imageExtensions;
        }

        /// <summary>
        /// Button is pressed execute method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void ButtonExecute(object parameter)
        {
            OpenFileDialogSetUp();

            if (!string.IsNullOrWhiteSpace(openFileDialogTitle))
            {
                openFileDialog.Title = openFileDialogTitle;
            }
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
        /// <summary>
        /// Reset the shown image.
        /// </summary>
        public void ResetImage()
        {
            ShowImageFile = null;
        }
        /// <summary>
        /// Sets the open file dialog header.
        /// </summary>
        /// <param name="str">The string.</param>
        private void SetOpenFileDialogHeader(string str)
        {
            openFileDialogTitle = str;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadAnimalImageButton"/> class.
        /// </summary>
        /// <param name="openFileHeader">The open file header.</param>
        public LoadAnimalImageButton(string openFileHeader)
        {
            SetOpenFileDialogHeader(openFileHeader);
            ButtonContent = openFileDialogTitle;
        }

    }
}
