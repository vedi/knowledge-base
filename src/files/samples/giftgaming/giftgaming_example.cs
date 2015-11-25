using UnityEngine;
using Soomla;
using Soomla.Store;
using Grow.Highway;
using Grow.Insights;
using giftgamingSDK;

public class SampleGifts : MonoBehaviour {

    public Texture2D sampleGiftIcon;
    bool isPlayerNonSpender = false;
    bool isInsightsRefreshed = false;

    void Start(){
        // Register callback for Grow Insights before initialization
        HighwayEvents.OnInsightsRefreshFinished += OnInsightsRefreshFinished;

        // Make sure to make this call in your earliest loading scene,
        // and before initializing any other SOOMLA/GROW components
        // i.e. before SoomlaStore.Initialize(...)
        GrowHighway.Initialize();


        // Initialize Grow Insights and SOOMLA Store
        GrowInsights.Initialize();
        SoomlaStore.Initialize(new YourStoreAssetsImplementation());

        // Start Gift Servicae AFTER callbacks have been registered
        giftgaming.setGiftClosedCallback(giftClosed);
        giftgaming.startGiftService();
    }

    void OnGUI() {
        // Example button for players to access their coupons
        if(GUILayout.Button ("giftgaming® Vault")) {
            giftgaming.openVault();
        }

        // Example gift icon when gift is ready and insights were loaded
        if(giftgaming.isGiftReady() && isInsightsRefreshed) {
            Rect sampleGiftIconRect =
                new Rect(Screen.width - 100, Screen.height - 100, 100, 100);

            if( GUI.Button(sampleGiftIconRect, sampleGiftIcon) ) {
                giftgaming.openReceivedGift();
            }
        }
    }

    // Determine if the player is a non-spender
    void OnInsightsRefreshFinished (){
        isInsightsRefreshed = true;
        if(GrowInsights.UserInsights.PayInsights.PayRankByGenre[Genre.Action] == 0) {
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
}
