using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTamplateButton : MonoBehaviour
{
    [SerializeField] private TamplateDesignData tamplateData;
    [SerializeField] private GameData gameData;

    public void SetTamplateSelected(int index)
    {
        for (int j = 0; j < tamplateData.tamplates.Count; j++)
        {
            if(j == index)
            {
                gameData.currenntTamplate = tamplateData.tamplates[j];
            }
        }
    }
}
