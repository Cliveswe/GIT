namespace ApusAnimalHotel.ViewModel.MenuUI.File.SaveFile
{
    interface ISaveFiles
    {
        void Save(string filePath, string extension);
        string ExtensionFilter(string name, string extention);
        string ExtensionSaveFileTitle(string title, string extention);
        void ToolTip(string save, string extension);
    }
}
