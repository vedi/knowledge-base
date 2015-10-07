---
layout: "content"
image: "Bundle"
title: "GrowViral"
text: "The perfect social engagement solution for your game. If you want your users to gift their friends in order to increase virality then this bundle is for you."
position: 12
theme: 'platforms'
collection: 'unity_grow'
module: 'grow'
platform: 'unity'
---

# GrowViral - Bundle

## Overview

GrowViral is the perfect social engagement solution for your game. If you want your users to gift their friends in order to increase virality then this bundle is for you. GrowViral connects you to GROW, SOOMLA's flagship - a community-driven data network. Mobile game studios can take advantage of the different GROW products in order to get valuable insights about their games' performance and increase retention and monetization. [Read more...](/unity/grow/Grow_About)

GrowViral includes:

- SOOMLA's open-source modules - [Store](/unity/store/Store_GettingStarted) and [Profile](/unity/profile/Profile_GettingStarted)
- [Gifting](/unity/grow/Grow_Gifting)
- [Analytics](/unity/grow/Grow_Analytics)
- [Whales Report](/unity/grow/Grow_WhalesReport)
- [Insights](/unity/grow/Grow_Insights)

## Integrating GrowViral

### New Game & Configurations

Go to the [GROW dashboard](http://dashboard.soom.la) and sign up \ login. Upon logging in, you will be directed to the main page of the dashboard. You will need to create a new game in order to start your journey with GROW.

1. In the games screen click on the "+" button to add a new game. If it's your first time in the dashboard, just click on the "+" button underneath the "Create your first game" label in the middle of the screen.

	  ![alt text](/img/tutorial_img/unity_grow/addNewApp.png "Add new app")

	* Once you created your game, you'll be redirected to a quick start process to download any of the GROW bundles (You can also click "Downloads" on the top right corner of the screen). Click on **GrowViral**. You'll see an instructions screen, you can continue with that or stay here for the extended version.  

2. Double-click on the downloaded Unity package, it'll import all the necessary files into your Unity project.

	![alt text](/img/tutorial_img/unity_grow/importUltimate.png "import")

3. Open your earliest loading scene.  Drag the `CoreEvents`, `StoreEvents`, `ProfileEvents` and `HighwayEvents` Prefabs from `Assets/Soomla/Prefabs` into the scene. You should see them listed in the "Hierarchy" panel.

	![alt text](/img/tutorial_img/unity_grow/prefabsNoLevelUp.png "Prefabs")

4. In the menu bar go to **Window > Soomla > Edit Settings**:

	![alt text](/img/tutorial_img/unity_grow/soomlaSettingsUltimate.png "SOOMLA Settings")

	a. **Change the value for "Soomla Secret"**: "Soomla Secret" is an encryption secret you provide that will be used to secure your data on the device. **NOTE:** Choose this secret wisely, you can't change it after you launch your game!

	<div class="info-box">Keep the SOOMLA secret in a safe place so you remember it. Changing secrets can cause your users to lose their balances and other important data.</div>

	b. **Copy the "Game Key" and "Environment Key"** given to you from the [dashboard](http://dashboard.soom.la) into the fields in the settings pane of the Unity Editor. At this point, you're probably testing your integration and you want to use the **Sandbox** environment key.

	<div class="info-box">The "game" and "environment" keys allow GROW to distinguish between multiple environments of your games. The dashboard pre-generates two fixed environments for your game: **Production** and **Sandbox**. When you decide to publish your game, **make sure to switch the environment key to <u>Production</u>**.  You can always generate more environments:  For example - you can choose to have a playground environment for your game's beta testers which will be isolated from your production environment and will thus prevent analytics data from being mixed between the two.  Another best practice might be to have a separate environment for each version of your game.</div>

	<img src="/img/tutorial_img/unity_grow/dashboardKeys.png" alt="Game key and Env key" style="border:0;">

	c. **Choose your social platform** by toggling Facebook, twitter or Google in the settings. Follow the instructions for integrating [Facebook](/unity/profile/Profile_GettingStarted#facebook), [Twitter](/unity/profile/Profile_GettingStarted#twitter) or [Google+](/unity/profile/Profile_GettingStarted#google-).

	d. If you're building for Android, click on the "Android Settings" option, and choose your billing provider. If you choose Google Play, you need to provide the Public Key, which is given to you from Google.

### Initialize modules

<div class="info-box">Make sure to initialize each module ONLY ONCE when your application loads, in the `Start()` function of a `MonoBehaviour` and **NOT** in the `Awake()` function. SOOMLA has its own `MonoBehaviour` and it needs to be "Awakened" before you initialize.</div>
<br>
<div class="info-box">The GrowHighway module is the module responsible for connecting your game to the GROW network. In order for it to operate it only needs to be initialized.</div>

1. Initialize Highway and Gifting:

	``` cs
	using Grow.Highway;
	using Grow.Gifting;

	// Make sure to make this call in your earliest loading scene,
	// and before initializing any other SOOMLA/GROW components
	// i.e. before SoomlaStore.Initialize(...)
	GrowHighway.Initialize();

	// Make sure to make this call BEFORE initializing STORE/PROFILE
	GrowGifting.Initialize();
	```

2. Initialize the open-source modules: Store & Profile (**AFTER** the initialization of Highway and Gifting).

	* **Initialize Store:** Create your own implementation of `IStoreAssets` in order to describe your specific game's assets ([example](https://github.com/soomla/unity3d-store/blob/master/Soomla/Assets/Examples/MuffinRush/MuffinRushAssets.cs)). Initialize SoomlaStore with the class you just created:

	``` cs
	SoomlaStore.Initialize(new YourStoreAssetsImplementation());
	```

	* **Initialize Profile:**

    **NOTE:** `SoomlaProfile` will initialize the social providers for you. Do NOT initialize them on your own (for example, don't call `FB.Init()`).

	``` cs
	SoomlaProfile.Initialize();
	```

## Module usage & event handling

The next step is to create your game specific implementation for each of the modules. Use SOOMLA's awesome products to create better in-game economy, social interactions and user experience.  
In order to be notified about (and handle) SOOMLA-related events, you will also need to create event-handling functions. Refer to the following sections for more information:

- **Store** - With Store you create your in-game virtual economy. It'll allow you to easily setup IAP and safely store your users' balances.  
[API](/unity/store/Store_Model) | [Main classes](/unity/store/Store_MainClasses) | [Events](/unity/store/Store_Events)

- **Profile** - This module will make your life extremely easy when it comes to connecting your users to Social Networks.  
[API](/unity/profile/Profile_MainClasses) | [Events](/unity/profile/Profile_Events)

- **Gifting** - Increase the virality of your game by letting your users gift each other with any virtual item in your game.  
[API](/unity/grow/Grow_Gifting) | [Events](/unity/grow/Grow_Gifting#Events)

## Example

Below is a short example of how to initialize SOOMLA's modules. We suggest you read about the different modules and their entities in SOOMLA's Knowledge Base: [Store](/unity/store/Store_Model), [Profile](/unity/profile/Profile_MainClasses) and [Gifting](/unity/grow/Grow_Gifting).

### IStoreAssets

``` cs
public class ExampleAssets : IStoreAssets {

	public int GetVersion() {
		return 0;
	}

	// NOTE: Even if you have no use in one of these functions, you still need to
	// implement them all and just return an empty array.

	public VirtualCurrency[] GetCurrencies() {
		return new VirtualCurrency[]{COIN_CURRENCY};
	}

	public VirtualGood[] GetGoods() {
		return new VirtualGood[] {SHIELD_GOOD, FIVE_SHIELD_GOOD};
	}

	public VirtualCurrencyPack[] GetCurrencyPacks() {
		return new VirtualCurrencyPack[] {TEN_COIN_PACK};
	}

	public VirtualCategory[] GetCategories() {
		return new VirtualCategory[]{GENERAL_CATEGORY};
	}

	/** Virtual Currencies **/
	public static VirtualCurrency COIN_CURRENCY = new VirtualCurrency(
		"Coin currency",                  // Name
		"Collect coins to buy items",     // Description
		"currency_coin"                   // Item ID
		);

	/** Virtual Currency Packs **/
	public static VirtualCurrencyPack TEN_COIN_PACK = new VirtualCurrencyPack(
		"10 Coins",                         // Name
		"This is a 10-coin pack",           // Description
		"coins_10",                         // Item ID
		10,                                 // Number of currencies in the pack
		COIN_CURRENCY.ID,                   // The currency associated with this pack
		new PurchaseWithMarket(             // Purchase type
	                       "[YOUR_COIN_PACK_MARKET_PRODUCT_ID]",    // Product ID
	                       0.99)                           			// Initial price
		);

	/** Virtual Goods **/

	// Shield that can be purchased for 150 coins.
	public static VirtualGood SHIELD_GOOD = new SingleUseVG(
		"Shield",                           // Name
		"Shields you from monsters",        // Description
		"shield_good",                      // Item ID
		new PurchaseWithVirtualItem(        // Purchase type
	                            COIN_CURRENCY.ID,               // Virtual item to pay with
	                            150)                            // Payment amount
		);

	// Pack of 5 shields that can be purchased for $2.99.
	public static VirtualGood FIVE_SHIELD_GOOD = new SingleUsePackVG(
		SHIELD_GOOD.ID,
		5,
		"5 Shields",                        // Name
		"This is a 5-shield pack",          // Description
		"shield_5",                         // Item ID
		new PurchaseWithMarket(             // Purchase type
	                       "[YOUR_SHIELD_MARKET_PRODUCT_ID]",   // Product ID
	                       2.99)                           		// Initial price
		);

	/** Virtual Categories **/

	public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
		"General", new List<string>(new string[] {SHIELD_GOOD.ItemId})
	);
}
```

<br>
### Initialization

``` cs
using Soomla;
using Soomla.Store;
using Soomla.Profile;
using Grow.Highway;
using Grow.Gifting;
using System.Collections.Generic;

public class ExampleWindow : MonoBehaviour {

	//
	// Various event handling methods
	//
	public void onGoodBalanceChanged(VirtualGood good, int balance, int amountAdded) {
		SoomlaUtils.LogDebug("TAG", good.ID + " now has a balance of " + balance);
	}
	public static void onLoginFinished(UserProfile userProfileJson, bool autoLogin, string payload){
		SoomlaUtils.LogDebug("TAG", "Logged in as: " + userProfileJson.toJSONObject().print());
	}
	void OnGrowGiftingInitialized () {
		Debug.Log("GROW Gifting has been initialized.");
	}
	void OnGiftHandOutSuccess (Gift gift){
		// ... Show a nice animation of receiving the gift ...
	}
	void OnGiftSendFinished (Gift gift) {
		Debug.Log("Successfully sent " + gift.Payload.AssociatedItemId);
	}

	//
	// Initialize SOOMLA's modules
	//
	void Start () {

		// Setup all event handlers - Make sure to set the event handlers before you initialize
		StoreEvents.OnGoodBalanceChanged += onGoodBalanceChanged;
		ProfileEvents.OnLoginFinished += onLoginFinished;

		HighwayEvents.OnGrowGiftingInitialized += OnGrowGiftingInitialized;
		HighwayEvents.OnGiftHandOutSuccess += OnGiftHandOutSuccess;
		HighwayEvents.OnGiftSendFinished += OnGiftSendFinished;

		// Make sure to make this call in your earliest loading scene,
		// and before initializing any other SOOMLA/GROW components
		// i.e. before SoomlaStore.Initialize(...)
		GrowHighway.Initialize();

		// Make sure to make this call BEFORE initializing STORE/PROFILE
		GrowGifting.Initialize();

		SoomlaStore.Initialize(new ExampleAssets());
		SoomlaProfile.Initialize();

	}
}
```
