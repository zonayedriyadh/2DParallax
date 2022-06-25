using UnityEngine;
using UnityEngine.Events;

public class AlertViewManager : MonoBehaviour
{
    public GameObject panelAlertView;
    public int buttonClickIndex;
    public new int tag;
    public UnityAction<int, int> callBack;

    public static AlertViewManager Instance = null;

    #region SharedManager 
    public static AlertViewManager sharedManager()
    {

        if (Instance == null)
        {
            Instance = AlertViewManager.Create();
        }
        return Instance;
    }

    private static AlertViewManager Create()
    {

        AlertViewManager ret = new AlertViewManager();
        if (ret != null && ret.init())
        {
            return ret;
        }
        else
        {
            return null;
        }
    }
    
    private bool init()
    {
        if (Instance == null)
            Instance = this;
        return true;
    }

    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }


    public GameObject CreatePanelAlertView(int _tag)
    {
        tag = _tag;
        GameObject g = Instantiate(panelAlertView, this.transform);
        return g;
    }
    public void AlertViewCallBack(int _index)
    {
        buttonClickIndex = _index;
        callBack.Invoke(tag, buttonClickIndex);
    }

}
