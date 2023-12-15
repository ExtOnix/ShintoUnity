using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class CustomDictionary<TKey, TValue>
{
    [SerializeField] List<KeyValuePair<TKey, TValue>> items = new List<KeyValuePair<TKey, TValue>>();
    public TValue this[TKey _key] => Get(_key);

    public bool Add(TKey _key, TValue _value)
    {
        if (Contains(_key))
            return false;
        items.Add(new KeyValuePair<TKey, TValue>() { Key = _key, Value = _value});
        return true;
    }
    public bool Contains(TKey _key)
    {
        foreach(KeyValuePair<TKey, TValue> _item in items)
        {
            if (_item.Key.Equals(_key))
                return true;
        }
        return false;
    }
    public TValue Get(TKey _key)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key.Equals(_key))
                return items[i].Value;
        }
        return default;
    }

    public int Count() => items.Count;
    public TValue At(int _index) => items[_index].Value;

}



[Serializable]
public struct KeyValuePair<TKey, TValue>
{
    [SerializeField] TKey key;
    [SerializeField] TValue myValue;
    public TKey Key { get => key; set => key = value; }
    public TValue Value { get => myValue; set => myValue = value; }
}