/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-04-09
///  
/// This class manages a register of the super type "Animal". It provides access and 
/// maintenance of the registered list of animals.
///

namespace ApusAnimalHotel.ViewModel.ButtonControls
{
    interface SubjectDeleteAnimalButton
    {
        void AttachToDeleteAnimalButton(ObserverAnimalButton observer);
        string GetStateOfAnimalButton();
        void NotifyAllObserversOfAnimalButton();

    }
}
