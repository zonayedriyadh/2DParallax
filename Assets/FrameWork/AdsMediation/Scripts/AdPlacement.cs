using System;
namespace FPG
{
    public class AdPlacement
    {
        private AdPlacement() { }
#if UNITY_ANDROID

        //Sea World Simulator GP
        public static readonly string admob_app_id = "ca-app-pub-1976199688158817~3706402689";
         

        public static readonly string admob_adunit_id_25 = "ca-app-pub-1976199688158817/7642095491";

        public static readonly string admob_adunit_id_20 = "ca-app-pub-9899159862856473/7446065700";
        public static readonly string admob_adunit_id_15 = "ca-app-pub-9899159862856473/6895880290";
        public static readonly string admob_adunit_id_10 = "ca-app-pub-9899159862856473/9880657352";
        public static readonly string admob_adunit_id_07 = "ca-app-pub-9899159862856473/2765063592";
        public static readonly string admob_adunit_id_05 = "ca-app-pub-9899159862856473/9877266859";
        public static readonly string admob_adunit_id_03 = "ca-app-pub-9899159862856473/3311858504";
        public static readonly string admob_adunit_id_default = "ca-app-pub-9899159862856473/9494123475";

        public static readonly string fb_placement_id = "261933124894182_261945908226237";

        public static readonly string fb_placement_id_high = "261933124894182_261945908226237";
        public static readonly string fb_placement_id_medium = "121514622519116_139399264063985";
        public static readonly string fb_placement_id_low = "121514622519116_139399457397299";
        public static readonly string fb_placement_id_default = "121514622519116_139399610730617";
        public static readonly string fb_placement_id_upperTH = "121514622519116_139399610730617";
        public static readonly string fb_placement_id_lowerTH = "121514622519116_139399610730617";

        public static readonly string facebook_interstitial_id_01 = "261933124894182_261939574893537";

        public static readonly string facebook_interstitial_id_02 = "121514622519116_139399900730588";

        public static readonly string admob_interstitial_id_01 = "ca-app-pub-1976199688158817/1651402210";

        public static readonly string admob_interstitial_id_02 = "ca-app-pub-9899159862856473/8100671820";
        public static readonly string admob_interstitial_id_03 = "ca-app-pub-9899159862856473/2768454090";
        public static readonly string admob_interstitial_id_04 = "ca-app-pub-9899159862856473/8195491856";


#elif UNITY_IOS
//Dino Water World 3D iOS
        public static readonly string admob_app_id = "ca-app-pub-9899159862856473~1639465011";


        public static readonly string admob_adunit_id_25 = "ca-app-pub-9899159862856473/2944807828";
        public static readonly string admob_adunit_id_20 = "ca-app-pub-9899159862856473/6309337764";
        public static readonly string admob_adunit_id_15 = "ca-app-pub-9899159862856473/5543051002";
        public static readonly string admob_adunit_id_10 = "ca-app-pub-9899159862856473/1412234300";
        public static readonly string admob_adunit_id_07 = "ca-app-pub-9899159862856473/2286332274";
        public static readonly string admob_adunit_id_05 = "ca-app-pub-9899159862856473/9781678919";
        public static readonly string admob_adunit_id_03 = "ca-app-pub-9899159862856473/8277025559";
        public static readonly string admob_adunit_id_default = "ca-app-pub-9899159862856473/9398535533";

        public static readonly string fb_placement_id = "502707693775628_502712630441801";
        public static readonly string fb_placement_id_high = "502707693775628_502711497108581";
        public static readonly string fb_placement_id_medium = "502707693775628_502711713775226";
        public static readonly string fb_placement_id_low = "502707693775628_502712010441863";
        public static readonly string fb_placement_id_default = "502707693775628_502716497108081";
        public static readonly string fb_placement_id_upperTH = "502707693775628_502716497108081";
        public static readonly string fb_placement_id_lowerTH = "502707693775628_502716497108081";

        public static readonly string facebook_interstitial_id_01 = "502707693775628_502712630441801";
        public static readonly string facebook_interstitial_id_02 = "502707693775628_502713243775073";

        public static readonly string admob_interstitial_id_01 = "ca-app-pub-9899159862856473/7682145097";
        public static readonly string admob_interstitial_id_02 = "ca-app-pub-9899159862856473/6528829612";
        public static readonly string admob_interstitial_id_03 = "ca-app-pub-9899159862856473/2398012912";
        public static readonly string admob_interstitial_id_04 = "ca-app-pub-9899159862856473/4641032876";
#endif
    }
}