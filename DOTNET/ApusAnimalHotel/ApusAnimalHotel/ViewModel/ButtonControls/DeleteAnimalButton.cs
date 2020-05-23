using ApusAnimalHotel.Model;
using ApusAnimalHotel.ViewModel.GroupListBoxIO;
using ApusAnimalHotel.ViewModel.ListAnimaObjectsInDetail;
using System.Collections.Generic;

namespace ApusAnimalHotel.ViewModel.ButtonControls
{
    class DeleteAnimalButton : AnimalButton, SubjectDeleteAnimalButton, Observer
    {
        private readonly string buttonTitle = "Delete";
        private ListAnimalObjectsInDetailUI listAnimalObjectsInDetail;
        private string id;//selected animals id

        /// <summary>
        /// Can the button be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Yes id true otherwise false</returns>
        public override bool ButtonCanExecute(object parameter)
        {
            bool res = false;

            if (!string.IsNullOrEmpty(id))
            {
                res = true;
            }
            return res;
        }
        /// <summary>
        /// Button is pressed execute method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void ButtonExecute(object parameter)
        {
            DeleteAnimal(id);
            id = null;//reset to effect button control
            NotifyAllObserversOfAnimalButton();
        }
        /// <summary>
        /// Delete a animal from the register.
        /// </summary>
        /// <param name="animalId">The animal identifier.</param>
        private void DeleteAnimal(string animalId)
        {
            AnimalManager instance = AnimalManager.GetInstance;

            for (int index = 0; index < instance.Count; index++)
            {
                if (animalId.Equals(instance.GetAnimalAt(index).AnimalID))
                {
                    animalItemWasDeleted(instance.GetAnimalAt(index).GetSpeciesType());
                    instance.DeleteAnimalAt(index);
                    break;
                }
            }

        }

        /// <summary>
        /// An animal item was deleted from the register.
        /// </summary>
        private void animalItemWasDeleted(string species)
        {
            publishAnimalObject = species;
        }

        /// <summary>
        /// Reset, for a new delete.
        /// </summary>
        private void ResetForNewDelete()
        {
            publishAnimalObject = string.Empty;
        }


        #region publisher
        private string publishAnimalObject;//animal type that was deleted
        private List<ObserverAnimalButton> observers = new List<ObserverAnimalButton>();

        /// <summary>
        /// Attaches to delete animal button.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void AttachToDeleteAnimalButton(ObserverAnimalButton observer)
        {
            observers.Add(observer);
        }
        /// <summary>
        /// Gets the state of animal button.
        /// </summary>
        /// <returns></returns>
        public string GetStateOfAnimalButton()
        {
            return publishAnimalObject;
        }
        /// <summary>
        /// Notifies all observers of animal button.
        /// </summary>
        public void NotifyAllObserversOfAnimalButton()
        {
            foreach (ObserverAnimalButton observer in observers)
            {
                observer.UpdateFromNewAnimalButton();
            }
            ResetForNewDelete();
        }
        #endregion

        #region subscription        
        /// <summary>
        /// Updates this instance via a publisher.
        /// </summary>
        public void Update()
        {
            id = listAnimalObjectsInDetail.GetState();
        }
        #endregion

        public DeleteAnimalButton(ListAnimalObjectsInDetailUI listAnimalObjectsInDetail)
        {
            ButtonContent = buttonTitle;
            this.listAnimalObjectsInDetail = listAnimalObjectsInDetail;
            listAnimalObjectsInDetail.Attach(this);
            AttachToDeleteAnimalButton(listAnimalObjectsInDetail);
            id = string.Empty;
            ResetForNewDelete();
        }
    }
}
