using System.Collections;

namespace ExampleBlog.Core.Domain.Common;

public class SortCriteria<TSortableFieldType> : IEnumerable<(TSortableFieldType, SortOrder)> where TSortableFieldType : Enum
{
    private List<TSortableFieldType> _keyOrder = [];
    private Dictionary<TSortableFieldType, SortOrder> _values = new();
    public void Add(TSortableFieldType key, SortOrder value)
    {
        if (_values.ContainsKey(key))
        {
            _keyOrder.Remove(key);
        }

        _values.Add(key, value);
        _keyOrder.Add(key);
    }


    public IEnumerator<(TSortableFieldType, SortOrder)> GetEnumerator()
    {
        foreach (var key in _keyOrder)
        {
            var value = _values[key];
            yield return (key, value);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
