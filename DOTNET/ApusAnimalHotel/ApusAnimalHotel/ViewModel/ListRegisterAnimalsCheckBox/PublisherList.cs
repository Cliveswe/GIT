using ApusAnimalHotel.ViewModel.GroupListBoxIO;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-11  
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListRegisterAnimalsCheckBox
{
    interface PublisherList
    {
        void Attach(Observer observer);
        AnimalSubscription GetState();
        void NotifyAllObservers();
    }
}
