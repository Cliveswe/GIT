
using System.Collections.Generic;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date: 2019-04-09
/// </summary>
namespace ApusAnimalHotel.Model.ListManagerRegister
{
    /// <summary>
    /// Interface for implementation by manager classes hosting a collection
    /// of type List<T> where T can be any object type. In this documentation,
    /// the collection is refereed to as m_list.
    /// IListManager can be implemented by different classes passing any type <T> at
    /// deceleration but then T MUST HAVE THE SAME TYPE IN ALL METHODS INCLUDED IN THIS
    /// INTERFACE.  
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    interface IListManager<T>
    {
        /// <summary>
        /// Return the number of items in the collection m_list
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; }

        /// <summary>
        /// Add an object to the collection m_list.
        /// </summary>
        /// <param name="aType">The aType.</param>
        /// <returns>True if successful otherwise false</returns>
        bool Add(T aType);

        bool ChangeAt(T aType, int anIndex);
        bool CheckIndex(int index);

        void DeleteAll();
        /// <summary>
        /// Remove an object from the collection m_list at
        /// a given position.
        /// </summary>
        /// <param name="anIndex">Index of object to be removed</param>
        /// <returns>True if successful otherwise false</returns>
        bool DeleteAt(int anIndex);

        T GetAt(int anIndex);

        string[] ToStringArray();

        List<string> ToSrtingList();

        //Assignment 4
        void BinarySerialize(string fileName);
        void BinaryDeSerialize(string fileName);
        void XMLSerialize(string fileName);
    }
}
