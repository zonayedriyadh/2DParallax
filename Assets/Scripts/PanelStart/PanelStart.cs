using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelStart : MonoBehaviour
{
    #region varible
    public Button BtnPlay2;
    public Button BtnPlay;
    public Button BtnSettings;
    public Button BtnStore;

    public Button BtnPlusNext;
    public Button BtnMinusNext;
    public Button BtnChangePlane;

    public GameObject PlaneShowerPrefab;  
    GameObject PlaneShowerObj;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //SetCallBackToBtns();
        //ShowPlane();
        //LoadAnimation();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void SetCallBackToBtns()
    {
        BtnPlay.onClick.AddListener(()=>BtnPLayCallBack());
        BtnPlusNext.onClick.AddListener(() => BtnPlusCallBack());
        BtnMinusNext.onClick.AddListener(() => BtnMinusCallBack());
        BtnChangePlane.onClick.AddListener(() => BtnChangePlaneCallBack());
        BtnStore.onClick.AddListener(() => BtnMarketPanel());
    }

    public void BtnPLayCallBack()
    {
        RemoveFromParent();
        
        GameHud.sharedManager().LoadeStorePanel((int)Panel.panelGameTypeSelect);
        //GameManager.SharedManager().StartGame();
        // PanelGameStart.sharedInstance.StartGame();
        //this.gameObject.SetActive(false);
    }

    public void BtnMarketPanel()
    {
        RemoveFromParent();
    }
    public void BtnChangePlaneCallBack()
    {
        RemoveFromParent();
        GameHud.sharedManager().LoadeStorePanel((int)Panel.planeSelectPanel);
    }

    public void RemoveFromParent()
    {
        Destroy(PlaneShowerObj.gameObject);
        Destroy(this.gameObject);
    }
    public void BtnPlusCallBack()
    {
    }

    public void BtnMinusCallBack()
    {
       /* PlaneShower.SharedManager().setCurrentPlane(-1);*/
    }

    public void ShowPlane()
    {
        PlaneShowerObj = Instantiate(PlaneShowerPrefab); 
    }

    public void LoadAnimation()
    {
        PlayButtonAnimation();
        Vector3 PosBtnPlanes = BtnChangePlane.transform.position;
        Vector3 PosBtnPlay = BtnPlay.transform.position;
        Vector3 PosBtnSetting = BtnSettings.transform.position;
        Vector3 PosBtnStore = BtnStore.transform.position;

        BtnChangePlane.gameObject.SetActive(false);
        BtnPlay.gameObject.SetActive(false);
        //BtnSettings.gameObject.SetActive(false);
        BtnStore.gameObject.SetActive(false);

        //BtnSettings.gameObject.transform.position = PosBtnPlay;
        BtnStore.gameObject.transform.position = PosBtnPlay;


        Sequence animationSeq2 = DOTween.Sequence();

        animationSeq2.PrependInterval(0.5f).OnComplete(()=>
        {
            BtnChangePlane.gameObject.SetActive(true);
            BtnPlay.gameObject.SetActive(true);
            //BtnSettings.gameObject.SetActive(false);

            BtnChangePlane.gameObject.transform.position = PosBtnPlay;

            Sequence animationSeq = DOTween.Sequence();

            /*animationSeq.Append(BtnPlay.transform.DOMove(PosBtnPlay, 0.2f).OnStepComplete(() => {
                //BtnSettings.gameObject.SetActive(true);
                BtnSettings.gameObject.SetActive(true);
                BtnSettings.gameObject.transform.position = PosBtnPlay;
            })).
                Append(BtnSettings.transform.DOMove(PosBtnSetting, 0.2f).OnStepComplete(() => {
                //BtnSettings.gameObject.SetActive(true);
                BtnStore.gameObject.SetActive(true);
                    BtnStore.gameObject.transform.position = PosBtnSetting;
                })).
                Append(BtnStore.transform.DOMove(PosBtnStore, 0.2f));*/

            animationSeq.Append(BtnChangePlane.transform.DOMove(PosBtnPlanes, 0.2f).OnStepComplete(() => {
                //BtnSettings.gameObject.SetActive(true);
                //BtnSettings.gameObject.SetActive(true);
                //BtnSettings.gameObject.transform.position = PosBtnPlay;
                BtnStore.gameObject.SetActive(true);
                BtnStore.gameObject.transform.position = PosBtnPlay;
            })).
            Append(BtnStore.transform.DOMove(PosBtnStore, 0.2f));

        });

    }

    public void PlayButtonAnimation()
    {
        Sequence animationSeq = DOTween.Sequence();

        Vector3 scaleHigh = new Vector3(1.2f,1.2f,1.2f);
        Vector3 scaleLow = new Vector3(1f, 1f, 1f);
        animationSeq.Append(BtnPlay.transform.DOScale(scaleHigh, 0.1f)).PrependInterval(0.1f).Append(BtnPlay.transform.DOScale(scaleLow, 0.1f)).PrependInterval(0.1f)
            .Append(BtnPlay.transform.DOScale(scaleHigh, 0.1f)).PrependInterval(0.1f).Append(BtnPlay.transform.DOScale(scaleLow, 0.1f)).PrependInterval(2f).SetLoops(-1);
    }

    public void firstSeq()
    {
       /* BtnSettings.gameObject.SetActive(true);
        BtnStore.gameObject.SetActive(false);
        BtnStore.gameObject.transform.position = */
    }

}
