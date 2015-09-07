---
layout: "content"
image: "Bundle"
title: "GrowViral"
text: "The perfect social engagement solution for your game. If you want your users to gift their friends in order to increase virality then this bundle is for you."
position: 12
theme: 'platforms'
collection: 'cocos2dx_grow'
module: 'grow'
lang: 'cpp'
platform: 'cocos2dx'
---

# GrowViral - Bundle

## Overview

GrowViral is the perfect social engagement solution for your game. If you want your users to gift their friends in order to increase virality then this bundle is for you. GrowViral connects you to GROW, SOOMLA's flagship - a community-driven data network. Mobile game studios can take advantage of the different GROW products in order to get valuable insights about their games' performance and increase retention and monetization. [Read more...](/cocos2dx/cpp/grow/Grow_About)

GrowViral includes:

- SOOMLA's open-source modules - [Store](/cocos2dx/cpp/store/Store_GettingStarted) and [Profile](/cocos2dx/cpp/profile/Profile_GettingStarted)
- [Gifting](/cocos2dx/cpp/grow/Grow_Gifting)
- [Analytics](/cocos2dx/cpp/grow/Grow_Analytics)
- [Whales Report](/cocos2dx/cpp/grow/Grow_WhalesReport)
- [Insights](/cocos2dx/cpp/grow/Grow_Insights)

## Integrating GrowViral

### New Game & Configurations

Go to the [GROW dashboard](http://dashboard.soom.la) and sign up \ login. Upon logging in, you will be directed to the main page of the dashboard. You will need to create a new game in order to start your journey with GROW.

<div class="info-box">If you didn't already, clone the Cocos2d-x framework from [here](https://github.com/cocos2d/cocos2d-x), or download it from the [Cocos2d-x website](http://www.cocos2d-x.org/download). Make sure the version you clone is supported by SOOMLA's modules (the tag is the version).</div>

1. In the games screen click on the "+" button to add a new game. If it's your first time in the dashboard, just click on the "+" button underneath the "Create your first game" label in the middle of the screen.

	  ![alt text](/img/tutorial_img/unity_grow/addNewApp.png "Add new app")

	* Once you created your game, you'll be redirected to a quick start process to download any of the GROW bundles (You can also click "Downloads" on the top right corner of the screen). Click on **GrowViral**. You'll see an instructions screen, you can continue with that or stay here for the extended version.  

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

4. Initialize Gifting:

	``` cpp
	// Make sure to make this call AFTER initializing SYNC,
	// and BEFORE initializing STORE/PROFILE
	grow::CCGrowGifting::initShared();
	```

2. Initialize the rest of the SOOMLA modules: `CCSoomlaStore` and `CCSoomlaProfile`.

    ```cpp
    // `YourImplementationAssets` should implement the `CCStoreAssets` interface
    // and should include your entire virtual economy
    YourImplementationAssets *assets = YourImplementationAssets::create();

    __Dictionary *storeParams = __Dictionary::create();
    soomla::CCSoomlaStore::initialize(assets, storeParams);

    __Dictionary *profileParams = __Dictionary::create();
    soomla::CCSoomlaProfile::initialize(profileParams);
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
    LOCAL_STATIC_LIBRARIES += cocos2dx_highway_static
    # add these lines along with your other LOCAL_STATIC_LIBRARIES

    $(call import-module, extensions/cocos2dx-store)
    $(call import-module, extensions/cocos2dx-profile)
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

The next step is to create your game specific implementation for each of the modules. Use SOOMLA's awesome products to create better in-game economy, social interactions and user experience.  
In order to be notified about (and handle) SOOMLA-related events, you will also need to create event-handling functions. Refer to the following sections for more information:

- **Store** - With Store you create your in-game virtual economy. It'll allow you to easily setup IAP and safely store your users' balances.  
[API](/cocos2dx/cpp/store/Store_Model) | [Main classes](/cocos2dx/cpp/store/Store_MainClasses) | [Events](/cocos2dx/cpp/store/Store_Events)

- **Profile** - This module will make your life extremely easy when it comes to connecting your users to Social Networks.  
[API](/cocos2dx/cpp/profile/Profile_MainClasses) | [Events](/cocos2dx/cpp/profile/Profile_Events)

- **Gifting** - Increase the virality of your game by letting your users gift each other with any virtual item in your game.  
[API](/cocos2dx/cpp/grow/Grow_Gifting) | [Events](/cocos2dx/cpp/grow/Grow_Gifting#Events)

## Example

Below is a short example of how to initialize SOOMLA's modules. We suggest you read about the different modules and their entities in SOOMLA's Knowledge Base: [Store](/cocos2dx/cpp/store/Store_Model), [Profile](/cocos2dx/cpp/profile/Profile_MainClasses) and [Gifting](/cocos2dx/cpp/grow/Grow_Gifting).

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
### Initialization

```cpp
using namespace grow;

bool AppDelegate::applicationDidFinishLaunching() {
	// Setup all event handlers - Make sure to set the event handlers before you initialize
	Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GROW_GIFTING_INITIALIZED, CC_CALLBACK_1(Example::onGrowGiftingInitialized, this));
	Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFT_HAND_OUT_SUCCESS, CC_CALLBACK_1(Example::onGiftHandOutSuccess, this));
	Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFT_SEND_FINISHED, CC_CALLBACK_1(Example::onGiftSendFinished, this));

	soomla::CCSoomla::initialize("ExampleCustomSecret");

	// Make sure to make this call in your AppDelegate's
	// applicationDidFinishLaunching method, and before
	// initializing any other SOOMLA/GROW components
	// i.e. before CCSoomlaStore::initialize(...)
	grow::CCGrowHighway::initShared(__String::create("yourGameKey"),
									__String::create("yourEnvKey"));

	// Make sure to make this call AFTER initializing SYNC,
	// and BEFORE initializing STORE/PROFILE/LEVELUP
	grow::CCGrowGifting::initShared();

	/** Set up and initialize Store, Profile, and LevelUp **/
	ExampleAssets *assets = ExampleAssets::create();

	__Dictionary *storeParams = __Dictionary::create();
	storeParams->setObject(__String::create("ExamplePublicKey"), "androidPublicKey");

	soomla::CCSoomlaStore::initialize(assets, storeParams);

	__Dictionary *profileParams = __Dictionary::create();
	soomla::CCSoomlaProfile::initialize(profileParams);
}


void AppDelegate::onGrowGiftingInitialized(EventCustom *event) {
	cocos2d::log("GROW Gifting has been initialized.");
}

void AppDelegate::onGiftHandOutSuccess(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    CCGift *gift = dynamic_cast<CCGift *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIFT));
    // ... Show a nice animation of receiving the gift ...
}

void AppDelegate::onGiftSendFinished(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    CCGift *gift = dynamic_cast<CCGift *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIFT));
	cocos2d::log("Successfully sent %s", gift->getPayload()->getAssociatedItemId()->getCString());
}
```
