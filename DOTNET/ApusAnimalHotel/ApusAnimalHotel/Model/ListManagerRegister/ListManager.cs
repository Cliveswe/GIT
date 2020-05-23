using ApusAnimalHotel.ViewModel.Util;
using System;
using System.Collections.Generic;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date: 2019-04-09
/// </summary>
namespace ApusAnimalHotel.Model.ListManagerRegister
{
    public class ListManager<T> : IListManager<T>
    {
        public List<T> m_list;

        /// <summary>
        /// Return the number of items in the collection m_list
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get {
                return m_list.Count;
            }
        }

        /// <summary>
        /// Add an object to the collection m_list.
        /// </summary>
        /// <param name="aType">The aType.</param>
        /// <returns>
        /// True if successful otherwise false
        /// </returns>
        public bool Add(T aType)
        {
            m_list.Add(aType);
            return m_list.Contains(aType);
        }

        /// <summary>
        /// Change the element at a specific index.
        /// </summary>
        /// <param name="aType">A new type.</param>
        /// <param name="anIndex">Change element at index.</param>
        /// <returns>True if successful otherwise false</returns>
        public bool ChangeAt(T aType, int anIndex)
        {
            bool res = false;

            if (CheckIndex(anIndex))
            {
                DeleteAt(anIndex);//first remove element 
                m_list.Insert(anIndex, aType);//insert new element
                res = true;
            }

            return res;
        }

        /// <summary>
        /// Checks if the index is in the range of the list.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>True if in range otherwise false</returns>
        public bool CheckIndex(int index)
        {
            bool res = true;

            if ((index < 0) || (index >= Count))
            {
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Delete all the elements from the list.
        /// </summary>
        public void DeleteAll()
        {
            m_list.Clear();
        }

        /// <summary>
        /// Remove an object from the collection m_list at
        /// a given position.
        /// </summary>
        /// <param name="anIndex">Index of object to be removed</param>
        /// <returns>
        /// True if successful otherwise false
        /// </returns>
        public bool DeleteAt(int anIndex)
        {
            bool res = false;
            if (CheckIndex(anIndex))
            {
                m_list.RemoveAt(anIndex);
                res = true;
            }

            return res;
        }
        /// <summary>
        /// Gets an element at index.
        /// </summary>
        /// <param name="anIndex">An index.</param>
        /// <returns>An element otherwise the default null</returns>
        public T GetAt(int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                return m_list[anIndex];
            }
            return default(T);
        }

        /// <summary>
        /// Converts the register to a list of strings.
        /// </summary>
        /// <returns>List<string></returns>
        public List<string> ToSrtingList()
        {
            List<string> newList = new List<string>();

            foreach (T item in m_list)
            {
                newList.Add(item.ToString());
            }

            return newList;
        }

        /// <summary>
        /// Converts the register to an string array.
        /// </summary>
        /// <returns>array of strings</returns>
        public string[] ToStringArray()
        {

            return ToSrtingList().ToArray();
        }

        #region Serialization
        /// <summary>
        /// Save register to a file.
        /// </summary>
        /// <param name="fileName">Path an name of file.</param>
        public void BinarySerialize(string fileName)
        {
            try
            {
                SerializationUtility.BinaryWriter(m_list, fileName);
            }
            catch (Exception e)//catch all exception
            {
                throw new Exception("Binary Serialize method. ", e);
            }
        }
        /// <summary>
        /// Load a file to the register.
        /// </summary>
        /// <param name="fileName">Path an name of file.</param>
        public void BinaryDeSerialize(string fileName)
        {
            try
            {
                m_list = SerializationUtility.BinaryReader<List<T>>(fileName);
            }
            catch (Exception e)//catch all exception
            {
                throw new Exception("Binary De-Serialize method. ", e);
            }
        }
        /// <summary>
        /// Save register to a file.
        /// </summary>
        /// <param name="fileName">Path an name of file.</param>
        public void XMLSerialize(string fileName)
        {
            try
            {
                SerializationUtility.TextWriter(m_list, fileName);
            }
            catch (Exception e)//catch all exception
            {
                throw new Exception("Text Serialize method. ", e);
            }
        }
        /// <summary>
        /// Load a file to the register.
        /// </summary>
        /// <param name="fileName">Path an name of file.</param>
        public void XMLDeSerialize(string fileName)
        {
            try
            {
                m_list = SerializationUtility.TextReader<List<T>>(m_list.GetType(), fileName);
            }
            catch (Exception e)//catch all exception
            {
                throw new Exception("Text De-Serialize method. ", e);
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ListManager{T}"/> class.
        /// </summary>
        public ListManager()
        {
            m_list = new List<T>();
        }
    }
}
