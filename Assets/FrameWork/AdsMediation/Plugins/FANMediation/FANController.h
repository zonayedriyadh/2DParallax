#pragma once

#import <UIKit/UIKit.h>
#import <FBAudienceNetwork/FBAudienceNetwork.h>

#if PLATFORM_IOS
#define UNITY_VIEW_CONTROLLER_BASE_CLASS UIViewController
#elif PLATFORM_TVOS
#import <GameController/GCController.h>
#define UNITY_VIEW_CONTROLLER_BASE_CLASS GCEventViewController
#endif
@interface FANController : UNITY_VIEW_CONTROLLER_BASE_CLASS <FBInterstitialAdDelegate, FBRewardedVideoAdDelegate>
{
    FBInterstitialAd *interstitialAd;
    FBRewardedVideoAd *rewardedVideoAd;
}
+(FANController*)sharedManager;

#pragma mark- Facebook Audience Network Interstitial

-(void)initInterstitialAd;
-(void)loadInterstitialAd:(NSString*)placementID;
-(BOOL)isInterstitialAdLoaded;
-(void)showInterstitialAd;

#pragma mark- FB Audience Network Rewarded Video
-(void)initRewardedAd;
-(void)loadRewardedAd:(NSString*)placementID;
-(BOOL)isRewardedAdLoaded;
-(void)showRewardedAd;

@end

