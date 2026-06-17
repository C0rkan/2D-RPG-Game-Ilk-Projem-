using System;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class Stat 
{
    [SerializeField] private float baseValue;

    public float GetValue() {
        return baseValue;
    }


}
