---
layout: "content"
image: "Bundle"
title: "GrowUltimate"
text: "The perfect All In One solution for your game. If you want your users to have the perfect experience in your game then this bundle is for you."
position: 14
theme: 'platforms'
collection: 'unity_grow'
module: 'grow'
platform: 'unity'
---

# GrowUltimate - Bundle

## Overview

GrowUltimate is the perfect All In One solution for your game. If you want your users to have the perfect experience in your game then this bundle is for you. GrowUltimate connects you to GROW, SOOMLA's flagship - a community-driven data network. Mobile game studios can take advantage of the different GROW products in order to get valuable insights about their games' performance and increase retention and monetization. [Read more...](/unity/grow/Grow_About)

GrowUltimate includes:

- All of SOOMLA's open-source modules - [Store](/unity/store/Store_GettingStarted), [Profile](/unity/profile/Profile_GettingStarted) and [LevelUp](/unity/levelup/Levelup_GettingStarted)
- [State & Economy Sync](/unity/grow/Grow_Sync)
- [Gifting](/unity/grow/Grow_Gifting)
- [IAP Fraud Protection](/unity/grow/Grow_FraudProtection)
- [Social Leaderboards](/unity/grow/Grow_Leaderboards)
- [Analytics](/unity/grow/Grow_Analytics)
- [Whales Report](/unity/grow/Grow_WhalesReport)
- [Insights](/unity/grow/Grow_Insights)

## Integrating GrowUltimate

### New Game & Configurations

Go to the [GROW dashboard](http://dashboard.soom.la) and sign up \ login. Upon logging in, you will be directed to the main page of the dashboard. You will need to create a new game in order to start your journey with GROW.

1. In the games screen click on the "+" button to add a new game. If it's your first time in the dashboard, just click on the "+" button underneath the "Create your first game" label in the middle of the screen.

	  ![alt text](/img/tutorial_img/unity_grow/addNewApp.png "Add new app")

	* Once you created your game, you'll be redirected to a quick start process to download any of the GROW bundles (You can also click "Downloads" on the top right corner of the screen). Click on **GrowUltimate**. You'll see an instructions screen, you can continue with that or stay here for the extended version.  

2. Double-click on the downloaded Unity package, it'll import all the necessary files into your Unity project.

	![alt text](/img/tutorial_img/unity_grow/importUltimate.png "import")
	
	<div class="info-box">Starting from `SOOMLA Unity3D LevelUp 1.0.19`, SOOMLA changed the location of binaries in `Plugins` directory. If you're upgrading from version lower than 1.0.19 to version higher than 1.0.19, please remove the following binaries manually:
            <ul>
              <li>`Assets/Plugins/iOS/libSoomlaiOSCore.a`</li>
              <li>`Assets/Plugins/iOS/libUnitySoomlaiOSCore.a`</li>
              <li>`Assets/Plugins/Android/SoomlaAndroidCore.jar`</li>
              <li>`Assets/Plugins/Android/UnitySoomlaAndroidCore.jar`</li>
              <li>`Assets/Plugins/Android/square-otto-1.3.2.jar`</li>
              <li>`Assets/Plugins/iOS/libAFNetworking.a`</li>
              <li>`Assets/Plugins/iOS/libSoomlaiOSRoadster.a`</li>
              <li>`Assets/Plugins/iOS/libUnityiOSHighway.a`</li>
              <li>`Assets/Plugins/Android/UnityAndroidHighway.jar`</li>
              <li>`Assets/Plugins/Android/AndroidViper.jar`</li>              
              <li>`Assets/Plugins/iOS/libSoomlaiOSSLevelUp.a`</li>              
              <li>`Assets/Plugins/iOS/libUnityiOSLevelUp.a`</li>
              <li>`Assets/Plugins/Android/AndroidLevelUp.jar`</li>              
              <li>`Assets/Plugins/Android/UnityAndroidLevelUp.jar`</li>                            
              <li>`Assets/Plugins/iOS/libSoomlaiOSSProfile.a`</li>      
              <li>`Assets/Plugins/iOS/libUnityiOSProfile.a`</li>            
              <li>`Assets/Plugins/iOS/libSoomlaiOSSProfileTwitter.a` (if you're using Twitter)</li>
              <li>`Assets/Plugins/iOS/libSoomlaiOSSProfileGoogle.a`(if you're using Google+)</li>
              <li>`Assets/Plugins/Android/AndroidProfile.jar`</li>      
              <li>`Assets/Plugins/Android/UnityAndroidProfile.jar`</li>            
              <li>`Assets/Plugins/Android/AndroidProfileTwitter.jar` (if you're using Twitter)</li>
              <li>`Assets/Plugins/Android/twitter4j-asyc-4.0.2.jar` (if you're using Twitter)</li>
              <li>`Assets/Plugins/Android/twitter4j-core-4.0.2.jar` (if you're using Twitter)</li>
              <li>`Assets/Plugins/Android/AndroidProfileGoogle.jar` (if you're using Google+)</li>
              <li>`Assets/Plugins/Android/google-play-services_lib` (if you're using Google+)</li>
              <li>`Assets/Plugins/iOS/libSoomlaiOSSStore.a`</li>      
              <li>`Assets/Plugins/iOS/libUnityiOSStore.a`</li>
              <li>`Assets/Plugins/Android/AndroidStore.jar`</li>      
              <li>`Assets/Plugins/Android/UnityAndroidStore.jar`</li>      
              <li>`Assets/Plugins/Android/square-otto-1.3.2.jar`</li>
              <li>`Assets/Plugins/Android/AndroidStoreAmazon.jar` (if you're using Amazon as billing service)</li>
              <li>`Assets/Plugins/Android/in-app-purchasing-2.0.1.jar` (if you're using Amazon as billing service)</li>
              <li>`Assets/Plugins/Android/AndroidStoreGooglePlay.jar` (if you're using Google Play as billing service)</li>                                                    
            </ul>		        	    
    </div>

3. ~~Open your earliest loading scene.  Drag the `CoreEvents`, `StoreEvents`, `ProfileEvents`, `LevelUpEvents` and `HighwayEvents` Prefabs from `Assets/Soomla/Prefabs` into the scene. You should see them listed in the "Hierarchy" panel.~~

	<div class="info-box">This step is no longer required starting from: <br>
	Core    v1.2.0 <br>
	Store   v1.9.0 <br>
	Profile v2.2.0 <br>
	LevelUp v1.1.0 <br>
	Highway v2.1.0</div>

	![alt text](/img/tutorial_img/unity_grow/prefabsAll.png "Prefabs")

4. In the menu bar go to **Window > Soomla > Edit Settings**:

	![alt text](/img/tutorial_img/unity_grow/soomlaSettingsUltimate.png "SOOMLA Settings")

	a. **Change the value for "Soomla Secret"**: "Soomla Secret" is an encryption secret you provide that will be used to secure your data on the device. **NOTE:** Choose this secret wisely, you can't change it after you launch your game!

	<div class="info-box">Keep the SOOMLA secret in a safe place so you remember it. Changing secrets can cause your users to lose their balances and other important data.</div>

	b. **Copy the "Game Key" and "Environment Key"** given to you from the [dashboard](http://dashboard.soom.la) into the fields in the settings pane of the Unity Editor. At this point, you're probably testing your integration and you want to use the **Sandbox** environment key.

	<div class="info-box">The "game" and "environment" keys allow GROW to distinguish between multiple environments of your games. The dashboard pre-generates two fixed environments for your game: **Production** and **Sandbox**. When you decide to publish your game, **make sure to switch the environment key to <u>Production</u>**.  You can always generate more environments:  For example - you can choose to have a playground environment for your game's beta testers which will be isolated from your production environment and will thus prevent analytics data from being mixed between the two.  Another best practice might be to have a separate environment for each version of your game.</div>

	<img src="/img/tutorial_img/unity_grow/dashboardKeys.png" alt="Game key and Env key" style="border:0;">

	c. **Choose your social platform** by toggling Facebook, twitter or Google in the settings. Follow the instructions for integrating [Facebook](/unity/profile/Profile_GettingStarted#facebook), [Twitter](/unity/profile/Profile_GettingStarted#twitter) or [Google+](/unity/profile/Profile_GettingStarted#google-).

	d. If you're building for Android, click on the "Android Settings" option, and choose your billing provider. If you choose Google Play, you need to provide the Public Key, which is given to you from Google.

5. Fraud Protection (<u>RECOMMENDED</u>):

	Fraud Protection is using SOOMLA's validation service to validate the receipt of every purchase made in your game. By using Fraud Protection you also get **Advanced Receipt Verification** to fully protect your game from fraudsters.
	To activate Fraud Protection:

	- In the menu bar go to **Window > Soomla > Edit Settings**.

	- Check the "Receipt Validation" option under the relevant platform (Android - Google Play / iOS).

	- (Google Play only) Follow the instructions posted [here](/android/store/store_googleplayverification/) to fill in the relevant fields.

### Initialize modules

<div class="info-box">Make sure to initialize each module ONLY ONCE when your application loads, in the `Start()` function of a `MonoBehaviour` and **NOT** in the `Awake()` function. SOOMLA has its own `MonoBehaviour` and it needs to be "Awakened" before you initialize.</div>
<br>
<div class="info-box">The GrowHighway module is the module responsible for connecting your game to the GROW network. In order for it to operate it only needs to be initialized.</div>

1. Initialize Highway, Insights, Sync and Gifting:

	``` cs
	using Grow.Highway;
	using Grow.Insights;
	using Grow.Sync;
	using Grow.Gifting;
	using Grow.Leaderboards;

	// Make sure to make this call in your earliest loading scene,
	// and before initializing any other SOOMLA/GROW components
	// i.e. before SoomlaStore.Initialize(...)
	GrowHighway.Initialize();

	// Make sure to make this call AFTER initializing HIGHWAY
    GrowInsights.Initialize();

	// Make sure to make this call AFTER initializing HIGHWAY,
	// and BEFORE initializing STORE/PROFILE/LEVELUP
	bool modelSync = true; 	// Remote Economy Management - Synchronizes your game's
                             // economy model between the client and server - enables
                             // you to remotely manage your economy.

	bool stateSync = true; // Synchronizes the users' balances data with the server
                           // and across his other devices.
						   // Must be TRUE in order to use LEADERBOARDS.

	// State sync and Model sync can be enabled/disabled separately.
	GrowSync.Initialize(modelSync, stateSync);

	// LEADERBOARDS requires no initialization,
	// but it depends on SYNC initialization with stateSync=true

	// Make sure to make this call AFTER initializing SYNC,
	// and BEFORE initializing STORE/PROFILE/LEVELUP
	GrowGifting.Initialize();
	```

2. Initialize the open-source modules: Store, Profile & LevelUp (**AFTER** the initialization of Highway, Sync and Gifting).

	* **Initialize Store:** Create your own implementation of `IStoreAssets` in order to describe your specific game's assets ([example](https://github.com/soomla/unity3d-store/blob/master/Soomla/Assets/Examples/MuffinRush/MuffinRushAssets.cs)). Initialize SoomlaStore with the class you just created:

	``` cs
	SoomlaStore.Initialize(new YourStoreAssetsImplementation());
	```

	* **Initialize Profile:**

    **NOTE:** `SoomlaProfile` will initialize the social providers for you. Do NOT initialize them on your own (for example, don't call `FB.Init()`).

	``` cs
	SoomlaProfile.Initialize();
	```

	* **Initialize LevelUp:** Create your own Initial World which should contain the entire 'blueprint' of the game (see [Model Overview](/unity/levelup/Levelup_Model)). Initialize LevelUp with the world you just created:

	``` cs
	SoomlaLevelUp.Initialize(initialWorld);
	```

## Module usage & event handling

The next step is to create your game specific implementation for each of the modules. Use SOOMLA's awesome products to create better in-game economy, social interactions, game design and user experience.  
In order to be notified about (and handle) SOOMLA-related events, you will also need to create event-handling functions. Refer to the following sections for more information:

- **Store** - With Store you create your in-game virtual economy. It'll allow you to easily setup IAP and safely store your users' balances.  
[API](/unity/store/Store_Model) | [Main classes](/unity/store/Store_MainClasses) | [Events](/unity/store/Store_Events)

- **Profile** - This module will make your life extremely easy when it comes to connecting your users to Social Networks.  
[API](/unity/profile/Profile_MainClasses) | [Events](/unity/profile/Profile_Events)

- **LevelUp** - When you want to easily create your game structure and handle your users' state, LevelUp is your guy.  
[API](/unity/levelup/Levelup_Model) | [Events](/unity/levelup/Levelup_Events)

- **Insights** - Getting in-game information about your users in real-time used to be a dream. Now it's here. Insights will tell you things about your users (as seen in other games) inside the code so you can take actions when it matters. This is the power of the GROW data network.  
[API](/unity/grow/Grow_Insights#MainClasses&Methods) | [Events](/unity/grow/Grow_Insights#Events)

- **State & Economy Sync** - Your users want to get their balances, levels and other game state parameters when they switch devices. Now you can let them do it.  
[Events](/unity/grow/Grow_Sync#Events)

- **Social Leaderboards** - Make your users compete with each other using their favorite social network. GROW's Social Leaderboards will let your users compete using their Facebook, Twitter or Google+ accounts.  
[API](/unity/grow/Grow_Leaderboards) | [Events](/unity/grow/Grow_Leaderboards#Events)

- **Gifting** - Increase the virality of your game by letting your users gift each other with any virtual item in your game.  
[API](/unity/grow/Grow_Gifting) | [Events](/unity/grow/Grow_Gifting#Events)

## Example

Below is a short example of how to initialize SOOMLA's modules. We suggest you read about the different modules and their entities in SOOMLA's Knowledge Base: [Store](/unity/store/Store_Model), [Profile](/unity/profile/Profile_MainClasses), [LevelUp](/unity/levelup/Levelup_Model), [State & Economy Sync](/unity/grow/Grow_Sync), [Insights](/unity/grow/Grow_Insights), [Gifting](/unity/grow/Grow_Gifting) and [Social Leaderboards](/unity/grow/Grow_Leaderboards).

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
		"General", new List<string>(new string[] {SHIELD_GOOD.ID})
	);
}
```

<br>
### Initialization

``` cs
using Soomla;
using Soomla.Store;
using Soomla.Profile;
using Soomla.Levelup;
using Grow.Highway;
using Grow.Insights;
using Grow.Sync;
using Grow.Gifting;
using Grow.Leaderboards;
using System.Collections.Generic;

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
			ExampleAssets.COIN_CURRENCY.ID,     // Associated item ID
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
	public static void onLoginFinished(UserProfile userProfileJson, bool autoLogin, string payload){
		SoomlaUtils.LogDebug("TAG", "Logged in as: " + userProfileJson.toJSONObject().print());
	}
	public void onLevelStarted(Level level) {
		SoomlaUtils.LogDebug("TAG", "Level started: " + level.toJSONObject().print());
	}
	public void onGrowInsightsInitialized () {
		Debug.Log("Grow insights has been initialized.");
	}
	public void onInsightsRefreshFinished (){
		if (GrowInsights.UserInsights.PayInsights.PayRankByGenre[Genre.Educational] > 3) {
			// ... Do stuff according to your business plan ...
		}
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
	public void onGrowSyncInitialized() {
		Debug.Log("GROW Sync has been initialized.");
	}
	public void onModelSyncFinished(IList<string> modules) {
		Debug.Log("Model Sync has finished.");
	}
	public void onStateSyncFinished(IList<string> changedComponents,
	                                IList<string> failedComponents) {
		Debug.Log("State Sync has finished.");
	}
	public void onFetchFriendsStatesFinished(int providerId,
	                                         IList<FriendState> friendStates) {
		Debug.Log("Finished fetching friends states.");
		// ... Display leaderboards to the user ...
	}

	//
	// Initialize all of SOOMLA's modules
	//
	void Start () {

		// Setup all event handlers - Make sure to set the event handlers before you initialize
		StoreEvents.OnGoodBalanceChanged += onGoodBalanceChanged;
		ProfileEvents.OnLoginFinished += onLoginFinished;
		LevelUpEvents.OnLevelStarted += onLevelStarted;

		HighwayEvents.OnGrowInsightsInitialized += onGrowInsightsInitialized;
		HighwayEvents.OnInsightsRefreshFinished += onInsightsRefreshFinished;

		HighwayEvents.OnGrowGiftingInitialized += OnGrowGiftingInitialized;
		HighwayEvents.OnGiftHandOutSuccess += OnGiftHandOutSuccess;
		HighwayEvents.OnGiftSendFinished += OnGiftSendFinished;

		HighwayEvents.OnGrowSyncInitialized += onGrowSyncInitialized;
		HighwayEvents.OnModelSyncFinished += onModelSyncFinished;
		HighwayEvents.OnStateSyncFinished += onStateSyncFinished;

		HighwayEvents.OnFetchFriendsStatesFinished += onFetchFriendsStatesFinished;

		// We can fetch friends states upon getting the player's friends list
		ProfileEvents.OnGetContactsFinished +=
			delegate(Provider provider,
			         SocialPageData<UserProfile> userProfiles,
			        string payload) {
			Debug.Log ("OnGetContactsFinished");

			// Extract a list of profile IDs from a list of friends
			System.Collections.Generic.List<string> profileIdList =
				userProfiles.PageData.ConvertAll(e => e.ProfileId);

			// Fetch friends' states
			GrowLeaderboards.FetchFriendsStates(provider.toInt(), profileIdList);

			if (userProfiles.HasMore) {
				SoomlaProfile.GetContacts(provider, false);
			} else {
				// no pages anymore
			}
		};

		// Make sure to make this call in your earliest loading scene,
		// and before initializing any other SOOMLA/GROW components
		// i.e. before SoomlaStore.Initialize(...)
		GrowHighway.Initialize();

		// Make sure to make this call AFTER initializing HIGHWAY
		GrowInsights.Initialize();

		// Make sure to make this call AFTER initializing HIGHWAY,
		// and BEFORE initializing STORE/PROFILE/LEVELUP
		bool modelSync = true; 	// Remote Economy Management - Synchronizes your game's
								// economy model between the client and server - enables
								// you to remotely manage your economy.

		bool stateSync = true; 	// Synchronizes the users' balances data with the server
								// and across his other devices.

		// State sync and Model sync can be enabled/disabled separately.
		GrowSync.Initialize(modelSync, stateSync);

		// Make sure to make this call AFTER initializing SYNC,
		// and BEFORE initializing STORE/PROFILE/LEVELUP
		GrowGifting.Initialize();

		SoomlaStore.Initialize(new ExampleAssets());
		SoomlaProfile.Initialize();
		SoomlaLevelUp.Initialize(createMainWorld());

	}
}
```
