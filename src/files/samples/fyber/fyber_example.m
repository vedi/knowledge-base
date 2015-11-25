#import "ViewController.h"

@interface ViewController ()

@end

@implementation ViewController

NSString *APP_ID = @"YOUR_FYBER_APPID";
NSString *SECURITY_TOKEN = @"YOUR_FYBER_CLIENT_SECURITY_TOKEN";
NSString *USER_ID = @"YOUR_USER_ID";

NSString *SOOMLA_SECRET = @"YOUR_SOOMLA_SECRET";

SPBrandEngageClient *brandEngageClient = nil;

- (void)viewDidLoad {
    [super viewDidLoad];

    // Setting the text for Show Video and Show Interstitial buttons
    [self.requestVideo setTitle:@"Request\nVideo" forState:UIControlStateNormal];
    [self.showVideo setTitle:@"Show\nVideo" forState:UIControlStateNormal];
    [self setVideoReady:NO];

    // Initialize SOOMLA Store
    [Soomla initializeWithSecret:SOOMLA_SECRET];
    [[SoomlaStore getInstance] initializeWithStoreAssets:[[YourStoreAssetsImplementation alloc] init]];

    // Initialize Fyber SDK
    [SponsorPaySDK startForAppId:APP_ID userId:USER_ID securityToken:SECURITY_TOKEN];
}

- (void)setVideoReady:(bool)ready {
    [self.showVideo setEnabled:ready];
}

// Request a video from the Fyber sdk
- (IBAction)requestVideoButton:(id)sender {
    brandEngageClient = [SponsorPaySDK requestBrandEngageOffersNotifyingDelegate:self
                                                                     placementId:nil
                                                          queryVCSWithCurrencyId:nil vcsDelegate:self];
}

// Show a video after being notified in brandEngageClient didReceiveOffers (implemented below)
// that there is a video available
- (IBAction)showVideoButton:(id)sender {
    [brandEngageClient startWithParentViewController:self];
    [self setVideoReady:NO];
}

// When a call from the VCS is received, store the amount of coins awarded,
// using the Virtual Currency ID specified in the Fyber Dashboard and your StoreAssets implementation
- (void)virtualCurrencyConnector:(SPVirtualCurrencyServerConnector *)connector
  didReceiveDeltaOfCoinsResponse:(double)deltaOfCoins
                      currencyId:(NSString *)currencyId
                    currencyName:(NSString *)currencyName
             latestTransactionId:(NSString *)transactionId
{
    [StoreInventory giveAmount:(int)deltaOfCoins ofItem:currencyId];
}

-(void)virtualCurrencyConnector:(SPVirtualCurrencyServerConnector *)vcConnector
                failedWithError:(SPVirtualCurrencyRequestErrorType)error
                      errorCode:(NSString *)errorCode
                   errorMessage:(NSString *)errorMessage
{


}

// Called when the brandEngageClient changes status
- (void)brandEngageClient:(SPBrandEngageClient *)brandEngageClient didChangeStatus:(SPBrandEngageClientStatus)newStatus {
    [self setVideoReady:NO];
}

// Called when a request for videos is complete and a video either is or is not available
- (void)brandEngageClient:(SPBrandEngageClient *)brandEngageClient didReceiveOffers:(BOOL)areOffersAvailable {
    [self setVideoReady:areOffersAvailable];
}

@end