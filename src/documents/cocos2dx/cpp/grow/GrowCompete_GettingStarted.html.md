---
layout: "content"
image: "Bundle"
title: "GrowCompete"
text: "The perfect competition stimulation solution for your game. If you want your users to compete with their friends in order to increase engagement then this bundle is for you."
position: 13
theme: 'platforms'
collection: 'cocos2dx_grow'
module: 'grow'
lang: 'cpp'
platform: 'cocos2dx'
---

# GrowCompete - Bundle

## Overview

GrowCompete is the perfect competition stimulation solution for your game. If you want your users to compete with their friends in order to increase engagement then this bundle is for you. GrowCompete connects you to GROW, SOOMLA's flagship - a community-driven data network. Mobile game studios can take advantage of the different GROW products in order to get valuable insights about their games' performance and increase retention and monetization. [Read more...](/cocos2dx/cpp/grow/Grow_About)

GrowCompete includes:

- All of SOOMLA's open-source modules - [Store](/cocos2dx/cpp/store/Store_GettingStarted), [Profile](/cocos2dx/cpp/profile/Profile_GettingStarted) and [LevelUp](/cocos2dx/cpp/levelup/Levelup_GettingStarted)
- [State Sync](/cocos2dx/cpp/grow/Grow_Sync)
- [Social Leaderboards](/cocos2dx/cpp/grow/Grow_Leaderboards)
- [Analytics](/cocos2dx/cpp/grow/Grow_Analytics)
- [Whales Report](/cocos2dx/cpp/grow/Grow_WhalesReport)
- [Insights](/cocos2dx/cpp/grow/Grow_Insights)

## Integrating GrowCompete

### New Game & Configurations

Go to the [GROW dashboard](http://dashboard.soom.la) and sign up \ login. Upon logging in, you will be directed to the main page of the dashboard. You will need to create a new game in order to start your journey with GROW.

<div class="info-box">If you didn't already, clone the Cocos2d-x framework from [here](https://github.com/cocos2d/cocos2d-x), or download it from the [Cocos2d-x website](http://www.cocos2d-x.org/download). Make sure the version you clone is supported by SOOMLA's modules (the tag is the version).</div>

1. In the games screen click on the "+" button to add a new game. If it's your first time in the dashboard, just click on the "+" button underneath the "Create your first game" label in the middle of the screen.

	  ![alt text](/img/tutorial_img/cocos_grow/addNewApp.png "Add new app")

	* Once you created your game, you'll be redirected to a quick start process to download any of the GROW bundles (You can also click "Downloads" on the top right corner of the screen). Click on **GrowCompete**. You'll see an instructions screen, you can continue with that or stay here for the extended version.  

2. Unzip the downloaded file and copy its contents into the cocos2d directory located at the root of your Cocos2d-x project.


### Initialize modules

<div class="info-box">Make sure to initialize each module ONLY ONCE when your application loads.</div>
<br>
<div class="info-box">The CCGrowHighway module is the module responsible for connecting your game to the GROW network. In order for it to operate it only needs to be initialized.</div>

1. In your `AppDelegate.cpp` include `Cocos2dxHighway.h`:

  ```cpp
  #include "Cocos2dxHighway.h"
  ```

2. Initialize `CCSoomla` with a custom secret of your choice (**Custom Secret** is an encryption secret you provide that will be used to secure your data.):

  ```cpp
  soomla::CCSoomla::initialize("ExampleCustomSecret");
  ```

  <div class="warning-box">Choose this secret wisely, you can't change it after you launch your game!</div>

3. Initialize `CCGrowHighway` with the "Game Key" and "Env Key" given to you in the [dashboard](http://dashboard.soom.la):

  **Copy the "Game Key" and "Environment Key"** given to you from the [dashboard](http://dashboard.soom.la) and initialize `CCGrowHighway` with them. At this point, you're probably testing your integration and you want to use the **Sandbox** environment key.

  Explanation: The "game" and "env" keys allow for your game to distinguish multiple environments for the same game. The dashboard pre-generates two fixed environments for your game: **Production** and **Sandbox**. When you decide to publish your game, make sure to switch the env key to **Production**.  You can always generate more environments.  For example - you can choose to have a playground environment for your game's beta testers which will be isolated from your production environment and will thus prevent analytics data from being mixed between the two.  Another best practice is to have a separate environment for each version of your game.

  ``` cpp
  // Make sure to make this call in your AppDelegate's
  // applicationDidFinishLaunching method, and before
  // initializing any other SOOMLA/GROW components
  // i.e. before CCSoomlaStore::initialize(...)
  grow::CCGrowHighway::initShared(
      __String::create("yourGameKey"),
      __String::create("yourEnvKey"));
  ```

  <img src="/img/tutorial_img/cocos_grow/dashboardKeys.png" alt="Game key and Env key" style="border:0;">

4. Initialize Sync:

	``` cpp
	// Make sure to make this call AFTER initializing HIGHWAY,
	// and BEFORE initializing STORE/PROFILE/LEVELUP
	bool modelSync = true; 	// Remote Economy Management - Synchronizes your game's
							 // economy model between the client and server - enables
							 // you to remotely manage your economy.

	bool stateSync = true; // Synchronizes the users' balances data with the server
						   // and across his other devices.
						   // Must be TRUE in order to use LEADERBOARDS.

	// State sync and Model sync can be enabled/disabled separately.
	grow::CCGrowSync::initShared(modelSync, stateSync);

	// LEADERBOARDS requires no initialization,
	// but it depends on SYNC initialization with stateSync=true
	```

2. Initialize the rest of the SOOMLA modules: `CCSoomlaStore`, `CCSoomlaProfile` and `CCSoomlaLevelUp`.

    ```cpp
    // `YourImplementationAssets` should implement the `CCStoreAssets` interface
    // and should include your entire virtual economy
    YourImplementationAssets *assets = YourImplementationAssets::create();

    __Dictionary *storeParams = __Dictionary::create();
    soomla::CCSoomlaStore::initialize(assets, storeParams);

    __Dictionary *profileParams = __Dictionary::create();
    soomla::CCSoomlaProfile::initialize(profileParams);

    // initialWorld - should be created here and contain all worlds and levels of the game
    soomla::CCSoomlaLevelUp::getInstance()->initialize(initialWorld);
    ```

<br>
<div class="info-box">The next steps are different according to which native platform you are building for.</div>

### **Instructions for iOS**

In your XCode project, perform the following steps:

1. Add `jansson` (**external/jansson/**) to your project (just add it as a source folder, make sure to check "create group").

2. For each of the following XCode projects:

  - `Cocos2dXHighway.xcodeproj` (**extensions/cocos2dx-highway/**)
  - `Cocos2dXCore.xcodeproj` (**extensions/soomla-cocos2dx-core/**)  
  - `Cocos2dXStore.xcodeproj` (**extensions/cocos2dx-store/**)
  - `Cocos2dXProfile.xcodeproj` (**extensions/cocos2dx-profile/**)  
  - `Cocos2dXLevelUp.xcodeproj` (**extensions/cocos2dx-levelup/**)

  perform the following:

  - Drag the project into your project
  - Add its targets to your **Build Phases->Target Dependencies**
  - Add the Products (\*.a) of the project to **Build Phases->Link Binary With Libraries**.

  ![alt text](/img/tutorial_img/cocos_grow/iosStep2.png "iOS Integration")

3. Add the following directories to **Build Settings->Header Search Paths** (with the `recursive` option):

  NOTE: This article assumes you have a `cocos2d` folder under your project folder which either contains the Cocos2d-x framework, or links to to its root folder.

 - `$(SRCROOT)/../cocos2d/extensions/soomla-cocos2dx-core/Soomla`
 - `$(SRCROOT)/../cocos2d/extensions/soomla-cocos2dx-core/build/ios/headers`
 - `$(SRCROOT)/../cocos2d/extensions/cocos2dx-store/Soomla`
 - `$(SRCROOT)/../cocos2d/extensions/cocos2dx-profile/Soomla`
 - `$(SRCROOT)/../cocos2d/extensions/cocos2dx-profile/build/ios/headers`
 - `$(SRCROOT)/../cocos2d/extensions/cocos2dx-levelup/Soomla`
 - `$(SRCROOT)/../cocos2d/extensions/cocos2dx-highway/Soomla`

  ![alt text](/img/tutorial_img/cocos_grow/headerSP.png "Header search paths")

5. Add the `-ObjC` to the **Build Setting->Other Linker Flags**

  ![alt text](/img/tutorial_img/ios_getting_started/linkerFlags.png "Linker Flags")

6. Make sure you have these 9 Frameworks linked to your XCode project:

  - Security
  - libsqlite3.0.dylib
  - StoreKit
  - CFNetwork
  - libicucore
  - SystemConfiguration
  - AdSupport
  - MediaPlayer
  - GameController

7. Connect the Profile module to a social network provider:

  - [Facebook](/cocos2dx/cpp/profile/Profile_GettingStarted#facebook-for-ios)

  - [Google+](/cocos2dx/cpp/profile/Profile_GettingStarted#google-for-ios)

  - [Twitter](/cocos2dx/cpp/profile/Profile_GettingStarted#twitter-for-ios)

That's it! Now all you have to do is build your XCode project and run your game.

### **Instructions for Android**

1. Import cocos2dx-highway, cocos2dx-store, cocos2dx-profile and cocos2dx-levelup modules into your project's Android.mk by adding the following:

    ```
    LOCAL_STATIC_LIBRARIES += cocos2dx_store_static
    LOCAL_STATIC_LIBRARIES += cocos2dx_profile_static
    LOCAL_STATIC_LIBRARIES += cocos2dx_levelup_static
    LOCAL_STATIC_LIBRARIES += cocos2dx_highway_static
    # add these lines along with your other LOCAL_STATIC_LIBRARIES

    $(call import-module, extensions/cocos2dx-store)
    $(call import-module, extensions/cocos2dx-profile)
    $(call import-module, extensions/cocos2dx-levelup)
    $(call import-module, extensions/cocos2dx-highway)
    # add these lines at the end of the file, along with the other import-module calls
    ```

2. Add the following jars to your android project's classpath (or into its libs dir):

  From `extensions/cocos2dx-highway/build/android`

    - AndroidViper.jar

    - Cocos2dxAndroidHighway.jar

    - google-play-services.jar

  From `extensions/soomla-cocos2dx-core/build/android`

    - SoomlaAndroidCore.jar

    - Cocos2dxAndroidCore.jar

    - square-otto-1.3.2.jar

  From `extensions/cocos2dx-store/build/android`

    - AndroidStore.jar

    - Cocos2dxAndroidStore.jar

  From `extensions/cocos2dx-profile/build/android`

    - AndroidProfile.jar

    - Cocos2dxAndroidProfile.jar

  From `extensions/cocos2dx-levelup/build/android`

    - AndroidLevelUp.jar

    - Cocos2dxAndroidLevelUp.jar

4. Update your `AndroidManifest.xml`:

  ``` xml
  <uses-permission android:name="android.permission.INTERNET"/>
  <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
  <uses-permission android:name="com.android.vending.BILLING"/>

  <!-- optional: required for uploadImage from SD card -->
  <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />

  <application
            ...
            android:name="com.soomla.SoomlaApp">
  </application>
  ```

5. Connect the Store module to your desired billing service:

  - [Google Play](/cocos2dx/cpp/store/Store_GettingStarted#google-play)

  - [Amazon Appstore](/cocos2dx/cpp/store/Store_GettingStarted#amazon)

6. Connect the Profile module to a social network provider:

  - [Facebook](/cocos2dx/cpp/profile/Profile_GettingStarted#facebook-for-android)

  - [Google+](/cocos2dx/cpp/profile/Profile_GettingStarted#google-for-android)

  - [Twitter](/cocos2dx/cpp/profile/Profile_GettingStarted#twitter-for-android)

That's it! Don't forget to run the **build_native.py** script so that SOOMLA sources will be built with cocos2d-x.

## Module usage & event handling

The next step is to create your game specific implementation for each of the modules. Use SOOMLA's awesome products to create better in-game economy, social interactions, game design and user experience.  
In order to be notified about (and handle) SOOMLA-related events, you will also need to create event-handling functions. Refer to the following sections for more information:

- **Store** - With Store you create your in-game virtual economy. It'll allow you to easily setup IAP and safely store your users' balances.  
[API](/cocos2dx/cpp/store/Store_Model) | [Main classes](/cocos2dx/cpp/store/Store_MainClasses) | [Events](/cocos2dx/cpp/store/Store_Events)

- **Profile** - This module will make your life extremely easy when it comes to connecting your users to Social Networks.  
[API](/cocos2dx/cpp/profile/Profile_MainClasses) | [Events](/cocos2dx/cpp/profile/Profile_Events)

- **LevelUp** - When you want to easily create your game structure and handle your users' state, LevelUp is your guy.  
[API](/cocos2dx/cpp/levelup/Levelup_Model) | [Events](/cocos2dx/cpp/levelup/Levelup_Events)

- **State & Economy Sync** - Your users want to get their balances, levels and other game state parameters when they switch devices. Now you can let them do it.  
[Events](/cocos2dx/cpp/grow/Grow_Sync#Events)

- **Social Leaderboards** - Make your users compete with each other using their favorite social network. GROW's Social Leaderboards will let your users compete using their Facebook, Twitter or Google+ accounts.  
[API](/cocos2dx/cpp/grow/Grow_Leaderboards) | [Events](/cocos2dx/cpp/grow/Grow_Leaderboards#Events)

## Example

Below is a short example of how to initialize SOOMLA's modules. We suggest you read about the different modules and their entities in SOOMLA's Knowledge Base: [Store](/cocos2dx/cpp/store/Store_Model), [Profile](/cocos2dx/cpp/profile/Profile_MainClasses), [LevelUp](/cocos2dx/cpp/levelup/Levelup_Model), [State & Economy Sync](/cocos2dx/cpp/grow/Grow_Sync) and [Social Leaderboards](/cocos2dx/cpp/grow/Grow_Leaderboards).

### CCStoreAssets

```cpp
/** ExampleAssets (your implementation of CCStoreAssets) **/
CCVirtualCurrency *coinCurrency = CCVirtualCurrency::create(
  CCString::create("Coins"),
  CCString::create(""),
  CCString::create("coin_currency_ID")
);

CCVirtualCurrencyPack *tenmuffPack = CCVirtualCurrencyPack::create(
  CCString::create("50 Coins"),                 // Name
  CCString::create("50 Coin pack"),             // Description
  CCString::create("coins_50"),                 // Item ID
  CCInteger::create(50),                        // Number of currencies in the pack
  CCString::create("coin_currency_ID"),         // Currency associated with this pack
  CCPurchaseWithMarket::create(                 // Purchase type
    CCString::create(50_COIN_PACK_PRODUCT_ID),
    CCDouble::create(0.99))
);

CCVirtualGood *shieldGood = CCSingleUseVG::create(
  CCString::create("Fruit Cake"),               // Name
  CCString::create("Delicios fruit cake!"),     // Description
  CCString::create("fruit_cake"),               // Item ID
  CCPurchaseWithVirtualItem::create(            // Purchase type
    CCString::create(MUFFIN_CURRENCY_ITEM_ID),
    CCInteger::create(225))
);

```
 <br>

```cpp
bool AppDelegate::applicationDidFinishLaunching() {

	// Add event listeners - Make sure to set the event handlers before you initialize
	Director::getInstance()->getEventDispatcher()->addCustomEventListener(grow::CCHighwayConsts::EVENT_FETCH_FRIENDS_STATES_FINISHED, CC_CALLBACK_1(AppDelegate::onFetchFriendsStatesFinished, this));
	Director::getInstance()->getEventDispatcher()->addCustomEventListener(soomla::CCProfileConsts::EVENT_GET_CONTACTS_FINISHED,                                                          CC_CALLBACK_1(AppDelegate::onGetContactsFinished, this));

	/** World **/
	CCWorld* mainWorld = CCWorld::create(
	CCString::create("mainWorld_ID")
	);

	/** Score **/
	CCScore *coinScore = CCVirtualItemScore::create(
	CCString::create("coinScore_ID"),
	CCString::create("coin_currency_ID")
	);

	/** Gate **/
	CCGate *gate = CCScheduleGate::create(
	CCString::create("gate_ID"),
	CCSchedule::createAnyTimeUnLimited()
	);

	/** Mission **/
	CCMission *mission = CCBalanceMission::create(
	CCString::create("mission_ID"),
	CCString::create("Collect 100 coins"),
	rewardsList,
	CCString::create("coinScore_ID"),
	CCInteger::create(100)
	);

	/** Levels **/
	// Add 5 levels to the main world with the gate, score, and mission templates we just created.
	mainWorld->batchAddLevelsWithTemplates(5, gate, score, mission);

	soomla::CCSoomla::initialize("ExampleCustomSecret");

	// Make sure to make this call in your AppDelegate's
	// applicationDidFinishLaunching method, and before
	// initializing any other SOOMLA/GROW components
	// i.e. before CCSoomlaStore::initialize(...)
	grow::CCGrowHighway::initShared(__String::create("yourGameKey"),
									__String::create("yourEnvKey"));

	// Make sure to make this call AFTER initializing HIGHWAY,
	// and BEFORE initializing STORE/PROFILE/LEVELUP
	bool modelSync = true; 	// Remote Economy Management - Synchronizes your game's
							 // economy model between the client and server - enables
							 // you to remotely manage your economy.

	bool stateSync = true; // Synchronizes the users' balances data with the server
						   // and across his other devices.
						   // Must be TRUE in order to use LEADERBOARDS.

	// State sync and Model sync can be enabled/disabled separately.
	grow::CCGrowSync::initShared(modelSync, stateSync);

	// LEADERBOARDS requires no initialization,
	// but it depends on SYNC initialization with stateSync=true

	/** Set up and initialize Store, Profile, and LevelUp **/
	ExampleAssets *assets = ExampleAssets::create();

	__Dictionary *storeParams = __Dictionary::create();
	storeParams->setObject(__String::create("ExamplePublicKey"), "androidPublicKey");

	soomla::CCSoomlaStore::initialize(assets, storeParams);

	__Dictionary *profileParams = __Dictionary::create();
	soomla::CCSoomlaProfile::initialize(profileParams);

	soomla::CCSoomlaLevelUp::getInstance()->initialize(mainWorld);
}

void AppDelegate::onFetchFriendsStatesFinished(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __Integer *providerId = dynamic_cast<__Integer *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_PROVIDER_ID));
    __Array *friendsStates = dynamic_cast<__Array *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_FRIENDS_STATES));
	cocos2d::log("Finished fetching friends states.");
    // ... Display leaderboards to the user ...
}

void AppDelegate::onGetContactsFinished(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __Integer *providerId = dynamic_cast<__Integer *>(eventData->objectForKey(CCProfileConsts::DICT_ELEMENT_PROVIDER));
    __Bool *hasMore = dynamic_cast<__Bool *>(eventData->objectForKey(CCProfileConsts::DICT_ELEMENT_HAS_MORE));
    __Array *contactsArray = dynamic_cast<__Array *>(eventData->objectForKey(CCProfileConsts::DICT_ELEMENT_CONTACTS));


    // ... handle page results ...
    __Array *friendsProfileIds = CCArray::create();
    for (Ref* userProfileRef : *contactsArray) {
        CCUserProfile *userProfile = (CCUserProfile*)userProfileRef;
        friendsProfileIds->addObject(userProfile->getProfileId());
    }

    CCGrowLeaderboards::fetchFriendsStates(providerId->getValue(), friendsProfileIds);

    if (hasMore != nullptr && hasMore->getValue()) {
        soomla::CCSoomlaProfile::getInstance()->getContacts(
                                                            soomla::FACEBOOK,
                                                            false,                              // going on with the pagination
                                                            NULL,                               // no reward
                                                            NULL                                // no error handling, to keep example simple
                                                            );
    } else {
        // no pages anymore
    }

}
```
