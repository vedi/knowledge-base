---
layout: "content"
image: "Tutorial"
title: "Gifting"
text: "Get started with GROW Gifting for Unity. Here you can find initialization instructions, event handling and usage examples."
position: 7
theme: 'platforms'
collection: 'unity_grow'
module: 'grow'
platform: 'unity'
---

# Gifting

## Overview

GROW Gifting brings you the tools to make your game go viral, by letting users send little surprises to one another. With GIFTING you can:  

- Send lives to your friend.
- Share rare items between friends to progress faster.
- Send coins to your loved one.
- A lot more ... you can create your own use cases and even share them with others on http://answers.soom.la.

## Integration

<div class="info-box">GROW Gifting is included in [GrowViral](/unity/grow/GrowViral_GettingStarted#SetupGrowViral) and [GrowUltimate](/unity/grow/GrowUltimate_GettingStarted#SetupGrowUltimate) bundles. Please refer to the relevant bundle for initialization instructions.</div>


1. Initialize `SoomlaGifting` according to the instructions of your relevant bundle.

* Create event handler functions in order to be notified about (and handle) GROW Gifting related events. See [Events](/unity/grow/Grow_Gifting/#Events) for more information.

* Once the user logs into any social provider, GROW Gifting will automatically start fetching gifts from the server. Once the gifts are ready (see [`OnGiftsRetrieveFinished`](/unity/grow/Grow_Gifting/#OnGiftsRetrieveFinished)) they will be handed out to the user as explained below.


## Events

Following is a list of all the events in GROW Gifting and an example of how to observe & handle them.

### OnSoomlaGiftingInitialized

This event is triggered when the GROW Gifting feature is initialized and ready.

``` cs
HighwayEvents.OnSoomlaGiftingInitialized += onSoomlaGiftingInitialized;

public void onSoomlaGiftingInitialized() {
// ... your game specific implementation here ...
}
```

### OnGiftsRetrieveStarted

This event is triggered when GIFTING starts fetching the user's gifts list (may be fired at any point in your application).

``` cs
HighwayEvents.OnGiftsRetrieveStarted += onGiftsRetrieveStarted;

public void onGiftsRetrieveStarted() {
// ... your game specific implementation here ...
}
```

### OnGiftsRetrieveFinished

This event is triggered when gifting finished fetching the player's gifts list (may be fired at any point in your application).
Provides a list of `Gift`s which will be handed out.
They will not be given to the player yet at this point (this is mainly to notify the player that gifts have arrived).

``` cs
HighwayEvents.OnGiftsRetrieveFinished += onGiftsRetrieveFinished;

public void onGiftsRetrieveFinished(IList<Gift> gifts) {
// ... your game specific implementation here ...
}
```

### OnGiftsRetrieveFailed

This event is triggered when fetching gifts fails (may be fired at any point in your application).
Provides the failure reason.

``` cs
HighwayEvents.OnGiftsRetrieveFailed += onGiftsRetrieveFailed;

public void onGiftsRetrieveFailed(string failReason) {
// ... your game specific implementation here ...
}
```

### OnGiftSendStarted

This event is triggered when a gift is about to be sent.
Provides the gift that is being sent.

``` cs
HighwayEvents.OnGiftSendStarted += onGiftSendStarted;

public void onGiftSendStarted(Gift gift) {
// ... your game specific implementation here ...
}
```

### OnGiftSendFinished

This event is triggered when a gift has been sent.
Provides the gift that was sent.

``` cs
HighwayEvents.OnGiftSendFinished += onGiftSendFinished;

public void onGiftSendFinished(Gift gift) {
// ... your game specific implementation here ...
}
```

### OnGiftSendFailed

This event is triggered when a gift failed to be sent.
Provides the gift that failed to be sent, and the failure reason.

``` cs
HighwayEvents.OnGiftSendFailed += onGiftSendFailed;

public void onGiftSendFailed(Gift gift, string failReason) {
// ... your game specific implementation here ...
}
```

### OnGiftHandOutSuccess

This event is triggered when a gift has been given to the player.
Provides the gift which was handed out to the user.

``` cs
HighwayEvents.OnGiftHandOutSuccess += onGiftHandOutSuccess;

public void onGiftHandOutSuccess(Gift gift) {
// ... your game specific implementation here ...
}
```

### OnGiftHandOutFailed

This event is triggered when a gift has failed to be given to the player.
Provides the gift which failed to be handed out to the user, and the failure reason.

``` cs
HighwayEvents.OnGiftHandOutFailed += onGiftHandOutFailed;

public void onGiftHandOutFailed(Gift gift, string failReason) {
// ... your game specific implementation here ...
}
```

## Main Classes & Methods

Here you can find descriptions of the main classes of GROW Gifting. These classes contain functionality for gifting-related operations such as initializing and sending gifts.

### SoomlaGifting

`SoomlaGifting` is the main class of GROW Gifting which is in charge of sending and fetching gifts.

#### Functions

**`Initialize()`**

Initializes the GROW Gifting feature. Once initialized, the `OnSoomlaGiftingInitialized` event is triggered.

**`SendGift(toProvider, toProfileId, itemId, amount, <optional>deductFromUser)`**

Sends a gift from the currently logged in user (with Profile) to the provided user. This method gives the gift from the game without affecting the player.
Returns `false` if the operation cannot be started, `true` otherwise.
Params:

- toProvider - The social provider ID of the user to send gift to.
- toProfileId - The social provider user ID to send gift to.
- itemId - The virtual item ID to give as a gift.
- amount - The amount of virtual items to gift.
- deductFromUser (optional) - Should the virtual items be deducted from the player upon sending the gift.

The `OnGiftSendStarted` event is triggered once the sending process is started, and one of `OnGiftSendFinished` or `OnGiftSendFailed` is triggered depending on the operation outcome.

### Gift

`Gift` represents a gift from one user to the other.

#### Members

**`ID`**

The Gift's ID, only when received from server.

**`FromUID`**

User's UID from which the gift originated.

**`ToProvider`**

Social provider ID of the user to which the gift was sent.

**`ToProfileId`**

The receiving user's ID on the destination social provider

**`Payload`**

GiftPayload for the gift.

### GiftPayload

`GiftPayload` represents a gift payload which provides information of actual gift value.

#### Members

**`AssociatedItemId`**

The associated virtual item ID to give as a part of the gifting process.

**`ItemsAmount`**

The amount of the associated virtual item that should be given as a part of the gifting process.

## example

Coming soon
