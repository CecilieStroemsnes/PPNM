using System;
using System.Collections;
using System.Collections.Generic;

public class genlist<T> : IEnumerable<T> {
    private T[] data;
    private int _size;

    public int size { get { return _size; } }

    public genlist() {
        data = new T[8];
        _size = 0;
    }

    public void add(T item) {
        if (_size == data.Length) {
            T[] newdata = new T[data.Length * 2];
            Array.Copy(data, newdata, _size);
            data = newdata;
        }
        data[_size++] = item;
    }

    public T this[int i] {
        get {
            if (i < 0 || i >= _size) throw new IndexOutOfRangeException();
            return data[i];
        }
    }

    public IEnumerator<T> GetEnumerator() {
        for (int i = 0; i < _size; i++) yield return data[i];
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}

