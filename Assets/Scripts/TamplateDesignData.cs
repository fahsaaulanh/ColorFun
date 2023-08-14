using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class TamplateDesignData : ScriptableObject
{
    [System.Serializable]
    public struct Tamplate
    {
        public int id;
        public string tamplateName;
        public DesignObjController tamplateObj;
    }

    public List<Tamplate> tamplates;
}
