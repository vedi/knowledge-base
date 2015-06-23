---
layout: "sample"
image: "Events"
title: "Events"
text: "Learn how to observe and handle economy events triggered by android-store to customize your game-specific behavior."
position: 5
theme: 'platforms'
collection: 'android_store'
module: 'store'
platform: 'android'
---

# Fyber + SOOMLA Store

## Use Case: Reward coins on video ads completion

<div role="tabpanel">

  <!-- Nav tabs -->
  <ul class="nav nav-tabs nav-tabs-use-case-code" role="tablist">
    <li role="presentation" class="active"><a href="#sample-unity" aria-controls="unity" role="tab" data-toggle="tab">Unity</a></li>
    <li role="presentation"><a href="#sample-ios" aria-controls="iod" role="tab" data-toggle="tab">iOS</a></li>
    <li role="presentation"><a href="#sample-android" aria-controls="android" role="tab" data-toggle="tab">Android</a></li>
  </ul>

  <!-- Tab panes -->
  <div class="tab-content tab-content-use-case-code">
    <div role="tabpanel" class="tab-pane active" id="sample-unity">
      <pre>
        <code class="cs">
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Soomla;
using Soomla.Store;
using SponsorPay;

//Just apply this script to an UI Button
public class freeCoins : MonoBehaviour {
    string appId = "12345";
    string userId = "user1";
    string securityToken = "1a2b3c4d5e6f7g8h";
    
    bool readyToShowAds = false;

    SponsorPayPlugin sponsorPayPlugin;

    Button btn;

    void Start () {
        btn = GetComponent<Button>();

        // Instantiate and initialize the Fyber Unity plugin with your app ID, security token, and your user's ID
        sponsorPayPlugin = SponsorPayPluginMonoBehaviour.PluginInstance;
        sponsorPayPlugin.OnBrandEngageRequestResponseReceived +=
            new SponsorPay.BrandEngageRequestResponseReceivedHandler (HandleRequestResult);
        sponsorPayPlugin.OnBrandEngageResultReceived +=
            new SponsorPay.BrandEngageResultHandler (HandleShowResult);
        sponsorPayPlugin.OnSuccessfulCurrencyRequestReceived +=
            new SponsorPay.SuccessfulCurrencyResponseReceivedHandler(HandleNewCurrency);

        sponsorPayPlugin.Start(appId, userId, securityToken); 

        // Ideally there would be some time (30 sec - 5 min) between initialization of the plugin and the first request for videos, to allow for successfull pre-caching of video ads

        sponsorPayPlugin.RequestBrandEngageOffers ("", "");
    }
    
    public void ShowAds()
    {
        if(readyToShowAds) {
            sponsorPayPlugin.StartBrandEngage();
        }
        
    }

    public void HandleRequestResult(bool offersAvailable)
    {
        if (offersAvailable) {
            btn.interactable = true;
            readyToShowAds = true;
        } else  {
            btn.interactable = false;
        }

    }

    void HandleShowResult(string result)
    {
        if(result == "CLOSE_FINISHED")
        {
            sponsorPayPlugin.RequestNewCoins();            
        }
    }

    void HandleNewCurrency(SuccessfulCurrencyResponse response)
    {
        if(response.DeltaOfCoins > 0) {
             StoreInventory.GiveItem(response.CurrencyId, response.DeltaOfCoins);
        }
    }
}

        </code>
      </pre>

    </div>
    <div role="tabpanel" class="tab-pane" id="sample-ios">
        <pre>
            <code class="cs">

            </code>
        </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-android">
                <pre>
            <code class="java">
import com.sponsorpay.publisher.SponsorPayPublisher;
import com.sponsorpay.publisher.mbe.SPBrandEngageRequestListener;

public class FyberRvActivity extends Activity implements SPBrandEngageRequestListener {

    private static final int RV_REQUEST_CODE = 13;
    private static final String APPID = "12345"
    private static final String SEC_TOKEN = "1a2b3c4d5e"
    
    private Intent rvIntent;    

    private Button rvRequest;
    private Button rvShow;
    
    @Override
    public void onResume(Bundle savedInstanceState) {
        super.onResume(savedInstanceState);
        
        SponsorPay.start(APPID, null, SEC_TOKEN, this);
        
        if(rvIntent == null) {
            rvShow.setEnabled(false);
        } else {
            rvShow.setEnabled(true);
            rvShow.setOnClickListener(new RvShowClickListener(rvIntent));
        }

        rvRequest.setOnClickListener(new RvRequestClickListener(this.getActivity(), this));

        return view;
    }

    @Override
    public void onSPBrandEngageOffersAvailable(Intent intent) {
        rvIntent = intent;
        rvShow.setOnClickListener(new RvShowClickListener(intent));
        rvShow.setEnabled(true);
    }

    @Override
    public void onSPBrandEngageOffersNotAvailable() {
        rvIntent = null;
        rvShow.setEnabled(false);
    }

    @Override
    public void onSPBrandEngageError(String s) {
        rvIntent = null;
        rvShow.setEnabled(false);
    }


    // All activity results are handled in this method
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == Activity.RESULT_OK && requestCode == RV_REQUEST_CODE) {
            rvIntent = null;
            rvShow.setEnabled(false);
        }
    }
    
    // Listener for the Virtual Currency Server call
    private class VirtualCurrencyServerListener implements SPCurrencyServerListener {
        @Override
        public void onSPCurrencyServerError(SPCurrencyServerErrorResponse response) {

        }

         @Override
         public void onSPCurrencyDeltaReceived(SPCurrencyServerSuccessfulResponse response) {
            double coins = response.getDeltaOfCoins();
            String currencyId = response.getCurrencyId();

            // SOOMLA coin processing here

         }
    }   

    private class RvRequestClickListener implements View.OnClickListener {

        private Activity a;
        private SPBrandEngageRequestListener l;

        public RvRequestClickListener(Activity a, SPBrandEngageRequestListener l) {
            this.a = a;
            this.l = l;
        }

        @Override
        public void onClick(View v) {
            SponsorPayPublisher.getIntentForMBEActivity(a, l, new VirtualCurrencyServerListener());
        }
    }

    private class RvShowClickListener implements View.OnClickListener {

        private Intent i;
        public RvShowClickListener(Intent i) {
            this.i = i;
        }

        @Override
        public void onClick(View v) {
            startActivityForResult(i, RV_REQUEST_CODE);
        }
    }


}
            </code>
        </pre>
        
    </div>
  </div>

</div>
