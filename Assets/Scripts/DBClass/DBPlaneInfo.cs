using System;
using SQLite4Unity3d;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class plane_info
{
    [PrimaryKey, AutoIncrement]
    public int plane_id { get; set; }
    public string plane_name { get; set; }
    public String plane_price { get; set; }
    public int plane_speed { get; set; }
    public int plane_turn_speed { get; set; }
    public int gun { get; set; }
    public int emp { get; set; }
    public int plane_level { get; set; }
    public int levelup_price { get; set; }
    public string description { get; set; }
    public int level { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public DBPlaneInfo GetDBPlaneInfo()
    {
        DBPlaneInfo productInfo = new DBPlaneInfo();
        GetDBProductInfo(productInfo);

        return productInfo;
    }

    public void GetDBProductInfo(DBPlaneInfo productInfo)
    {
        productInfo.plane_id = this.plane_id;
        productInfo.plane_name = this.plane_name;
        productInfo.plane_price = this.plane_price;
        productInfo.plane_speed = this.plane_speed;
        productInfo.plane_turn_speed = this.plane_turn_speed;
        productInfo.gun = this.gun;
        productInfo.emp = this.emp;
        productInfo.levelup_price = this.levelup_price;
        productInfo.description = this.description;
        productInfo.level = this.level;
        productInfo.c1 = this.c1;
        productInfo.c2 = this.c2;
        productInfo.c3 = this.c3;
        productInfo.c4 = this.c4;
        productInfo.c5 = this.c5;
        productInfo.c6 = this.c6;
        productInfo.c7 = this.c7;
        productInfo.c8 = this.c8;
        productInfo.c9 = this.c9;
    }
}

public class DBPlaneInfo
{
    [PrimaryKey, AutoIncrement]
    public int plane_id { get; set; }
    public string plane_name { get; set; }
    public string plane_price { get; set; }
    public int plane_speed { get; set; }
    public int plane_turn_speed { get; set; }
    public int gun { get; set; }
    public int emp { get; set; }
    public int plane_level { get; set; }
    public int levelup_price { get; set; }
    public string description { get; set; }
    public int level { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    private static List<DBPlaneInfo> allPlaneInfo = new List<DBPlaneInfo>();
    private static List<DBPlaneInfo> allNotBuyProduct = new List<DBPlaneInfo>();

    public static DBPlaneInfo Create()
    {
        DBPlaneInfo ret = new DBPlaneInfo();
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

    public static DBPlaneInfo Create(int primaryKey)
    {
        DBPlaneInfo ret = new DBPlaneInfo();
        if (ret != null && ret.Init(primaryKey))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    public bool Init(int primaryKey)
    {



        return true;
    }

    private void SetDefaultValue()
    {
        plane_id = 0;
        plane_name = "0";
        plane_price = "0";
        plane_speed = 0;
        plane_turn_speed = 0;
        gun = 0;
        emp = 0;
        levelup_price = 0;
        description = "0";
        level = 0;
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

    public void showProductInfo()
    {
        /*Log.L("-------------------------");
        Log.L("productid " + productid);
        Log.L("catergoryid " + catergoryid);
        Log.L("product_name " + product_name);
        Log.L("level " + level);
        Log.L("elementid " + elementid);
        Log.L("rarity " + rarity);
        Log.L("star " + star);
        Log.L("tier " + tier);
        Log.L("pieces " + pieces);
        Log.L("buy_price " + buy_price);
        Log.L("sell_price " + sell_price);
        Log.L("levelup_price " + levelup_price);
        Log.L("sound " + sound);
        Log.L("description " + description);
        Log.L("adult_time " + adult_time);
        Log.L("fusionCreation " + fusionCreation);
        Log.L("collect_coins_amount " + collect_coins_amount);
        Log.L("collect_coins_time " + collect_coins_time);
        Log.L("c5 " + c5);
        Log.L("c6 " + c6);
        Log.L("c7 " + c7);
        Log.L("c8 " + c8);
        Log.L("c9 " + c9);*/
    }
/*
    public override string ToString()
    {
        //  return string.Format("[product_info: productid={0}, catergoryid={1},  product_name ={2}, level={3},elementid={4},rarity={5},star = {6}, ]", ,);

        return string.Format("[plane_info :plane_id={0}," +
        "categoryid={1},product_name={2},level ={3},elementid={4},rarity={5}," +
        "star={6},tier={7},pieces={8},buy_price={9},sell_price={10}," +
        "levelup_price={11},sound={12},description={13}," +
        "c1={14},c2={15},c3={16},c4={17},c5={18},c6={19},c7={20},c8={21},c9={22}]", productid,
        catergoryid, product_name, level, elementid, rarity, star, tier
        , pieces, buy_price, sell_price, levelup_price, sound, description,
        adult_time, fusionCreation, collect_coins_amount, collect_coins_time, c5, c6, c7, c8, c9
        );
    }
    */
    public static List<DBPlaneInfo> GetAllPlaneInfo()
    {
        if (allPlaneInfo.Count == 0)
        {
            //IEnumerable<plane_info> ieumAllPlaneInfo = (DatabaseManager.sharedManager().databaseBinary.Table<plane_info>()).Where(t => t.plane_id < 18).OrderBy(t => t.plane_id);
            IEnumerable<plane_info> ieumAllPlaneInfo = DatabaseManager.sharedManager().databaseBinary.Table<plane_info>();
            foreach (var product_info in ieumAllPlaneInfo)
            {
                DBPlaneInfo productInfo = product_info.GetDBPlaneInfo();
                allPlaneInfo.Add(productInfo);
            }
        }

        //Debug.Log(allPlaneInfo.Count);
        return allPlaneInfo;
    }

    public static DBPlaneInfo GetPlaneInfo(int _productId)
    {
        if (allPlaneInfo.Count == 0)
            allPlaneInfo = GetAllPlaneInfo();

        for (int i = 0; i < allPlaneInfo.Count; i++)
        {
            DBPlaneInfo productInfo = allPlaneInfo[i];
            if (productInfo.plane_id == _productId)
            {
                return productInfo;
            }
        }

        return DBPlaneInfo.GetPlaneInfo(AppDelegate.sharedManager().startProductId);
    }

}
