---
layout: "content"
image: "Gifting"
title: "Gifting"
text: "Get started with GROW Gifting for Unity. Here you can find initialization instructions, event handling and usage examples."
position: 2
theme: 'platforms'
collection: 'cocos2dx_grow'
module: 'grow'
lang: 'cpp'
platform: 'cocos2dx'
---

# Gifting

## Overview

GROW Gifting brings you the tools to make your game go viral, by letting users send little surprises to one another. With GIFTING you can:  

- Send lives to your friend.
- Share rare items between friends to progress faster.
- Send coins to your loved one.
- A lot more ... you can create your own use cases and even share them with others on SOOMLA's [Forums](http://answers.soom.la).

## Integration

<div class="info-box">GROW Gifting is included in [GrowViral](/cocos2dx/cpp/grow/GrowViral_GettingStarted#SetupGrowViral) and [GrowUltimate](/cocos2dx/cpp/grow/GrowUltimate_GettingStarted#SetupGrowUltimate) bundles. Please refer to the relevant bundle for initialization instructions.</div>


1. Initialize `CCGrowGifting` according to the instructions of your relevant bundle.

2. Create event handler functions in order to be notified about (and handle) GROW Gifting related events. See [Events](/cocos2dx/cpp/grow/Grow_Gifting/#Events) for more information.

3. Once the user logs into any social provider, GROW Gifting will automatically start fetching gifts from the server. Once the gifts are ready (see [`EVENT_GIFTS_RETRIEVE_FINISHED`](/cocos2dx/cpp/grow/Grow_Gifting/#EVENT_GIFTS_RETRIEVE_FINISHED)) they will be handed out to the user as explained below.

## Observing & Handling Events

GROW Insights uses the Cocos2d-x facilities to dispatch its own custom events.
The names of such events are defined in `CCHighwayConsts`.

### Cocos2d-x v3 Events

** Subscribing **

Subscribe to events through the Cocos2d-x  [`EventDispatcher`](http://www.cocos2d-x.org/wiki/EventDispatcher_Mechanism):

```cpp
cocos2d::Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFTS_RETRIEVE_FINISHED, CC_CALLBACK_1(ExampleScene::onGiftsRetrieveFinished, this));
```

** Handling **

Handle the event through your own custom function:

```cpp
void ExampleScene::onGiftsRetrieveFinished(cocos2d::EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __Array *retrievedGifts = dynamic_cast<__Array *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIVEN_GIFTS));
	// ... your game specific implementation here ...
}
```


### Cocos2d-x v2 Events

** Subscribing **

Subscribe to events through the Cocos2d-x `CCNotificationCenter`:

```cpp
cocos2d::CCNotificationCenter::sharedNotificationCenter()->addObserver(this, callfuncO_selector(ExampleScene::onGiftsRetrieveFinished), CCHighwayConsts::EVENT_GIFTS_RETRIEVE_FINISHED, NULL);
```

** Handling **

Handle the event through your own custom function:

```cpp
void ExampleScene::onGiftsRetrieveFinished(cocos2d::CCDictionary *eventData) {
	__Array *retrievedGifts = dynamic_cast<__Array *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIVEN_GIFTS));
	// ... your game specific implementation here ...
}
```

## Events

Following is a list of all the events in GROW Gifting and an example of how to observe & handle them.

### EVENT_GROW_GIFTING_INITIALIZED

This event is triggered when the GROW Gifting feature is initialized and ready.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GROW_GIFTING_INITIALIZED, CC_CALLBACK_1(Example::onGrowGiftingInitialized, this));

void Example::onGrowGiftingInitialized(EventCustom *event) {
// ... your game specific implementation here ...
}
```

### EVENT_GIFTS_RETRIEVE_STARTED

This event is triggered when GIFTING starts fetching the user's gifts list (may be fired at any point in your application).

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFTS_RETRIEVE_STARTED, CC_CALLBACK_1(Example::onGiftsRetrieveStarted, this));

void Example::onGiftsRetrieveStarted(EventCustom *event) {
// ... your game specific implementation here ...
}
```

### EVENT_GIFTS_RETRIEVE_FINISHED

This event is triggered when gifting finished fetching the player's gifts list (may be fired at any point in your application).
Provides a list of `Gift`s which will be handed out.
They will not be given to the player yet at this point (this is mainly to notify the player that gifts have arrived).

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFTS_RETRIEVE_FINISHED, CC_CALLBACK_1(Example::onGiftsRetrieveFinished, this));

void Example::onGiftsRetrieveFinished(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __Array *retrievedGifts = dynamic_cast<__Array *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIVEN_GIFTS));
	// ... your game specific implementation here ...
}
```

### EVENT_GIFTS_RETRIEVE_FAILED

This event is triggered when fetching gifts fails (may be fired at any point in your application).
Provides the failure reason.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFTS_RETRIEVE_FAILED, CC_CALLBACK_1(Example::onGiftsRetrieveFailed, this));

void Example::onGiftsRetrieveFailed(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __String *errorMessage = dynamic_cast<__String *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_ERROR_MESSAGE));
	// ... your game specific implementation here ...
}
```

### EVENT_GIFT_SEND_STARTED

This event is triggered when a gift is about to be sent.
Provides the gift that is being sent.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFT_SEND_STARTED, CC_CALLBACK_1(Example::onGiftSendStarted, this));

void Example::onGiftSendStarted(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    CCGift *gift = dynamic_cast<CCGift *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIFT));
	// ... your game specific implementation here ...
}
```

### EVENT_GIFT_SEND_FINISHED

This event is triggered when a gift has been sent.
Provides the gift that was sent.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFT_SEND_FINISHED, CC_CALLBACK_1(Example::onGiftSendFinished, this));

void Example::onGiftSendFinished(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    CCGift *gift = dynamic_cast<CCGift *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIFT));
	// ... your game specific implementation here ...
}
```

### EVENT_GIFT_SEND_FAILED

This event is triggered when a gift failed to be sent.
Provides the gift that failed to be sent, and the failure reason.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFT_SEND_FAILED, CC_CALLBACK_1(Example::onGiftSendFailed, this));

void Example::onGiftSendFailed(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    CCGift *gift = dynamic_cast<CCGift *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIFT));
    __String *errorMessage = dynamic_cast<__String *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_ERROR_MESSAGE));
	// ... your game specific implementation here ...
}
```

### EVENT_GIFT_HAND_OUT_SUCCESS

This event is triggered when a gift has been given to the player.
Provides the gift which was handed out to the user.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFT_HAND_OUT_SUCCESS, CC_CALLBACK_1(Example::onGiftHandOutSuccess, this));

void Example::onGiftHandOutSuccess(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    CCGift *gift = dynamic_cast<CCGift *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIFT));
	// ... your game specific implementation here ...
}
```

### EVENT_GIFT_HAND_OUT_FAILED

This event is triggered when a gift has failed to be given to the player.
Provides the gift which failed to be handed out to the user, and the failure reason.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFT_HAND_OUT_FAILED, CC_CALLBACK_1(Example::onGiftHandOutFailed, this));

void Example::onGiftHandOutFailed(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    CCGift *gift = dynamic_cast<CCGift *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIFT));
	// ... your game specific implementation here ...
}
```

## Main Classes & Methods

Let's go through the main classes of GROW Gifting. These classes contain functionality for gifting-related operations such as initializing and sending gifts.

### CCGrowGifting

`CCGrowGifting` is the main class of Gifting which is in charge of sending and fetching gifts.

#### Functions

**`CCGrowGifting::initShared()`**

Initializes the GROW Gifting feature. Once initialized, the `EVENT_GROW_GIFTING_INITIALIZED` event is triggered.

**`CCGrowGifting::sendGift(toProvider, toProfileId, itemId, amount, <optional>deductFromUser)`**

Sends a gift from the currently logged in user (with [Profile]()) to the provided user. This method gives the gift from the game without affecting the player.
Returns `false` if the operation cannot be started, `true` otherwise.
Params:

- toProvider - The social provider ID of the user to send gift to.
- toProfileId - The social provider user ID to send gift to.
- itemId - The virtual item ID to give as a gift.
- amount - The amount of virtual items to gift.
- deductFromUser (optional) - Should the virtual items be deducted from the player upon sending the gift.

The `EVENT_GIFT_SEND_STARTED` event is triggered once the sending process is started, and one of `EVENT_GIFT_SEND_FINISHED` or `EVENT_GIFT_SEND_FAILED` is triggered depending on the operation outcome.

### CCGift

`CCGift` represents a gift from one user to the other.

#### Functions

**`CCGift::getId()`**

Returns the Gift's ID, only when received from server.

**`CCGift::getFromUid()`**

Returns the user's UID from which the gift originated.

**`CCGift::getToProvider()`**

Returns the social provider ID of the user to which the gift was sent.

**`CCGift::getToProfileId()`**

Returns the receiving user's ID on the destination social provider

**`CCGift::getPayload()`**

Returns the `CCGiftPayload` for the gift.

### CCGiftPayload

`CCGiftPayload` represents a gift payload which provides information of actual gift value.

#### Functions

**`CCGiftPayload::getAssociatedItemId()`**

Returns the associated virtual item ID to give as a part of the gifting process.

**`CCGiftPayload::getItemsAmount()`**

Returns the amount of the associated virtual item that should be given as a part of the gifting process.

## Example

Below is a short example of how to initialize GROW Gifting. We suggest you read about the different modules and their entities in SOOMLA's Knowledge Base: [STORE](/cocos2dx/cpp/store/Store_Model) and [PROFILE](/cocos2dx/cpp/profile/Profile_MainClasses).

### CCStoreAssets

```cpp
/** ExampleAssets (your implementation of CCStoreAssets) **/
CCVirtualCurrency *coinCurrency = CCVirtualCurrency::create(
  CCString::create("Coins"),
  CCString::create(""),
  CCString::create("coin_currency_ID")
);

CCVirtualCurrencyPack *tenmuffPack = CCVirtualCurrencyPack::create(
  CCString::create("50 Coins"),                 // Name
  CCString::create("50 Coin pack"),             // Description
  CCString::create("coins_50"),                 // Item ID
  CCInteger::create(50),                        // Number of currencies in the pack
  CCString::create("coin_currency_ID"),         // Currency associated with this pack
  CCPurchaseWithMarket::create(                 // Purchase type
    CCString::create(50_COIN_PACK_PRODUCT_ID),
    CCDouble::create(0.99))
);

CCVirtualGood *shieldGood = CCSingleUseVG::create(
  CCString::create("Fruit Cake"),               // Name
  CCString::create("Delicios fruit cake!"),     // Description
  CCString::create("fruit_cake"),               // Item ID
  CCPurchaseWithVirtualItem::create(            // Purchase type
    CCString::create(MUFFIN_CURRENCY_ITEM_ID),
    CCInteger::create(225))
);

```

<br>
### Initialization

```cpp
using namespace grow;

bool AppDelegate::applicationDidFinishLaunching() {

	// Add event listeners - Make sure to set the event handlers before you initialize
	Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GROW_GIFTING_INITIALIZED, CC_CALLBACK_1(AppDelegate::onGrowGiftingInitialized, this));
	Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFT_HAND_OUT_SUCCESS, CC_CALLBACK_1(AppDelegate::onGiftHandOutSuccess, this));
	Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_GIFT_SEND_FINISHED, CC_CALLBACK_1(AppDelegate::onGiftSendFinished, this));

	soomla::CCSoomla::initialize("ExampleCustomSecret");

	// Make sure to make this call in your AppDelegate's
	// applicationDidFinishLaunching method, and before
	// initializing any other SOOMLA/GROW components
	// i.e. before CCSoomlaStore::initialize(...)
	CCGrowHighway::initShared(__String::create("yourGameKey"),
							  __String::create("yourEnvKey"));

	// Make sure to make this call AFTER initializing SYNC,
	// and BEFORE initializing STORE/PROFILE/LEVELUP
	grow::CCGrowGifting::initShared();

	/** Set up and initialize Store and Profile **/
	ExampleAssets *assets = ExampleAssets::create();

	__Dictionary *storeParams = __Dictionary::create();
	storeParams->setObject(__String::create("ExamplePublicKey"), "androidPublicKey");

	soomla::CCSoomlaStore::initialize(assets, storeParams);

	__Dictionary *profileParams = __Dictionary::create();
	soomla::CCSoomlaProfile::initialize(profileParams);
}

void AppDelegate::onGrowGiftingInitialized(EventCustom *event) {
	cocos2d::log("GROW Gifting has been initialized.");
}

void AppDelegate::onGiftHandOutSuccess(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    CCGift *gift = dynamic_cast<CCGift *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIFT));
    // ... Show a nice animation of receiving the gift ...
}

void AppDelegate::onGiftSendFinished(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    CCGift *gift = dynamic_cast<CCGift *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_GIFTING_GIFT));
	cocos2d::log("Successfully sent %s", gift->getPayload()->getAssociatedItemId()->getCString());
}
```
