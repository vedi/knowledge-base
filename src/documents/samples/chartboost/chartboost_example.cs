using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;
using Soomla.Levelup;
using Grow.Highway;
using Grow.Insights;
using ChartboostSDK;


public class SoomlaCharboost : MonoBehaviour {

	private bool isInsightsRefreshed = false;
	private int payLikelihood;

	void Start () {

		// Register callbacks for Grow Insights before initialization
		HighwayEvents.OnInsightsRefreshFinished += OnInsightsRefreshFinished;

		// Register callbacks for SOOMLA LevelUp before initialization
		LevelUpEvents.OnLevelEnded += OnLevelEnded;

		// Register callbacks for Chartboost events
		SetChartboostEvents ();
		Chartboost.cacheInterstitial(CBLocation.LevelComplete);
		Chartboost.cacheRewardedVideo(CBLocation.LevelComplete);


		// Make sure to make this call in your earliest loading scene,
		// and before initializing any other SOOMLA/GROW components
		// i.e. before SoomlaStore.Initialize(...)
		GrowHighway.Initialize();
		GrowInsights.Initialize();
		
		// Initialize SOOMLA Store & LevelUp
		// Assumes you've implemented your store assets
		// and an initial world with levels and missions
		SoomlaStore.Initialize (new YourStoreAssetsImplementation ());
		SoomlaLevelUp.Initialize (WORLD);

	}
	
	// Determine if the player is a non-spender
	void OnInsightsRefreshFinished (){
		isInsightsRefreshed = true;
		payLikelihood = GrowInsights.UserInsights.PayInsights.PayRankByGenre [Genre.Action];
	}

	public void OnLevelEnded(Level level) {
		int times = level.GetTimesStarted ();


		if (isInsightsRefreshed) {

			// User insights are available, adapt ad frequency
			// according to user behavior history

			if (payLikelihood == 0) {

				// User isn't likely to pay, increase ad frequency:
				// every 2 tries
				if (times % 2 == 0) {
					ShowInterstitial();
				}
			} else if (payLikelihood == 1) {
				
        // Minnows - paid a small amount in other games,
				// decrease ad frequency: every 10 tries
				if (times % 10 == 0) {
					ShowInterstitial();
				}
			} else {

        // Dolphins and Whales - paid significant amounts in other games
				// Don't show any ads at all
				// (This 'else' block is just for explanatory purposes)
				return;
			}
		} else {
			
			// Fallback: in case we don't have any user insights
			// Show the ad every 3 tries
			if (times % 3 == 0) {
				ShowInterstitial();
			}
		}
	}

	public static void ShowInterstitial() {
		#if UNITY_EDITOR
		return;
		#endif
		
		if (Chartboost.hasInterstitial(CBLocation.Default)) {
			Chartboost.showInterstitial(CBLocation.Default);
		}
	}

	// Register Chartboost events
	private static void SetChartboostEvents() {
		Chartboost.didFailToLoadInterstitial += (location, error) => {
			Chartboost.cacheInterstitial(location);
		};
		
		Chartboost.didDismissInterstitial += location => {
			Chartboost.cacheInterstitial(location);
		};
	}
}
