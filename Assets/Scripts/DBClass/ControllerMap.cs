using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMap
{
    public DBMapInfo mapInfo;
    public DBMyMap myMap;

    private static List<ControllerMap> allMapController = new List<ControllerMap>();
    //private static List<ControllerBattle> allNotBuyProduct = new List<ProductPlane>();
    #region Create ProductPlane
    public static ControllerMap Create()
    {
        ControllerMap ret = new ControllerMap();
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
        return true;
    }

    public static ControllerMap Create(int _myId, bool _isMyProductId)
    {
        ControllerMap ret = new ControllerMap();
        if (ret != null && ret.Init(_myId, _isMyProductId))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    public bool Init(int _myId, bool _isMyProductId)
    {
        if (_isMyProductId)
        {
            mapInfo = DBMapInfo.GetMapInfo(_myId);
            myMap = DBMyMap.GetMyMap(_myId);
            if (myMap == null)
                myMap = InsertIntoMyBattle(mapInfo);
            //SetUpdateInfo();
        }
        else
        {
            myMap = null;
            mapInfo = null;
        }

        //productObject = NULL;

        //SetSkillData();

        return true;
    }

    #endregion

    #region Create Database

    public static DBMyMap InsertIntoMyBattle(DBMapInfo _mapInfo)
    {

        DBMyMap myMap = DBMyMap.Create();

        //Random random = new Random();
        //Debug.Log("plane id --> "+ _planeInfo.plane_id);
        myMap.my_mapid = _mapInfo.map_id;
        myMap.map_id = _mapInfo.map_id;
        myMap.is_unlock = 0;
        myMap.is_claim = 0;
        myMap.number_of_play = 0;
        myMap.c1 = "0";
        myMap.c2 = "0";
        myMap.c3 = "0";
        myMap.c4 = "0";
        myMap.c5 = "0";
        myMap.c6 = "0";
        myMap.c7 = "0";
        myMap.c8 = "0";
        myMap.c9 = "0";


        //Debug.Log("my_battleid ->"+ myBattle.my_battleid+"battle_id->"+ myBattle.battleid);
        if (_mapInfo.map_id == 1)
        {
            myMap.is_unlock = 1;
        }
        myMap.InsertIntoDatabase();

        return myMap;
    }

    public static ControllerMap GetControllerBattle(int mapId)
    {
        if (allMapController.Count == 0)
            GetAllControllerMapinfo();

        foreach (ControllerMap map in allMapController)
        {

            if (map.myMap.map_id == mapId)
                return map;
        }

        return null;
    }

    public static List<ControllerMap> GetAllControllerMapinfo()
    {
        if (allMapController.Count == 0)
        {
            List<DBMapInfo> allBattleInfo = DBMapInfo.GetAllMapInfo();

            foreach (var mapInfo in allBattleInfo)
            {
                allMapController.Add(ControllerMap.Create(mapInfo.map_id, true));
            }
        }

        return allMapController;
    }

    public static ControllerMap GetControllerMapLastUnlock()
    {
        ControllerMap lastUnlockMap = null;
        if (allMapController.Count == 0)
            GetAllControllerMapinfo();

        foreach (var _ControllerMap in allMapController)
        {
            if (_ControllerMap.myMap.is_unlock == 1)
            {
                lastUnlockMap = _ControllerMap;
            }
            
        }
        return lastUnlockMap;
    }
    public static void UnlockMapId(int mapId)
    {
        ControllerMap Controller_map = GetControllerBattle( mapId);
        Controller_map.myMap.is_unlock = 1;
        Controller_map.myMap.UpdateDatabase();
    }

    public static void CompleteMapId(int mapId)
    {
        ControllerMap Controller_map = GetControllerBattle(mapId);
        Controller_map.myMap.is_claim = 1;
        Controller_map.myMap.UpdateDatabase();
    }
    /*public static List<ProductPlane> GetAllNotBuyProduct()
    {
        if (allNotBuyProduct.Count == 0)
        {
            List<ProductPlane> allProductInfo = ProductPlane.GetAllProductInfo();

            foreach (var productInfo in allProductInfo)
            {
                if (productInfo.myPlane.plane_purchased == 0)
                {
                    allNotBuyProduct.Add(productInfo);
                }
            }
        }

        return allNotBuyProduct;
    }*/
    /*
    public static void SetNotBuyToBuyProduct(int productId)
    {
        foreach (var planeProductInfo in allNotBuyProduct)
        {
            if (planeProductInfo.planeInfo.plane_id == productId)
            {
                allNotBuyProduct.Remove(planeProductInfo);
                break;
            }
        }
    }*/
    #endregion
}

