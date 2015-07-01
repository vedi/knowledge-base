---
layout: "sample"
image: "tune_logo"
title: "TUNE"
text: "Show rewarded video to earn coins"
position: 5
relates: ["supersonic"]
collection: 'samples'
theme: 'samples'
---

# TUNE + SOOMLA

<div class="samples-title">Use Cases</div>

* Measure SOOMLA Store purchase events to see which ad networks and publishers are sending you valuable users making in-app purchases

* Measure SOOMLA Profile social network logins to see which ad networks and publishers are sending you social users, and which social network they login to.

* Measure SOOMLA LevelUp events to see which ad networks and publishers are sending you engaged users that make more in-game progress.

<br>


The biggest hurdle for marketing a mobile app is integrating SDKs for every ad network and publisher you want to work with. With <a href="http://www.tune.com" target="_blank">TUNE</a>, you never have to integrate another advertising SDK. Not only have we integrated with mobile ad networks and publishers, but you can easily pass conversion information to any third party partner you choose.

The <a href="https://developers.mobileapptracking.com/mobile-sdks/" target="_blank">TUNE SDK</a> provides application session and event logging functionality. To begin measuring sessions and installs, initiate the `measureSession` method. You can then rely on TUNE to log in-app events (such as purchases, game levels, and any other user engagement).

This document will show you how to measure events from all SOOMLA modules - Store, Profile and LevelUp - so you can identify which ad networks and publishers send you the most valuable users.

<div>

  <!-- Nav tabs -->
  <ul class="nav nav-tabs nav-tabs-use-case-code sample-tabs" role="tablist">
    <li role="presentation" class="active"><a href="#sample-unity-store" aria-controls="unity" role="tab" data-toggle="tab">Unity - Purchases</a></li>
    <li role="presentation"><a href="#sample-ios-store" aria-controls="ios" role="tab" data-toggle="tab">iOS - Purchases</a></li>
    <li role="presentation"><a href="#sample-android-store" aria-controls="android" role="tab" data-toggle="tab">Android - Purchases</a></li>
    <li role="presentation"><a href="#sample-unity-profile" aria-controls="unity" role="tab" data-toggle="tab">Unity - Social</a></li>
    <li role="presentation"><a href="#sample-ios-profile" aria-controls="ios" role="tab" data-toggle="tab">iOS - Social</a></li>
    <li role="presentation"><a href="#sample-android-profile" aria-controls="android" role="tab" data-toggle="tab">Android - Social</a></li>
    <li role="presentation"><a href="#sample-unity-levelup" aria-controls="unity" role="tab" data-toggle="tab">Unity - Levels</a></li>
  </ul>

  <!-- Tab panes -->
  <div class="tab-content tab-content-use-case-code">
    <div role="tabpanel" class="tab-pane active" id="sample-unity-store">
      <pre>
        <code class="cs">
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MATSDK;
using Soomla.Store;

public class TuneSoomlaStoreScript : MonoBehaviour {
    void Start () {
        // Initialize TUNE SDK
        MATBinding.Init("tune_advertiser_id", "tune_conversion_key");
        // Measure initial app open
        MATBinding.MeasureSession();

        // Listen for Soomla OnMarketPurchase event
        StoreEvents.OnMarketPurchase += onMarketPurchase;

        // Initialize Soomla Store with your Store Assets
        SoomlaStore.Initialize(new YourStoreAssetsImplementation());
    }

    public void onMarketPurchase(PurchasableVirtualItem pvi, string payload,
            Dictionary<string, string> extra) {

        // On purchase complete, set purchase info and measure purchase in TUNE
        double revenue = 0;
        string currency = "";
        MATItem[] items = new MATItem[] {};
        PurchaseType type = pvi.PurchaseType;
        if (type is PurchaseWithMarket) {
            MarketItem item = ((PurchaseWithMarket)type).MarketItem;
            revenue = item.MarketPriceMicros / 1000000;
            currency = item.MarketCurrencyCode;

            // Create event item to store purchase item data
            MATItem matItem = new MATItem();
            matItem.name = item.MarketTitle;
            matItem.attribute1 = item.ProductId;

            // Add event item to MATItem array in order to pass to TUNE SDK
            items[items.Length] = matItem;
        }

        // Get order ID and receipt data for purchase validation
        string receipt = "";
        string receiptSignature = "";
        string orderId = "";
#if UNITY_ANDROID
        extras.TryGetValue("originalJson", out receipt);
        extras.TryGetValue("signature", out receiptSignature);
        extras.TryGetValue("orderId", out orderId);
#elif UNITY_IOS
        extras.TryGetValue("receiptBase64", out receipt);
        extras.TryGetValue("transactionIdentifier", out orderId);
#endif

        // Create a MATEvent with this purchase data
        MATEvent purchaseEvent = new MATEvent("purchase");
        purchaseEvent.revenue = revenue;
        purchaseEvent.currencyCode = currency;
        purchaseEvent.advertiserRefId = orderId;
        purchaseEvent.receipt = receipt;
        purchaseEvent.receiptSignature = receiptSignature;

        // Set event item if it exists
        if (items.Length != 0) {
            purchaseEvent.eventItems = items;
        }
        // Measure "purchase" event
        MATBinding.MeasureEvent(purchaseEvent);
    }
}
        </code>
      </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-ios-store">
      <pre>
        <code class="objectivec">
#import "ViewController.h"
#import "MarketItem.h"
#import "PurchasableVirtualItem.h"
#import "PurchaseWithMarket.h"
#import "SoomlaStore.h"
#import "StoreEventHandling.h"
#import <MobileAppTracker/MobileAppTracker.h>

@interface ViewController ()

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];

    // Listen for Soomla EVENT_MARKET_PURCHASED event
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(marketPurchased:)
                                                 name:EVENT_MARKET_PURCHASED object:nil];

    // Initialize Soomla Store with your Store Assets
    [Soomla initializeWithSecret:@"[YOUR CUSTOM GAME SECRET HERE]"];
    [[SoomlaStore getInstance] initializeWithStoreAssets:[[YourStoreAssetsImplementation alloc] init]];
}

// On purchase complete, set purchase info and measure purchase in TUNE
- (void)marketPurchased:(NSNotification \*)notification {
    CGFloat revenue;
    NSString \*currency;
    NSArray \*items;

    PurchaseType \*type = [notification.userInfo[DICT_ELEMENT_PURCHASABLE] purchaseType];
    if ([type isKindOfClass:[PurchaseWithMarket class]]) {
        MarketItem \*item = ((PurchaseWithMarket \*)type).marketItem;
        revenue = (CGFloat)([item marketPriceMicros] / 1000000);
        currency = [item marketCurrencyCode];

        // Create event item to store purchase item data
        MATEventItem \*eventItem = [MATEventItem eventItemWithName:[item marketTitle]
                                                        attribute1:[item productId]
                                                        attribute2:nil
                                                        attribute3:nil
                                                        attribute4:nil
                                                        attribute5:nil];
        // Add event item to MATItem array in order to pass to TUNE SDK
        items = @[eventItem];
    }

    // Get transaction ID and receipt data for purchase validation
    NSDictionary \*dict = notification.userInfo[DICT_ELEMENT_EXTRA_INFO];
    NSString \*transactionId = dict[@"transactionIdentifier"];
    NSString \*receipt = dict[@"receiptBase64"];
    NSData \*receiptData = [[NSData alloc] initWithBase64EncodedString:receipt options:1];

    // Create a MATEvent with this purchase data
    MATEvent \*purchaseEvent = [MATEvent eventWithName:MAT_EVENT_PURCHASE];
    purchaseEvent.revenue = revenue;
    purchaseEvent.currencyCode = currency;
    purchaseEvent.refId = transactionId;
    purchaseEvent.receipt = receiptData;
    purchaseEvent.eventItems = items;

    // Measure "purchase" event
    [MobileAppTracker measureEvent:purchaseEvent];
}

@end
        </code>
      </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-android-store">
      <pre>
        <code class="java">
import android.app.Activity;
import android.os.Bundle;

import com.mobileapptracker.MATEvent;
import com.mobileapptracker.MATEventItem;
import com.mobileapptracker.MobileAppTracker;
import com.soomla.BusProvider;
import com.soomla.Soomla;
import com.soomla.store.IStoreAssets;
import com.soomla.store.SoomlaStore;
import com.soomla.store.domain.MarketItem;
import com.soomla.store.events.MarketPurchaseEvent;
import com.soomla.store.purchaseTypes.PurchaseType;
import com.soomla.store.purchaseTypes.PurchaseWithMarket;
import com.squareup.otto.Subscribe;

public class MainActivity extends Activity {
    private MobileAppTracker mobileAppTracker;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Initialize the TUNE SDK
        mobileAppTracker = MobileAppTracker.init(
                getApplicationContext(),
                "tune_advertiser_id",
                "tune_conversion_key");

        // Initialize Soomla and Soomla Store
        Soomla.initialize("[YOUR CUSTOM GAME SECRET HERE]");
        SoomlaStore.getInstance().initialize(new YourStoreAssetsImplementation());
    }

    @Override
    protected void onResume() {
        super.onResume();
        // Register to receive Soomla events
        BusProvider.getInstance().register(this);
        // Measure an app open in TUNE
        mobileAppTracker.setReferralSources(this);
        mobileAppTracker.measureSession();
    }

    @Override
    protected void onPause() {
        // Unregister from Soomla events
        BusProvider.getInstance().unregister(this);
        super.onPause();
    }

    // On purchase complete, set purchase info and measure purchase in TUNE
    @Subscribe
    public void onMarketPurchase(MarketPurchaseEvent marketPurchaseEvent) {
        double revenue;
        String currency;
        List<MATEventItem> items = new ArrayList<MATEventItem>();

        PurchaseType type = marketPurchaseEvent.getPurchasableVirtualItem().getPurchaseType();
        if (type instanceof PurchaseWithMarket) {
            MarketItem item = ((PurchaseWithMarket) type).getMarketItem();
            revenue = item.getMarketPriceMicros() / 1000000;
            currency = item.getMarketCurrencyCode();
            // Create event item to store purchase item data
            MATEventItem eventItem = new MATEventItem(item.getMarketTitle())
                    .withAttribute1(item.getProductId());
            // Add event item to MATItem array in order to pass to TUNE SDK
            items.add(eventItem);
        }

        // Get order ID and receipt data for purchase validation
        String orderId = marketPurchaseEvent.getOrderId();
        String receiptData = marketPurchaseEvent.getOriginalJson();
        String receiptSignature = marketPurchaseEvent.getSignature();

        // Create a MATEvent with this purchase data
        MATEvent purchaseEvent = new MATEvent(MATEvent.PURCHASE)
                .withRevenue(revenue)
                .withCurrencyCode(currency)
                .withAdvertiserRefId(orderId)
                .withReceipt(receiptData, receiptSignature);
        // Set event item if it exists
        if (!items.isEmpty()) {
            purchaseEvent.withEventItems(items);
        }
        // Measure "purchase" event
        mobileAppTracker.measureEvent(purchaseEvent);
    }
}
        </code>
      </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-unity-profile">
      <pre>
        <code class="cs">
using UnityEngine;
using System.Collections;
using MATSDK;
using Soomla.Profile;

public class TuneSoomlaProfileScript : MonoBehaviour {
    void Start () {
        // Initialize TUNE SDK
        MATBinding.Init("tune_advertiser_id", "tune_conversion_key");
        // Measure initial app open
        MATBinding.MeasureSession();

        // Listen for Soomla OnLoginFinished event
        ProfileEvents.OnLoginFinished += onLoginFinished;

        // Initialize Soomla Profile
        SoomlaProfile.Initialize();
    }

    void OnApplicationPause(bool pauseStatus) {
        if (!pauseStatus) {
            // Measure app resumes from background
            MATBinding.MeasureSession();
        }
    }

    // Set user ID and measure login event upon login finished
    public void onLoginFinished(UserProfile userProfileJson, string payload) {
        Provider provider = userProfileJson.Provider;
        string userId = userProfileJson.ProfileId;

        // Set different user IDs in TUNE SDK based on provider
        if (provider == Provider.FACEBOOK) {
            MATBinding.SetFacebookUserId(userId);
        } else if (provider == Provider.GOOGLE) {
            MATBinding.SetGoogleUserId(userId);
        } else if (provider == Provider.TWITTER) {
            MATBinding.SetTwitterUserId(userId);
        } else {
            MATBinding.SetUserId(userId);
        }
        // Measure a login event for this user ID
        MATBinding.MeasureEvent("login");
    }
}
        </code>
      </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-ios-profile">
      <pre>
        <code class="objectivec">
#import "ViewController.h"
#import "SoomlaProfile.h"
#import "ProfileEventHandling.h"
#import "UserProfile.h"
#import "UserProfileUtils.h"
#import <MobileAppTracker/MobileAppTracker.h>

@interface ViewController ()

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];

    // Register for Soomla login finished event
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(loginFinished:) name:EVENT_UP_LOGIN_FINISHED object:nil];

    // Initialize Soomla Profile
    [Soomla initializeWithSecret:@"[YOUR CUSTOM GAME SECRET HERE]"];
    [[SoomlaProfile getInstance] initialize:providerParams];
}

// Set user ID and measure login event upon login finished
- (void)loginFinished:(NSNotification\*)notification {
    UserProfile \*userProfile = notification.userInfo[DICT_ELEMENT_USER_PROFILE];
    NSString \*userId = [userProfile profileId];

    // Set different user IDs in TUNE SDK based on provider
    NSString \*provider = [UserProfileUtils providerEnumToString:[userProfile provider]];
    if ([provider isEqualToString:@"facebook"]) {
        [MobileAppTracker setFacebookUserId:userId];
    } else if ([provider isEqualToString:@"google"]) {
        [MobileAppTracker setGoogleUserId:userId];
    } else if ([provider isEqualToString:@"twitter"]) {
        [MobileAppTracker setTwitterUserId:userId];
    } else {
        [MobileAppTracker setUserId:userId];
    }
    // Measure a login event for this user ID
    [MobileAppTracker measureEventName:MAT_EVENT_LOGIN];
}

@end
        </code>
      </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-android-profile">
      <pre>
        <code class="java">
import android.app.Activity;
import android.os.Bundle;

import com.mobileapptracker.MATEvent;
import com.mobileapptracker.MobileAppTracker;
import com.soomla.BusProvider;
import com.soomla.Soomla;
import com.soomla.SoomlaConfig;
import com.soomla.profile.SoomlaProfile;
import com.soomla.profile.domain.IProvider;
import com.soomla.profile.domain.UserProfile;
import com.soomla.profile.events.auth.LoginFinishedEvent;
import com.squareup.otto.Subscribe;

public class MainActivity extends Activity {
    private MobileAppTracker mobileAppTracker;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Initialize the TUNE SDK
        mobileAppTracker = MobileAppTracker.init(
                getApplicationContext(),
                "tune_advertiser_id",
                "tune_conversion_key");

        // Initialize Soomla and Soomla Profile
        Soomla.initialize("[YOUR CUSTOM GAME SECRET HERE]");
        SoomlaProfile.getInstance().initialize();
    }

    @Override
    protected void onResume() {
        super.onResume();
        // Register to receive Soomla events
        BusProvider.getInstance().register(this);
        // Measure an app open in TUNE
        mobileAppTracker.setReferralSources(this);
        mobileAppTracker.measureSession();
    }

    @Override
    protected void onPause() {
        // Unregister from Soomla events
        BusProvider.getInstance().unregister(this);
        super.onPause();
    }

    @Subscribe
    public void onLoginFinished(LoginFinishedEvent loginFinishedEvent) {

        // On login finished, set the user ID in TUNE based on provider, and measure a login
        UserProfile user = loginFinishedEvent.UserProfile;
        if (user != null) {
            IProvider.Provider provider = user.getProvider();
            String userId = user.getProfileId();

            // Set different user IDs in TUNE SDK based on provider
            if (provider == IProvider.Provider.FACEBOOK) {
                mobileAppTracker.setFacebookUserId(userId);
            } else if (provider == IProvider.Provider.GOOGLE) {
                mobileAppTracker.setGoogleUserId(userId);
            } else if (provider == IProvider.Provider.TWITTER) {
                mobileAppTracker.setTwitterUserId(userId);
            } else {
                mobileAppTracker.setUserId(userId);
            }
            // Measure a login event for this user ID
            mobileAppTracker.measureEvent(MATEvent.LOGIN);
        }
    }
}
        </code>
      </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-unity-levelup">
      <pre>
        <code class="cs">
using UnityEngine;
using System.Collections;
using MATSDK;
using Soomla.Levelup;

public class TuneSoomlaLevelUpScript : MonoBehaviour {
    void Start () {
        // Initialize TUNE SDK
        MATBinding.Init("tune_advertiser_id", "tune_conversion_key");
        // Measure initial app open
        MATBinding.MeasureSession();

        // Listen for Soomla OnLevelStarted event
        LevelUpEvents.OnLevelStarted += onLevelStarted;

        // Create an example World with Level
        World world = new World("exampleWorld");
        Level lvl1 = new Level("Level 1");
        world.AddInnerWorld(lvl1);

        // Initialize Soomla LevelUp with the example world containing a level
        SoomlaLevelUp.Initialize(world);

        // Start the level, will trigger OnLevelStarted event
        lvl1.Start();
    }

    void OnApplicationPause(bool pauseStatus) {
        if (!pauseStatus) {
            // Measure app resumes from background
            MATBinding.MeasureSession();
        }
    }

    // Set level ID and measure a level achieved event upon level start
    public void onLevelStarted(Level level) {

        // Create a MATEvent for level_achieved with the level ID
        MATEvent levelEvent = new MATEvent("level_achieved");
        levelEvent.contentId = level.ID;

        // Measure "level_achieved" event for this level ID
        MATBinding.MeasureEvent (levelEvent);
    }
}
        </code>
      </pre>
    </div>

  </div>

</div>

<div class="samples-title">Getting started</div>


1. [Sign up](https://platform.mobileapptracking.com/#!/advertiser) with TUNE to get started on your home for attribution and analytics.

2. Download and integrate the [TUNE SDK](https://developers.mobileapptracking.com/mobile-sdks/).

3. Integrate SOOMLA Store and LevelUp.  Follow all steps in the platform specific getting started guides: <br>
    <a href="/unity/" target="_blank">Unity</a> |
    <a href="/ios/" target="_blank">iOS</a> |
    <a href="/android/" target="_blank">Android</a>


<div class="samples-title">Support</div>

* For more information on SDK implementation and events to measure, please visit our <a href="https://developers.mobileapptracking.com/mobile-sdks/" target="_blank">documentation</a>.

* If you have further questions, please feel free to contact us at [support@mobileapptracking.com](mailto:support@mobileapptracking.com).
