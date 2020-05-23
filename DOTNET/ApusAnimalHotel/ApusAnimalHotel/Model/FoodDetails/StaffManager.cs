using ApusAnimalHotel.Model.ListManagerRegister;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date: 2019-04-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.FoodDetails
{
    class StaffManager : ListManager<Staff>
    {

        #region Singleton properties
        public static StaffManager GetInstance
        {
            get {
                return _instance;
            }
        }
        private static readonly StaffManager _instance = new StaffManager();

        /// <summary>
        /// Prevents a default instance of the <see cref="RecipeManager"/> class from being created.
        /// </summary>
        private StaffManager()
        {
        }

        /// <summary>
        /// Explicit static constructor to tell C# compiler
        /// not to mark type as beforefieldinit.
        /// Read article <see cref="http://csharpindepth.com/Articles/Singleton"/> C# in Depth.
        /// </summary>
        static StaffManager() { }
        #endregion

    }
}
