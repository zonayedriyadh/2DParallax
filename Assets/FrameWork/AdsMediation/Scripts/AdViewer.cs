using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPG
{
    public class AdViewer
    {
        #region Singleton
        private static AdViewer instance;
        public static AdViewer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdViewer();
                }
                return instance;
            }
        }
        #endregion Singleton

        #region Public APIs
        
        public void ShowVideoAdOrCrossPromo(UnityAction<bool> adViewCompleteCallback, string adSource = "DefaultAdSource")
        {
            this.adViewCompleteCallback = adViewCompleteCallback;
            this.adSource = adSource;
            if (string.IsNullOrEmpty(this.adSource)) this.adSource = "DefaultAdSource";

            //FPG.FirebaseManager.GetInstance().LogAnalyticsEvent(adSource + "AdWatchCalled");
            
            if (ShowOnlineVideoAd()) return;
            else ShowCrossPromoAd();
        }
        #endregion Public APIs

        #region Online Ad
        private bool ShowOnlineVideoAd()
        {
            if (!VideoAdsManager.GetInstance().IsVideoAdsAvailable()) return false;

            ThingsToDoBeforeAdOpened(true);
            VideoAdsManager.GetInstance().ShowVideoAds(OnlineAdViewCompleted);
            ThingsToDoAfterAdOpened(true);

            return true;
        }
        private void OnlineAdViewCompleted(bool success)
        {
            Debug.Log($"AdViewer OnlineAdViewCompleted. success: {success}");
            ThingsToDoAfterAdClosed(success, true);
            GiveRewardToCaller(success);
        }
        #endregion Online Ad

        #region CrossPromo

        private void ShowCrossPromoAd()
        {
            ThingsToDoBeforeAdOpened(false);
            //FPG.AdLoader.Instance.ShowAd(CrossPromoAdViewCompleted);
            ThingsToDoAfterAdOpened(false);
        }
        private void CrossPromoAdViewCompleted(bool success)
        {
            Debug.Log($"AdViewer CrossPromoAdViewCompleted. success: {success}");
            ThingsToDoAfterAdClosed(success, false);
            GiveRewardToCaller(success);
        }
        #endregion CrossPromo

        #region Callbacks

        private UnityAction<bool> adViewCompleteCallback;
        public string adSource;
        private void GiveRewardToCaller(bool success)
        {
            adViewCompleteCallback?.Invoke(success);
            adViewCompleteCallback = null;
            adSource = string.Empty;
        }

        #endregion Callbacks

        #region Routines
        private void ThingsToDoBeforeAdOpened(bool isOnlineAd)
        {
            Debug.Log($"AdViewer ThingsToDoBeforeAdOpened. isOnlineAd: {isOnlineAd} adSource: {adSource}");
            if (isOnlineAd)
            {
                //FirebaseManager.GetInstance().LogAnalyticsEvent(adSource + "OnlineAdOpen");
                //Networking.getInstance().video_ad_source = adSource + "AdView";
            }
            else
            {
                //FirebaseManager.GetInstance().LogAnalyticsEvent(adSource + "CrossPromoOpen");
                //Networking.getInstance().cross_promo_ad_source = adSource + "AdView";
            }
        }
        private void ThingsToDoAfterAdOpened(bool isOnlineAd)
        {
            Debug.Log($"AdViewer ThingsToDoAfterAdOpened. crosspromo: {!isOnlineAd} adSource: {adSource}");

        }
        private void ThingsToDoAfterAdClosed(bool success, bool isOnlineAd)
        {
            Debug.Log($"AdViewer ThingsToDoAfterAdClosed. success: {success} isOnlineAd: {isOnlineAd} adSource: {adSource}");

            if (success)
            {
                if (isOnlineAd)
                {
                    //FirebaseManager.GetInstance().LogAnalyticsEvent(adSource + "OnlineAdComplete");
                }
                else
                {
                    //FirebaseManager.GetInstance().LogAnalyticsEvent(adSource + "CrossPromoComplete");
                }
            }
        }
        #endregion Routines
        
    }
}