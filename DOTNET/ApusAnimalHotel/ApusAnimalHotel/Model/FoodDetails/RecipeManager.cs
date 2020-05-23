using ApusAnimalHotel.Model.ListManagerRegister;
using ApusAnimalHotel.ViewModel.MenuUI.File;
using System;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date: 2019-04-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.FoodDetails
{
    class RecipeManager : ListManager<Recipe>
    {


        #region Singleton properties
        public static RecipeManager GetInstance
        {
            get {
                return _instance;
            }
        }
        public static readonly RecipeManager _instance = new RecipeManager();

        /// <summary>
        /// Prevents a default instance of the <see cref="RecipeManager"/> class from being created.
        /// </summary>
        private RecipeManager()
        {
        }

        /// <summary>
        /// Explicit static constructor to tell C# compiler
        /// not to mark type as beforefieldinit.
        /// Read article <see cref="http://csharpindepth.com/Articles/Singleton"/> C# in Depth.
        /// </summary>
        static RecipeManager() { }
        #endregion

        /// <summary>
        /// Instance of object that holds the current name of a file.
        /// </summary>
        private FileName filePath = new FileName();

        public void AddRecipe(Recipe recipe)
        {
            Add(recipe);
            RegisterNotSaved();
        }

        /// <summary>
        /// Reset the register by clearing the register of its content.
        /// </summary>
        public void ResetManager()
        {
            DeleteAll();
            RegisterSaved();
            ResetFilePath();
        }
        /// <summary>
        /// Reset the file path to its initial state.
        /// </summary>
        private void ResetFilePath()
        {
            filePath.Reset();
        }
        /// <summary>
        /// The register has been saved.
        /// </summary>
        private void RegisterSaved()
        {
            filePath.IsSaved();
        }
        /// <summary>
        /// Has the registered been saved to a file.
        /// </summary>
        /// <returns>True if the file has been saved, otherwise false.</returns>
        public bool RegisterHasBeenSaved()
        {
            return filePath.HasAFileBeenSaved();
        }

        /// <summary>
        /// The register has not been saved.
        /// </summary>
        private void RegisterNotSaved()
        {
            filePath.NotSaved();
        }

        /// <summary>
        /// The Text file name and path that the register is saved to.
        /// </summary>
        /// <param name="filePath">File name and Path.</param>
        private void RegisterSavedAsXMLFile(string filePath)
        {
            this.filePath.SetFileNameAsXML(filePath);
        }

        /// <summary>
        /// Export the register to an XML file.
        /// </summary>
        /// <param name="fileName">File name and Path.</param>
        public void XMLExportTo(string fileName)
        {
            try
            {
                RegisterSavedAsXMLFile(fileName);
                XMLSerialize(fileName);
                RegisterSaved();
            }
            catch (Exception ex)
            {
                ResetFilePath();
                RegisterNotSaved();
                throw new Exception("Save register as a XML file. ", ex);
            }
        }
        /// <summary>
        /// Update the register from and XMK file.
        /// </summary>
        /// <param name="fileName">File name and Path.</param>
        public void XMLImportFrom(string fileName)
        {
            try
            {
                XMLDeSerialize(fileName);
            }
            catch (Exception ex)
            {
                throw new Exception("XML import from file.", ex);
            }
        }
    }
}
