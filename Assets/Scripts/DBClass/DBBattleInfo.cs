using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using System.Linq;

public class battle_info
{
    [PrimaryKey, AutoIncrement]
    public int battle_id { get; set; }
    public int map_id { get; set; }
    public string battle_name { get; set; }
    public string battle_location { get; set; }
    public string fighter_info { get; set; }
    public string colling_time { get; set; }
    public string auto_reward_value { get; set; }
    public string reward_value { get; set; }
    public string reward_product { get; set; }
    public string loot_value { get; set; }
    public int missile_speed_plane_Lvel { get; set; }
    public string battle_Objectives { get; set; }
    public string equipment_available { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public DBBattleInfo GetBattleInfo()
    {
        DBBattleInfo battleInfo = new DBBattleInfo();
        GetBattleInfo(battleInfo);
        return battleInfo;
    }

    public void GetBattleInfo(DBBattleInfo battleInfo)
    {
        battleInfo.battle_id = this.battle_id;
        battleInfo.map_id = this.map_id;
        battleInfo.battle_name = this.battle_name;
        battleInfo.battle_location = this.battle_location;
        battleInfo.fighter_info = this.fighter_info;
        battleInfo.colling_time = this.colling_time;
        battleInfo.auto_reward_value = this.auto_reward_value;
        battleInfo.reward_value = this.reward_value;
        battleInfo.reward_product = this.reward_product;
        battleInfo.loot_value = this.loot_value;
        battleInfo.missile_speed_plane_Lvel = this.missile_speed_plane_Lvel;
        battleInfo.battle_Objectives = this.battle_Objectives;
        battleInfo.equipment_available = this.equipment_available;
        battleInfo.bgNo = this.c1;
        battleInfo.c2 = this.c2;
        battleInfo.c3 = this.c3;
        battleInfo.c4 = this.c4;
        battleInfo.c5 = this.c5;
        battleInfo.c6 = this.c6;
        battleInfo.c7 = this.c7;
        battleInfo.c8 = this.c8;
        battleInfo.c9 = this.c9;
        Debug.Log("battle Objectives --> "+ battleInfo.battle_Objectives + "this -> "+ this.battle_Objectives);

    }
}


public class DBBattleInfo
{
    public int battle_id { get; set; }
    public int map_id { get; set; }
    public string battle_name { get; set; }
    public string battle_location { get; set; }
    public string fighter_info { get; set; }
    public string colling_time { get; set; }
    public string auto_reward_value { get; set; }
    public string reward_value { get; set; }
    public string reward_product { get; set; }
    public string loot_value { get; set; }
    public int missile_speed_plane_Lvel { get; set; }
    public string battle_Objectives { get; set; }
    public string equipment_available { get; set; }
    public string bgNo { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    private static List<DBBattleInfo> allBattleInfo = new List<DBBattleInfo>();

    public static DBBattleInfo Create()
    {
        DBBattleInfo ret = new DBBattleInfo();
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
        SetDefaultValue();
        return true;
    }

    public static DBBattleInfo Create(int primarykey)
    {
        DBBattleInfo ret = new DBBattleInfo();
        if (ret != null && ret.Init(primarykey))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }
    public bool Init(int primarykey)
    {
        DatabaseManager.sharedManager().databaseBinary.Table<battle_info>().Where(t => t.battle_id == primarykey);
        IEnumerable<battle_info> ieumAllBattleInfo = DatabaseManager.sharedManager().databaseDocument.Table<battle_info>().Where(t => t.battle_id == primarykey);
        foreach (var t in ieumAllBattleInfo)
        {
            battle_info _battleInfo = t;
            _battleInfo.GetBattleInfo(this);
        }
        return true;
    }


    private void SetDefaultValue()
    {
        battle_id = 0;
        map_id = 0;
        battle_name = "0";
        battle_location = "0";
        fighter_info = "0";
        colling_time = "0";
        auto_reward_value = "0";
        reward_value = "0";
        reward_product = "0";
        missile_speed_plane_Lvel = 0;
        battle_Objectives = "0";
        equipment_available = "0";
        bgNo = "0";
        c2 = "0";
        c3 = "0";
        c4 = "0";
        c5 = "0";
        c6 = "0";
        c7 = "0";
        c8 = "0";
        c9 = "0";
    }

    public void printValue()
    {
        Debug.Log("Battle Id -> "+ this.battle_id);
        Debug.Log("map_id -> " + this.map_id);
        Debug.Log("battle_name -> " + this.battle_name);
        Debug.Log("battle_location -> " + this.battle_location);
        Debug.Log("auto_reward_value -> " + this.auto_reward_value);
        Debug.Log("reward_product -> " + this.reward_product);
        Debug.Log("missile_speed_plane_Lvel-> " + this.missile_speed_plane_Lvel);
        Debug.Log("battle_Objectives -> " + this.battle_Objectives);
    }

    public static DBBattleInfo GetBattleInfo(int _productId)
    {
        if (allBattleInfo.Count == 0)
            allBattleInfo = GetAllBattleInfo();

        for (int i = 0; i < allBattleInfo.Count; i++)
        {
            DBBattleInfo productInfo = allBattleInfo[i];
            if (productInfo.battle_id == _productId)
            {
                return productInfo;
            }
        }

        return null;// DBPlaneInfo.GetPlaneInfo(AppDelegate.sharedManager().startProductId);
    }

    public static List<DBBattleInfo> GetAllBattleInfo()
    {
        if(allBattleInfo.Count==0)
        {
            IEnumerable<battle_info> ieumAllBattleInfo = (DatabaseManager.sharedManager().databaseBinary.Table<battle_info>()).OrderBy(t => t.battle_id);
            foreach (var battle_info in ieumAllBattleInfo)
            {
                DBBattleInfo battleInfo = battle_info.GetBattleInfo();
                //battleInfo.printValue();
                allBattleInfo.Add(battleInfo);
            }
        }
        return allBattleInfo;
    }
    public static List<DBBattleInfo> GetAllBattleInfo(int _mapId)
    {
        List<DBBattleInfo> allBattleForMap = new List<DBBattleInfo>();
        if(allBattleInfo.Count==0)
        {
            allBattleInfo = GetAllBattleInfo();
        }

        foreach (var item in allBattleInfo)
        {
            if(item.map_id==_mapId)
            {
                allBattleForMap.Add(item);
            }
        }

        return allBattleForMap;
    }

    public static DBBattleInfo GetBattleFromBattleId(int _battle_id)
    {
        if(allBattleInfo.Count==0)
        {
            allBattleInfo = GetAllBattleInfo();
        }
        for (int i = 0; i < allBattleInfo.Count; i++)
        {
            DBBattleInfo battleInfo = allBattleInfo[i];
            if(battleInfo.battle_id==_battle_id)
            {
                return battleInfo;
            }

        }
        return DBBattleInfo.Create();
    }
}