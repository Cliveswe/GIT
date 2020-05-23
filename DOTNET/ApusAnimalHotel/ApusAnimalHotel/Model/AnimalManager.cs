using AnimalMotel.Model.Animals;
using AnimalMotel.Model.Animals.Birds;
using AnimalMotel.Model.Animals.Mammals;
using ApusAnimalHotel.ViewModel;
using System;
using ApusAnimalHotel.Model.ListManagerRegister;
using AnimalMotel.Model.Animals.Insects;
using ApusAnimalHotel.ViewModel.MenuUI.File;
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-09
///  
/// This class manages a register of the super type "Animal". It provides access and 
/// maintenance of the registered list of animals.
/// 
/// Update (2019-04-09)
/// This class has been modified to use a generic register called ListManager<T>
/// Thus the previous local list has bee removed.
/// 
namespace ApusAnimalHotel.Model
{
    /// <summary>
    /// This class is a register of all animals currently residing at 
    /// the animal hotel.
    /// </summary>
    internal class AnimalManager : ListManager<Animal>
    {
        #region Singleton properties
        public static AnimalManager GetInstance
        {
            get {

                return _instance;
            }
        }
        private static readonly AnimalManager _instance = new AnimalManager();

        /// <summary>
        /// Prevents a default instance of the <see cref="AnimalManager"/> class from being created.
        /// </summary>
        private AnimalManager()
        {
        }

        /// <summary>
        /// Explicit static constructor to tell C# compiler
        /// not to mark type as beforefieldinit.
        /// Read article <see cref="http://csharpindepth.com/Articles/Singleton"/> C# in Depth.
        /// </summary>
        static AnimalManager()
        {
        }
        #endregion

        #region Class Properties and Fields
        /// <summary>
        /// Instance of object that holds the current name of a file.
        /// </summary>
        private FileName filePath = new FileName();

        /// <summary>
        /// Gets the number of animal residents at the hotel.
        /// </summary>
        /// <value>
        /// The number of animal residents.
        /// </value>
        public int NumberOfAnimalResidents
        {
            get {
                return Count;
            }
        }
        #endregion

        #region Add an Animal
        /// <summary>
        /// Adds the animal.
        /// </summary>
        /// <param name="animal">The animal.</param>
        public void AddAnimal(Animal animal)
        {
            animal.AnimalID = GenerateNewId(animal.GetType().Name);
            Add(animal);
            filePath.NotSaved();
        }

        #endregion

        #region Delete Animal
        /// <summary>
        /// Adds the animal.
        /// </summary>
        /// <param name="animal">The animal.</param>
        public bool DeleteAnimal(Animal animal)
        {
            bool res = false;
            for (int index = 0; index < Count; index++)
            {
                if (GetAnimalAt(index).AnimalID == animal.AnimalID)
                {
                    res = Delete(index);
                    break;
                }
            }
            return res;
        }

        private bool Delete(int index)
        {
            if (DeleteAt(index))
            {
                filePath.NotSaved();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Delete animal at index.
        /// </summary>
        /// <param name="index">Position in the list of animals.</param>
        /// <returns>True if successful otherwise false.</returns>
        public bool DeleteAnimalAt(int index)
        {
            bool res = false;
            if (CheckIndex(index))
            {
                res = Delete(index);
            }

            return res;
        }
        #endregion

        #region Copy animal
        /// <summary>
        /// Gets a copy of an animal.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>An object animal</returns>
        private Animal GetAnimalCopy(int index)
        {
            Animal animal = GetAt(index);
            string species = animal.GetType().Name;


            BirdEnum tryBird;
            MammalEnum tryMammal;
            InsectEnum tryInsect;

            if (animal != null)
            {
                if (Enum.TryParse<BirdEnum>(species, out tryBird))
                {
                    return CopyBird(tryBird, animal);
                }

                if (Enum.TryParse<MammalEnum>(species, out tryMammal))
                {
                    return CopyMammal(tryMammal, animal);
                }

                if (Enum.TryParse<InsectEnum>(species, out tryInsect))
                {
                    return CopyInsect(tryInsect, animal);
                }
            }
            return null;
        }
        /// <summary>
        /// Copies a bird.
        /// </summary>
        /// <param name="species">The species.</param>
        /// <param name="animal">The animal.</param>
        /// <returns></returns>
        private Animal CopyBird(BirdEnum species, Animal animal)
        {
            Animal res;

            switch (species)
            {
                case BirdEnum.Ostrich:
                    res = new Ostrich((Ostrich)animal);
                    break;
                case BirdEnum.Penguin:
                    res = new Penguin((Penguin)animal);
                    break;
                default:
                    res = null;
                    break;
            }

            return res;
        }
        /// <summary>
        /// Copies a mammal.
        /// </summary>
        /// <param name="species">The species.</param>
        /// <param name="animal">The animal.</param>
        /// <returns></returns>
        private Animal CopyMammal(MammalEnum species, Animal animal)
        {
            Animal res;

            switch (species)
            {
                case MammalEnum.Dog:
                    res = new Dog((Dog)animal);
                    break;
                case MammalEnum.Dolphin:
                    res = new Dolphin((Dolphin)animal);
                    break;
                default:
                    res = null;
                    break;
            }

            return res;
        }
        /// <summary>
        /// Copies an insect.
        /// </summary>
        /// <param name="species">The species.</param>
        /// <param name="animal">The animal.</param>
        /// <returns></returns>
        private Animal CopyInsect(InsectEnum species, Animal animal)
        {
            Animal res;

            switch (species)
            {
                case InsectEnum.Bee:
                    res = new Bee((Bee)animal);
                    break;
                case InsectEnum.Fly:
                    res = new Fly((Fly)animal);
                    break;
                default:
                    res = null;
                    break;
            }

            return res;
        }
        #endregion

        /// <summary>
        /// Retrieve an animal from the register.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>An object animal</returns>
        public Animal GetAnimalAt(int index)
        {
            if (CheckIndex(index))
            {
                return GetAt(index);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// New Id consists of a string and a unique number that is padded to 3 decimals.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>A string</returns>
        private string GenerateNewId(string str)
        {
            string newId = null;
            int id;

            //generate a new unique id
            do
            {
                id = AnimalID.GetNewID();
                newId = str + string.Format("{0:000}", id);
            } while (IsUniqueID(newId) != true);

            return newId;
        }
        /// <summary>
        /// Check that the id is unique.
        /// </summary>
        /// <returns></returns>
        private bool IsUniqueID(string id)
        {
            if (Count > 0)
            {
                for (int index = 0; index < Count; index++)
                {
                    if (id == GetAnimalAt(index).AnimalID)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #region Save and Load File Management
        /// <summary>
        /// Has the registered been saved to a file.
        /// </summary>
        /// <returns>True if the file has been saved, otherwise false.</returns>
        public bool RegisterHasBeenSaved()
        {
            return filePath.HasAFileBeenSaved();
        }

        /// <summary>
        /// The register has been saved.
        /// </summary>
        private void RegisterSaved()
        {
            filePath.IsSaved();
        }

        /// <summary>
        /// The register has not been saved.
        /// </summary>
        private void RegisterNotSaved()
        {
            filePath.NotSaved();
        }
        /// <summary>
        /// Reset the file path to its initial state.
        /// </summary>
        private void ResetFilePath()
        {
            filePath.Reset();

        }
        /// <summary>
        /// The binary file name and path that the register is saved to.
        /// </summary>
        /// <param name="filePath">File name and Path.</param>
        private void RegisterSavedAsBinaryFile(string filePath)
        {
            this.filePath.SetFileNameAsBinary(filePath);
        }

        /// <summary>
        /// The Text file name and path that the register is saved to.
        /// </summary>
        /// <param name="filePath">File name and Path.</param>
        private void RegisterSavedAsTextFile(string filePath)
        {
            this.filePath.SetFileNameAsText(filePath);
        }

        #region Binary File Save Methods
        /// <summary>
        /// Save the animal register as a binary file.
        /// </summary>
        /// <param name="filePath">Path and name of file</param>
        public void SaveFileAsBinary(string filePath)
        {
            try
            {
                RegisterSavedAsBinaryFile(filePath);
                BinarySerialize(@filePath);
                RegisterSaved();
            }
            catch (Exception ex)
            {
                ResetFilePath();
                RegisterNotSaved();
                throw new Exception("Save register as a binary file. ", ex);
            }
        }
        /// <summary>
        /// Save the animal register to a binary file.
        /// </summary>
        /// <returns>True if the register is saved to a binary file, otherwise false.</returns>
        public bool SaveBinaryFile()
        {
            if ((filePath.GetFileName() != null) &&
                (filePath.FileType == FileName.FileTypes.Binary))
            {
                try
                {
                    SaveFileAsBinary(filePath.GetFileName());
                }
                catch (Exception ex)
                {
                    throw new Exception("Save register to an existing binary file. ", ex);
                }
                return true;
            }
            return false;
        }
        #endregion

        #region File Load Methods
        /// <summary>
        /// Load a saved file to the register.
        /// </summary>
        /// <param name="filePath">Path and name of a file</param>
        public void LoadBinaryFile(string filePath)
        {
            try
            {
                BinaryDeSerialize(filePath);
                RegisterNotSaved();
            }
            catch (Exception ex)
            {
                ResetFilePath();
                RegisterNotSaved();
                throw new Exception("Load File as a binary file. ", ex);
            }
        }
        /// <summary>
        /// Load a saved file to the register
        /// </summary>
        /// <param name="filePath">Path and name of a file</param>
        public void LoadTextFile(string filePath)
        {
            try
            {
                XMLDeSerialize(filePath);
                RegisterNotSaved();
            }
            catch (Exception ex)
            {
                ResetFilePath();
                RegisterNotSaved();
                throw new Exception("Load File as a text file. ", ex);
            }
        }
        #endregion

        #region Text file Save Methods
        /// <summary>
        /// Save the animal register as a text(or XML) file.
        /// The file extension is .txt for both Text and XML.
        /// </summary>
        /// <param name="filePath">Path and name of file</param>
        public void SaveFileAsText(string filePath)
        {
            try
            {
                RegisterSavedAsTextFile(filePath);
                XMLSerialize(@filePath);
                RegisterSaved();
            }
            catch (Exception ex)
            {
                ResetFilePath();
                RegisterNotSaved();
                throw new Exception("Save register as a text file. ", ex);
            }
        }
        /// <summary>
        /// Save the animal register to a text or XML file.
        /// </summary>
        /// <returns>True if the register is saved to a text file, otherwise false.</returns>
        public bool SaveTextFile()
        {
            if ((filePath.GetFileName() != null) &&
                (filePath.FileType == FileName.FileTypes.XML))
            {
                try
                {
                    SaveFileAsText(filePath.GetFileName());
                }
                catch (Exception ex)
                {
                    throw new Exception("Save register to an existing text file. ", ex);
                }
                return true;
            }
            return false;
        }
        #endregion


        #endregion

        /// <summary>
        /// Reset the register by clearing the register of its content.
        /// </summary>
        public void ResetManager()
        {
            DeleteAll();
            RegisterSaved();
            ResetFilePath();
        }
    }
}
