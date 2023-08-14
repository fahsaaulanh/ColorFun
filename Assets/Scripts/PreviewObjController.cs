using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreviewObjController : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] private TMP_Text boardName;

    [SerializeField] private List<BlokController> blokList;
    public DatabaseDesign databaseDesign;

    private int index;
    private void Start()
    {
        databaseDesign = MarqueController.instance.databaseDesignList;
    }

    public void SetInfoDesign(int ind)
    {
        index = ind;
        StartCoroutine(SetInfroDesignCoroutine());
    }

    IEnumerator SetInfroDesignCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(true);
        for (int i = 0; i < blokList.Count; i++)
        {
            Color unityColor;
            string hexColor = "#" + databaseDesign.dataHexaList[index].blok[i].hexaBlok;
            if (UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out unityColor))
            {
            }
            else
            {
            }

            var blokColor = unityColor;

            blokList[i].SetSelfColor(blokColor);
        }

        boardName.text = databaseDesign.dataHexaList[0].author;
    }
}
