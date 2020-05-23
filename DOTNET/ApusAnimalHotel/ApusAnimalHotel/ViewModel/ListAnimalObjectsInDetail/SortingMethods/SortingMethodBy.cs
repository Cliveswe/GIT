using System.Collections.Generic;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-25
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListAnimaObjectsInDetail.SortingMethods
{
     internal abstract class SortingMethodBy : IComparer<ListedAnimalDetails>
    {
         public abstract int Compare(ListedAnimalDetails x, ListedAnimalDetails y);
    }
}
