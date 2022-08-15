using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class ItemsPanel<T> : ScriptableObject
{
    public bool battleStarted = false;
    public List<T> items = new List<T>();

    public void Setup(int size)
    {
        for (int i = 0; i < size; i++)
        {
            items.Add(default(T));
        }
    }

    public void Clear()
    {
        items = new List<T>();
        battleStarted = false;
    }

    public void Add(T thing, int index)
    {
        if (index < items.Count)
        {
            items[index] = thing;
        }
    }

    public void Remove(int index)
    {
        if (index < items.Count)
        {
            items[index] = default(T);
        }
    }

    public T Get(int index)
    {
        if (index < items.Count)
        {
            return items[index];
        }
        else return default(T);
    }
}
