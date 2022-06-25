using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBattle
{
    public DBBattleInfo battleInfo;
    public DBMyBattle myBattle;

    private static List<ControllerBattle> allBattleController = new List<ControllerBattle>();
    //private static List<ControllerBattle> allNotBuyProduct = new List<ProductPlane>();
    #region Create ProductPlane
    public static ControllerBattle Create()
    {
        ControllerBattle ret = new ControllerBattle();
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

    public static ControllerBattle Create(int _myId, bool _isMyProductId)
    {
        ControllerBattle ret = new ControllerBattle();
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
            battleInfo = DBBattleInfo.GetBattleInfo(_myId);
            myBattle = DBMyBattle.GetMyBattle(_myId);
            if (myBattle == null)
                myBattle = InsertIntoMyBattle(battleInfo);
            //SetUpdateInfo();
        }
        else
        {
            myBattle = null;
            battleInfo = null;
        }

        //productObject = NULL;

        //SetSkillData();

        return true;
    }

    #endregion

    #region Create Database

    public static DBMyBattle InsertIntoMyBattle(DBBattleInfo _battleInfo)
    {

        DBMyBattle myBattle = DBMyBattle.Create();

        //Random random = new Random();
        //Debug.Log("plane id --> "+ _planeInfo.plane_id);
        myBattle.my_battleid = _battleInfo.battle_id;
        myBattle.battleid = _battleInfo.battle_id;
        myBattle.is_unlock = 0;
        myBattle.is_claim = 0;
        myBattle.last_time_collect =0;
        myBattle.number_of_play = 0;
        myBattle.number_of_win = 0;
        myBattle.c1 = "0";
        myBattle.c2 = "0";
        myBattle.c3 = "0";
        myBattle.c4 = "0";
        myBattle.c5 = "0";
        myBattle.c6 = "0";
        myBattle.c7 = "0";
        myBattle.c8 = "0";
        myBattle.c9 = "0";


        //Debug.Log("my_battleid ->"+ myBattle.my_battleid+"battle_id->"+ myBattle.battleid);
        if (_battleInfo.battle_id == 1)
        {
            myBattle.is_unlock = 1;
        }
        myBattle.InsertIntoDatabase();

        return myBattle;
    }

    public static ControllerBattle GetControlletBattle(int battleId)
    {
        if (allBattleController.Count == 0)
            GetAllControllerBattleinffo();

        foreach (ControllerBattle battle in allBattleController)
        {

            if (battle.myBattle.battleid == battleId)
                return battle;
        }

        return null;
    }

    public static List<ControllerBattle> GetAllControllerBattleinffo()
    {
        if (allBattleController.Count == 0)
        {
            List<DBBattleInfo> allBattleInfo = DBBattleInfo.GetAllBattleInfo();

            foreach (var battleInfo in allBattleInfo)
            {
                allBattleController.Add(ControllerBattle.Create(battleInfo.battle_id, true));
            }
        }

        return allBattleController;
    }

    public static ControllerBattle GetControllerBattleLastUnlock(int mapIndex)
    {
        ControllerBattle lastUnlockBattle=null;
        if (allBattleController.Count == 0)
            GetAllControllerBattleinffo();

        foreach (var _Controllerbattle in allBattleController)
        {
            if (_Controllerbattle.battleInfo.map_id == mapIndex)
            {
                if (_Controllerbattle.myBattle.is_unlock == 1)
                {
                    lastUnlockBattle = _Controllerbattle;
                }
            }
        }
        return lastUnlockBattle;


    }

    public static void BattleComplete(int battleId)
    {

        ControllerBattle Controller_battle = GetControlletBattle(battleId);

        Controller_battle.myBattle.is_claim = 1;
        Controller_battle.myBattle.UpdateDatabase();
        UnlockNext(battleId+1);
        CheckMapComplete(Controller_battle.battleInfo.map_id);
    }

    public static void UnlockNext (int battleId)
    {
        ControllerBattle Controller_battle = GetControlletBattle(battleId);
        Controller_battle.myBattle.is_unlock = 1;
        Controller_battle.myBattle.UpdateDatabase();

        ControllerMap.UnlockMapId(Controller_battle.battleInfo.map_id);
    }

    public static void CheckMapComplete(int mapIndex)
    {
        bool isAllCompelete = true;
        foreach (var _Controllerbattle in allBattleController)
        {
            if (_Controllerbattle.battleInfo.map_id == mapIndex)
            {
                if (_Controllerbattle.myBattle.is_claim != 1)
                {
                    isAllCompelete = false;
                }
            }
        }

        if (isAllCompelete)
            ControllerMap.CompleteMapId(mapIndex);
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

