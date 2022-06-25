using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using System.Linq;

public class my_battle
{
    [PrimaryKey, AutoIncrement]
    public int my_battleid { get; set; }
    public int battleid { get; set; }
    public int is_unlock { get; set; }
    public int is_claim { get; set; }
    public int last_time_collect { get; set; }
    public int number_of_play { get; set; }
    public int number_of_win { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    internal static my_battle Create(DBMyBattle myBattle)
    {
        my_battle ret = new my_battle();
        if (ret != null && ret.Init(myBattle))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    private bool Init(DBMyBattle my_battle)
    {
        
        this.my_battleid = my_battle.my_battleid;
        this.battleid = my_battle.battleid;
        this.is_unlock = my_battle.is_unlock;
        this.is_claim = my_battle.is_claim;
        this.last_time_collect = my_battle.last_time_collect;
        this.number_of_play = my_battle.number_of_play;
        this.number_of_win = my_battle.number_of_win;
        this.c1 = my_battle.c1;
        this.c2 = my_battle.c2;
        this.c3 = my_battle.c3;
        this.c4 = my_battle.c4;
        this.c5 = my_battle.c5;
        this.c6 = my_battle.c6;
        this.c7 = my_battle.c7;
        this.c8 = my_battle.c8;
        this.c9 = my_battle.c9;
        //Debug.Log("my_battleid ->" + my_battle.my_battleid + "battle_id->" + my_battle.battleid);
        return true;
    }

    public void GetMyBattle(DBMyBattle myBattle)
    {
        myBattle.my_battleid = this.my_battleid;
        myBattle.battleid = this.battleid;
        myBattle.is_unlock = this.is_unlock;
        myBattle.is_claim = this.is_claim;
        myBattle.last_time_collect = this.last_time_collect;
        myBattle.number_of_play = this.number_of_play;
        myBattle.number_of_win = this.number_of_win;
        myBattle.c1 = this.c1;
        myBattle.c2 = this.c2;
        myBattle.c3 = this.c3;
        myBattle.c4 = this.c4;
        myBattle.c5 = this.c5;
        myBattle.c6 = this.c6;
        myBattle.c7 = this.c7;
        myBattle.c8 = this.c8;
        myBattle.c9 = this.c9;
    }

    public DBMyBattle GetDBMyBattleInfo()
    {
        DBMyBattle productInfo = new DBMyBattle();
        GetMyBattle(productInfo);

        return productInfo;
    }
}

public class DBMyBattle
{
    public int my_battleid { get; set; }
    public int battleid { get; set; }
    public int is_unlock { get; set; }
    public int is_claim { get; set; }
    public int last_time_collect { get; set; }
    public int number_of_play { get; set; }
    public int number_of_win { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public static DBMyBattle Create()
    {
        DBMyBattle ret = new DBMyBattle();
        if (ret != null && ret.Init())
        {
            return ret;
        }
        return null;
    }

    private bool Init()
    {
        return true;
    }
    public static DBMyBattle Create(int primarykey, bool isMyBattleId)
    {
        DBMyBattle ret = new DBMyBattle();
        if (ret != null && ret.Init(primarykey, isMyBattleId))
        {
            return ret;
        }
        return null;
    }
    private static List<DBMyBattle> allMyBattleInfo = new List<DBMyBattle>();

    public static List<DBMyBattle> GetAllMyBattleInfo()
    {
        //Debug.Log("in plane info --> " );
        if (allMyBattleInfo.Count == 0)
        {
            // Debug.Log("in plane info --> ");
            //IEnumerable<plane_info> ieumAllPlaneInfo = (DatabaseManager.sharedManager().databaseBinary.Table<plane_info>()).Where(t => t.plane_id < 18).OrderBy(t => t.plane_id);
            IEnumerable<my_battle> ieumAllBattleInfo = DatabaseManager.sharedManager().databaseDocument.Table<my_battle>();

            foreach (var product_info in ieumAllBattleInfo)
            {
                DBMyBattle productInfo = product_info.GetDBMyBattleInfo();
                // Debug.Log("my plane --> "+ productInfo.plane_name);
                allMyBattleInfo.Add(productInfo);
            }
        }

        return allMyBattleInfo;
    }
    private bool Init(int primarykey, bool isMyBattleId)
    {
        IEnumerable<my_battle> ieumAllMyBattle;

        if (isMyBattleId)
            ieumAllMyBattle = DatabaseManager.sharedManager().databaseDocument.Table<my_battle>().Where(t => t.my_battleid == primarykey);
        else
            ieumAllMyBattle = DatabaseManager.sharedManager().databaseDocument.Table<my_battle>().Where(t => t.battleid == primarykey);

        foreach (var t in ieumAllMyBattle)
        {
            my_battle _my_battle = t;
            _my_battle.GetMyBattle(this);
        }

        return true;
    }

    public void InsertIntoDatabase()
    {
        my_battle _my_battle = my_battle.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Insert(_my_battle);
        //this.my_battleid = _my_battle.my_battleid;
    }

    public static DBMyBattle GetMyBattle(int battle_id)
    {
        if (allMyBattleInfo.Count == 0)
            GetAllMyBattleInfo();

        foreach (DBMyBattle myBattle in allMyBattleInfo)
        {
            if (myBattle.my_battleid == battle_id)
                return myBattle;
        }
        Debug.Log("battle id "+battle_id);
        return null;
    }

    public void UpdateDatabase()
    {
        my_battle _my_battle = my_battle.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Update(_my_battle);
    }

    public void DeleteDatabase(int primarykey)
    {
        DatabaseManager.sharedManager().databaseDocument.Delete(primarykey);
    }

    public bool IsFirstTime()
    {
        bool isFirstTime = true;

        List<my_battle> str_user = DatabaseManager.sharedManager().databaseDocument.Table<my_battle>().ToList();
        foreach (my_battle item in str_user)
        {
            if(item.is_claim==1)
            {
                isFirstTime = false;
            }
        }

        return isFirstTime;
    }
}
