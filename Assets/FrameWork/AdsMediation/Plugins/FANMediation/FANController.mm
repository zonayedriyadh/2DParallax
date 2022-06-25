#import "FANController.h"

static FANController *sharedInstance = nil;
static NSString * GAME_OBJECT = @"FANController";

static NSString * CALLBACK_INT_AD_LOAD = @"FanINTAdLoadCompleted";
static NSString * CALLBACK_INT_AD_CLOSE = @"FanINTAdClosed";

static NSString * CALLBACK_RW_AD_LOAD = @"FanRWAdLoadCompleted";
static NSString * CALLBACK_RW_AD_CLOSE = @"FanRWAdClosed";


@implementation FANController


#pragma mark Singleton Methods
+ (FANController*)sharedManager {
    if (sharedInstance == nil)sharedInstance = [[self alloc] init];
    
    return sharedInstance;
}

- (id)init
{
    self = [super init];
    if (self) {
        // Initialization code here.
    }
    return self;
}

extern "C"{
    void PrepareFAN (const char *gameObjectName){
        NSLog(@"extern FANController: PrepareFAN Gameobject: %s",gameObjectName);
        GAME_OBJECT = [NSString stringWithUTF8String:gameObjectName];
    }
}

#pragma mark- Facebook Audience Network Interstitial

extern "C"{
    void InitFANInterstitialAdextern (){
        NSLog(@"extern FANController: InitFANInterstitialAdextern.");
        
        [[FANController sharedManager] initInterstitialAd];
    }
    
    void LoadFANInterstitialAdextern (const char *placement, const char *onLoadCallback){
        NSLog(@"extern FANController: LoadFANInterstitialAdextern placement: %s",placement);
        
        CALLBACK_INT_AD_LOAD = [NSString stringWithUTF8String:onLoadCallback];
        
        NSString * placementID = [NSString stringWithUTF8String:placement];
        [[FANController sharedManager] loadInterstitialAd:placementID];
    }
    
    BOOL IsFANInterstitialAdAvailableextern (){
        NSLog(@"extern FANController: IsFANInterstitialAdAvailableextern");
        return [[FANController sharedManager] isInterstitialAdLoaded];
    }
    
    void ShowFANInterstitialAdextern (const char *onCloseCallback){
        NSLog(@"extern FANController: ShowFANInterstitialAdextern");
        
        CALLBACK_INT_AD_CLOSE = [NSString stringWithUTF8String:onCloseCallback];
        
        [[FANController sharedManager] showInterstitialAd];
    }
}

-(void)initInterstitialAd
{
    NSLog(@"FANController: initInterstitialAd. Nothing to do here");
}

-(void)loadInterstitialAd:(NSString*)placementID
{
    NSLog(@"FANController: initInterstitialAd");
    if (interstitialAd) interstitialAd=NULL;
    interstitialAd = [[FBInterstitialAd alloc] initWithPlacementID:placementID];
    interstitialAd.delegate = [FANController sharedManager];
    
    if (interstitialAd)
    {
        NSLog(@"FANController: loadFBAudienceAd");
        [interstitialAd loadAd];
    }
    else
    {
        NSLog(@"FANController: loadFBAudienceAd. interstitialAd not initialized.");
    }
}

-(BOOL) isInterstitialAdLoaded
{
    bool isLoaded = interstitialAd && interstitialAd.isAdValid;
    NSLog(@"FANController: isInterstitialAdLoaded. isLoaded: %d",isLoaded);
    return isLoaded;
}

-(void)showInterstitialAd
{
    if ([[FANController sharedManager] isInterstitialAdLoaded])
    {
        NSLog(@"FANController: showInterstitialAd. Ad available. Showing.");
        [interstitialAd showAdFromRootViewController:[UIApplication sharedApplication].keyWindow.rootViewController];
    } else {
        NSLog(@"FANController: showInterstitialAd. Ad not available.");
    }
}


#pragma mark- FB Audience Network Interstitial Ad Delegate
- (void)interstitialAd:(FBInterstitialAd *)interstitialAd didFailWithError:(NSError *)error
{
    NSLog(@"FANController: interstitialAd error: %ld msg: %@",(long)error.code,error);
    UnitySendMessage([GAME_OBJECT UTF8String], [CALLBACK_INT_AD_LOAD UTF8String], "0");
}

- (void)interstitialAdDidClose:(FBInterstitialAd *)interstitialAd
{
    NSLog(@"FANController: interstitialAdDidClose");
    UnitySendMessage([GAME_OBJECT UTF8String], [CALLBACK_INT_AD_CLOSE UTF8String], "1");
}

- (void)interstitialAdDidLoad:(FBInterstitialAd *)interstitialAd
{
    NSLog(@"FANController: interstitialAdDidLoad");
    UnitySendMessage([GAME_OBJECT UTF8String], [CALLBACK_INT_AD_LOAD UTF8String], "1");
}


#pragma mark- FB Audience Network Rewarded Video
extern "C"{
    void InitFANRewardedAdextern (){
        NSLog(@"extern FANController: InitFANRewardedAdextern");
        
        [[FANController sharedManager] initRewardedAd];
    }

    void LoadFANRewardedAdextern (const char *placement, const char *onLoadCallback){
        NSLog(@"extern FANController: LoadFANRewardedAdextern placement: %s",placement);
        
        CALLBACK_RW_AD_LOAD = [NSString stringWithUTF8String:onLoadCallback];
        
        NSString * placementID = [NSString stringWithUTF8String:placement];
        [[FANController sharedManager] loadRewardedAd:placementID];
    }

    BOOL IsFANRewardedAdAvailableextern (){
        NSLog(@"extern FANController: IsFANRewardedAdAvailableextern");
        return [[FANController sharedManager] isRewardedAdLoaded];
    }

    void ShowFANRewardedAdextern (const char *onCloseCallback){
        NSLog(@"extern FANController: ShowFANRewardedAdextern");
        
        CALLBACK_RW_AD_CLOSE = [NSString stringWithUTF8String:onCloseCallback];
        
        [[FANController sharedManager] showRewardedAd];
    }
}

-(void)initRewardedAd
{
    NSLog(@"FANController: initRewardedAd. Nothing to do here");
}

-(void)loadRewardedAd:(NSString*)placementID
{
    NSLog(@"FANController: loadRewardedAd: %@",placementID);
    if (rewardedVideoAd)
    {
        rewardedVideoAd = NULL;
    }
    rewardedVideoAd = [[FBRewardedVideoAd alloc] initWithPlacementID:placementID];
    rewardedVideoAd.delegate = [FANController sharedManager];
    [rewardedVideoAd loadAd];
}

-(BOOL)isRewardedAdLoaded
{
    bool available = rewardedVideoAd && rewardedVideoAd.isAdValid;
    NSLog(@"FANController: isRewardedAdLoaded. available: %d",available);
    if(!available){
        [[FANController sharedManager] initRewardedAd];
    }
    return available;
}

-(void)showRewardedAd
{
    if ([[FANController sharedManager] isRewardedAdLoaded]) {
        NSLog(@"FANController: showRewardedAd. Ad Available. Showing.");
        [rewardedVideoAd showAdFromRootViewController:[UIApplication sharedApplication].keyWindow.rootViewController];
    }
    else{
        NSLog(@"FANController: showRewardedAd. Ad not Available.");
    }
}

#pragma mark- FB Audience Network Rewarded Video Listener
- (void)rewardedVideoAd:(FBRewardedVideoAd *)rewardedVideoAd didFailWithError:(NSError *)error
{
    NSLog(@"FANController: rewardedVideoAd error: %ld msg: %@",(long)error.code,error);
    UnitySendMessage([GAME_OBJECT UTF8String], [CALLBACK_RW_AD_LOAD UTF8String], "0");
}

- (void)rewardedVideoAdDidLoad:(FBRewardedVideoAd *)rewardedVideoAd
{
    NSLog(@"FANController: rewardedVideoAd is loaded and ready to be displayed");
    UnitySendMessage([GAME_OBJECT UTF8String], [CALLBACK_RW_AD_LOAD UTF8String], "1");
}

- (void)rewardedVideoAdDidClick:(FBRewardedVideoAd *)rewardedVideoAd
{
    NSLog(@"FANController: rewardedVideoAdDidClick");
}

bool watchedWholeAd = false;

- (void)rewardedVideoAdVideoComplete:(FBRewardedVideoAd *)rewardedVideoAd;
{
    NSLog(@"FANController: video complete - this is called after a full video view, before the ad end card is shown. You can use this event to initialize your reward.");
    
    //init reward
    watchedWholeAd = true;
}

- (void)rewardedVideoAdDidClose:(FBRewardedVideoAd *)rewardedVideoAd
{
    NSLog(@"FANController: rewardedVideoAdDidClose - this can be triggered by closing the application, or closing the video end card");
    //give reward
    if(watchedWholeAd){
        UnitySendMessage([GAME_OBJECT UTF8String], [CALLBACK_RW_AD_CLOSE UTF8String], "1");
    }
    else{
        UnitySendMessage([GAME_OBJECT UTF8String], [CALLBACK_RW_AD_CLOSE UTF8String], "0");
    }
    watchedWholeAd = false;
}

@end
