using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameHud : MonoBehaviour
{
    #region Variable
    
    public Canvas GameCanvas;
    public Canvas AnimationCanvas;

    public GameObject PanelStartPrefab;
    
    [Header("Resource Claim Animation Prefab")]
    public GameObject resourceClaimAnimPrefab;

    GameObject PanelStart;
    
    #endregion

    #region version
    bool isNewVersion = false;
    float currentVersion;
    float oldVersion = 0.0f;
    #endregion

    public static GameHud SharedInstance;

    public static GameHud sharedManager()
    {
        return SharedInstance;
    }
    private void Awake()
    {
        SharedInstance = this;
        AppDelegate appDelegate = AppDelegate.sharedManager();

        isNewVersion = CanReplaceDBFile();
        appDelegate.LoadUserInfo(isNewVersion);
        ProductPlane.GetAllProductInfo();
        ControllerBattle.GetAllControllerBattleinffo();
        ControllerMap.GetAllControllerMapinfo();
        Instantiate(resourceClaimAnimPrefab, this.transform.parent);
        //appDelegate.LoadUserInfo(true);
    }

    private bool CanReplaceDBFile()
    {
        currentVersion = float.Parse(Application.version);
        oldVersion = PlayerPrefs.GetFloat(Constants.dbVersionString, 0);
        if (oldVersion < currentVersion)
        {
            PlayerPrefs.SetFloat(Constants.dbVersionString, currentVersion);
            return true;
        }
        else
            return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        LoadeStorePanel((int)Panel.panelStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadeStorePanel(int panelId)
    {
        switch (panelId)
        {
            case (int)Panel.panelStart:
                {
                    PanelStart = Instantiate(PanelStartPrefab, AnimationCanvas.gameObject.transform);
                    break;
                }

            default:
                break;
        }

    }
}
