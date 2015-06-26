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

# TUNE + SOOMLA Store

## Use Case: Measure SOOMLA Store purchases

The biggest hurdle for marketing a mobile app is integrating SDKs for every ad network and publisher you want to work with. With [TUNE](http://www.tune.com), you never have to integrate another advertising SDK. Not only have we integrated with mobile ad networks and publishers, but you can easily pass conversion information to any third party partner you choose.

The [TUNE SDK](https://developers.mobileapptracking.com/mobile-sdks/) provides application session and event logging functionality. To begin measuring sessions and installs, initiate the “measureSession” method. You can then rely on TUNE to log in-app events (such as purchases, game levels, and any other user engagement).

This document will show you how to measure purchase events from SOOMLA Store, so you can see which ad networks and publishers are sending you valuable users making in-app purchases.

<div role="tabpanel">

  <!-- Nav tabs -->
  <ul class="nav nav-tabs nav-tabs-use-case-code" role="tablist">
    <li role="presentation" class="active"><a href="#sample-unity" aria-controls="unity" role="tab" data-toggle="tab">Unity</a></li>
    <li role="presentation"><a href="#sample-cocos2dx" aria-controls="cocos2dx" role="tab" data-toggle="tab">Cocos2d-x</a></li>
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
using System.Collections.Generic;
using MATSDK;
using Soomla.Store;

public class TuneSoomlaStoreScript : MonoBehaviour {
    void Start () {
        // Initialize TUNE SDK
        MATBinding.Init("tune_advertiser_id", "tune_conversion_key");
        // Measure initial app open
        MATBinding.MeasureSession();

        // Listen for Soomla OnMarketPurchase event
        StoreEvents.OnMarketPurchase += onMarketPurchase;

        // Initialize Soomla Store with your Store Assets
        SoomlaStore.Initialize(new YourStoreAssetsImplementation());
    }
    
    public void onMarketPurchase(PurchasableVirtualItem pvi, string payload,
            Dictionary<string, string> extra) {
        // On purchase complete, set purchase info and measure purchase in TUNE
        double revenue = 0;
        string currency = "";
        MATItem[] items = new MATItem[] {};
        PurchaseType type = pvi.PurchaseType;
        if (type is PurchaseWithMarket) {
            MarketItem item = ((PurchaseWithMarket)type).MarketItem;
            revenue = item.MarketPriceMicros / 1000000;
            currency = item.MarketCurrencyCode;
            // Create event item to store purchase item data
            MATItem matItem = new MATItem();
            matItem.name = item.MarketTitle;
            matItem.attribute1 = item.ProductId;
            // Add event item to MATItem array in order to pass to TUNE SDK
            items[items.Length] = matItem;
        }

        // Get order ID and receipt data for purchase validation
        string receipt = "";
        string receiptSignature = "";
        string orderId = "";
#if UNITY_ANDROID
        extras.TryGetValue("originalJson", out receipt);
        extras.TryGetValue("signature", out receiptSignature);
        extras.TryGetValue("orderId", out orderId);
#elif UNITY_IOS
        extras.TryGetValue("receiptBase64", out receipt);
        extras.TryGetValue("transactionIdentifier", out orderId);
#endif

        // Create a MATEvent with this purchase data
        MATEvent purchaseEvent = new MATEvent("purchase");
        purchaseEvent.revenue = revenue;
        purchaseEvent.currencyCode = currency;
        purchaseEvent.advertiserRefId = orderId;
        purchaseEvent.receipt = receipt;
        purchaseEvent.receiptSignature = receiptSignature;
        // Set event item if it exists
        if (items.Length != 0) {
            purchaseEvent.eventItems = items;
        }
        // Measure "purchase" event
        MATBinding.MeasureEvent(purchaseEvent);
    }
}
        </code>
      </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-cocos2dx">...</div>
    <div role="tabpanel" class="tab-pane" id="sample-ios">...</div>
    <div role="tabpanel" class="tab-pane" id="sample-android">
      <pre>
        <code class="java">
import android.app.Activity;
import android.os.Bundle;

import com.mobileapptracker.MATEvent;
import com.mobileapptracker.MATEventItem;
import com.mobileapptracker.MobileAppTracker;
import com.soomla.BusProvider;
import com.soomla.Soomla;
import com.soomla.store.IStoreAssets;
import com.soomla.store.SoomlaStore;
import com.soomla.store.domain.MarketItem;
import com.soomla.store.events.MarketPurchaseEvent;
import com.soomla.store.purchaseTypes.PurchaseType;
import com.soomla.store.purchaseTypes.PurchaseWithMarket;
import com.squareup.otto.Subscribe;

public class MainActivity extends Activity {
    private MobileAppTracker mobileAppTracker;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Initialize the TUNE SDK
        mobileAppTracker = MobileAppTracker.init(
                getApplicationContext(),
                "tune_advertiser_id",
                "tune_conversion_key");

        // Initialize Soomla and Soomla Store
        Soomla.initialize("[YOUR CUSTOM GAME SECRET HERE]");
        SoomlaStore.getInstance().initialize(new YourStoreAssetsImplementation());
    }

    @Override
    protected void onResume() {
        super.onResume();
        // Register to receive Soomla events
        BusProvider.getInstance().register(this);
        // Measure an app open in TUNE
        mobileAppTracker.setReferralSources(this);
        mobileAppTracker.measureSession();
    }

    @Override
    protected void onPause() {
        // Unregister from Soomla events
        BusProvider.getInstance().unregister(this);
        super.onPause();
    }

    @Subscribe
    public void onMarketPurchase(MarketPurchaseEvent marketPurchaseEvent) {
        // On purchase complete, set purchase info and measure purchase in TUNE
        double revenue = 0;
        String currency = "";
        MATEventItem[] items = new MATEventItem[0];
        PurchaseType type = marketPurchaseEvent.getPurchasableVirtualItem().getPurchaseType();
        if (type instanceof PurchaseWithMarket) {
            MarketItem item = ((PurchaseWithMarket) type).getMarketItem();
            revenue = item.getMarketPriceMicros() / 1000000;
            currency = item.getMarketCurrencyCode();
            // Create event item to store purchase item data
            MATEventItem eventItem = new MATEventItem(item.getMarketTitle())
                    .withAttribute1(item.getProductId());
            // Add event item to MATItem array in order to pass to TUNE SDK
            items[items.length] = eventItem;
        }

        // Get order ID and receipt data for purchase validation
        String orderId = marketPurchaseEvent.getOrderId();
        String receiptData = marketPurchaseEvent.getOriginalJson();
        String receiptSignature = marketPurchaseEvent.getSignature();

        // Create a MATEvent with this purchase data
        MATEvent purchaseEvent = new MATEvent("purchase")
                .withRevenue(revenue)
                .withCurrencyCode(currency)
                .withAdvertiserRefId(orderId)
                .withReceipt(receiptData, receiptSignature);
        // Set event item if it exists
        if (items.length != 0) {
            purchaseEvent.withEventItems(items);
        }
        // Measure "purchase" event
        mobileAppTracker.measureEvent(purchaseEvent);
    }
}
        </code>
      </pre>
    </div>
  </div>

</div>

## Getting started with the two SDKs

1. [Sign up](https://platform.mobileapptracking.com/#!/advertiser) with TUNE to get started on your home for attribution and analytics.

2. Download and integrate the [TUNE SDK](https://developers.mobileapptracking.com/mobile-sdks/).

3. Download and integrate [SOOMLA Store](http://know.soom.la/).

## Support

For more information on SDK implementation and events to measure, please visit our [documentation](https://developers.mobileapptracking.com/mobile-sdks/).

If you have further questions, please feel free to contact us at [support@mobileapptracking.com](mailto:support@mobileapptracking.com).
