using System;
using SQLite4Unity3d;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class my_plane
{
    [PrimaryKey, AutoIncrement]
    public int my_plane_id { get; set; }
    public int plane_id { get; set; }
    public string plane_name { get; set; }
    public int plane_purchased { get; set; }
    public int time_stamp { get; set; }
    public int my_level { get; set; }
    public string cost { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public static my_plane Create(DBMyPLane myProduct)
    {
        my_plane ret = new my_plane();
        if (ret != null && ret.Init(myProduct))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    private bool Init(DBMyPLane myProduct)
    {
        
        this.my_plane_id = myProduct.my_plane_id;
        this.plane_id = myProduct.plane_id;
        this.plane_name = myProduct.plane_name;
        this.plane_purchased = myProduct.plane_purchased;
        this.time_stamp = myProduct.time_stamp;
        this.my_level = myProduct.my_level;
        this.cost = myProduct.cost;
        this.c1 = myProduct.c1;
        this.c2 = myProduct.c2;
        this.c3 = myProduct.c3;
        this.c4 = myProduct.c4;
        this.c5 = myProduct.c5;
        this.c6 = myProduct.c6;
        this.c7 = myProduct.c7;
        this.c8 = myProduct.c8;
        this.c9 = myProduct.c9;

        //Debug.Log("my plane id --> " + this.my_plane_id);
        return true;
    }
    public DBMyPLane GetDBMyPlaneInfo()
    {
        DBMyPLane productInfo = new DBMyPLane();
        GetMyProduct(productInfo);

        return productInfo;
    }
    public void GetMyProduct(DBMyPLane myProduct)
    {
        myProduct.my_plane_id = this.my_plane_id;
        myProduct.plane_id = this.plane_id;
        myProduct.plane_name = this.plane_name;
        myProduct.plane_purchased = this.plane_purchased;
        myProduct.time_stamp = this.time_stamp;
        myProduct.my_level = this.my_level;
        myProduct.cost = this.cost;
        myProduct.c1 = this.c1;
        myProduct.c2 = this.c2;
        myProduct.c3 = this.c3;
        myProduct.c4 = this.c4;
        myProduct.c5 = this.c5;
        myProduct.c6 = this.c6;
        myProduct.c7 = this.c7;
        myProduct.c8 = this.c8;
        myProduct.c9 = this.c9;
    }
}





public class DBMyPLane
{
    public int my_plane_id { get; set; }
    public int plane_id { get; set; }
    public string plane_name { get; set; }
    public int plane_purchased { get; set; }
    public int time_stamp { get; set; }
    public int my_level { get; set; }
    public string cost { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public static DBMyPLane Create()
    {
        DBMyPLane ret = new DBMyPLane();
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
        SetDefaultValue();
        return true;
    }

    public static DBMyPLane Create(int primaryKey)
    {
        DBMyPLane ret = new DBMyPLane();
        if (ret != null && ret.Init(primaryKey))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    private bool Init(int primaryKey)
    {
        IEnumerable<my_plane> ieumAllMyProduct = DatabaseManager.sharedManager().databaseDocument.Table<my_plane>().Where(t => t.my_plane_id == primaryKey);

        foreach (var t in ieumAllMyProduct)
        {
            my_plane _my_Plane = t;
            _my_Plane.GetMyProduct(this);
            return true;
        }

        return false;
    }

    private void SetDefaultValue()
    {
        my_plane_id = 0;
        plane_id = 0;
        plane_name = "0";
        plane_purchased = 0;
        time_stamp = 0;
        my_level = 0;
        cost = "0";
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
    private static List<DBMyPLane> allMyPlaneInfo = new List<DBMyPLane>();

    

    public static List<DBMyPLane> GetAllMyPlaneInfo()
    {
        //Debug.Log("in plane info --> " );
        if (allMyPlaneInfo.Count == 0)
        {
           // Debug.Log("in plane info --> ");
            //IEnumerable<plane_info> ieumAllPlaneInfo = (DatabaseManager.sharedManager().databaseBinary.Table<plane_info>()).Where(t => t.plane_id < 18).OrderBy(t => t.plane_id);
            IEnumerable<my_plane> ieumAllPlaneInfo = DatabaseManager.sharedManager().databaseDocument.Table<my_plane>();
            
            foreach (var product_info in ieumAllPlaneInfo)
            {
                DBMyPLane productInfo = product_info.GetDBMyPlaneInfo();
               // Debug.Log("my plane --> "+ productInfo.plane_name);
                allMyPlaneInfo.Add(productInfo);
            }
        }

        return allMyPlaneInfo;
    }

    public static DBMyPLane GetMyPlane(int plane_id)
    {
        if (allMyPlaneInfo.Count == 0)
            GetAllMyPlaneInfo();

        foreach(DBMyPLane myplane in allMyPlaneInfo)
        {
            if (myplane.my_plane_id == plane_id)
                return myplane;
        }

        return null;
    }
    public void showDBMyProduct()
    {
        /*Log.L("-------------------------");
        Log.L("my_productid " + my_plane_id);
        Log.L("productid " + product_id);
        Log.L("screenid " + screenid);
        Log.L("my_name " + plane_name);
        Log.L("time_stamp " + time_stamp);
        Log.L("my_level " + my_level);
        Log.L("my_tier " + my_tier);
        Log.L("my_pieces " + my_pieces);
        Log.L("cost " + cost);
        Log.L("slot_info " + slot_info);
        Log.L("c2 " + c2);
        Log.L("c3 " + c3);
        Log.L("c4 " + c4);
        Log.L("c5 " + c5);
        Log.L("c6 " + c6);
        Log.L("c7 " + c7);
        Log.L("c8 " + c8);
        Log.L("c9 " + c9);*/
    }

    public void InsertIntoDatabase()
    {
        //Debug.Log("my plae id --> " + this.my_plane_id);
        my_plane _my_Plane = my_plane.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Insert(_my_Plane);

        this.my_plane_id = _my_Plane.my_plane_id;
    }

    public void UpdateDatabase()
    {
        my_plane _my_Product = my_plane.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Update(_my_Product);
    }

    public static void DeleteDatabase(int primaryKey)
    {
        DatabaseManager.sharedManager().databaseDocument.Delete<my_plane>(primaryKey);
    }

    public static void DeleteAllDatabase()
    {
        DatabaseManager.sharedManager().databaseDocument.DeleteAll<my_plane>();
    }

    #region Update Price

    static string[] updatePrice =
     {
        "2,0;1,0",
        "2,20;1,5",
        "2,25;1,7",
        "2,30;1,10",
        "2,35;1,12",
        "2,40;1,15",
        "2,45;1,16",
        "2,50;1,18",
        "2,55;1,18",
        "2,60;1,20",
        "2,72;1,20",
        "2,122;1,25",
        "2,137;1,25",
        "2,151;1,30",
        "2,166;1,30",
        "2,180;1,35",
        "2,259;1,35",
        "2,278;1,35",
        "2,298;1,40",
        "2,317;1,40",
        "2,336;1,45",
        "2,576;1,45",
        "2,648;1,50",
        "2,720;1,50",
        "2,792;1,50",
        "2,864;1,55",
        "2,1248;1,55",
        "2,1344;1,55",
        "2,1440;1,60",
        "2,1536;1,60",
        "2,1632;1,60",
        "2,2160;1,60",
        "2,2280;1,65",
        "2,2400;1,65",
        "2,2520;1,65",
        "2,2640;1,70",
        "2,3312;1,70",
        "2,3456;1,70",
        "2,3600;1,75",
        "2,3744;1,75",
        "2,3888;1,80",
        "2,4704;1,100",
        "2,4872;1,100",
        "2,5040;1,100",
        "2,5208;1,100",
        "2,5376;1,100",
        "2,6336;1,100",
        "2,6528;1,100",
        "2,6720;1,100",
        "2,6912;1,100",
        "2,7296;1,100",
        "2,8640;1,100",
        "2,9072;1,100",
        "2,10560;1,100",
        "2,11040;1,100",
        "2,11520;1,100",
        "2,12000;1,100",
        "2,12480;1,100",
        "2,14256;1,100",
        "2,14784;1,100",
        "2,15312;1,100",
        "2,16104;1,100",
        "2,16896;1,100",
        "2,19296;1,100",
        "2,20160;1,100",
        "2,21024;1,100",
        "2,23712;1,100",
        "2,24648;1,100",
        "2,27552;1,100",
        "2,28560;1,100",
        "2,29568;1,100",
        "2,30912;1,100",
        "2,32256;1,100",
        "2,36000;1,100",
        "2,37440;1,100",
        "2,38880;1,100",
        "2,40320;1,100",
        "2,41760;1,100",
        "2,46080;1,100",
        "2,47616;1,100",
        "2,49152;1,100",
        "2,54264;1,100",
        "2,56304;1,100",
        "2,61776;1,100",
        "2,63936;1,100",
        "2,66096;1,100",
        "2,72048;1,100",
        "2,74328;1,100",
        "2,80640;1,100",
        "2,83040;1,100",
        "2,85440;1,100",
        "2,92736;1,100",
        "2,95760;1,100",
        "2,104000;1,100",
        "2,107000;1,100",
        "2,110000;1,100",
        "2,119000;1,100",
        "2,122000;1,100",
        "2,131000;1,100",
        "2,134000;1,100"
    };

    public string getUpdatePrice()
    {
        //AppDelegate* appDelegate = AppDelegate::sharedApplication();
        string strUpdataePrice = "2,134000;1,186000";
        int levelCount = updatePrice.Length;
        if (my_level >= 0 && my_level < levelCount)
            strUpdataePrice = updatePrice[my_level];

        /*if (appDelegate->isOnlyBucksEnable == true)
        {
            strUpdataePrice = AppDelegate::getBucksOnlyPrice(strUpdataePrice, 0);
        }*/
        return strUpdataePrice;
    }
    #endregion
}
