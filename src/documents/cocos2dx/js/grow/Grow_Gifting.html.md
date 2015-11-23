---
layout: "content"
image: "Gifting"
title: "Gifting"
text: "Get started with GROW Gifting for Unity. Here you can find initialization instructions, event handling and usage examples."
position: 5
theme: 'platforms'
collection: 'cocos2djs_grow'
module: 'grow'
lang: 'js'
platform: 'cocos2dx'
---

# Gifting

## Overview

GROW Gifting brings you the tools to make your game go viral, by letting users send little surprises to one another.
With GIFTING you can:  

- Send lives to your friend.

- Share rare items between friends to progress faster.

- Send coins to your loved one.

- A lot more ... you can create your own use cases and even share them with others on SOOMLA's [Forums](http://answers.soom.la).

## Integration

<div class="info-box">GROW Gifting is included in [GrowViral](/cocos2dx/js/grow/GrowViral_GettingStarted#SetupGrowViral)
and [GrowUltimate](/cocos2dx/js/grow/GrowUltimate_GettingStarted#SetupGrowUltimate) bundles. Please refer to the relevant
bundle for initialization instructions.</div>


1. Initialize `GrowGifting` according to the instructions of your relevant bundle.

2. Create event handler functions in order to be notified about (and handle) GROW Gifting related events. See
[Events](/cocos2dx/js/grow/Grow_Gifting/#Events) for more information.

3. Once the user logs into any social provider, GROW Gifting will automatically start fetching gifts from the server.
Once the gifts are ready (see [`EVENT_GIFTS_RETRIEVE_FINISHED`](/cocos2dx/js/grow/Grow_Gifting/#EVENT_GIFTS_RETRIEVE_FINISHED))
they will be handed out to the user as explained below.

## Observing & Handling Events

### Subscribing

The names of events are defined in `Soomla.Models.HighwayConsts`.
Subscribe to events in the following way:

```js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFTS_RETRIEVE_FINISHED, this.onGiftsRetrieveFinished, this);
```

### Handling

Handle the event through your own custom function:

```js
this.onGiftsRetrieveFinished = function (retrievedGifts) {
  // ... your game specific implementation here ...
}
```

## Events

Following is a list of all the events in GROW Gifting and an example of how to observe & handle them.

### EVENT_GROW_GIFTING_INITIALIZED

This event is triggered when the GROW Gifting feature is initialized and ready.

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GROW_GIFTING_INITIALIZED, this.onGrowGiftingInitialized, this);

this.onGrowGiftingInitialized = function () {
  // ... your game specific implementation here ...
}
```

### EVENT_GIFTS_RETRIEVE_STARTED

This event is triggered when GIFTING starts fetching the user's gifts list (may be fired at any point in your application).

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFTS_RETRIEVE_STARTED, this.onGiftsRetrieveStarted, this);

this.onGiftsRetrieveStarted = function () {
  // ... your game specific implementation here ...
}
```

### EVENT_GIFTS_RETRIEVE_FINISHED

This event is triggered when gifting finished fetching the player's gifts list (may be fired at any point in your application).
Provides a list of `Gift`s which will be handed out.
They will not be given to the player yet at this point (this is mainly to notify the player that gifts have arrived).

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFTS_RETRIEVE_FINISHED, this.onGiftsRetrieveFinished, this);

this.onGiftsRetrieveFinished = function (retrievedGifts) {
  // ... your game specific implementation here ...
}
```

### EVENT_GIFTS_RETRIEVE_FAILED

This event is triggered when fetching gifts fails (may be fired at any point in your application).
Provides the failure reason.

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFTS_RETRIEVE_FAILED, this.onGiftsRetrieveFailed, this);

this.onGiftsRetrieveFailed = function (errorMessage) {
  // ... your game specific implementation here ...
}
```

### EVENT_GIFT_SEND_STARTED

This event is triggered when a gift is about to be sent.
Provides the gift that is being sent.

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFT_SEND_STARTED, this.onGiftSendStarted, this);

this.onGiftSendStarted = function (gift) {
  // ... your game specific implementation here ...
}
```

### EVENT_GIFT_SEND_FINISHED

This event is triggered when a gift has been sent.
Provides the gift that was sent.

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFT_SEND_FINISHED, this.onGiftSendFinished, this);

this.onGiftSendFinished = function (gift) {
  // ... your game specific implementation here ...
}
```

### EVENT_GIFT_SEND_FAILED

This event is triggered when a gift failed to be sent.
Provides the gift that failed to be sent, and the failure reason.

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFT_SEND_FAILED, this.onGiftSendFailed, this);

this.onGiftSendFailed = function (gift, errorMessage) {
  // ... your game specific implementation here ...
}
```

### EVENT_GIFT_HAND_OUT_SUCCESS

This event is triggered when a gift has been given to the player.
Provides the gift which was handed out to the user.

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFT_HAND_OUT_SUCCESS, this.onGiftHandOutSuccess, this);

this.onGiftHandOutSuccess = function (gift) {
  // ... your game specific implementation here ...
}
```

### EVENT_GIFT_HAND_OUT_FAILED

This event is triggered when a gift has failed to be given to the player.
Provides the gift which failed to be handed out to the user, and the failure reason.

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFT_HAND_OUT_FAILED, this.onGiftHandOutFailed, this);

this.onGiftHandOutFailed = function (gift, errorMessage) {
  // ... your game specific implementation here ...
}
```

## Main Classes & Methods

Let's go through the main classes of GROW Gifting. These classes contain functionality for gifting-related operations
such as initializing and sending gifts.

### GrowGifting

`Soomla.GrowGifting` is the main class of Gifting which is in charge of sending and fetching gifts.

#### Functions

**`GrowGifting.createShared()`**

Initializes the GROW Gifting feature. Once initialized, the `EVENT_GROW_GIFTING_INITIALIZED` event is triggered.

**`GrowGifting.sendGift(toProvider, toProfileId, itemId, amount, deductFromUser)`**

Sends a gift from the currently logged in user (with [Profile]()) to the provided user. This method gives the gift from
the game without affecting the player.
Returns `false` if the operation cannot be started, `true` otherwise.
Params:

- toProvider - The social provider ID of the user to send gift to.

- toProfileId - The social provider user ID to send gift to.

- itemId - The virtual item ID to give as a gift.

- amount - The amount of virtual items to gift.

- deductFromUser (optional) - Should the virtual items be deducted from the player upon sending the gift.

The `EVENT_GIFT_SEND_STARTED` event is triggered once the sending process is started, and one of `EVENT_GIFT_SEND_FINISHED`
or `EVENT_GIFT_SEND_FAILED` is triggered depending on the operation outcome.

### Gift

`Gift` represents a gift from one user to the other.

#### Fields

**`Gift.giftId`**

Returns the Gift's ID, only when received from server.

**`Gift.fromUid`**

Returns the user's UID from which the gift originated.

**`Gift.toProvider`**

Returns the social provider ID of the user to which the gift was sent.

**`Gift.toProfileId`**

Returns the receiving user's ID on the destination social provider

**`Gift.payload`**

Returns the `GiftPayload` for the gift.

### GiftPayload

`GiftPayload` represents a gift payload which provides information of actual gift value.

#### Fields

**`GiftPayload.associatedItemId`**

Returns the associated virtual item ID to give as a part of the gifting process.

**`GiftPayload.itemsAmount`**bc

Returns the amount of the associated virtual item that should be given as a part of the gifting process.

## Example

Below is a short example of how to initialize GROW Gifting. We suggest you read about the different modules and their entities in SOOMLA's Knowledge Base: [STORE](/cocos2dx/js/store/Store_Model) and [PROFILE](/cocos2dx/js/profile/Profile_MainClasses).

### IStoreAssets

```js
/** ExampleAssets (your implementation of IStoreAssets) **/
var coinCurrency = Soomla.Models.VirtualCurrency.create({
  name: 'Coins',
  description: '',
  itemId: 'coin_currency_ID'  
});

var tenmuffPack = Soomla.Models.VirtualCurrencyPack.create({
    name: "50 Coins",                           // Name
    description: "50 Coin pac",                 // Description
    itemId: "coins_50",                         // Item ID
    currency_amount: 50,                        // Number of currency units
    currency_itemId: coin_currency_ID,          // Associated currency
    purchasableItem: Soomla.Models.PurchaseWithMarket.createWithMarketItem(
        50_COIN_PACK_PRODUCT_ID,                // Product ID
        0.99)                                   // Initial price
  });
);

var shieldGood = Soomla.Models.SingleUseVG.create({
    name: "Fruit Cake",                         // Name
    description: "Delicious fruit cake!",       // Description
    itemId: "fruit_cake",                       // Item id
    purchasableItem: Soomla.Models.PurchaseWithVirtualItem.create({
      pvi_itemId: MUFFIN_CURRENCY_ITEM_ID,      // Target item ID
      pvi_amount: 225                           // Initial price
    })
  });

```

<br>
### Initialization

```js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GROW_GIFTING_INITIALIZED, this.onGrowGiftingInitialized, this);
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFT_HAND_OUT_SUCCESS, this.onGiftHandOutSuccess, this);
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_GIFT_SEND_FINISHED, this.onGiftSendFinished, this);

Soomla.initialize('ExampleCustomSecret');

// Make sure to make this call in your AppDelegate's
// applicationDidFinishLaunching method, and before
// initializing any other SOOMLA/GROW components
// i.e. before SoomlaStore.initialize(...)
Soomla.GrowHighway.createShared('yourGameKey', 'yourEnvKey');
// Make sure to make this call AFTER initializing SYNC,
// and BEFORE initializing STORE/PROFILE/LEVELUP
Soomla.GrowGifting.createShared();
// LEADERBOARDS requires no initialization,
// but it depends on SYNC initialization with stateSync=true


var assets = new ExampleAssets();
var storeParams = {
  androidPublicKey: "ExamplePublicKey",
  testPurchases: true
};

Soomla.soomlaStore.initialize(assets, storeParams);

var profileParams = {};
Soomla.SoomlaProfile.createShared(profileParams);


this.onGrowGiftingInitialized = function (providerId, friendsStates) {
    // ... GROW Gifting has been initialized. ...
};

this.onGiftHandOutSuccess = function (gift) {
    // ... Show a nice animation of receiving the gift ...
};

this.onGiftSendFinished = function (gift) {
    // ... Show a nice animation of receiving the gift ...
};

```
