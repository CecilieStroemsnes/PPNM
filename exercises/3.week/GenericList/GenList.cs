using System;

public class GenList<T> {
    public T[] data;
    private int count = 0; 
	public int Size => count;
    public T this[int i] => data[i]; // indexer

    public GenList() { data = new T[0]; }

    public void Add(T item) { 
		if (count == data.Length) {
			Array.Resize(ref data, count == 0 ? 4 : count * 2); // Expand dynamically
		}
		data[count++] = item; // Increase count
	}
}