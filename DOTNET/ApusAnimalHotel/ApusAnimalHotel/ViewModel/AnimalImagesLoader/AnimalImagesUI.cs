using ApusAnimalHotel.ViewModel.ButtonControls;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.AnimalImagesLoader
{
    /// <summary>
    /// Holder class that locates and displays an image.
    /// </summary>
    class AnimalImagesUI
    {
        private readonly string buttonTitle = "Load animal image";

        /// <summary>
        /// Gets or sets the image button.
        /// </summary>
        /// <value>
        /// The image button.
        /// </value>
        public LoadAnimalImageButton ImageButton
        {
            get {
                return _imageButton;
            }
            set {
                _imageButton = value;
            }
        }
        private LoadAnimalImageButton _imageButton;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalImagesUI"/> class.
        /// </summary>
        public AnimalImagesUI()
        {
            ImageButton = new LoadAnimalImageButton(buttonTitle);
        }
    }
}
