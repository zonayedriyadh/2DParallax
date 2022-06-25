using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using System.Linq;

public class my_map
{
    [PrimaryKey, AutoIncrement]
    public int my_mapid { get; set; }
    public int map_id { get; set; }
    public int is_unlock { get; set; }
    public int is_claim { get; set; }
    public int number_of_play { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    internal static my_map Create(DBMyMap myMap)
    {
        my_map ret = new my_map();
        if(ret!=null && ret.Init(myMap))
        {
            return ret;
        }
        return null;
    }

    private bool Init(DBMyMap myMap)
    {
        this.my_mapid = myMap.my_mapid;
        this.map_id = myMap.map_id;
        this.is_unlock = myMap.is_unlock;
        this.is_claim = myMap.is_claim;
        this.number_of_play = myMap.number_of_play;
        this.c1 = myMap.c1;
        this.c2 = myMap.c2;
        this.c3 = myMap.c3;
        this.c4 = myMap.c4;
        this.c5 = myMap.c5;
        this.c6 = myMap.c6;
        this.c7 = myMap.c7;
        this.c8 = myMap.c8;
        this.c9 = myMap.c9;

        return true;
    }
    public void GetMyMap(DBMyMap myMap)
    {
        myMap.my_mapid = this.my_mapid;
        myMap.map_id = this.map_id;
        myMap.is_unlock=  this.is_unlock;
        myMap.is_claim = this.is_claim;
        myMap.number_of_play = this.number_of_play ;
        myMap.c1 = this.c1;
        myMap.c2 = this.c2;
        myMap.c3 = this.c3;
        myMap.c4 = this.c4;
        myMap.c5 = this.c5;
        myMap.c6 = this.c6;
        myMap.c7 = this.c7;
        myMap.c8 = this.c8;
        myMap.c9 = this.c9;
    }

    public DBMyMap GetDBMyMapInfo()
    {
        DBMyMap productInfo = new DBMyMap();
        GetMyMap(productInfo);

        return productInfo;
    }
}
public class DBMyMap
{
    public int my_mapid { get; set; }
    public int map_id { get; set; }
    public int is_unlock { get; set; }
    public int is_claim { get; set; }
    public int number_of_play { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public static DBMyMap Create()
    {
        DBMyMap ret = new DBMyMap();
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
    public static DBMyMap Create(int primarykey, bool isMyMapId)
    {
        DBMyMap ret = new DBMyMap();
        if (ret != null && ret.Init(primarykey, isMyMapId))
        {
            return ret;
        }
        return null;
    }

    private bool Init(int primarykey, bool isMyMapId)
    {
        if (isMyMapId)
            DatabaseManager.sharedManager().databaseDocument.Table<my_map>().Where(t => t.my_mapid == primarykey);
        else
            DatabaseManager.sharedManager().databaseDocument.Table<my_map>().Where(t => t.map_id == primarykey);
        return true;
    }

    private static List<DBMyMap> allMymap = new List<DBMyMap>();

    public static List<DBMyMap> GetAllMyMapInfo()
    {
        //Debug.Log("in plane info --> " );
        if (allMymap.Count == 0)
        {
            // Debug.Log("in plane info --> ");
            //IEnumerable<plane_info> ieumAllPlaneInfo = (DatabaseManager.sharedManager().databaseBinary.Table<plane_info>()).Where(t => t.plane_id < 18).OrderBy(t => t.plane_id);
            IEnumerable<my_map> ieumAllBattleInfo = DatabaseManager.sharedManager().databaseDocument.Table<my_map>();

            foreach (var product_info in ieumAllBattleInfo)
            {
                DBMyMap productInfo = product_info.GetDBMyMapInfo();
                // Debug.Log("my plane --> "+ productInfo.plane_name);
                allMymap.Add(productInfo);
            }
        }

        return allMymap;
    }

    public static DBMyMap GetMyMap(int map_id)
    {
        if (allMymap.Count == 0)
            GetAllMyMapInfo();

        foreach (DBMyMap myBattle in allMymap)
        {
            if (myBattle.my_mapid == map_id)
                return myBattle;
        }
        Debug.Log("battle id " + map_id);
        return null;
    }

    public void InsertIntoDatabase()
    {
        my_map _my_map = my_map.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Insert(_my_map);
    }
    public void UpdateDatabase()
    {
        my_map _my_map = my_map.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Update(_my_map);
    }
    public void DeleteDatabase(int primarykey)
    {
        DatabaseManager.sharedManager().databaseDocument.Delete(primarykey);
    }

    public static DBMyMap GetNextUnlockMyMap()
    {
        DBMyMap myMap = null;
        List<my_map> sqlfStr = DatabaseManager.sharedManager().databaseDocument.Table<my_map>().ToList();
        foreach (my_map item in sqlfStr)
        {
            if(item.is_unlock==0)
            {
                int mapId = item.map_id; //int mapId=sqlite3_column_int(iproduct_statement,0);
                myMap = DBMyMap.Create(mapId, true);
            }
        }
        if(myMap == null)
        {
            myMap = DBMyMap.Create(sqlfStr.Count-1, true);

        }
        return myMap;
    }

}
