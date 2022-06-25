using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;


public class ResourceUtility
{
    public static int Id1;
    public static int Id2;
    public static int Currency1;
    public static int Currency2;
    public static int Image1;
    public static int Image2;

    public static void  StringParser (String Utility)
        
        {

        var Str = Utility;
        var ParsedString = Utility.Split(char.Parse(";"));

        var ParsedString1 = ParsedString[0];
        
       // var ParsedString2 = ParsedString[1];

        var value1 = ParsedString1.Split(char.Parse(","));
      //  var value2 = ParsedString2.Split(char.Parse(","));

         Id1 = Int32.Parse(value1[0]);
        
         Currency1 = Int32.Parse(value1[1]);


       // Id2 = Int32.Parse(value2[0]);
       // Currency2 = Int32.Parse(value2[1]);
        

        

    }

    public static Sprite ResourceImageGetter(int id, bool isForStore =false)
    {
        Sprite img = null;
        switch (id)
        {
            case (int)ResourceIndex.bucksIndex :

                if (!isForStore)
                {
                    
                    img = Resources.Load<Sprite>(Constants.folderResourcesImagesResourceBar + "bucks");
                    if(img == null)
                        Debug.Log(Constants.folderResourcesImagesResourceBar + "bucks");
                }
                else
                {
                    img = Resources.Load<Sprite>(Constants.folderResourcesImagesResourceBar + "bucks_store");
                }
               
                break;
            case (int)ResourceIndex.coinsIndex:
                if (!isForStore)
                {
                    img = Resources.Load<Sprite>(Constants.folderResourcesImagesResourceBar + "coins");
                }
                else
                {
                    img = Resources.Load<Sprite>(Constants.folderResourcesImagesResourceBar + "coins_store");
                }

                break;
            case (int)ResourceIndex.evoMatarialsIndex:

                img = Resources.Load<Sprite>(Constants.folderResourcesImagesResourceBar+ "evo-materials");

                break;
            case (int)ResourceIndex.foodsIndex:

                img = Resources.Load<Sprite>(Constants.folderResourcesImagesResourceBar +"foods");

                break;
           

            case (int)ResourceIndex.xpIndex:

                 img = Resources.Load<Sprite>(Constants.folderResourcesImagesResourceBar+"xpr");
                break;
        }

        
        return img;
      

    }

    //public static int GetResourceOfType(int resourcetype)
    //{
    //    int resource = 0;




    //    return resource;

    //}
   /* public static Sprite GetElementFlagImage(int id)
    {
        Sprite img = null;

      img =  Resources.Load<Sprite>(Constants.folderResourcesImagesElementFlag+"element-flag-" + id.ToString());


        return img;
        
    }*/

    /*public static Sprite  GetFrontFaceSprite(int productId)
    {
        string imagePath = Path.Combine("Animal", "FrontFace");
        imagePath += "/pi" + productId+ "b";

        return Resources.Load<Sprite>(imagePath);

    }
    public static Sprite GetElementalBGSprite(int ElementId)
    {
        Sprite img = null;
        img = Resources.Load<Sprite>(Constants.folderResourcesImagesStoreProductUpdateBackground + "CardLevelBack" + ElementId.ToString());
        return img;
    }*/
    /*public static Sprite GetPlayerPositionImage(int position)
    {
        Sprite img = null;

        img = Resources.Load<Sprite>(Constants.folderResourcesImagesplacingnumber + position.ToString());
        return img;


    }*/
}
