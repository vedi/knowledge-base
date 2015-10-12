package com.fyber.soomlasample;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.soomla.Soomla;
import com.soomla.store.SoomlaStore;
import com.soomla.store.StoreInventory;
import com.soomla.store.exceptions.VirtualItemNotFoundException;

import com.sponsorpay.SponsorPay;
import com.sponsorpay.publisher.SponsorPayPublisher;
import com.sponsorpay.publisher.currency.SPCurrencyServerErrorResponse;
import com.sponsorpay.publisher.currency.SPCurrencyServerListener;
import com.sponsorpay.publisher.currency.SPCurrencyServerSuccessfulResponse;
import com.sponsorpay.publisher.mbe.SPBrandEngageRequestListener;

public class FyberRvActivity extends Activity implements SPBrandEngageRequestListener {

    private static final int RV_REQUEST_CODE = 42;
    private static final String FYBER_APPID = "YOUR_FYBER_APPID";
    private static final String FYBER_SEC_TOKEN = "YOUR_FYBER_SECURITY_TOKEN";
    private static final String SOOMLA_SECRET = "YOUR_SOOMLA_SECRET";

    private Intent rvIntent;

    private Button rvRequest;
    private Button rvShow;

    @Override
    public void onResume(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Initialize SOOMLA and the SoomlaStore Instance, assuming you have
        // your own implementation of IStoreAssets describing your game's assets
        Soomla.initialize(SOOMLA_SECRET);
        SoomlaStore.getInstance().initialize(new YourStoreAssetsImplementation());

        // Initialize Fyber SDK
        SponsorPay.start(FYBER_APPID, null, FYBER_SEC_TOKEN, this);

        // Initialize UI components
        rvRequest = (Button) findViewById(R.id.rv_request_button);
        rvShow = (Button) findViewById(R.id.rv_show_button);

        if(rvIntent == null) {
            rvShow.setEnabled(false);
        } else {
            rvShow.setEnabled(true);
            rvShow.setOnClickListener(new RvShowClickListener(rvIntent));
        }

        rvRequest.setOnClickListener(new RvRequestClickListener(this, this));
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
    private class VCSServerListener implements SPCurrencyServerListener {
        @Override
        public void onSPCurrencyServerError(SPCurrencyServerErrorResponse response) {

        }

        @Override
        public void onSPCurrencyDeltaReceived(SPCurrencyServerSuccessfulResponse response) {
            //
            //  amount: Amount of VC to give
            //  currencyId: The currency ID defined in the Fyber Dashboard
            //  This example assumes you have used the same ID for setting up
            //  your virtual currency with both Fyber and Soomla
            //
            int amount = (int) response.getDeltaOfCoins();
            String currencyId = response.getCurrencyId();

            try {
                StoreInventory.giveVirtualItem(currencyId, amount);
            } catch(VirtualItemNotFoundException e) {
                // Currency not identified
            }

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
            SponsorPayPublisher.getIntentForMBEActivity(a, l, new VCSServerListener());
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