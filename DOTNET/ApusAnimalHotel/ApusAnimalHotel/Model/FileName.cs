/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date: 2019-04-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.MenuUI.File
{
    /// <summary>
    /// Class that encapsulates a name of a file and its path.
    /// </summary>
    class FileName
    {
        #region Class properties
        /// <summary>
        /// Instance of object that holds the status of a file.
        /// The status is true if the file has been saved, otherwise false.
        /// </summary>
        private SaveFileState fileState;

        private string filePath;
        /// <summary>
        /// Current file type.
        /// </summary>
        public FileTypes FileType
        {
            get {
                return _fileType;
            }
            private set {
                _fileType = value;
            }
        }
        private FileTypes _fileType;
        #endregion

        /// <summary>
        /// Self contained enum of file types.
        /// </summary>
        public enum FileTypes { Binary, XML, Txt };

        /// <summary>
        /// Get the file path.
        /// </summary>
        /// <returns>File path or null.</returns>
        public string GetFileName()
        {
            return filePath;
        }
        /// <summary>
        /// Get the current file type.
        /// </summary>
        /// <returns>Internal class Enum FileTypes</returns>
        public FileTypes GetCurrentFileType()
        {
            return FileType;
        }
        /// <summary>
        /// Set the file path.
        /// </summary>
        /// <param name="filepath">File path or null.</param>
        private void SetFileName(string filepath)
        {
            if (filepath != null)
            {
                filePath = filepath;
            }
            else
            {

                filePath = null;
            }
        }
        /// <summary>
        /// The file been saved is a binary file.
        /// </summary>
        /// <param name="filePath">File name and path.</param>
        public void SetFileNameAsBinary(string filePath)
        {
            SetFileName(filePath);
            SetFileAsBinary();
        }

        /// <summary>
        /// Set the file state as binary.
        /// </summary>
        private void SetFileAsBinary()
        {
            SetTypeFile(FileTypes.Binary);
        }
        /// <summary>
        /// The file has been saved is a Text file.
        /// </summary>
        /// <param name="filePath">File name and path.</param>
        public void SetFileNameAsText(string filePath)
        {
            SetFileName(filePath);
            SetFileAsText();
        }
        /// <summary>
        /// The file has been saved as a XML file.
        /// </summary>
        /// <param name="filePath"></param>
        public void SetFileNameAsXML(string filePath)
        {
            SetFileName(filePath);
            SetFileAsXML();
        }
        /// <summary>
        /// Set the file state as XML
        /// </summary>
        private void SetFileAsXML()
        {
            SetTypeFile(FileTypes.XML);
        }
        /// <summary>
        /// Set the file state as Text.
        /// </summary>
        private void SetFileAsText()
        {
            SetTypeFile(FileTypes.Txt);
        }
        /// <summary>
        /// Set the file type of the current file. 
        /// </summary>
        /// <param name="type"></param>
        private void SetTypeFile(FileTypes type)
        {
            FileType = type;
        }

        /// <summary>
        /// Reset the state of the object.
        /// </summary>
        public void Reset()
        {
            filePath = null;
            FileType = FileTypes.Binary;
        }
        /// <summary>
        /// The file status is saved.
        /// </summary>
        public void IsSaved()
        {
            fileState.IsSaved();
        }
        /// <summary>
        /// Has a file been saved.
        /// </summary>
        /// <returns>True if the file has been saved, otherwise false.</returns>
        public bool HasAFileBeenSaved()
        {
            return fileState.HasBeenSaved();
        }
        /// <summary>
        /// The file status is unsaved.
        /// </summary>
        public void NotSaved()
        {
            fileState.NotSaved();
        }
        public FileName()
        {
            filePath = null;
            SetFileAsBinary();
            fileState = new SaveFileState();
        }
    }
}
