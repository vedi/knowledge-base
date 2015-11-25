using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;
using GoogleMobileAds.Api;

public class SoomlaAdmob : MonoBehaviour {

	private bool RemoveAdsOwned = false;
	public float intervalTime = 2.0f;
	public float totalTime = 0.0f;
	private AdRequest requestBanner;
	private AdRequest requestInterstitial;
	private BannerView bannerView;
	private InterstitialAd interstitialAd;
	private bool BannerIsActive = true;

	// Use this for initialization
	void Start () {

		// Initialize SOOMLA Store
		SoomlaStore.Initialize (new SoomlaAssets ());

		// Request banner and interstitial form Admob
		requestNewBanner ();
		requestNewInterstitial ();
	}

	void Update() {

		if (BannerIsActive && StoreInventory.GetItemBalance (SoomlaAssets.REMOVE_ADS_ITEM_ID) >= 1) {
			// The user purchased "Remove ADs"
			bannerView.Hide (); 		// Hide the banner
			bannerView.Destroy (); 		// Destroy the Banner
			BannerIsActive = false;
			interstitialAd.Destroy();	// Destroy the Interstitial
		}

		// Check every 2 seconds if the user purchased the Remove Ads.
		if (Time.timeSinceLevelLoad > totalTime) {
			if (StoreInventory.GetItemBalance (SoomlaAssets.REMOVE_ADS_ITEM_ID) >= 1) {
				RemoveAdsOwned = true;
			}
			totalTime = Time.timeSinceLevelLoad + intervalTime;
		}
	}

	private void requestNewBanner() {
		bannerView = new BannerView ("YOUR_AD_UNIT_ID", AdSize.SmartBanner, AdPosition.Bottom);
		requestBanner = new AdRequest.Builder ()
			.AddTestDevice ("<YOUR_HASHED_DEVICE_ID>") // Add a test device
				.Build ();

		bannerView.LoadAd (requestBanner); // Load the Banner
	}

	private void requestNewInterstitial() {
		interstitialAd = new InterstitialAd("<YOUR_HASHED_DEVICE_ID>");

		requestInterstitial = new AdRequest.Builder()
			.AddTestDevice("<YOUR_HASHED_DEVICE_ID>") //Add a test device
				.Build();
		interstitialAd.LoadAd(requestInterstitial); // Load the interstitial
	}

	void OnGUI() {

		// Show a reset button only in the Unity Editor for debugging.
		#if UNITY_EDITOR
		if (GUILayout.Button ("Reset", GUILayout.Width(400),GUILayout.Height(200))) {
			PlayerPrefs.DeleteAll ();
		}
		#endif

		// Show an interstitial.
		if (GUILayout.Button ("Show Interstitials", GUILayout.Width(400),GUILayout.Height(200))) {
			showInterstitial();
		}

		// Show a button for purchasing "Remove Ads"
		if(GUILayout.Button("Remove Ads", GUILayout.Width(400),GUILayout.Height(200))) {
			if (RemoveAdsOwned) {

				// User already purchased "Remove Ads" - do nothing
				return;
			} else {

				// Purchase "Remove Ads"
				try {
					StoreInventory.BuyItem(SoomlaAssets.REMOVE_ADS_ITEM_ID);
				} catch(VirtualItemNotFoundException e) {
					Debug.Log ("ERROR = " + e.Message);
				} catch(InsufficientFundsException e) {
					Debug.Log ("ERROR = " + e.Message);
				}
			}
		}
	}

	// Utility method for showing the interstitial, for example at the end of a level.
	private void showInterstitial() {

		// If the user purchased "Remove Ads" destroy the interstitial.
		// Otherwise, show it
		if (StoreInventory.GetItemBalance (SoomlaAssets.REMOVE_ADS_ITEM_ID) >= 1) {
			interstitialAd.Destroy();
		} else {
			interstitialAd.Show ();

			// Interstitial may take some time to load
			// In case of latency, request a new one
			if(!interstitialAd.IsLoaded ()) {
				requestNewInterstitial();
			}
		}
	}
}
