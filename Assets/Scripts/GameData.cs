using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class GameData : ScriptableObject
{
    public TamplateDesignData.Tamplate currenntTamplate;
    public string currentAuthor;
}
