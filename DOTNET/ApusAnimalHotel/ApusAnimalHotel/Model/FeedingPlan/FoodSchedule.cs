using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-20
/// 
/// Every species is provided with a food schedule containing information about 
/// the feeding plan for the species object,
/// </summary>
namespace AnimalMotel.Model.FeedingPlan
{
    [Serializable]
    public class FoodSchedule : ISerializable
    {
        #region Class properties
        /// <summary>
        /// Number of items in the feeding plan.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get {
                return foodDescriptionList.Count;
            }
        }
        #endregion

        //description of an animals feeding schedule
        public List<string> foodDescriptionList;

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodSchedule"/> class.
        /// </summary>
        public FoodSchedule()
        {
            foodDescriptionList = new List<string>();
        }
        /// <summary>
        /// Initializes a new copy instance of the <see cref="FoodSchedule"/> class.
        /// </summary>
        /// <param name="other">The other.</param>
        public FoodSchedule(FoodSchedule other)
        {
            this.foodDescriptionList = other.foodDescriptionList;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodSchedule"/> class.
        /// </summary>
        /// <param name="foodList">The food list.</param>
        public FoodSchedule(List<string> foodList)
        {
            foodDescriptionList = foodList;
        }

        /// <summary>
        /// Adds the food schedule item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>True if new item is added, otherwise false.</returns>
        internal bool AddFoodScheduleItem(string item)
        {
            if (!foodDescriptionList.Contains(item))
            {
                foodDescriptionList.Add(item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes a food schedule item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        /// <returns>True if the item is non-trivial and index is within range, otherwise false.</returns>
        bool ChangeFoodScheduleItem(string item, int index)
        {
            if (!(string.IsNullOrEmpty(item)) &&
                (ValidateIndex(index)))
            {
                foodDescriptionList.Insert(index, item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes a food schedule item.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>True if index is valid, otherwise false.</returns>
        bool DeleteFoodScheduleItem(int index)
        {
            if (ValidateIndex(index))
            {
                foodDescriptionList.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Describes the no feeding required.
        /// </summary>
        /// <returns>String with information.</returns>
        string DescribeNoFeedingRequired()
        {
            return "In hibernation or pupa state.";
        }

        /// <summary>
        /// Gets a food schedule.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>A food schedule item as non-empty string, otherwise and empty string.</returns>
        internal string GetFoodSchedule(int index)
        {
            if (ValidateIndex(index))
            {
                return foodDescriptionList.ElementAt(index);
            }
            return string.Empty;
        }

        /// <summary>
        /// Validates the index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>False if the index is not valid, otherwise true.</returns>
        bool ValidateIndex(int index)
        {
            if ((index >= Count) ||
                (index < 0))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Food schedule as a string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string str = string.Empty;

            foreach (string item in foodDescriptionList)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    str = ", " + item;
                }
                else
                {
                    str = item;
                }
            }
            return str + ".";
        }

        #region Serialization
        /// <summary>
        /// Serialize the object.
        /// </summary>
        /// <param name="info">Serialize information from the object in a key value pair.</param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FoodSchedule", foodDescriptionList);
        }
        /// <summary>
        /// De-serialize the object to restore its content.
        /// </summary>
        /// <param name="info">Serialize information get the object using a key value pair.</param>
        /// <param name="context"></param>
        public FoodSchedule(SerializationInfo info, StreamingContext context)
        {
            foodDescriptionList = (List<string>)info.GetValue("FoodSchedule", typeof(List<string>));
        }
        #endregion
    }
}
