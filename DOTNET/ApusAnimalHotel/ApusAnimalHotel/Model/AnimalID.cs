/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-09
///  
/// This class manages a register of the super type "Animal". It provides access and 
/// maintenance of the registered list of animals.
/// 
namespace ApusAnimalHotel.Model
{
    static class AnimalID
    {
        private static int idNumber;
        /// <summary>
        /// Get a new identifier.
        /// </summary>
        /// <returns></returns>
        public static int GetNewID()
        {
            int idOld;

            idOld = idNumber;
            idNumber++;

            return idOld;
        }

        static AnimalID()
        {
            idNumber = 1;
        }
    }
}
