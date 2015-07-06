using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;
using SponsorPay;

public class FyberRvExample : MonoBehaviour {
    string appId = "YOUR_FYBER_APPID";
    string securityToken = "YOUR_FYBER_SECURITY_TOKEN";
    
    bool readyToShowAds = false;

    SponsorPayPlugin sponsorPayPlugin;

    void Start () {

        SoomlaStore.Initialize(new YourStoreAssetsImplementation());

        // Instantiate and initialize the Fyber Unity plugin with your app ID,
        // security token, and your user's ID

        sponsorPayPlugin = SponsorPayPluginMonoBehaviour.PluginInstance;

        sponsorPayPlugin.Start(appId, userId, securityToken); 

        sponsorPayPlugin.OnBrandEngageRequestResponseReceived +=
            new SponsorPay.BrandEngageRequestResponseReceivedHandler (HandleRequestResult);
        sponsorPayPlugin.OnSuccessfulCurrencyRequestReceived +=
            new SponsorPay.SuccessfulCurrencyResponseReceivedHandler(HandleNewCurrency);
    }
    
    void OnGUI() {
        //
        //  Ideally there would be some time (30 sec - 5 min) between
        //  initialization of the plugin and the first request for videos,
        //  to allow for successfull pre-caching of video ads.
        //
        //  The second parameter tells our plugin to request the Virtual Currency
        //  Server immediately after the video is finished. The result of this
        //  call will be handled by the HandleNewCurrency method
        //

        if(GUILayout.Button("Request video from Fyber!")) {
            sponsorPayPlugin.RequestBrandEngageOffers ("", true);
        }
    
        if (!readyToShowAds) {
            GUI.enabled = false;
        }
        
        if(GUILayout.Button("Watch a video from Fyber!")) {
            sponsorPayPlugin.StartBrandEngage();
        }
        
        GUI.enabled = true;
    }

    public void HandleRequestResult(bool offersAvailable)
    {
        if (offersAvailable) {
            readyToShowAds = true;
        } else  {
            // No offers are available
        }
    }

    void HandleNewCurrency(SuccessfulCurrencyResponse response)
    {
        //
        //  CurrencyId: ID of the Virtual Currency defined in the Fyber Dashboard
        //  This example assumes the ID is identical to the Virtual Item ID
        //  defined in your Store Assets Implementation
        //  DeltaOfCoins: Amount of VC to be given
        //
        int amount = (int) response.DeltaOfCoins; 

        if(amount > 0) {
             StoreInventory.GiveItem(response.CurrencyId, amount);
        }
    }
}
