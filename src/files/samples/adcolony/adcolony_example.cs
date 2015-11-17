using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Soomla;
using Soomla.Store;

public class AdColonySample : MonoBehaviour {

	public string ADCOL_ZONE_ID = "";
	public string ADCOL_APP_ID = "";	
	public string COIN_ID = "coin_ID";

	Button btn;

	// Use this for initialization
	void Start () {
		btn = GetComponent<Button>();

		// Initialize AdColony & SOOMLA Store
		InitializeAdColony();
		StoreEvents.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;
		SoomlaStore.Initialize(new YourStoreAssetsImplementation());
	}

	void LateUpdate() {
		if (AdColony.IsV4VCAvailable(ADCOL_ZONE_ID)) {
			btn.interactable = true;
		} else {
			btn.interactable = false;
		}
	}

	public void InitializeAdColony() {
		// Assign any AdColony Delegates before calling Configure
		AdColony.OnVideoFinished = this.OnVideoFinished;
		AdColony.OnV4VCResult = this.OnV4VCResult;

		// If you wish to use a the customID feature, you should call that now.
		// Then, configure AdColony:
		AdColony.Configure(
			"version:1.0,store:google", // Arbitrary app version and Android app store declaration.
			ADCOL_APP_ID,   			// ADC App ID from adcolony.com
			ADCOL_ZONE_ID 				// A zone ID from adcolony.com
		);
	}

	public void ShowAdColonyAds() {
		PlayV4VCAd(ADCOL_ZONE_ID, true, true);
	}


	// When a video is available, you may choose to play it in any fashion you like.
	// Generally you will play them automatically during breaks in your game,
	// or in response to a user action like clicking a button.
	// Below is a method that could be called, or attached to a GUI action.
	public void PlayV4VCAd( string zoneID, bool prePopup, bool postPopup ) {
		// Check to see if a video for V4VC is available in the zone.
		if (AdColony.IsV4VCAvailable(zoneID)) {
			Debug.Log("Play AdColony V4VC Ad");
			// The AdColony class exposes two methods for showing V4VC Ads.
			// ---------------------------------------
			// The first `ShowV4VC`, plays a V4VC Ad and, optionally, displays
			// a popup when the video is finished.
			// ---------------------------------------
			// The second is `OfferV4VC`, which popups a confirmation before
			// playing the ad and, optionally, displays popup when the video 
			// is finished.
			
			// Call one of the V4VC Video methods:
			// Note that you should also pause your game here (audio, etc.) AdColony will not
			// pause your app for you.
			if (prePopup)
				AdColony.OfferV4VC( postPopup, zoneID );
			else
				AdColony.ShowV4VC( postPopup, zoneID );
		} else {
			Debug.Log("V4VC Ad Not Available");
		}
	}


	private void OnV4VCResult(bool success, string name, int amount) {
		if(success) {
			Debug.Log("V4VC SUCCESS: name = " + name + ", amount = " + amount);

			// Reward the user with coins
			StoreInventory.GiveItem(COIN_ID, amount);
		} else {
			Debug.LogWarning("V4VC FAILED!");
		}
	}

	private void OnVideoFinished(bool ad_was_shown) {
		Debug.Log("On Video Finished");
		// Resume your app here.
	}

	public void onCurrencyBalanceChanged(VirtualCurrency virtualCurrency, int balance, int amountAdded) {
		// Update the UI with the new currency balance
	}
}