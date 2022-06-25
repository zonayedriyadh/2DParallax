
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppDelegate
{
    public int selectedTankIndex;
    public DBUserInfo userInfo;
    public bool isLowDevice = false;
    public string animalName = "Shark";
    public string bundleIdentifier = "";
    public bool isPortraitEnable = false;

    public int startProductId = 2808;
    public bool isNoAdsBtnEnabled = false;

    public static int currentMapIndex = 0;
    public static bool isArcadeMode = false;
    public static AppDelegate sharedInstance = null;

    public static AppDelegate sharedManager()
    {
        if (sharedInstance == null)
        {
            sharedInstance = AppDelegate.Create();
        }
        return sharedInstance;
    }

    public static AppDelegate Create()
    {
        AppDelegate ret = new AppDelegate();
        if (ret != null && ret.Init())
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    public bool Init()
    {
        //isVillageLevelBattleLevelSame=true;

        if (Screen.height > Screen.width)
            isPortraitEnable = true;
        else
            isPortraitEnable = false;
        return true;
    }

    #region String

    public static string[] ComponentsSeparatedByString(string _string, string separator)
    {
        string[] arrSeparator = { separator };
        string[] arrString = _string.Split(arrSeparator, System.StringSplitOptions.RemoveEmptyEntries);

        return arrString;
    }

    #endregion

    public static int GetNumberOfDaysDifference(Date dt1, Date dt2)
    {
        int[] monthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        long n1 = dt1.y * 365 + dt1.d;

        for (int i = 0; i < dt1.m - 1; i++)
            n1 += monthDays[i];

        n1 += AppDelegate.GetCountLeapYears(dt1);

        long n2 = dt2.y * 365 + dt2.d;
        for (int i = 0; i < dt2.m - 1; i++)
            n2 += monthDays[i];
        n2 += AppDelegate.GetCountLeapYears(dt2);

        return (int)(n2 - n1);
    }

    private static int GetCountLeapYears(Date d)
    {
        int years = d.y;

        if (d.m <= 2)
            years--;

        return years / 4 - years / 100 + years / 400;
    }

    public static DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }


    public static int GetUnixTimstamp(DateTime date)
    {
        DateTime point = new DateTime(1970, 1, 1);
        TimeSpan time = date.Subtract(point);

        return (int)time.TotalSeconds;
    }

    public static int GetTime()
    {
        int timestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        return timestamp;
    }

    #region UserInfo

    public void LoadUserInfo(bool replaceDBFile = false)
    {

        int current_time = AppDelegate.GetTime();

        userInfo = null;

        DatabaseManager databaseManager = DatabaseManager.sharedManager(replaceDBFile);
        IEnumerable<users> arrUserInfo = databaseManager.databaseDocument.Table<users>();

        bool isUserFound = false;

        foreach (users u in arrUserInfo)
        {

            userInfo = DBUserInfo.Create(u.uid);

            selectedTankIndex = userInfo.active_screenid - 1;

            userInfo.last_visited = current_time;
            userInfo.UpdateDatabase(false);

            isUserFound = true;

            break;
        }

        if(isUserFound==false)
        {
            Debug.Log("user not found");

            DBUserInfo newuser = DBUserInfo.Create();
            newuser.UDID = "deviceUDID";
            newuser.coins = 1800;//1800
            newuser.bucks = 24;//24
            newuser.experience = 0;//59900
            newuser.active_screenid = 1;
            newuser.level = 1;//10
            newuser.last_visited = AppDelegate.GetTime();
            newuser.exp_current = 0;//59900
            newuser.exp_remain = 47;//94900-59900
            newuser.uid = newuser.InsertIntoDatabase();

            userInfo = DBUserInfo.Create(newuser.uid);

            bool isDefaultObject = true;

            if (isDefaultObject == true)
                AddDefaultObjects();
        }
        if(PlayerPrefs.GetInt(Constants.SelectedProdcutId) == 0)
        {
            PlayerPrefs.SetInt(Constants.SelectedProdcutId, startProductId);
        }
        //userInfo.ShowDBUserInfo();
    }

    void AddDefaultObjects()
    {
        /*ProductData.CreateProductInDatabase(startProductId, false);
        PlayerPrefs.SetInt(Constants.SelectedProdcutId, startProductId);*/
    }

    #endregion
    #region WeeklyQuest

    public void LoadWeeklyQuest()
    {
        //DailyTaskData.RestoreDailyTaskData();
    }

    #endregion

    #region DailyGift

    public void LoadDailyGift()
    {
        //DailyGiftData.RestoreDailyGiftData();
    }

    #endregion

    # region Cureent PlaneName

    public static void SetCurrentPlane(int planeNo)
    {
        PlayerPrefs.SetInt(Constants.CurrentPlane, planeNo);
    }

    public static int GetCurrentPlane()
    {
        if(!PlayerPrefs.HasKey(Constants.CurrentPlane))
        {
            PlayerPrefs.SetInt(Constants.CurrentPlane, 1);
            ProductPlane plane = ProductPlane.GetPlaneProduct(1);
            if(plane.myPlane.plane_purchased == 0)
            {
                plane.myPlane.plane_purchased = 1;
                plane.myPlane.UpdateDatabase();
                ProductPlane.SetNotBuyToBuyProduct(plane.myPlane.my_plane_id);
            }
        }

        return PlayerPrefs.GetInt(Constants.CurrentPlane, 0);
    }

    #endregion
}
