using System;
using System.Collections.Generic;

[Serializable]
public class SerDictionary<TKey, TVal>
{
    public Dictionary<TKey, TVal> data;
}
