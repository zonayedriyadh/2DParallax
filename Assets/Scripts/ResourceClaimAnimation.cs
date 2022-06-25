using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

using Random = UnityEngine.Random;

public class ResourceClaimAnimation : MonoBehaviour
{
    public static ResourceClaimAnimation Instance = null;

    private float transitionTime = 0.45f;
    private WaitForSeconds spinnerWaitTime;
    private WaitForSeconds resourceClaimWaitTime;
    private WaitForSeconds textIncrementWaitTime;
    private int totalRemove;
    
    public GameObject resourceSprite;
    IEnumerator Spinning;

    private Coroutine jumpTextCoroutine;
    bool isTextJumpingStarted = false;

    #region CoinAniationFor PGBOARD
    System.Random rand = new System.Random();
    public float afterBornMove = 0.5f;
    public float moveTocounter = 1f;
    //public float coingenerateTime = 0.05f;
    private WaitForSeconds coingenerateTime = new WaitForSeconds(0.05f);
    private WaitForSeconds DestroyTime = new WaitForSeconds(1.5f);
    #endregion
    //[Header("container where the coins will be a Child of")]
    //[Tooltip("This container should have the largest sorting layer and canvas mode should be Overlay")]
    //[SerializeField] Transform container;

    public static ResourceClaimAnimation SharedManager()
    {
        return Instance;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        //if (container == null)
        //{
        //    container = GameObject.FindGameObjectWithTag("Canvas").transform;
        //}
    }

    public void PlayResourceClaimAnimationPGBoard(int _type, int _total, RectTransform _startPos, RectTransform _endPos, Transform canvas, UnityAction funcTocall = null)
    {
        Log.L("playresourceclaimanimation");
        totalRemove = _total > 10 ? 10 : _total;

        StartCoroutine(ShowResourceFlyPGBoard(_type, _startPos, _endPos, canvas, funcTocall));
    }

    #region Spinner
    public void SpinningAnimation(RectTransform t, float speed, float time)
    {
        if (Spinning != null)
        {
            StopCoroutine(Spinning);
        }
        Spinning = SpinnerPlay(t, speed, time);
        spinnerWaitTime = new WaitForSeconds(time);
        StartCoroutine(Spinning);
    }

    IEnumerator SpinnerPlay(RectTransform t, float speed, float time)
    {
       
        t.DORotate(new Vector3(0, 0, 360),speed, RotateMode.FastBeyond360  ).SetLoops(-1,LoopType.Incremental);
       
        yield return spinnerWaitTime;
    }
    #endregion

    #region TextIncrement

    public void IncrementText(TextMeshProUGUI t, int initialValue, int AddedValue, float TimeToDisplay)
    {
        StartCoroutine(TextIncrementAnimation( t, initialValue, AddedValue, TimeToDisplay));
    }
    
    private IEnumerator TextIncrementAnimation(TextMeshProUGUI t , int initialValue, int AddedValue, float TimeToDisplay)
    {
        Log.L("called incement");
        textIncrementWaitTime = new WaitForSeconds ((float)(TimeToDisplay / (float)AddedValue));
        Log.L("initial value ..." + initialValue);
        Log.L("AddedValue....." + AddedValue);
        Log.L("TimeToDisplay..." + TimeToDisplay);
        Log.L("textIncrementWaitTime......." + ((float)(TimeToDisplay / (float)AddedValue)).ToString());

        //while(initialValue != (initialValue + AddedValue))
        //{
        //    initialValue++;
        //    t.text = (initialValue).ToString();
        //    yield return textIncrementWaitTime;
        //}

        if (AddedValue < 50)
        {
            for (int i = 1; i <= AddedValue; i++)
            {
                t.text = (initialValue + i).ToString();
                yield return textIncrementWaitTime;

            }
            yield return null;

        }
        else
        {
            for (int i = 1; i <= AddedValue; i+=3)
            {
                t.text = (initialValue + i).ToString();
                yield return textIncrementWaitTime;

            }
            t.text = (initialValue + AddedValue).ToString();
            yield return null;

        }

        
    }
    #endregion

    #region DoShakeScale

    public void ShakeScaleInfinite(RectTransform t, float duration, float Strength)
    {
        StartCoroutine(DoShakeAnimation(t, duration, Strength));
    }

    public IEnumerator DoShakeAnimation(RectTransform t, float duration,float Strength  )
    {
        t.DOShakeScale(duration, Strength).SetLoops(-1, LoopType.Yoyo);

        yield return null;
    }

    public void DoPunchMove(RectTransform t, float duration)
    {
        StartCoroutine(PunchmoveAnimation(t, duration));
    }
    IEnumerator PunchmoveAnimation(RectTransform t, float duration )
    {
        t.DOShakePosition(duration).SetLoops(-1, LoopType.Yoyo);
        yield return null;
    }

    #endregion
    
    
    

    IEnumerator ShowResourceFlyPGBoard(int resourceType, RectTransform _startPos, RectTransform _endPos, Transform canvas, UnityAction funcTocall = null)
    {
        Vector3 endPosition =_endPos.position;
        Vector3 startPosition = _startPos.position;
        Log.L("in show resources flay");
        if (_endPos == null)
        {
            endPosition = _endPos.position;
            Log.L("null");
        }
        List<GameObject> g = new List<GameObject>();
        resourceClaimWaitTime = new WaitForSeconds(.4f);
        // 0==Coin
        
        for (int i = 0; i <= totalRemove; i++)
        {
            if (_startPos != null)
            {
                GameObject prefabInstance = Instantiate(resourceSprite, startPosition, Quaternion.identity, canvas.transform);
                prefabInstance.GetComponent<Image>().sprite = ResourceUtility.ResourceImageGetter(resourceType); //coin
                g.Add(prefabInstance);

                /*if (i != 0) */                                        //For some unknown _startpos error
                                                                        // _gameObject.transform.DOMove(_endPos.position, transitionTime);

                //sakib anim modify
                Vector3 pos = prefabInstance.GetComponent<RectTransform>().position;

                // prefabInstance.GetComponent<RectTransform>().DOMove(new Vector3(pos.x + Random.Range(-range, range), pos.y + Random.Range(-range, range), pos.z), .6f);

                //sakib anim modify
                float offset = 200;
                float canvasScale = Screen.width / canvas.GetComponent<CanvasScaler>().referenceResolution.x;
                Log.L("Canvas Scaler --> "+ canvasScale);
                float x = rand.Next((int)(pos.x - (offset * canvasScale)), (int)(pos.x + offset * canvasScale));
                float y = rand.Next((int)(pos.y - (offset * canvasScale)), (int)(pos.y + offset * canvasScale));

                Vector3 pos2 = new Vector3(x, y, 0);

                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(prefabInstance.transform.DOMove(pos2, afterBornMove)).SetEase(Ease.Linear)
                  .Append(prefabInstance.transform.DOMove(endPosition, moveTocounter));

                Sequence mySequence2 = DOTween.Sequence();
                mySequence2.Append(prefabInstance.transform.DORotate(new Vector3(0, 180, 0), 0.5f)).Append(prefabInstance.transform.DORotate(new Vector3(0, 360, 0), 0.5f));
                //_gameObject.GetComponent<RectTransform>().DOMove(_endPos.position, transitionTime);
            }
            yield return coingenerateTime;

        }

        yield return DestroyTime;
        foreach (var item in g)
        {
            Destroy(item);
        }
        funcTocall?.Invoke();
    }

   /* private IEnumerator JumpText(GameObject textToJumpGo,int resourceType,int resourceAmount)
    {
        
        yield return new WaitForSeconds(0.4f);
        if (textToJumpGo == null)
            yield break;
        TextMeshProUGUI textToJump = textToJumpGo.GetComponent<TextMeshProUGUI>();
        Vector3 originalScale = textToJumpGo.transform.localScale;
        float scalefactor = 0.5f;
        float duration = 0.07f;
        int amountToAddPerJump = resourceAmount / 10;
        int totalAmountAdded = 0;
        int currentAmount = Convert.ToInt32( textToJump.text);
        ResourcesData.sharedManager().AddResource(resourceType, resourceAmount);

        while (isTextJumpingStarted)
        {
            textToJumpGo.transform.DOScale(new Vector3(originalScale.x + scalefactor, originalScale.y + scalefactor,1), duration);
            yield return new WaitForSeconds(duration);
            totalAmountAdded += amountToAddPerJump;
            //if (totalAmountAdded < resourceAmount) ResourcesData.sharedManager().AddResource(resourceType, amountToAddPerJump);
            //textToJump.text = StorePanel.GetStringFromValue(ResourcesData.sharedManager().AmountOfResources(resourceType));
            if (totalAmountAdded < resourceAmount) currentAmount += amountToAddPerJump;
            textToJump.text = currentAmount.ToString();
            textToJumpGo.transform.DOScale(originalScale, duration);
            yield return new WaitForSeconds(duration);
        }
        
        //if(totalAmountAdded<resourceAmount) ResourcesData.sharedManager().AddResource(resourceType, resourceAmount-totalAmountAdded);
        textToJump.text = StorePanel.GetStringFromValue(ResourcesData.sharedManager().AmountOfResources(resourceType));
        textToJumpGo.transform.localScale = originalScale;
    }*/

    private void StopTextJumping()
    {
        StopCoroutine(jumpTextCoroutine);
    }
}
