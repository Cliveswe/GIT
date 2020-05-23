namespace ApusAnimalHotel.ViewModel.MenuUI.File.OpenFile
{
    interface IOpenFile
    {
        void Open(string filePath, string extension);
        string ExtensionFilter(string name, string extention);
        string ExtensionOpenFileTitle(string title, string extention);
        void ToolTip(string save, string extension);
    }

}