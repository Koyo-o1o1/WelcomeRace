using NUnit.Framework;
using UnityEngine;

using System.Collections.Generic;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;


    public List<int> modefiers;
    

    //様々なバフ等を考慮した値を返す関数
    public int GetValue()
    {
        int finalValue = baseValue;

        foreach(int modefier in modefiers)
        {
            finalValue += modefier;
        }

        return finalValue;
    }

    private void AddModefier(int _modefier)
    {
        modefiers.Add(_modefier);
    }

    private void RemoveModefier(int _modefier)
    {
        modefiers.Remove(_modefier);
    }
}
