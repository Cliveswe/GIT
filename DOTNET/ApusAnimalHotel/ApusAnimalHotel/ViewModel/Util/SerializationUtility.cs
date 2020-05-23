using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date: 2019-05-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.Util
{
    /// <summary>
    /// Class that contains static utility methods that can serialize data.
    /// </summary>
    public class SerializationUtility
    {
        #region Binary serialization
        /// <summary>
        /// Binary writer that writes objects to a file. 
        /// </summary>
        /// <typeparam name="T">Generic object to be serialized</typeparam>
        /// <param name="newObject">Object to be serialized</param>
        /// <param name="fileName">Name of file including its path.</param>
        public static void BinaryWriter<T>(T newObject, string fileName)
        {
            string BinaryWriterMessage = "Serialization Utility Binary Writer! ";
            Stream stream = null;
            BinaryFormatter bf;
            
            try
            {
                //make sure that the stream object is disposed.
                using (stream = File.Open(fileName, FileMode.Create, FileAccess.Write))
                {

                    bf = new BinaryFormatter();
                    bf.Serialize(stream, newObject);
                    stream.Close();
                }
            }
            //catch an exception
            catch (SerializationException ex)
            {
                //re-throw the serialization error
                throw new SerializationException(BinaryWriterMessage, ex);
            }
            catch (FileNotFoundException ex)
            {
                // re-throw the open file error
                throw new FileNotFoundException(BinaryWriterMessage, ex);
            }
            catch (Exception e)
            {
                throw new Exception(BinaryWriterMessage, e);
            }
            finally
            {
                //make sure that the stream is closed.
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// ************* Note ********************
        /// This method works, however we can use the method variable position to traverse
        /// the open serialized file.
        /// ***************************************
        /// 
        /// Binary reader, reads the contents of a files and converts them to objects. 
        /// </summary>
        /// <param name="position">Offset position in the file.</param>
        /// <param name="fileName">Name of file including its path.</param>
        /// <returns>Return a read section from the file as an object, Object.</returns>
        public static object BinaryReader(ref long position, string fileName)
        {
            string BinaryReaderMessage = "Serialization Utility Binary Reader! ";
            Stream stream = null;
            BinaryFormatter bf;
            object obj = null;

            try
            {

                //make sure that the stream object is disposed.
                using (stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {

                    bf = new BinaryFormatter();
                    stream.Seek(position, SeekOrigin.Begin);//start at beginning plus offset position
                    if (position < stream.Length)
                    {
                        obj = bf.Deserialize(stream);
                        position = stream.Position;//new offset
                    }
                    stream.Close();
                }

            }
            catch (SerializationException ex)
            {
                // re-throw the serialization error
                throw new SerializationException(BinaryReaderMessage, ex);
            }
            catch (FileNotFoundException ex)
            {
                // re-throw the open file error
                throw new FileNotFoundException(BinaryReaderMessage, ex);
            }
            finally
            {
                //make sure that the stream is closed.
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return obj;
        }

        /// <summary>
        ///  Binary reader, reads the contents of a files and converts them to generic objects.
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="fileName">Path and name of file </param>
        /// <returns>Generic object</returns>
        public static T BinaryReader<T>(string fileName)
        {
            string BinaryReaderMessage = "Serialization Utility Binary Reader! ";
            Stream stream = null;
            BinaryFormatter bf;
            object obj = null;

            try
            {

                //make sure that the stream object is disposed.
                using (stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {

                    bf = new BinaryFormatter();
                    stream.Seek(0, SeekOrigin.Begin);//start at beginning plus offset position
                                                     // if (position < stream.Length)
                                                     //{
                    obj = bf.Deserialize(stream);
                    //  position = stream.Position;//new offset
                    // }
                    stream.Close();
                }

            }
            catch (SerializationException ex)
            {
                // re-throw the serialization error
                throw new SerializationException(BinaryReaderMessage, ex);
            }
            catch (FileNotFoundException ex)
            {
                // re-throw the open file error
                throw new FileNotFoundException(BinaryReaderMessage, ex);
            }
            catch (Exception e)
            {
                throw new Exception(BinaryReaderMessage, e);
            }
            finally
            {
                //make sure that the stream is closed.
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return (T)obj;
        }
        #endregion

        /// <summary>
        /// Text writer, writes the content of an generic object to a file.
        /// </summary>
        /// <typeparam name="T">Generic object to be serialized</typeparam>
        /// <param name="newObject">Generic object content to be serialized</param>
        /// <param name="fileName">Path and name of file to save the generic content.</param>
        #region XML serialization
        public static void TextWriter<T>(T newObject, string fileName)
        {
            string TextWriterMessage = "Serialization Utility Text Writer! ";
            //object obj = (object)newObject;
            try
            {
                XmlSerializer serializer = new XmlSerializer(newObject.GetType());

                //make sure that the stream object is disposed.
                //using (TextWriter textWriter = new StreamWriter(fileName))
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(stream, newObject);
                    stream.Close();
                }
            }
            catch (SerializationException ex)
            {
                throw new SerializationException(TextWriterMessage, ex);

            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(TextWriterMessage, ex);
            }
            catch (Exception e)
            {
                throw new Exception(TextWriterMessage, e);
            }
        }

        /// <summary>
        /// Text reader, reads the content of a file.
        /// </summary>
        /// <typeparam name="T">Generic object to be serialized</typeparam>
        /// <param name="type">The Type of data object to be stored to file.</param>
        /// <param name="fileName">Path and name of file.</param>
        /// <returns>Generic object.</returns>
        public static T TextReader<T>(Type type, string fileName)
        {
            string TextReaderMessage = "Serialization Utility Text Reader! ";
            object obj = null;

            try
            {
                XmlSerializer deSerializer = new XmlSerializer(type);

                //make sure that the stream object is disposed.
                using (TextReader textReader = new StreamReader(fileName))
                {
                    obj = deSerializer.Deserialize(textReader);
                    textReader.Close();
                }
            }
            catch (SerializationException ex)
            {
                throw new SerializationException(TextReaderMessage, ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(TextReaderMessage, ex);
            }
            catch (Exception e)
            {
                throw new Exception(TextReaderMessage, e);
            }
            return (T)obj;
        }
        #endregion
    }
}
