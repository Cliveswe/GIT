using ApusAnimalHotel.ViewModel.MenuUI.File.OpenFile;
using ApusAnimalHotel.ViewModel.MenuUI.File.XML;

namespace ApusAnimalHotel.ViewModel.MenuUI.File
{
    class MenuFile
    {
        #region Class Properties
        /// <summary>
        /// The File menu item "File" header.
        /// </summary>
        public string MenuFileHeader
        {
            get {
                return ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.File);
            }
        }
        #endregion

        #region Save and SaveAs properties
        /// <summary>
        /// The File menu item "SaveAs" header.
        /// </summary>
        public string MenuFileSaveAsHeader
        {
            get {
                return ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Save_as);
            }
        }
        /// <summary>
        /// The File menu item "Save" header.
        /// </summary>
        public string MenuFileSaveHeader
        {
            get {
                return ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Save);
            }
        }
        /// <summary>
        /// The File menu item "XML" header.
        /// </summary>
        public string MenuExportToXMLHeader
        {
            get {
                return ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.XML);
            }
        }
        /// <summary>
        /// Save file as a named binary file.
        /// </summary>
        public MenuFileItems MenuFileBinarySaveAs
        {
            get; set;
        }
        /// <summary>
        /// Save file as a binary file.
        /// </summary>
        public MenuFileItems MenuFileBinarySave
        {
            get; set;
        }
        /// <summary>
        /// Save file as a named text file.
        /// </summary>
        public MenuFileItems MenuFileTextSaveAs
        {
            get; set;
        }
        /// <summary>
        /// Save file as a text file.
        /// </summary>
        public MenuFileItems MenuFileTextSave
        {
            get; set;
        }
        /// <summary>
        /// Export the register to XML.
        /// </summary>
        public MenuFileItems XMLExportTo
        {
            get; set;
        }
        /// <summary>
        /// Import an XML file to the register.
        /// </summary>
        public MenuFileItems XMLImportFrom
        {
            get; set;
        }

        #endregion

        #region Open
        /// <summary>
        /// The File menu item "Open" header.
        /// </summary>
        public string MenuFileOpenHeader
        {
            get {
                return ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Open);
            }
        }

        /// <summary>
        /// Open a named text file.
        /// </summary>
        public MenuFileItems MenuFileTextOpenAs
        {
            get; set;
        }
        /// <summary>
        /// Open a named binary file.
        /// </summary>
        public MenuFileItems MenuFileBinaryOpenAs
        {
            get; set;
        }

        #endregion

        #region New
        /// <summary>
        /// The File menu item "New".
        /// </summary>
        public MenuFileNew MenuFileNew
        {
            get; set;
        }
        #endregion

        #region Exit
        /// <summary>
        /// The File menu item "Exit".
        /// </summary>
        public MenuFileExit MenuFileExit
        {
            get; set;
        }
        #endregion

        /// <summary>
        /// Instance of object that holds the name of a file.
        /// </summary>
        public FileName FileName
        {
            get {
                return _fileName;
            }
            private set {
                _fileName = value;
            }
        }
        private FileName _fileName;

        /// <summary>
        /// Reference to parent menu.
        /// </summary>
        private Menu MainMenu
        {
            get {
                return _mainMenu;
            }
            set {
                _mainMenu = value;
            }
        }
        private Menu _mainMenu;

        public void ResetApp()
        {
            MainMenu.ResetApp();
        }
        public void ResetAppForNew()
        {
            MainMenu.ResetAppForNew();
        }
        public void RefreshApp()
        {
            MainMenu.RefreshApp();
        }
        /// <summary>
        /// Initialise the command dictionary of menu command items
        /// </summary>
        private void InitialiseMenuItems()
        {
            //status of the menu file
            FileName = new FileName();

            //instantiate the menu file
            MenuFileNew = new MenuFileNew(this);
            MenuFileExit = new MenuFileExit();

            #region Save a file
            //Save 
            //Binary
            MenuFileBinarySaveAs = new MenuFileSaveAs(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Save_as),
               ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File));
            MenuFileBinarySave = new MenuFileSave(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Save),
                ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File));
            //Text
            MenuFileTextSaveAs = new MenuFileSaveAs(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Save_as),
                ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File));
            MenuFileTextSave = new MenuFileSave(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Save),
                ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File));
            //XML
            XMLExportTo = new XMLExportTo(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.XML),
                ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Export_to_XML_File),
                this);
            XMLImportFrom = new XMLExportTo(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.XML),
                ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Import_from_XML_File),
                this);
            #endregion

            #region Open a file
            //Open
            //Binary
            MenuFileBinaryOpenAs = new MenuFileOpenAs(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Open),
                ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File),
                this);
            //Text
            MenuFileTextOpenAs = new MenuFileOpenAs(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Open),
                ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File),
                this);
            #endregion
        }
        /// <summary>
        /// Class constructor
        /// </summary>
        public MenuFile(Menu mainMenu)
        {
            MainMenu = mainMenu;
            InitialiseMenuItems();

        }

    }

}
