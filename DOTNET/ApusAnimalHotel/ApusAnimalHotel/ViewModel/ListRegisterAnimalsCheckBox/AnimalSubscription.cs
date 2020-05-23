using System.Collections.Generic;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-11
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListRegisterAnimalsCheckBox
{
    /// <summary>
    /// A structure that will hold data concerning the different animal species in a register.
    /// </summary>
    class AnimalSubscription
    {
        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive
        {
            get {
                return _isActive;
            }
            private set {
                _isActive = value;
            }
        }
        private bool _isActive;
        /// <summary>
        /// Gets the animal objects.
        /// </summary>
        /// <value>
        /// The animal objects.
        /// </value>
        public List<string> AnimalObjects {
            get 
                {
                return _animalObjects;
            }
            private set
                {
                _animalObjects = value;
            }
        }
        private List<string> _animalObjects;
        /// <summary>
        /// Gets the number of animals objects in the list.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get {
                return AnimalObjects.Count;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalSubscription"/> class.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="listOfAnimalObj">The list of animal object.</param>
        public AnimalSubscription(bool isActive, List<string> listOfAnimalObj)
        {
            IsActive = isActive;
            if (isActive)
            {
                AnimalObjects = new List<string>(listOfAnimalObj);
            }
            else
            {
                AnimalObjects = null;
            }
        }
    }
}
