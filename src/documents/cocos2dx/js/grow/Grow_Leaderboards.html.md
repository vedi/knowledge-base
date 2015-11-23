---
layout: "content"
image: "Leaderboards"
title: "Social Leaderboards"
text: "Get started with GROW Social Leaderboards for Unity. Here you can find initialization instructions, event handling and usage examples."
position: 4
theme: 'platforms'
collection: 'cocos2djs_grow'
module: 'grow'
lang: 'js'
platform: 'cocos2dx'
---

# Social Leaderboards

## Overview

GROW Leaderboards lets you turn your game into a competition, by showing users their friends' progress. This will keep
your users engaged and informed of where they stand compared to others. Your users can use their favorite social network
to login into Leaderboards.
With Leaderboards you can:

- Create a leaderboard of all the user's friends playing your game.

- Pop up a friend's better score in a level just completed by the user.

- A lot more ... you can create your own use cases and even share them with others on SOOMLA's [Forums](http://answers.soom.la).

## Integration

<div class="info-box">GROW Leaderboards is included in [GrowCompete](/cocos2dx/js/grow/GrowCompete_GettingStarted#SetupGrowCompete)
and [GrowUltimate](/cocos2dx/js/grow/GrowUltimate_GettingStarted#SetupGrowUltimate) bundles. Please follow the relevant
bundle instruction first to properly integrate and initialize GROW Leaderboards.</div>


1. Initialize all necessary modules according to the instructions of your relevant bundle.

* Create event handler functions in order to be notified about (and handle) GROW Leaderboards related events. See
[Events](/cocos2dx/js/grow/Grow_Leaderboards/#Events) for more information.

* Once the user logs into a social provider it will be possible to start retrieving his friends' states, as explained below.

## Observing & Handling Events

### Subscribing

Subscribe to events in the following way:

```js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_FETCH_FRIENDS_STATES_STARTED, this.onFetchFriendsStatesStarted, this);
```

### Handling

Handle the event through your own custom function:

```js
this.onFetchFriendsStatesStarted = function (providerId) {
  // ... your game specific implementation here ...
}
```


## Events

Following is a list of all the events in GROW Leaderboards and an example of how to observe & handle them.

### EVENT_FETCH_FRIENDS_STATES_STARTED

This event is triggered when the leaderboards operation (friends states) starts with the provider ID which it queries.
Provides the social provider ID for which the leaderboards operation started.

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_FETCH_FRIENDS_STATES_STARTED, this.onFetchFriendsStatesStarted, this);

this.onFetchFriendsStatesStarted = function (providerId) {
  // ... your game specific implementation here ...
}
```

### EVENT_FETCH_FRIENDS_STATES_FINISHED

This event is triggered when the leaderboards operation (friends states) ends.
Provides the social provider ID for which the leaderboards operation finished,
and a list of `FriendState`s with the friends' states in it.

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_FETCH_FRIENDS_STATES_FINISHED, this.onFetchFriendsStatesFinished, this);

this.onFetchFriendsStatesFinished = function (providerId, friendsStates) {
  // ... your game specific implementation here ...
}
```

### EVENT_FETCH_FRIENDS_STATES_FAILED

This event is triggered if the leaderboards operation (friends states) fails.
Provides the social provider ID for which the leaderboards operation failed
and an error message which is the reason for the failure.

``` js
Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_FETCH_FRIENDS_STATES_FAILED, this.onFetchFriendsStatesFailed, this);

this.onFetchFriendsStatesFailed = function (providerId, errorMessage) {
  // ... your game specific implementation here ...
}
```

## Main Classes & Methods

Here you can find descriptions of the main classes of GROW Leaderboards. These classes contain functionality for
leaderboards-related operations such as fetching friends state.

### GrowLeaderboards

`Soomla.GrowLeaderboards` represents a manager class which is in charge of retrieving leaderboard information from the server.

#### Functions

**`GrowLeaderboards.fetchFriendsStates(providerId, friendsProfileIds)`**

Fetches the friends' state from the server. The friends' state contains relevant information on completed levels and
highscores for the provided list of users.
Returns `false` if the operation cannot be started, `true` otherwise.
Params:

- providerId - The social provider ID for which to get the friends' state.

- friendsProfileIds - a List of friends' profile IDs in the network provided. Can be obtained by calling
[`Soomla.soomlaProfile.getContacts()`](/cocos2dx/js/profile/Profile_MainClasses#getContacts).

The `EVENT_FETCH_FRIENDS_STATES_STARTED` event is triggered once the sending process is started, and one of
`EVENT_FETCH_FRIENDS_STATES_FINISHED` or `EVENT_FETCH_FRIENDS_STATES_FAILED` is triggered depending on the operation outcome.

### FriendState

`FriendState` represents a friend's state in the game. It contains all relevant information to create a leaderboard
between a player and his friends.

#### Fields

**`FriendState.profileId`**

Returns the profile ID of the user in the social network.

**`FriendState.lastCompletedWorlds`**

Returns a Map of worlds having levels completed in them by the user. It maps between the world ID and a completed inner world ID.

**`FriendState.records`**

Returns a Map of records made by the user. It maps between score ID and the highest record done by the user.

## Example

```js

Soomla.addHandler(Soomla.Models.HighwayConsts.EVENT_FETCH_FRIENDS_STATES_FINISHED, this.onGrowInsightsInitialized, this);
Soomla.addHandler(Soomla.Models.ProfileConsts.EVENT_GET_CONTACTS_FINISHED, this.onGetContactsFinished, this);

Soomla.initialize('ExampleCustomSecret');

// Initialize GrowHighway
Soomla.GrowHighway.createShared('yourGameKey', 'yourEnvKey');

// Make sure to make this call AFTER initializing HIGHWAY,
// and BEFORE initializing STORE/PROFILE/LEVELUP
Soomla.GrowSync.createShared();
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

Soomla.soomlaLevelUp.initialize(mainWorld);

this.onFetchFriendsStatesFinished = function(providerId, friendsStates) {
    // ... Display leaderboards to the user ...
}

this.onGetContactsFinished = function(provider, contactsDict, payload, hasMore) {

    var friendsProfileIds = [];
    _.each(contactsDict, function (userProfile) {
        friendsProfileIds.push(userProfile.profileId);
    });

    Soomla.GrowLeaderboards.fetchFriendsStates(provider, friendsProfileIds);

    if (hasMore) {
        Soomla.soomlaProfile.getContacts(
            provider,
            null,
            false
        );
    } else {
        // no pages anymore
    }
}
```
