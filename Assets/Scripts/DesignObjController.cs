using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DesignObjController : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] private TMP_Text boardName;

    [SerializeField] private List<BlokController> blokList;

    public DesignObject.Object designObject = new DesignObject.Object();

    public DesignObject.ObjectHexa designObjHexa = new DesignObject.ObjectHexa();

    private void Start()
    {
        var bloks = designObject.blok;

        for (int i = 0; i < blokList.Count; i++)
        {
            blokList[i].SetId(i);

            var blok = new DesignObject.Blok();
            blok.colorBlok = Color.white;
            blok.nameBlok = "Blok" + i;
            bloks.Add(blok);
        }

        boardName.text = designObject.author;

        Camera mainCamera = Camera.main;
        canvas.worldCamera = mainCamera;
    }

    private void OnEnable()
    {
        BlokController.OnBlokColorChange += SaveBlokColor;
        GameManager.OnSaveDesign += SaveDesign;
    }

    private void OnDisable()
    {
        BlokController.OnBlokColorChange -= SaveBlokColor;
        GameManager.OnSaveDesign -= SaveDesign;
    }

    private void SaveBlokColor(int blokID, Color color)
    {
        var blokData = new DesignObject.Blok()
        {
            nameBlok = designObject.blok[blokID].nameBlok,
            colorBlok = color
        };

        designObject.blok[blokID] = blokData;
    }

    public void SetDesignInfo(int templateId ,string tamplateName, string authorName)
    {
        designObject.tamplateId = templateId;
        designObject.author = authorName;
        designObject.name = tamplateName;
    }

    private void SaveDesign()
    {
        List<DesignObject.BlokHexa> newBlokHexaList = new();

        for(int i = 0; i < designObject.blok.Count; i++)
        {
            Color unityColor = designObject.blok[i].colorBlok;

            string hex = ColorUtility.ToHtmlStringRGBA(unityColor);

            DesignObject.BlokHexa newBlokHexa = new DesignObject.BlokHexa()
            {
                nameBlok = designObject.blok[i].nameBlok,
                hexaBlok = hex
            };

            newBlokHexaList.Add(newBlokHexa);
        }

        designObjHexa.tamplateId = designObject.tamplateId;
        designObjHexa.author = designObject.author;
        designObjHexa.name = designObject.name;
        designObjHexa.blok = newBlokHexaList;

    }


}
