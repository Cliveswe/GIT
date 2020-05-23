using AnimalMotel.Model.FeedingPlan;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-15
/// </summary>
namespace ApusAnimalHotel.ViewModel.FeedingScheduleCheckBox
{
    /// <summary>
    /// This class displays a feeding schedule of an animal by genus.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class FeedingScheduleDetails : INotifyPropertyChanged
    {
        #region Class properties
        /// <summary>
        /// Gets or sets the food schedule detail list.
        /// </summary>
        /// <value>
        /// The food schedule detail list.
        /// </value>
        public ObservableCollection<string> FoodScheduleDetailList
        {
            get {
                return _foodScheduleDetailList;
            }
            set {
                _foodScheduleDetailList = value;
                OnPropertyChanged("FoodScheduleDetailList");
            }
        }
        private ObservableCollection<string> _foodScheduleDetailList;

        /// <summary>
        /// Gets or sets the type of the diet.
        /// </summary>
        /// <value>
        /// The type of the diet.
        /// </value>
        public string DietType
        {
            get 
                {
                return _dietType;
            }
            set 
                {
                _dietType = value;
                OnPropertyChanged("DietType");
            }
        }
        private string _dietType;
        #endregion

        /// <summary>
        /// Sets the type of the diet.
        /// </summary>
        /// <param name="str">The string.</param>
        public void SetDietType(EaterEnum dietType)
        {
            DietType = Enum.GetName(typeof(EaterEnum), dietType).ToString();
        }

        /// <summary>
        /// Sets the feeding diet.
        /// </summary>
        /// <param name="dietList">The diet list.</param>
        public void SetFeedingDiet(FoodSchedule dietList)
        {
            int count = dietList.Count;
            if (count > 0)
            {
                for (int index = 0; count > index; index++)
                {
                    FoodScheduleDetailList.Add(dietList.GetFoodSchedule(index));
                }
            }
        }

        /// <summary>
        /// Rests the feeding schedule.
        /// </summary>
        public void RestFeedingSchedule()
        {
            FoodScheduleDetailList.Clear();
            DietType = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedingScheduleDetails"/> class.
        /// </summary>
        public FeedingScheduleDetails()
        {
            FoodScheduleDetailList = new ObservableCollection<string>();

        }

        #region INotifyPropertyChanged members
        /// <summary>
        /// This is boiler plate code that was added when one want to notify a change on a class
        /// property. This is where the code and the UI communicate through the event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise a notification that a property has been changed.
        /// </summary>
        /// <param name="propertyName">A string of the property name</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
