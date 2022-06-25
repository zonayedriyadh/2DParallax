using System;
using System.Collections.Generic;
using UnityEngine;
public enum ScenceIndex
{
    Game = 0,
    Battle = 1,
};

public enum BattleType
{
    None = 0,
    Campaign = 1,
    TowerOblivion = 1094,
    Challenge9 = 1095,
    Colosseum = 1099,
};

public enum FightDragonType
{
    Auto = 0,
    Skill = 1,
};

public enum FightSystemType
{
    AllAuto = 1,
    CampaignSkillAllAuto = 2,
    TowerOblivionSkillAllAuto = 3,
};

public enum OwnerShip
{
    owned = 1,
    opponent = 2,
};

public enum ResourceIndex
{
    productRewardIndex = -2,
    xpIndex = -1,
    coinsIndex = 1,
    foodsIndex = 2,
    bucksIndex = 3,
    evoMatarialsIndex = 4,
    dollarIndex = 5
};

public enum SkillType
{
    Elemental = 1,
    Static = 2,
};

public enum Panel
{
    Home                    = 1,
    panelStart              = 2,
    panelGameOver           = 3,
    planeSelectPanel        = 4,
    panelGameTypeSelect     = 5,
    panelArcade             = 6,
    panelObjectives         = 7,
    panelMarket             = 8,
};

enum Age
{
    Baby = 1,
    BabyX = 2,
    BabyY = 3,
    Adult = 4,
    AdultX = 5,
    AdultY = 6,
    Old = 7,
    OldX = 8,
    OldY = 9,
};

enum kAnimSeq
{
    WalkSwim = 0,
    Fly = 1,
    ExtMove = 2, //Later Use
    Eat = 3,
    Fire = 4,
    ExtActivity = 5, //Later Use
};

public enum FXType
{
    deathFx = 0,
    magicFx = 1,
};

public struct Date
{
    public int d, m, y;
};

static class Constants
{
    public const int divideValue = 1;
    public const int HealthMultiplier = 10;
    public const float FastBattle = 1.8f;
    public const int MaxPlayCount = 10;
    public const int MaxIsland = 10;
    #region Score
    public const int winningBonus = 100;
    public const int scoreForFirstPosition = 50;
    #endregion

    public static List<string> BotName = new List<string>
    {
        "Jack",
        "Harold",
        "Marrie",
        "Thomas",
        "Ryan",
        "Robert",
        "Sanchez",
        "Annie",
        "Sara",
        "Sean",
        "Alice",
        "Mellissa",
        "Jane",
        "Taylor",
        "Russell",
        "Lee",
        "Julian",
        "Freddie",
        "Ahmed",
        "Imran",
        "Karen",
    };

    public static List<string> GetBotNameList(int length)
    {
        List<string> botName = new List<string>();

        while (true)
        {
            int count = 0;
            string name = BotName[UnityEngine.Random.Range(0, BotName.Count)];
            for (int i = 0; i < botName.Count; i++)
            {
                if (name == botName[i])
                {
                    break;
                }
                else
                {
                    count++;
                }
            }
            if (count == botName.Count)
            {
                botName.Add(name);
            }
            if (botName.Count == length)
            {
                break;
            }
        }

        return botName;

    }

    public static string[] allFighterName =
    {
        "Soi",
        "MiKe",
        "Gigi",
        "Furu",
        "Kuku",
        "Mikeey",
    };

    #region DB Version

    public static string dbVersionString = "DBversion";

    #endregion

    public static string SelectedProdcutId = "SelectedProdcutId";
    public static string SelectedProdcutIdForStore = "SelectedProdcutIdForStore";

    public static string folderResources = "Resources/";
    public static string folderResourcesImages = "Images/";
    public static string folderResourcesImagesResourceBar = folderResourcesImages+ "ResourceBar/";
    public static string folderResourcesImagesMissions = folderResourcesImages + "Missions/";
    public static string folderResourcesImagesObjectiveImage = folderResourcesImages + "ObjectiveImage/";

    public static string folderResourcesAnimal = "Animal/";
     
    
    public static string GetMapIndexKey(int mapIndex)
    {
        return "PlayCount" + mapIndex;
    }

    public static int CoinLevelMultiplier(int score, int mapIndex)
    {
        return score * (mapIndex + 1);
    }

    public static int enegeryTime = 10;
    #region WaitTime
    public static WaitForSeconds oneSec = new WaitForSeconds(1);
    public static WaitForSeconds poinTwoSecond = new WaitForSeconds(0.5f);
    public static WaitForSeconds missilesSpwanTime = new WaitForSeconds(6.5f);
    public static WaitForSeconds starSpwanTime = new WaitForSeconds(7);
    public static WaitForSeconds baseSpwanTime = new WaitForSeconds(8);
    public static WaitForSeconds energySpwanTime = new WaitForSeconds(6);
    #endregion
    #region PlayersPref Name

    public static string crossPromoMiniAdCounter = "crossPromoMiniAdCounter";
    public static string vibration = "virbration";
    public static string sound = "sound";
    public static readonly string SessionCounter = "gameOpenCounter";
    public static readonly string NoAdsFlag = "NoAdsFlag";
    public static string CurrentPlane = "CurrentPlane";

    #endregion
}