using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPG
{
    public class VideoAdsManager : MonoBehaviour
    {
        private static VideoAdsManager Instance;
        public static VideoAdsManager GetInstance()
        {
            return Instance;
        }

        private VideoAdsType runningAdType;
        int adSearchIndex = 0;
        string strRunningAdsUnitId;

        public static readonly string AD_NAME_RW = "Rewarded";
        public static readonly string AD_NAME_INT = "Interstitial";

        private WaitForSeconds restartAdLoadDelay = new WaitForSeconds(10f);

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }


        public void Init()
        {
            _ = AdViewer.Instance;
            SetRunningAdType();
            AdmobController.GetInstance().Init(AdmobInitialized);
            FANController.GetInstance().Init(FANInitialized);
        }

        #region Callbacks

        private void AdmobInitialized(bool success)
        {
            //Log.L("VideoAdsManager->AdmobInitialized success: "+success);
            LoadAds();
        }
        private void FANInitialized(bool success)
        {
            //Log.L("VideoAdsManager->FANInitialized success: " + success);
            //this is a dummy method
        }

        private void OnINTAdLoadComplete(bool success)
        {
            //Log.L("VideoAdsManager->OnINTAdLoadComplete success: " + success);
            if (success)
            {
                AdLoadCompleted();
            }
            else
            {
                AdLoadFailed();
            }
        }
        private void OnINTAdClose()
        {
            //Log.L("VideoAdsManager->OnINTAdClose");

            //UpdateAdShowCounter(AD_NAME_INT);
            GiveRewardToCaller(true);
            LoadAds();
        }

        private void OnRWAdLoadComplete(bool success)
        {
            //Log.L("VideoAdsManager->OnRWAdLoadComplete success:" + success);
            if (success)
            {
                AdLoadCompleted();
            }
            else
            {
                AdLoadFailed();
            }
        }
        private void OnRWAdClose(bool success)
        {
            //Log.L("VideoAdsManager-> OnRWAdClose:" + success);

            IncrementRewardedAdImpressionCount();
            if(success)
            {
                //UpdateAdShowCounter(AD_NAME_RW);

            }
            else
            {
                //Networking.getInstance().sendUserAdStatus(0, 0, 0, 0, 1, 0, 0, Networking.getInstance().video_ad_source);
            }

            GiveRewardToCaller(success);
            LoadAds();
        }

        #endregion Callbacks

        #region Ad Sequence

        public static readonly string KEY_AD_SEQUENCE = "adSearchOrder2";
        //public static readonly string VALUE_AD_SEQUENCE = "201,101,203,102,205,402,207,302,206,404,208,104,301,401";
        public static readonly string VALUE_AD_SEQUENCE = "201,101,401,301";
        private static int[] adOrderList;
        public static int[] GetAdSequence()
        {
            //if (adOrderList is null)
            {
                string[] orderListStr;
                orderListStr = VALUE_AD_SEQUENCE.Split(',');

                adOrderList = new int[orderListStr.Length];

                for (int i = 0; i < orderListStr.Length; i++)
                {
                    adOrderList[i] = System.Int32.Parse(orderListStr[i]);
                }
            }

            return adOrderList;
        }

        #endregion

        #region Ad Router
        private void SetRunningAdType()
        {
            runningAdType = VideoAdsType.VA_AdMob;

            if (adSearchIndex < GetAdSequence().Length)
            {
                int adPlacementKey = GetAdSequence()[adSearchIndex];

                if ( adPlacementKey >= 101 && adPlacementKey <= 200)
                {
                    runningAdType = VideoAdsType.VA_FaceBookAds;
                }
                else if (adPlacementKey >= 201 && adPlacementKey <= 300)
                {
                    runningAdType = VideoAdsType.VA_AdMob;
                }
                else if (adPlacementKey >= 301 && adPlacementKey <= 400)
                {
                    runningAdType = VideoAdsType.VA_FaceBookAds_Interstitial;
                }
                else if (adPlacementKey >= 401 && adPlacementKey <= 500)
                {
                    runningAdType = VideoAdsType.VA_Admob_Interstitial;
                }
                else
                {
                    runningAdType = VideoAdsType.VA_AdMob;
                }
            }
        }
        public string GetAdsUnitId()
        {
            if (adSearchIndex < GetAdSequence().Length)
            {
                AdPlacementKey placementKey = (AdPlacementKey)GetAdSequence()[adSearchIndex];
                switch (placementKey)
                {
                    case AdPlacementKey.adKey_facebook_high:
                        {
                            return AdPlacement.fb_placement_id_high;
                        }
                    case AdPlacementKey.adKey_facebook_medium:
                        {
                            return AdPlacement.fb_placement_id_medium;
                        }
                    case AdPlacementKey.adKey_facebook_low:
                        {
                            return AdPlacement.fb_placement_id_low;
                        }
                    case AdPlacementKey.adKey_facebook_upperTH:
                        {
                            return AdPlacement.fb_placement_id_upperTH;
                        }
                    case AdPlacementKey.adKey_facebook_lowerTH:
                        {
                            return AdPlacement.fb_placement_id_lowerTH;
                        }
                    case AdPlacementKey.adKey_facebook_default:
                        {
                            return AdPlacement.fb_placement_id_default;
                        }
                    case AdPlacementKey.adKey_admob_25:
                        {
                            return AdPlacement.admob_adunit_id_25;
                        }
                    case AdPlacementKey.adKey_admob_20:
                        {
                            return AdPlacement.admob_adunit_id_20;
                        }
                    case AdPlacementKey.adKey_admob_15:
                        {
                            return AdPlacement.admob_adunit_id_15;
                        }
                    case AdPlacementKey.adKey_admob_10:
                        {
                            return AdPlacement.admob_adunit_id_10;
                        }
                    case AdPlacementKey.adKey_admob_07:
                        {
                            return AdPlacement.admob_adunit_id_07;
                        }
                    case AdPlacementKey.adKey_admob_05:
                        {
                            return AdPlacement.admob_adunit_id_05;
                        }
                    case AdPlacementKey.adKey_admob_03:
                        {
                            return AdPlacement.admob_adunit_id_03;
                        }
                    case AdPlacementKey.adKey_facebook_interstitial_01:
                        {
                            return AdPlacement.facebook_interstitial_id_01;
                        }
                    case AdPlacementKey.adKey_facebook_interstitial_02:
                        {
                            return AdPlacement.facebook_interstitial_id_02;
                        }

                    case AdPlacementKey.adKey_admob_interstitial_01:
                        {
                            return AdPlacement.admob_interstitial_id_01;
                        }
                    case AdPlacementKey.adKey_admob_interstitial_02:
                        {
                            return AdPlacement.admob_interstitial_id_02;
                        }

                    case AdPlacementKey.adKey_admob_interstitial_03:
                        {
                            return AdPlacement.admob_interstitial_id_03;
                        }
                    case AdPlacementKey.adKey_admob_interstitial_04:
                        {
                            return AdPlacement.admob_interstitial_id_04;
                        }

                    case AdPlacementKey.adKey_admob_default:
                        {
                            return AdPlacement.admob_adunit_id_default;
                        }
                }
            }
            return string.Empty;
        }
        public void LoadAds()
        {
            //Log.L("VideoAdsManager->LoadAds");
            if (adSearchIndex < GetAdSequence().Length)
            {
                SetRunningAdType();

                //Networking.getInstance().selectedAd = TagManager.GetAdSequence()[adSearchIndex];
                strRunningAdsUnitId = GetAdsUnitId();

                if (!string.IsNullOrEmpty(strRunningAdsUnitId))
                {
                    if (runningAdType == VideoAdsType.VA_AdMob)
                    {
                        AdmobController.GetInstance().LoadRewardedAd(strRunningAdsUnitId, OnRWAdLoadComplete);
                    }
                    else if (runningAdType == VideoAdsType.VA_FaceBookAds)
                    {
                        FANController.GetInstance().LoadRewardedAd(strRunningAdsUnitId, OnRWAdLoadComplete);
                    }
                    else if (runningAdType == VideoAdsType.VA_FaceBookAds_Interstitial)
                    {
                        FANController.GetInstance().LoadInterstitialAd(strRunningAdsUnitId, OnINTAdLoadComplete);
                    }
                    else if (runningAdType == VideoAdsType.VA_Admob_Interstitial)
                    {
                        AdmobController.GetInstance().LoadInterstitialAd(strRunningAdsUnitId, OnINTAdLoadComplete);
                    }
                }
                else
                {
                    //Log.L("VideoAdsManager->LoadAds placement null");
                    AdLoadFailed();
                }
            }
            else
            {
                StartCoroutine(RestartAdLoad());
            }
        }
        public IEnumerator RestartAdLoad()
        {
            //Log.L("VideoAdsManager->RestartAdLoad delay: " + delay);
            yield return restartAdLoadDelay;
            if (adSearchIndex >= GetAdSequence().Length)
            {
                adSearchIndex = Math.Max(GetAdSequence().Length - 2, 0);
                LoadAds();
                //Networking.getInstance().sendUserAdStatus(0, 0, 1, 0, 0, 0, 0, "0");
            }
        }
        #endregion Ad Router

        #region Api
        private UnityAction<bool> rewardCallback;
        public void ShowVideoAds(UnityAction<bool> callback)
        {
            if (!IsVideoAdsAvailable())
            {
                Debug.Log("ad is not available");
                return;
            }

            if (runningAdType == VideoAdsType.VA_AdMob)
            {
                AdmobController.GetInstance().ShowRewardedAd(OnRWAdClose);
				//Networking.getInstance().sendUserAdStatus(0, 0, 0, 1, 0, 0, 0, Networking.getInstance().video_ad_source);
			}
            else if (runningAdType == VideoAdsType.VA_FaceBookAds)
            {
                FANController.GetInstance().ShowRewardedAd(OnRWAdClose);
				//Networking.getInstance().sendUserAdStatus(0, 0, 0, 1, 0, 0, 0, Networking.getInstance().video_ad_source);
			}
            else if (runningAdType == VideoAdsType.VA_FaceBookAds_Interstitial)
            {
                FANController.GetInstance().ShowIterstitialAd(OnINTAdClose);
            }
            else if (runningAdType == VideoAdsType.VA_Admob_Interstitial)
            {
                AdmobController.GetInstance().ShowIterstitialAd(OnINTAdClose);
            }

            rewardCallback = callback;
        }
        public bool IsVideoAdsAvailable()
        {
            if (runningAdType == VideoAdsType.VA_AdMob)
            {
                if (AdmobController.GetInstance().IsRewardedAdAvailable())
                    return true;
            }
            else if (runningAdType == VideoAdsType.VA_FaceBookAds)
            {
                if (FANController.GetInstance().IsRewardedAdAvailable())
                    return true;
            }
            else if (runningAdType == VideoAdsType.VA_FaceBookAds_Interstitial)
            {
                if (FANController.GetInstance().IsInterstitialAdAvailable())
                    return true;
            }
            else if (runningAdType == VideoAdsType.VA_Admob_Interstitial)
            {
                if (AdmobController.GetInstance().IsInterstitialAdAvailable())
                    return true;
            }
            return false;
        }

        private void GiveRewardToCaller(bool eligibleForReward)
        {
            //Log.L("VideoAdsManager->GiveRewardToCaller ");
            if (rewardCallback != null)
            {
                rewardCallback(eligibleForReward);
                rewardCallback = null;
            }
        }
        #endregion Api

        #region Add Response

        public void AdLoadCompleted()
        {
            adSearchIndex = Math.Max(adSearchIndex - 2, 0);
            //Networking.getInstance().sendUserAdStatus(0, 1, 0, 0, 0, 0, 0, "0");
        }

        public void AdLoadFailed()
        {
            adSearchIndex++;

            if (adSearchIndex < GetAdSequence().Length)
            {
                //Networking.getInstance().sendUserAdStatus(0, 0, 1, 0, 0, 0, 0, "0");
                LoadAds();
            }
            else
            {
                //Networking.getInstance().selectedAd = 0;

                StartCoroutine(RestartAdLoad());
            }
        }



        public static readonly string AdCounterKey = "AdCounter";
       /* public void UpdateAdShowCounter(string adType)
        {
            //Log.L("VideoAdsManager->UpdateAdShowCounter adType: "+ adType);
            DailyTaskData.CurrentDailyTaskComplete((int)TaskType.WatchAd);
            //if (adType == AD_NAME_RW)
            {
                Networking.getInstance().sendUserAdStatus(0, 0, 0, 0, 1, 0, 1, Networking.getInstance().video_ad_source);

                int savedAdCounter = PlayerPrefs.GetInt(AdCounterKey, 0);
                PlayerPrefs.SetInt(AdCounterKey, savedAdCounter + 1);
                Networking.getInstance().total_ad_show_counter += 1;
                Networking.getInstance().sendUserAdStatus(1, 0, 0, 0, 0, 0, 0, Networking.getInstance().video_ad_source);
            }
            
        }*/


        public static readonly string RWAdsImpressionKey = "videoAdsImpression";
        public void IncrementRewardedAdImpressionCount()
        {
            int totalVideoAdsImpression = PlayerPrefs.GetInt(RWAdsImpressionKey, 0);
            totalVideoAdsImpression++;
            PlayerPrefs.SetInt(RWAdsImpressionKey, totalVideoAdsImpression);
        }
        #endregion Add Response
    }

    public enum VideoAdsType
    {
        VA_None = 0,
        VA_AdMob = 1,
        VA_FaceBookAds = 3,
        VA_FaceBookAds_Interstitial = 4,
        VA_Admob_Interstitial = 5,
    }

    public enum AdPlacementKey
    {
        adKey_facebook_high = 101,
        adKey_facebook_medium = 102,
        adKey_facebook_low = 103,
        adKey_facebook_default = 104,

        adKey_facebook_upperTH = 105,
        adKey_facebook_lowerTH = 106,

        adKey_admob_25 = 201,
        adKey_admob_20 = 202,
        adKey_admob_15 = 203,
        adKey_admob_10 = 204,
        adKey_admob_07 = 205,
        adKey_admob_05 = 206,
        adKey_admob_03 = 207,

        adKey_admob_default = 208,

        adKey_facebook_interstitial_01 = 301,
        adKey_facebook_interstitial_02 = 302,

        adKey_admob_interstitial_01 = 401,
        adKey_admob_interstitial_02 = 402,
        adKey_admob_interstitial_03 = 403,
        adKey_admob_interstitial_04 = 404,
    };


}
