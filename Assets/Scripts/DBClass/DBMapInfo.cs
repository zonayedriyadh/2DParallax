using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;

public class map_info
{
    [PrimaryKey,AutoIncrement]
    public int map_id { get; set; }
    public string map_name { get; set; }
    public int category { get; set; }
    public string probability { get; set; }
    public string reward_value { get; set; }
    public string time_revive { get; set; }
    public string reward_product { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }
    public DBMapInfo GetMapInfo()
    {
        DBMapInfo mapInfo= new DBMapInfo();
        GetMapInfo(mapInfo);
        return mapInfo;
    }

    public void GetMapInfo(DBMapInfo mapInfo)
    {
        mapInfo.map_id = this.map_id;
        mapInfo.map_name = this.map_name;
        mapInfo.category = this.category;
        mapInfo.probability = this.probability;
        mapInfo.reward_value = this.reward_value;
        mapInfo.time_revive = this.time_revive;
        mapInfo.reward_product = this.reward_product;
        mapInfo.c1 = this.c1;
        mapInfo.c2 = this.c2;
        mapInfo.c3 = this.c3;
        mapInfo.c4 = this.c4;
        mapInfo.c5 = this.c5;
        mapInfo.c6 = this.c6;
        mapInfo.c7 = this.c7;
        mapInfo.c8 = this.c8;
        mapInfo.c9 = this.c9;
    }
}

public class DBMapInfo
{
    public int map_id { get; set; }
    public string map_name { get; set; }
    public int category { get; set; }
    public string probability { get; set; }
    public string reward_value { get; set; }
    public string time_revive { get; set; }
    public string reward_product { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    private static List<DBMapInfo> allMapInfo = new List<DBMapInfo>();

    public static DBMapInfo Create()
    {
        DBMapInfo ret = new DBMapInfo();
        if(ret!=null && ret.Init())
        {
            return ret;
        }
        return null;
    }

    private bool Init()
    {
        SetDefaultValue();
        return true;
    }
    public static DBMapInfo Create(int primarykey)
    {
        DBMapInfo ret = new DBMapInfo();
        if (ret != null && ret.Init(primarykey))
        {
            return ret;
        }
        return null;
    }

    private bool Init(int primarykey)
    {
        DatabaseManager.sharedManager().databaseBinary.Table<map_info>().Where(t => t.map_id == primarykey);
        IEnumerable<map_info> ieumAllMapInfo = DatabaseManager.sharedManager().databaseDocument.Table<map_info>().Where(t => t.map_id == primarykey);
        foreach (var t in ieumAllMapInfo)
        {
            map_info _map_info = t;
            _map_info.GetMapInfo(this);
        }
        return true;
    }

    private void SetDefaultValue()
    {
        map_id = 0;
        map_name = "0";
        category = 0;
        probability = "0";
        time_revive = "0";
        reward_value = "0";
        reward_product = "0";
        c1 = "0";
        c2 = "0";
        c3 = "0";
        c4 = "0";
        c5 = "0";
        c6 = "0";
        c7 = "0";
        c8 = "0";
        c9 = "0";
    }

    public static List<DBMapInfo> GetAllMapInfo()
    {
        IEnumerable<map_info> ieumAllMapInfo = (DatabaseManager.sharedManager().databaseBinary.Table<map_info>()).OrderBy(t => t.map_id);
        foreach (var item in ieumAllMapInfo)
        {
            DBMapInfo mapInfo = item.GetMapInfo();
            allMapInfo.Add(mapInfo);
        }
        return allMapInfo;
    }

    public static List<DBMapInfo> GetAllMapInfo(int _battleType)
    {
        List<DBMapInfo> allMapForBattleType = new List<DBMapInfo>();
        if(allMapInfo.Count==0)
        {
            allMapInfo = GetAllMapInfo();
        }

        foreach (var item in allMapInfo)
        {
            if(item.category==_battleType)
            {
                allMapForBattleType.Add(item);
            }
        }
        return allMapForBattleType;
    }
    public static DBMapInfo GetMapInfo(int _map_id)
    {
        if(allMapInfo.Count==0)
        {
            allMapInfo = GetAllMapInfo();
        }
        for (int i = 0; i < allMapInfo.Count; i++)
        {
            DBMapInfo mapInfo = allMapInfo[i];
            if(mapInfo.map_id==_map_id)
            {
                return mapInfo;
            }
        }
        return DBMapInfo.Create(_map_id);
    }
}
