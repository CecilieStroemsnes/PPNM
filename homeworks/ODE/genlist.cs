using System.Collections.Generic;

public class genlist<T> {
    private List<T> data = new List<T>();

    public void add(T item) => data.Add(item);
    public T this[int i] => data[i];
    public int size => data.Count;
}

