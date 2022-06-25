using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesData
{
    public static ResourcesData Instance = null;
    public List<DBResourceInfo> allResourceInfo;

    public static ResourcesData sharedManager()
    {


        if (Instance == null)
        {
            Instance = ResourcesData.Create();
        }
        return Instance;


    }

    public static void Method()
    {

    }

    private static ResourcesData Create()
    {
        ResourcesData ret = new ResourcesData();
        if (ret != null && ret.Init())
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    private bool Init()
    {
        allResourceInfo = new List<DBResourceInfo>();
        LoadData();
        return true;
    }

    private void LoadData()
    {
        if (AlertViewManager.sharedManager() != null)
        {
            AlertViewManager.sharedManager().callBack += AlertViewCallBack;
        }
        allResourceInfo.Clear();

        IEnumerable<resources> allResourceData = DatabaseManager.sharedManager().databaseDocument.Table<resources>().ToList();
        foreach (var resource_Info in allResourceData)
        {

            DBResourceInfo resourceInfo = resource_Info.GetDBResourceInfo();
            allResourceInfo.Add(resourceInfo);
        }
       
    }

    public DBResourceInfo GetResourceInfo(int _resid)
    {

        // Log.L(allResourceInfo.Count);
        for (int i = 0; i < allResourceInfo.Count; i++)
        {
            DBResourceInfo resourceInfo = allResourceInfo[i];
            //Log.L("...." + resourceInfo.resid);
            if (_resid == resourceInfo.resid)
            {
                //Log.L("........." + resourceInfo.res_name);
                return resourceInfo;
            }
        }
        return DBResourceInfo.Create(_resid);
    }

    public int AmountOfResources(int _resid)
    {

        for (int i = 0; i < allResourceInfo.Count; i++)
        {
            DBResourceInfo resourceInfo = allResourceInfo[i];
            if (_resid == resourceInfo.resid)
            {
                return resourceInfo.amount;
            }
        }
        return 0;
    }

    public void AddResource(int _resid, int _amount, bool willUpdateData = true)
    {


        for (int i = 0; i < allResourceInfo.Count; i++)
        {
            DBResourceInfo resourceInfo = allResourceInfo[i];
            if (_resid == resourceInfo.resid)
            {
                resourceInfo.amount = resourceInfo.amount + _amount;
                resourceInfo.UpdateDatabase();
            }
        }
        if (willUpdateData)
        {
            if (_resid == (int)ResourceIndex.bucksIndex)
            {
                if (_amount > 0)
                {

                }
                else if (_amount < 0)
                {

                }
            }
            if (_resid == (int)ResourceIndex.evoMatarialsIndex)
            {
                if (_amount > 0)
                {

                }
                else if (_amount < 0)
                {

                }
            }
            /*if (ITIWScorebar.sharedManager() != null)
            {
                ITIWScorebar.sharedManager().UpdateScoreBar();
            }*/
        }

    }

    public void AddResource(string strResourceValue)
    {

        List<String> arrResourceData = strResourceValue.Split(";".ToCharArray()).ToList();
        for (int i = 0; i < arrResourceData.Count; i++)
        {
            string strSingleResource = arrResourceData[i];
            List<String> arrSingleResource = strSingleResource.Split(",".ToCharArray()).ToList();
            int resourceType = Int32.Parse(arrSingleResource[0]);
            int resourceValue = Int32.Parse(arrSingleResource[1]);

            if (resourceType == (int)ResourceIndex.xpIndex)
            {
                //appDelegate->userInfo->experience = appDelegate->userInfo->experience + resourceValue;
                //appDelegate->userInfo->updateDatabase(appDelegate->userInfo->uid, appDelegate->database);
            }
            else
            {
                AddResource(resourceType, resourceValue, true);
            }
        }
    }

    public void AddResourceDouble(string strResourceValue)
    {

        List<String> arrResourceData = strResourceValue.Split(";".ToCharArray()).ToList();
        for (int i = 0; i < arrResourceData.Count; i++)
        {
            string strSingleResource = arrResourceData[i];
            List<String> arrSingleResource = strSingleResource.Split(",".ToCharArray()).ToList();
            int resourceType = Int32.Parse(arrSingleResource[0]);
            int resourceValue = 2 * Int32.Parse(arrSingleResource[1]);

            if (resourceType == (int)ResourceIndex.xpIndex)
            {
                //appDelegate->userInfo->experience = appDelegate->userInfo->experience + resourceValue;
                //appDelegate->userInfo->updateDatabase(appDelegate->userInfo->uid, appDelegate->database);
            }
            else
            {
                AddResource(resourceType, resourceValue, true);
            }
        }
    }

    public void AddResourceToBuy(string strResourceValue)
    {
        Debug.Log("resource string --> " + strResourceValue);
        List<String> arrResourceData = strResourceValue.Split(char.Parse(";")).ToList();


        for (int i = 0; i < arrResourceData.Count; i++)
        {
            
            string strSingleResource = arrResourceData[i];
            //Debug.Log("resource string --> " + strSingleResource);
            List<String> arrSingleResource = AppDelegate.ComponentsSeparatedByString(strSingleResource, ",").ToList();
            int resourceType = Int32.Parse(arrSingleResource[0]);
            //Debug.Log("resource type --> "+ resourceType);
            int resourceValue = Int32.Parse(arrSingleResource[1]); // error


            if (resourceType == (int)ResourceIndex.xpIndex)
            {
                //appDelegate->userInfo->experience = appDelegate->userInfo->experience + resourceValue;
                //appDelegate->userInfo->updateDatabase(appDelegate->userInfo->uid, appDelegate->database);
            }
            else
            {
                AddResource(resourceType, -resourceValue, true);
            }
        }
    }

    public float BucksValueForResource(int _resid)
    {
        for (int i = 0; i < allResourceInfo.Count; i++)
        {
            DBResourceInfo resourceInfo = allResourceInfo[i];
            if (_resid == resourceInfo.resid)
            {
                return ((float)resourceInfo.bucks_value / 100.0f);
            }
        }

        return 0;
    }
    public float GetAmountResource(string strResourceValue, int type)
    {
        int amount = 0;
        List<String> arrResourceData = strResourceValue.Split(";".ToCharArray()).ToList();
        for (int i = 0; i < arrResourceData.Count; i++)
        {
            string strSingleResource = arrResourceData[i];
            List<String> arrSingleResource = strSingleResource.Split(",".ToCharArray()).ToList();
            int resourceType = Int32.Parse(arrSingleResource[0]);
            int resourceValue = Int32.Parse(arrSingleResource[1]);

            if (resourceType == type)
            {
                amount = resourceValue;
            }
        }
        return amount;
    }
    // show alertview only if isShow is true
    public bool CheckResourceAvailable(string strResourceValue, bool _isShow)
    {


        List<String> arrResourceData = strResourceValue.Split(";".ToCharArray()).ToList();

        for (int i = 0; i < arrResourceData.Count; i++)
        {
            string strSingleResource = arrResourceData[i];

            List<String> arrSingleResource = strSingleResource.Split(",".ToCharArray()).ToList();
            if (arrSingleResource.Count == 2)
            {
                int resourceType = Int32.Parse(arrSingleResource[0]);
                int resourceValue = Int32.Parse(arrSingleResource[1]);

                if (!CheckResourceAvailable(resourceType, resourceValue, _isShow))
                {
                    Log.L( resourceType + "........rsrc value......." + resourceValue);
                    Log.L("Resource Not Available!");
                    return false;
                }
                else
                {
                    //Log.L("Resource Available!");
                }
            }

        }
        return true;
    }

    public bool CheckResourceAvailable(int _resid, int _amount, bool _isShow)
    {

        ResourcesData resource = ResourcesData.sharedManager();
        
        if (resource.AmountOfResources(_resid) >= _amount)
        {
            
            return true;
        }
        else
        {
            if (_isShow)
            {
                GameObject alertViewPanel = AlertViewManager.sharedManager().CreatePanelAlertView(1);
                AlertView alertView = alertViewPanel.GetComponent<AlertView>();
                string alert = "Can't Purchase!";
                string message = "You do not have enough resource to buy this item.";
                alertView.InitilizeAlertView(alert, message);
                alertView.AddButtonWithTitle("BUY");
                alertView.AddButtonWithTitle("LATER");

                //if (_resid == bucksIndex)
                //{
                //    AppDelegate* appDelegate = AppDelegate::sharedApplication();
                //    VideoAdsManager::sharedManager()->addDelegate = NULL;
                //    AdViewPanel* adViewPanel = AdViewPanel::create();
                //    appDelegate->gHud->addChild(adViewPanel, 3);
                //    //                adViewPanel->addBuyButton();
                //}
                //else
                //{
                //    CCLOG("Show Alert");
                //    string message = "You do not have enough resource to buy this item.";
                //    AlertView* alert = AlertView::create("Can't Purchase!", message, this);
                //    alert->addButtonWithTitle("BUY");
                //    alert->addButtonWithTitle("LATER");
                //    alert->tag = 1;
                //    alert->show();
                //}
            }

            //if (_resid == bucksIndex)
            //    ITIWFirebaseManager::sharedManager()->sendAnalyticsEvent("gaeShortage_Bucks", "0", "0", 1);
            //else if (_resid == coinsIndex)
            //    ITIWFirebaseManager::sharedManager()->sendAnalyticsEvent("gaeShortage_Coin", "0", "0", 1);
            //else if (_resid == foodsIndex)
            //    ITIWFirebaseManager::sharedManager()->sendAnalyticsEvent("gaeShortage_Food", "0", "0", 1);
            //else if (_resid == evoMatarialsIndex)
            //    ITIWFirebaseManager::sharedManager()->sendAnalyticsEvent("gaeShortage_Evo", "0", "0", 1);

            return false;
        }

    }

    public bool CheckLevelAvailability(int _itemLevel)
    {
        if (AppDelegate.sharedManager().userInfo.level >= _itemLevel)
        {
            return true;
        }
        else
        {
            GameObject alertViewPanel = AlertViewManager.sharedManager().CreatePanelAlertView(2);
            AlertView alertView = alertViewPanel.GetComponent<AlertView>();
            string alert = "Can't Purchase!";
            string message = "You do not have enough level to buy this item.";
            alertView.InitilizeAlertView(alert, message);
            alertView.AddButtonWithTitle("CLOSE");

            return false;
        }
    }

    public void AlertViewCallBack(int tag, int buttonIndex)
    {
        Log.L("ButtonIndex : " + buttonIndex);

        if (tag == 1)
        {
            if (buttonIndex == 1)
            {
                Log.L("Buy resource.....");
            }
            else if (buttonIndex == 2)
            {
                Log.L("Later.......");
            }
        }
        else if (tag == 2)
        {
            if (buttonIndex == 1)
            {
                Log.L("Not enough Level, Closing...");
            }
        }
        else if (tag == 3)
        {

        }
    }

    public void AddResourceEvent(string action, string label, int _resid, int _amount)
    {

    }

    public void AddResourceEvent(string action, string label, string strResourceValue)
    {
        List<string> arrResourceData = strResourceValue.Split(";".ToCharArray()).ToList();
        for (int i = 0; i < arrResourceData.Count; i++)
        {
            string strSingleResource = arrResourceData[i];
            List<string> arrSingleResource = strSingleResource.Split(",".ToCharArray()).ToList();

            int resourceType = int.Parse(arrSingleResource[0]);
            int resourceValue = int.Parse(arrSingleResource[1]);

            AddResourceEvent(action, label, resourceType, resourceValue);

        }
    }
}


