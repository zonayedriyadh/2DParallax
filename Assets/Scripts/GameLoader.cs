using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor;

namespace FPG
{
    public class GameLoader : MonoBehaviour
    {
        public static GameLoader Instance;
        public static bool loadCompleted;

        private byte currentTextIndex;
        private string[] texts = { "Loading", "Loading.", "Loading..", "Loading..." };

        [SerializeField] TextMeshProUGUI loadingText;
        [SerializeField] Image progressBarImg;

        private void Awake()
        {
            Instance = this;
            SetDomain();
        }
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(InitializeStuff());

            if (loadCompleted)
            {
                CloseLoadingPanel();
            }
            maxProgress = 0.9f;
            progressStep = 0.1f;
            elapsedLimit = 0.25f;
        }
        
        //[SerializeField] GameObject NotificationManagerPrefab;
        private IEnumerator InitializeStuff()
        {
            WaitForSeconds time = new WaitForSeconds(0.2f);

            //FPG.FirebaseManager.GetInstance().Init();
            //FirebaseManager.GetInstance().FetchCompleteCallback = OnLoadingCompleted;
            //yield return (time);
            FPG.VideoAdsManager.GetInstance().Init();
            yield return (time);
            /*FPG.FacebookManager.GetInstance().Init();
            yield return (time);
            FPG.MobileAdsManager.GetInstance().Init();
            yield return (time);
            FPG.Networking.getInstance().SetGameStatus();
            FPG.FirebaseManager.GetInstance().Init();
            yield return (time);
            FPG.IAPManager.GetInstance().Init();
            yield return (time);
            FirebaseManager.GetInstance().FetchCompleteCallback = OnLoadingCompleted;*/
            yield return (time);
            /*if (NotificationManagerPrefab != null)
            {
                //_ = NotificationSamples.GameNotificationsManager.Instance;
                Instantiate(NotificationManagerPrefab);
            }*/
        }

        // Update is called once per frame
        private float totalTimeElapsed;
        private float elapsed;
        private float elapsedLimit;
        private float maxProgress;
        private float progressStep;

        private void Update()
        {
            if (!loadCompleted)
            {
                totalTimeElapsed += Time.deltaTime;
                if (totalTimeElapsed >= 3f)
                {
                    OnLoadingCompleted();
                }
            }

            elapsed += Time.deltaTime;
            if (elapsed >= elapsedLimit)
            {
                elapsed = 0;
                elapsedLimit += 0.1f;
                UpdateLoadingUI(progressBarImg != null ? (progressBarImg.fillAmount + progressStep) : 0.1f);
            }
        }

        private void UpdateLoadingUI(float progress)
        {
            if (progress > 1) progress = 1.0f;
            if (progressBarImg != null && progressBarImg.fillAmount < maxProgress)
            {
                progressBarImg.fillAmount = progress;

                if (loadingText != null)
                {
                    int progressPercentage = (int)(progress * 100);
                    loadingText.text = texts[currentTextIndex++] + " " + progressPercentage + "%";
                    if (currentTextIndex >= texts.Length) currentTextIndex = 0;
                }
            }
        }

        public void OnLoadingCompleted()
        {
            if (loadCompleted) return;

            loadCompleted = true;
            UpdateLoadingUI(1);
            SetGameProperties();
            Invoke("CloseLoadingPanel", 0.2f);
        }


        private void CloseLoadingPanel()
        {
            //gameObject.transform.parent.gameObject.SetActive(false);
            Destroy(gameObject.transform.parent.gameObject);
        }

        private void OpenLoadingPanel()
        {
            gameObject.transform.parent.gameObject.SetActive(true);
        }

        private void SetGameProperties()
        {
            /*if (TagManager.GetCrossPromoMiniStatus()) UIManager.sharedManager().LoadStorePanel((int)Panel.CrossPromoMini);

            if (NotificationManager.Instance != null)
            {
                NotificationManager.Instance.OnApplicationLoaded();
            }*/

            //if (TagManager.IsStartWithStorePanelEnabled())
            //    StartPanel.SharedManager().OpenStartPanel();

            /*if (PlayerPrefs.GetInt("gameOpenCounter", 0) == 1)
            {
                // Things to do only at 1st session
            }*/

            // Give iap pending rewards
            /*if (FPG.IAPManager.IAPPendingRewardStatus.IsRewardPending())
            {
                UIManager.sharedInstance.LoadStorePanel((int)Panel.IAP);
                GameObject iapPanel = UIManager.sharedInstance.GetPanel((int)Panel.IAP);
                if (iapPanel != null)
                {
                    IAP.IapManagerDIno iapManagerDino = iapPanel.GetComponent<IAP.IapManagerDIno>();
                    if (iapManagerDino != null)
                    {
                        iapManagerDino.ProcessPendingReward();
                    }
                }
            }*/
        }

        #region DomainId
        public static void SetDomain()
        {
            //Debug.Log("DomainType "+(int)AppDelegate.GetDomainType());
            /*if (!PlayerPrefs.HasKey(Constants.domainId))
            {
                int randomDomain = UnityEngine.Random.Range(1, 5);
                PlayerPrefs.SetInt(Constants.domainId, randomDomain);
                //Debug.Log("domain id " + GetDomainId());
            }*/
            
        }

       /* public static int GetDomainId()
        {
            return PlayerPrefs.GetInt(Constants.domainId, 0);
        }*/

        #endregion

    }
}