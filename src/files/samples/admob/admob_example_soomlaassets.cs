using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

/// This class defines our game's economy.

public class SoomlaAssets : IStoreAssets {


	public int GetVersion() {
		return 0;
	}

	public const string REMOVE_ADS_ITEM_ID = "remove_ads";

	#if UNITY_IOS
	public const string REMOVE_ADS_PRODUCT_ID = SoomlaAssets.REMOVE_ADS_ITEM_ID;
	#elif UNITY_ANDROID
	public const string REMOVE_ADS_PRODUCT_ID = SoomlaAssets.REMOVE_ADS_ITEM_ID;
	#else
	public const string REMOVE_ADS_PRODUCT_ID = SoomlaAssets.REMOVE_ADS_ITEM_ID;
	#endif

	public static VirtualGood REMOVE_ADS = new LifetimeVG(
		"Remove Ads",                    					// name
		"Clean up your APP from ads forever",               // description
		REMOVE_ADS_ITEM_ID, 								// item ID
		new PurchaseWithMarket(REMOVE_ADS_PRODUCT_ID, 0.99)	// purchase with real transaction
		);

	public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
		"General", new List<string>(new string[] {REMOVE_ADS_ITEM_ID})
		);

	public VirtualCategory[] GetCategories() {
		return new VirtualCategory[]{GENERAL_CATEGORY};
	}

	public VirtualCurrency[] GetCurrencies() {
		return new VirtualCurrency[]{};
	}

	public VirtualCurrencyPack[] GetCurrencyPacks() {
		return new VirtualCurrencyPack[]{};
	}

	public VirtualGood[] GetGoods() {
		return new VirtualGood[]{REMOVE_ADS};
	}
}
