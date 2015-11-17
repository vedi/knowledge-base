package com.mycompany.myapp;

import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;

import com.adience.adboost.AdBoost;
import com.adience.sdk.EventTracker;
import com.soomla.BusProvider;
import com.soomla.Soomla;
import com.soomla.store.domain.MarketItem;
import com.soomla.store.domain.PurchasableVirtualItem;
import com.soomla.store.events.MarketPurchaseEvent;
import com.soomla.store.purchaseTypes.PurchaseWithMarket;
import com.squareup.otto.Subscribe;

import java.util.HashMap;


public class MainActivity extends Activity {

		@Override
		protected void onCreate(Bundle savedInstanceState) {
				super.onCreate(savedInstanceState);
				setContentView(R.layout.activity_main);
				AdBoost.appStarted();

				// Register this instance to receive SOOMLA events.
				BusProvider.getInstance().register(this);

				// Initialize SOOMLA Store
				// Assumes you've implemented your store assets.
				Soomla.initialize("<GAME SECRET GOES HERE>");
				SoomlaStore.getInstance().initialize(new YourStoreAssetsImplementation());
		}

		@Subscribe
		public void onMarketPurchase(MarketPurchaseEvent marketPurchaseEvent) {
				PurchasableVirtualItem pvi = marketPurchaseEvent.PurchasableVirtualItem;
				PurchaseWithMarket purchaseWithMarket =
						PurchaseWithMarket.class.isInstance(pvi.getPurchaseType()) ?
						(PurchaseWithMarket)pvi.getPurchaseType() : null;

				if(purchaseWithMarket != null) {
						MarketItem marketItem = purchaseWithMarket.getMarketItem();
						HashMap<String, String> extraInfo = marketPurchaseEvent.ExtraInfo;
						EventTracker tracker = AdBoost.getEventTracker();
						tracker.sendRevenueEvent(
								marketItem.getMarketCurrencyCode(),
								marketItem.getPrice(),
								pvi.getItemId()
						);
				}
		}
}
