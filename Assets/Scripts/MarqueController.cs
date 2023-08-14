using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarqueController : MonoBehaviour
{
    public DatabaseDesign databaseDesignList;

    [Header("Marquee Variabel")]
    public float scrollSpeed = 2f; // Speed of the scrolling
    public GameObject tampObj;
    public List<GameObject> currentDesignDisplay;
    public Transform marqueeSpawn;


    #region authinfo
    public static readonly string email = "fahsaaula8@gmail.com";
    public static readonly string password = "admin123";
    #endregion

    [HideInInspector] public LoginResult loginResult;

    public static MarqueController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        AuthLogin();

        InvokeRepeating("UpdateObject", 2, 2);
    }

    private void FixedUpdate()
    {
        if(marqueeSpawn.transform.childCount > 0)
        {
            Invoke("MarqueeMove", 0.2f);
        }
    }

    private void MarqueeMove()
    {
        var obj = marqueeSpawn;
        obj.transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);



        if (obj.transform.GetChild(currentDesignDisplay.Count - 1).GetComponent<Transform>().transform.position.x > 30)
        {
            float leftmostPosition = -30 / 2f;
            obj.transform.position = new Vector3(leftmostPosition, obj.transform.position.y, obj.transform.position.z);
        }
    }

    private void UpdateObject()
    {
       StartCoroutine(UpdateObject_Coroutine());
    }

    IEnumerator UpdateObject_Coroutine()
    {
        LoadData();
        yield return new WaitForSeconds(0.5f);

        int currentObjectDisplay = currentDesignDisplay.Count;
        int currentObjectInDatabase = databaseDesignList.dataHexaList.Count;

        if(currentObjectInDatabase > currentObjectDisplay)
        {
            UpdateDesignList(currentObjectDisplay);
        }
        
    }

    private void UpdateDesignList(int currentDisplay)
    {
        /* foreach(var obj in currentDesignDisplay)
         {
             Destroy(obj);
         }*/

        int startInstantiate = currentDisplay;

        for(int i = 0; i < databaseDesignList.dataHexaList.Count; i++)
        {
            if( i > startInstantiate)
            {
                float marqueeWidth = tampObj.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
                Vector3 startPos = tampObj.transform.position;
                Vector3 targetPos = new Vector3((startPos.x + (-marqueeWidth * i)), startPos.y * i, startPos.z * i);
                var obj = Instantiate(tampObj, marqueeSpawn.transform.position, Quaternion.identity);

                obj.GetComponent<PreviewObjController>().SetInfoDesign(i);
                obj.transform.SetParent(marqueeSpawn.transform, true);
                obj.transform.localPosition = targetPos;

                currentDesignDisplay.Add(obj);
            }
        }
    }

    private void AuthLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        loginResult = result;
        LoadData();
    }

    private void OnLoginError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void LoadData()
    {
        StartCoroutine(LoadDataCoroutine());
    }

    IEnumerator LoadDataCoroutine()
    {
        yield return new WaitForSeconds(2f);
        var request = new GetUserDataRequest()
        {
            PlayFabId = loginResult.PlayFabId,
            Keys = null
        };

        PlayFabClientAPI.GetUserData(request, OnGetDataSuccess, error => Debug.Log(error));
    }

    private void OnGetDataSuccess(GetUserDataResult result)
    {
        if (result.Data.ContainsKey("databaseDesignList"))
        {
            var data = JsonConvert.DeserializeObject<DatabaseDesign>(result.Data["databaseDesignList"].Value);
            databaseDesignList = data;
        }
        else
        {
            LoadData();
        }
    }

}
