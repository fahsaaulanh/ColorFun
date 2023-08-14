using System.Collections;
using System.Collections.Generic;
using TMPro;
using TS.ColorPicker.Demo;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static UnityAction OnSaveDesign;

    [SerializeField] private DatabaseDesign databaseDesign;
    [SerializeField] private GameData gameData;

    [SerializeField] private List<ColorButtonController> buttonControllers;

    public static ColorPalatteData.ColorsState currentState;
    public static ColorPalatteData.Colors currentColor;

    public DesignObjController designObjController;


    public List<GameObject> objectNotExport;
    public enum GameState
    {
        none,
        onPickColor
    }

    public static GameState gameState;

    private void Start()
    {
        SpawnDesignTamplate();
        currentState = ColorPalatteData.ColorsState.none;

        gameState = GameState.none;
    }

    private void OnEnable()
    {
        ColorPickerButton.OnPickColor += SetGameState;
    }

    private void OnDisable()
    {
        ColorPickerButton.OnPickColor -= SetGameState;
    }

    private void SetGameState(bool onPickColorOpen)
    {
        if (onPickColorOpen)
        {
            gameState = GameState.onPickColor;
        }
        else
        {
            gameState = GameState.none;
        }
    }

    private void SpawnDesignTamplate()
    {
        var objToSpawn = gameData.currenntTamplate.tamplateObj;

        var obj = Instantiate(objToSpawn);

        designObjController = obj.GetComponent<DesignObjController>();
        designObjController.SetDesignInfo(gameData.currenntTamplate.id, gameData.currenntTamplate.tamplateName, gameData.currentAuthor);

    }

    public void SaveDesignData()
    {
        Animator anim = designObjController.GetComponent<Animator>();

        anim.SetBool("finish", true);

        for (int i = 0; i < objectNotExport.Count; i++)
        {
            objectNotExport[i].SetActive(false);
        }
        OnSaveDesign?.Invoke();
        StartCoroutine(SavePicDesign());
    }

    IEnumerator SavePicDesign()
    {
        yield return new WaitForSeconds(1.5f);
        gameData.currenntTamplate.tamplateObj = null;
        gameData.currentAuthor = null;
        SceneManager.LoadScene("MainMenuScene");
    }
}
