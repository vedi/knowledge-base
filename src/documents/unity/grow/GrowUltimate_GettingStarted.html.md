---
layout: "content"
image: "Tutorial"
title: "GrowUltimate"
text: "Get started with GrowUltimate for Unity. Includes all of SOOMLA's modules: CORE, STORE, PROFILE, LEVELUP and HIGHWAY. Learn how to easily integrate everything SOOMLA has to offer into your game."
position: 12
theme: 'platforms'
collection: 'unity_grow'
module: 'grow'
platform: 'unity'
---

# GrowUltimate

## Overview

GrowUltimate is a part of SoomlaGrow, which is our flagship community-driven analytics dashboard. Developers using GROW can gain valuable insights about their games' performance and compare the data to benchmarks of other games in the GROW community. [Read more...](/unity/grow/Grow_About)

**Note:** GrowUltimate uses all of SOOMLA's open source modules: STORE, PROFILE, LEVELUP. This document describes how to incorporate these modules as part of the setup. You may choose to use only specific modules, however, to benefit from the full power of GrowUltimate we recommend that you integrate STORE, PROFILE and LEVELUP.

The HIGHWAY module is responsible for connecting your game to the GROW network, and includes features such as [SYNC](/unity/grow/Grow_Sync), [INSIGHTS](/unity/grow/Grow_Insights), [GIFTING](/unity/grow/Grow_Gifting) and [LEADERBOARDS](/unity/grow/Grow_Leaderboards).

## Setup GrowUltimate

Go to the [GROW dashboard](http://dashboard.soom.la) and sign up \ login. Upon logging in, you will be directed to the main page of the dashboard. On the left side panel, you can click on "Demo Game" in order to know what to expect once you start using GROW.

1. Click on the right pointing arrow next to "Demo Game" > "Add New App" and fill in the required fields.

	  ![alt text](/img/tutorial_img/unity_grow/addNewApp.png "Add new app")

* Go to the "Download" window on the left side-panel, or click [here](http://dashboard.soom.la/downloads), and choose "Unity". Download the **GROW Spend**.

* Double-click on the downloaded Unity package, it'll import all the necessary files into your Unity project.

	![alt text](/img/tutorial_img/unity_grow/importUltimate.png "import")

* Open your earliest loading scene.  Drag the `CoreEvents`, `StoreEvents`, `ProfileEvents`, `LevelUpEvents`, and `HighwayEvents` Prefabs from `Assets/Soomla/Prefabs` into the scene. You should see them listed in the "Hierarchy" panel.

	![alt text](/img/tutorial_img/unity_grow/prefabsUltimate.png "Prefabs")

* In the menu bar go to **Window > Soomla > Edit Settings**:

	![alt text](/img/tutorial_img/unity_grow/soomlaSettingsAll.png "SOOMLA Settings")

	a. **Change the value for "Soomla Secret"**: "Soomla Secret" is an encryption secret you provide that will be used to secure your data. **NOTE:** Choose this secret wisely, you can't change it after you launch your game!

	<div class="info-box">If you're already using earlier versions of SOOMLA, make sure that you use the same secret. Changing secrets can cause your users to lose their balances.</div>

	b. **Copy the "Game Key" and "Environment Key"** given to you from the [dashboard](http://dashboard.soom.la) into the fields in the settings pane of the Unity Editor. At this point, you're probably testing your integration and you want to use the **Sandbox** environment key.

	Explanation: The "game" and "env" keys allow for your game to distinguish multiple environments for the same game. The dashboard pre-generates two fixed environments for your game: **Production** and **Sandbox**. When you decide to publish your game, make sure to switch the env key to **Production**.  You can always generate more environments.  For example - you can choose to have a playground environment for your game's beta testers which will be isolated from your production environment and will thus prevent analytics data from being mixed between the two.  Another best practice is to have a separate environment for each version of your game.

	![alt text](/img/tutorial_img/unity_grow/dashboardKeys.png "Game key and Env key")

	c. **Choose your social platform** by toggling Facebook, twitter or Google in the settings. Follow the directions for integrating [Facebook](/unity/profile/Profile_GettingStarted#facebook), [Twitter](/unity/profile/Profile_GettingStarted#twitter) or [Google+](/unity/profile/Profile_GettingStarted#google-).

	d. If you're building for Android, click on the "Android Settings" option, and choose your billing provider. If you choose Google Play, you need to provide the Public Key, which is given to you from Google.

* Fraud Protection (RECOMMENDED):

	Fraud Protection is using SOOMLA's complimentary validation service to validate the receipt of every purchase made in your game. By using the GROW services you also get **Advanced Receipt Verification** to fully protect your game from fraudsters.
	To activate Fraud Protection:

	- In the menu bar go to **Window > Soomla > Edit Settings**.

	- Check the "Receipt Validation" option under the relevant platform (Android - Google Play / iOS).

	- (Google Play only) Follow the instructions posted [here](/android/store/store_googleplayverification/) to fill in the relevant fields.

* Initialize modules:

	<div class="info-box">Make sure to initialize each module ONLY ONCE when your application loads, in the `Start()` function of a `MonoBehaviour` and **NOT** in the `Awake()` function. SOOMLA has its own `MonoBehaviour` and it needs to be "Awakened" before you initialize.</div>

	* Initialize HIGHWAY, INSIGHTS, SYNC and GIFTING:

		``` cs
		using Soomla.Highway;
		using Soomla.Insights;
		using Soomla.Sync;
		using Soomla.Gifting;
		using Soomla.Query;

		// Make sure to make this call in your earlieast loading scene,
		// and before initializing any other SOOMLA components
		// i.e. before SoomlaStore.Initialize(...)
		SoomlaHighway.Initialize();

		// Make sure to make this call AFTER initializing HIGHWAY
	    SoomlaInsights.Initialize();

		// Make sure to make this call AFTER initializing HIGHWAY,
		// and BEFORE initializing STORE/PROFILE/LEVELUP
		bool economySync = true; // Remote Economy Management - Synchronizes your game's
                               // economy between the client and server - enables
                                // you to remotely manage your economy.

		bool stateSync = true; // Synchronizes the users' balances data with the server
                                // and across his other devices.
								// Make sure you set this to TRUE in order to use LEADERBOARDS.

		// State sync and Economy sync can be enabled/disabled separately.
		SoomlaSync.Initialize(economySync, stateSync);

		// LEADERBOARDS requires no initialization,
		// but it depends on SYNC initialization with stateSync=true

		// Make sure to make this call AFTER initializing SYNC,
		// and BEFORE initializing STORE/PROFILE/LEVELUP
		SoomlaGifting.Initialize();
		```

	* Initialize the rest of the modules: STORE, PROFILE & LEVELUP (**AFTER** the initialization of HIGHWAY, SYNC and GIFTING).

		* **Initialize STORE:** Create your own implementation of `IStoreAssets` in order to describe your specific game's assets ([example](https://github.com/soomla/unity3d-store/blob/master/Soomla/Assets/Examples/MuffinRush/MuffinRushAssets.cs)). Initialize SoomlaStore with the class you just created:

		``` cs
		SoomlaStore.Initialize(new YourStoreAssetsImplementation());
		```

		* **Initialize PROFILE:**

	    **NOTE:** SoomlaProfile will initialize the social providers for you. Do NOT initialize them on your own (for example, don't call `FB.Init()`).

		``` cs
		SoomlaProfile.Initialize();
		```

		* **Initialize LEVELUP:** Create your own _Initial World_ which should contain the entire 'blueprint' of the game (see [Model Overview](/unity/levelup/Levelup_Model)). Initialize _LevelUp_ with the world you just created:

		``` cs
		SoomlaLevelUp.Initialize(initialWorld);
		```

## Module usage & event handling

The next step is to create your game specific implementation for each of the modules. In order to be notified about (and handle) SOOMLA-related events, you will also need to create event handler functions. Refer to the following sections for more information:

- **STORE** [API](/unity/store/Store_Model), [Main classes](/unity/store/Store_MainClasses) & [Events](/unity/store/Store_Events)

- **PROFILE** [API](/unity/profile/Profile_MainClasses) & [Events](/unity/profile/Profile_Events)

- **LEVELUP** [API](/unity/levelup/Levelup_Model) & [Events](/unity/levelup/Levelup_Events)

- **INSIGHTS** [API](/unity/grow/Grow_Insights#MainClasses&Methods) & [Events](/unity/grow/Grow_Insights#Events)

- **SYNC** [Events](/unity/grow/Grow_Sync#Events)

- **LEADERBOARDS** [API](/unity/grow/Grow_Leaderboards) & [Events](/unity/grow/Grow_Leaderboards#Events)

- **GIFTING** [API](/unity/grow/Grow_Gifting) & [Events](/unity/grow/Grow_Gifting#Events)

## Example

Below is a short example of how to initialize SOOMLA's modules. We suggest you read about the different modules and their entities in SOOMLA's Knowledge Base: [STORE](/unity/store/Store_Model), [PROFILE](/unity/profile/Profile_MainClasses), [LEVELUP](/unity/levelup/Levelup_Model), [SYNC](/unity/grow/Grow_Sync), [INSIGHTS](/unity/grow/Grow_Insights), [GIFTING](/unity/grow/Grow_Gifting) and [LEADERBOARDS](/unity/grow/Grow_Leaderboards).

### IStoreAssets

``` cs
public class ExampleAssets : IStoreAssets {

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
        "currency_coin",                    // The currency associated with this pack
        new PurchaseWithMarket(             // Purchase type
            TEN_COIN_PACK_PRODUCT_ID,       // Product ID
            0.99)                           // Initial price
    );

    /** Virtual Goods **/

    // Shield that can be purchased for 150 coins.
    public static VirtualGood SHIELD_GOOD = new SingleUseVG(
        "Shield",                           // Name
	    "Shields you from monsters",        // Description
	    "shield_good",                      // Item ID
        new PurchaseWithVirtualItem(        // Purchase type
            "currency_coin",                // Virtual item to pay with
            150)                            // Payment amount
    );

    // Pack of 5 shields that can be purchased for $2.99.
    public static VirtualGood 5_SHIELD_GOOD = new SingleUsePackVG(
        "5 Shields",                        // Name
	    "This is a 5-shield pack",          // Description
	    "shield_5",                         // Item ID
        new PurchaseWithMarket(             // Purchase type
            SHIELD_PACK_PRODUCT_ID,         // Product ID
            2.99)                           // Initial price
    );

    ...
}
```

<br>
### Initialization

``` cs
using Soomla;
using Soomla.Store;
using Soomla.Profile;
using Soomla.Levelup;
using Soomla.Highway;
using Soomla.Insights;
using Soomla.Sync;
using Soomla.Gifting;
using Soomla.Query;
...


public class ExampleWindow : MonoBehaviour {

	//
	// Utility method for creating the game's worlds
	// and levels hierarchy
	//
	private World createMainWorld() {
		World worldA = new World("world_a");
		World worldB = new World("world_b");

		Reward coinReward = new VirtualItemReward(
			"coinReward",                       // ID
			"100 Coins",                        // Name
			COIN_CURRENCY.ID,                   // Associated item ID
			100                                 // Amount
		);

		Mission likeMission = new SocialLikeMission(
			"likeMission",                      // ID
			"Like Mission",                     // Name
			new List<Reward>(){coinReward},     // Reward
			Soomla.Profile.Provider.FACEBOOK,   // Social Provider
			"[page name]"                       // Page to "Like"
		);

		// Add 10 levels to each world
		worldA.BatchAddLevelsWithTemplates(10, null,
			null, new List<Mission>(){likeMission});
		worldB.BatchAddLevelsWithTemplates(10, null,
			null, new List<Mission>(){likeMission});

		// Create a world that will contain all worlds of the game
		World mainWorld = new World("main_world");
		mainWorld.InnerWorldsMap.Add(worldA.ID, worldA);
		mainWorld.InnerWorldsMap.Add(worldB.ID, worldB);

		return mainWorld;
	}

	//
	// Various event handling methods
	//
	public void onGoodBalanceChanged(VirtualGood good, int balance, int amountAdded) {
		SoomlaUtils.LogDebug("TAG", good.ID + " now has a balance of " + balance);
	}
	public static void onLoginFinished(UserProfile userProfileJson, string payload){
		SoomlaUtils.LogDebug("TAG", "Logged in as: " + UserProfile.toJSONObject().print());
	}
	public void onLevelStarted(Level level) {
		SoomlaUtils.LogDebug("TAG", "Level started: " + level.toJSONObject().print());
	}
	public void onSoomlaInsightsInitialized () {
    	Debug.Log("Soomla insights has been initialized.");
	}
	public void onSoomlaInsightsRefreshFinished (){
	    if (SoomlaInsights.UserInsights.PayInsights.PayRankByGenre[Genre.Educational] > 3) {
	        // ... Do stuff according to your business plan ...
	    }
	}


	//
	// Initialize all of SOOMLA's modules
	//
	void Start () {
		...

		// Setup event handlers - Make sure to set the event handlers before you initialize
		StoreEvents.OnGoodBalanceChanged += onGoodBalanceChanged;
		ProfileEvents.OnLoginFinished += onLoginFinished;
		LevelUpEvents.OnLevelStarted += onLevelStarted;

	    HighwayEvents.OnInsightsInitialized += onSoomlaInsightsInitialized;
    	HighwayEvents.OnInsightsRefreshFinished += onSoomlaInsightsRefreshFinished;

		//TODO add gifting Events
		//TODO add query events

		// Make sure to make this call in your earlieast loading scene,
		// and before initializing any other SOOMLA components
		// i.e. before SoomlaStore.Initialize(...)
		SoomlaHighway.Initialize();

		// Make sure to make this call AFTER initializing HIGHWAY
	    SoomlaInsights.Initialize();

		// Make sure to make this call AFTER initializing HIGHWAY,
		// and BEFORE initializing STORE/PROFILE/LEVELUP
		bool economySync = true; // Remote Economy Management - Synchronizes your game's
                               // economy between the client and server - enables
                                // you to remotely manage your economy.

		bool stateSync = true; // Synchronizes the users' balances data with the server
                                // and across his other devices.

		// State sync and Economy sync can be enabled/disabled separately.
		SoomlaSync.Initialize(economySync, stateSync);

		// Make sure to make this call AFTER initializing SYNC,
		// and BEFORE initializing STORE/PROFILE/LEVELUP
		SoomlaGifting.Initialize();

		SoomlaStore.Initialize(new ExampleAssets());
		SoomlaProfile.Initialize();
		SoomlaLevelup.Initialize(createMainWorld());

	}
}
```
