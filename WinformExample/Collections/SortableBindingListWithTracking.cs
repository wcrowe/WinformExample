using System.ComponentModel;
/*
 This code uses a comparer class PropertyComparer<T> that implements the IComparer<T> interface and compares the 
values of two items based on the sort property. The ApplySortCore method uses the PropertyComparer to sort the items 
in the list, and the RemoveSortCore method clears the sort settings.

    It also need to be mentioned that you can use this class in case you want to track adds, updates and deletes, 
you can simply add or remove the desired items from the SortableBindingListWithTracking<T> and it will raise the ListChangedEvent 
which will notify the UI controls that are bound to this list about the changes.
 */

namespace WinformExample.Collections;

public class SortableBindingListWithTracking<T> : BindingList<T> where T : class
{
    private bool isSorted;
    private ListSortDirection sortDirection;
    private PropertyDescriptor sortProperty;

    public SortableBindingListWithTracking()
    {
    }

    public SortableBindingListWithTracking(IList<T> list)
        : base(list)
    {
    }

    protected override bool SupportsSortingCore
    {
        get { return true; }
    }

    protected override bool IsSortedCore
    {
        get { return isSorted; }
    }

    protected override PropertyDescriptor SortPropertyCore
    {
        get { return sortProperty; }
    }

    protected override ListSortDirection SortDirectionCore
    {
        get { return sortDirection; }
    }

    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        List<T> items = this.Items as List<T>;

        if (items != null)
        {
            PropertyComparer<T> pc = new PropertyComparer<T>(prop, direction);
            items.Sort(pc);
            isSorted = true;
            sortDirection = direction;
            sortProperty = prop;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        else
        {
            isSorted = false;
            sortDirection = ListSortDirection.Ascending;
            sortProperty = null;
        }
    }

    protected override void RemoveSortCore()
    {
        isSorted = false;
        sortDirection = ListSortDirection.Ascending;
        sortProperty = null;
    }
}
public class PropertyComparer<T> : IComparer<T> where T : class
{
    private PropertyDescriptor property;
    private ListSortDirection direction;

    public PropertyComparer(PropertyDescriptor prop, ListSortDirection direction)
    {
        property = prop;
        this.direction = direction;
    }

    public int Compare(T x, T y)
    {
        object xValue = x.GetType().GetProperty(property.Name).GetValue(x, null);
        object yValue = y.GetType().GetProperty(property.Name).GetValue(y, null);

        int returnValue;
        if (xValue == null && yValue == null)
        {
            returnValue = 0;
        }
        else if (xValue == null)
        {
            returnValue = -1;
        }
        else if (yValue == null)
        {
            returnValue = 1;
        }
        else if (xValue is IComparable)
        {
            returnValue = ((IComparable)xValue).CompareTo(yValue);
        }
        else if (xValue.Equals(yValue))
        {
            returnValue = 0;
        }
        else
        {
            returnValue = xValue.ToString().CompareTo(yValue.ToString());
        }

        if (direction == ListSortDirection.Ascending)
        {
            return returnValue;
        }
        else
        {
            return returnValue * -1;
        }
    }
}
