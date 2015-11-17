using UnityEngine;
using AdBoost;
using Soomla.Store;
using System.Collections.Generic;
using System.Linq;

public class MyBehaviour : MonoBehaviour {

    private void Start() {

        // Register SOOMLA Store event handlers
        StoreEvents.OnMarketPurchase += MyBehaviour.OnMarketPurchase;

        // Initialize SOOMLA Store
        // Assumes you've implemented your store assets
        SoomlaStore.Initialize(new YourStoreAssetsImplementation());
    }

    private static void OnMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra) {
        PurchaseWithMarket purchaseWithMarket = pvi.PurchaseType as PurchaseWithMarket;

        if (purchaseWithMarket != null) {
            MarketItem marketItem = purchaseWithMarket.MarketItem;
            if (Application.platform == RuntimePlatform.Android) {
                EventTracker tracker = AdBoostUnity.getEventTracker();
                tracker.sendRevenueEvent(
                    marketItem.MarketCurrencyCode,
                    marketItem.Price,
                    pvi.ItemId
                );
            }
        }
    }
}

