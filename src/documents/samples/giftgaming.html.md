---
layout: "sample"
image: "giftgaming_logo"
title: "giftgaming®"
text: "Give surprise in-game gifts containing currency and coupons"
position: 9
relates: ["supersonic", "unity_ads"]
collection: 'samples'
navicon: "nav-icon-giftgaming.png"
backlink: "https://www.giftgaming.com/"
theme: 'samples'
---

# giftgaming Integration

<div>
  <div class="samples-title">Ads Suck.</div>
  <p>
  	giftgaming&reg; monetises games non-intrusively by delivering in-game gifts containing free power-ups, currency and real-world coupons.
  	Make money whilst making players feel good.
  </p>
  
  <div class="samples-title">Why giftgaming</div>
  <p>
  	Unlike competitors, giftgaming provides a better gaming experience by:
  	<ul>
  	<li>Not asking players for credit card information, email address or phone number</li>
  	<li>Enabling developers to select the most appropriate brands for their games</li>
  	<li>Adapting to the look and feel of the game</li>
  	</ul>
  </p>
</div>

<div class="samples-title">Getting started with giftgaming + SOOMLA</div>

1. Download a <a href="https://www.giftgaming.com/publishers#getPlugin?referrer=soomla">giftgaming Plugin</a> and follow the integration instructions.

2. Integrate SOOMLA Store. See <a href="/unity/store/store_gettingstarted/" target="_blank">Full instructions</a>.

3. In your <code>giftClosed</code> callback call Soomla's <code>StoreInventory.GiveItem</code>

<div>
  <!-- Nav tabs -->
  <ul class="nav nav-tabs nav-tabs-use-case-code sample-tabs" role="tablist">
    <li role="presentation" class="active"><a href="#sample-unity" aria-controls="unity" role="tab" data-toggle="tab">Unity</a></li>
  	<li role="presentation"><a href="#sample-android" aria-controls="android" role="tab" data-toggle="tab">Android</a></li>
  </ul>

  <!-- Tab panes -->
  <div class="tab-content tab-content-use-case-code">
    <div role="tabpanel" class="tab-pane active" id="sample-unity">
    <p>In Unity, this would be in your copy of <code>SampleGifts.cs</code>:</p>
    
     <pre>
```
using Soomla;
using Soomla.Store;
using Soomla.Highway;
using Soomla.Insights;
using giftgamingSDK;

public Texture2D sampleGiftIcon;
bool isPlayerNonSpender = false;

void Start(){
	// Register callback for Soomla Insights
	HighwayEvents.OnInsightsRefreshFinished += OnSoomlaInsightsRefreshFinished;

    // Initialize SOOMLA Store, Highway and Insights
    SoomlaStore.Initialize(new YourStoreAssetsImplementation());
    SoomlaHighway.Initialize();
    SoomlaInsights.Initialize();

    // Start Gift Service AFTER callbacks have been registered
	giftgaming.setGiftClosedCallback(giftClosed);
	giftgaming.startGiftService();
}

void OnGUI() {
	// Example button for players to access their coupons
	if(GUILayout.Button ("giftgaming® Vault")) {
		giftgaming.openVault();
	}
	
	// Example gift icon when gift is ready
	if(giftgaming.isGiftReady()) {
		Rect sampleGiftIconRect = 
			new Rect(Screen.width - 100, Screen.height - 100, 100, 100);

		if( GUI.Button(sampleGiftIconRect, sampleGiftIcon) ) {
			giftgaming.openReceivedGift();
		}
	}
}


void OnSoomlaInsightsRefreshFinished (){
   if(SoomlaInsights.UserInsights.PayInsights.PayRankByGenre[Genre.Action] == 0) {
       isPlayerNonSpender = true;
   }
}

// The giftCode is set from giftgaming Dashboard
// Must correspond to your Soomla Store itemId
public void giftClosed(string giftCode) {
	int AMOUNT = 1; // Amount of thing you want to gift
	StoreInventory.GiveItem(giftCode, AMOUNT);
	
	// If player is non-spender then set gift timing to be 5-30 seconds
	if(isPlayerNonSpender) {
		giftgaming.overrideTimeBetweenGifts(5, 30);
	}
}
```
     </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-android">
     <pre>
```
import com.soomla.Soomla;
import com.soomla.store.SoomlaStore;
import com.soomla.store.StoreInventory;
import com.soomla.store.exceptions.VirtualItemNotFoundException;

// giftCode is set from the giftgaming Dashboard and must correspond to your Soomla Store itemId
public void giftClosed(string giftCode) {
	try {
		int AMOUNT = 1; // Amount of thing you want to gift
		StoreInventory.GiveVirtualItem(giftCode, AMOUNT);
	} catch(VirtualItemNotFoundException e) {
    	// Currency not identified
    }
}
```
     </pre>
    </div>
    
  </div>
  
</div>


<div class="samples-title">Additional tips and recommendations</div>

1. <a href="http://dashboard.giftgaming.com">Get a giftgaming&reg; Account</a> to receive direct support.

2. Make periodic gifts a part of your game as <a href="http://www.ccsenet.org/journal/index.php/ijms/article/download/11547/8155">studies</a> show surprise gifts improve loyalty, spending and recommendations.

3. Use a unique gift icon for giftgaming so as to not confuse your players.