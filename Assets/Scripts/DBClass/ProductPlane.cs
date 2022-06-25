using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPlane 
{
    public DBPlaneInfo planeInfo;
    public DBMyPLane myPlane;

    private static List<ProductPlane> allProductPlane = new List<ProductPlane>();
    private static List<ProductPlane> allNotBuyProduct = new List<ProductPlane>();
    #region Create ProductPlane
    public static ProductPlane Create()
    {
        ProductPlane ret = new ProductPlane();
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

    public static ProductPlane Create(int _myId, bool _isMyProductId)
    {
        ProductPlane ret = new ProductPlane();
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
            planeInfo = DBPlaneInfo.GetPlaneInfo(_myId);
            myPlane = DBMyPLane.GetMyPlane(_myId);
            if (myPlane == null)
                myPlane=InsertIntoMyPlane(planeInfo);
            //SetUpdateInfo();
        }
        else
        {
            myPlane = null;
            planeInfo = null;
        }

        //productObject = NULL;

        //SetSkillData();

        return true;
    }

    #endregion

    #region Create Database

    public static DBMyPLane InsertIntoMyPlane(DBPlaneInfo _planeInfo)
    {

        DBMyPLane myPlane = DBMyPLane.Create();

        //Random random = new Random();
        //Debug.Log("plane id --> "+ _planeInfo.plane_id);
        myPlane.my_plane_id = _planeInfo.plane_id;
        myPlane.plane_id = _planeInfo.plane_id;
        myPlane.plane_name = _planeInfo.plane_name;
        myPlane.plane_purchased = 0;
        myPlane.time_stamp = 0;
        myPlane.my_level = 0;
        myPlane.cost = _planeInfo.plane_price;
        myPlane.c1 = _planeInfo.c1;
        myPlane.c2 = _planeInfo.c2;
        myPlane.c3 = _planeInfo.c3;
        myPlane.c4 = _planeInfo.c4;
        myPlane.c5 = _planeInfo.c5;
        myPlane.c6 = _planeInfo.c6;
        myPlane.c7 = _planeInfo.c7;
        myPlane.c8 = _planeInfo.c8;
        myPlane.c9 = _planeInfo.c9;

        if(_planeInfo.plane_id == 1)
        {
            myPlane.plane_purchased = 1;
        }
        myPlane.InsertIntoDatabase();

        return myPlane;
    }

    public static ProductPlane GetPlaneProduct(int planeId)
    {
        if (allProductPlane.Count == 0)
            GetAllProductInfo();
    
        foreach (ProductPlane plane in allProductPlane)
        {

            if (plane.myPlane.my_plane_id == planeId)
                return plane;
        }

        return null;
    }

    public static List<ProductPlane> GetAllProductInfo()
    {
        if (allProductPlane.Count == 0)
        {
            List<DBPlaneInfo> allplaneInfo = DBPlaneInfo.GetAllPlaneInfo();

            foreach (var productInfo in allplaneInfo)
            {
                allProductPlane.Add(ProductPlane.Create(productInfo.plane_id,true));
                Debug.Log("product Plane id -> "+ productInfo.plane_id);
            }
        }

        return allProductPlane;
    }
    public static List<ProductPlane> GetAllNotBuyProduct()
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
    }

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
    }
    #endregion
}
