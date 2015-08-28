---
layout: "content"
image: "Tutorial"
title: "Social Leaderboards"
text: "Get started with GROW Social Leaderboards for Unity. Here you can find initialization instructions, event handling and usage examples."
position: 9
theme: 'platforms'
collection: 'unity_grow'
module: 'grow'
platform: 'unity'
---

# Social Leaderboards

## Overview

GROW Leaderboards lets you turn your game into a competition, by showing users their friends' progress. This will keep your users engaged and informed of where they stand compared to others. Your users can use their favorite social network to login into Leaderboards.
With Leaderboards you can:

- Create a leaderboard of all the user's friends playing your game.
- Pop up a friend's better score in a level just completed by the user.
- A lot more ... you can create your own use cases and even share them with others on SOOMLA's [Forums](http://answers.soom.la).

## Integration

<div class="info-box">GROW Leaderboards is included in [GrowCompete](/unity/grow/GrowCompete_GettingStarted#SetupGrowCompete) and [GrowUltimate](/unity/grow/GrowUltimate_GettingStarted#SetupGrowUltimate) bundles. Please follow the relevant bundle instruction first to properly integrate and initialize GROW Leaderboards.</div>


1. Initialize all necessary modules according to the instructions of your relevant bundle.

* Create event handler functions in order to be notified about (and handle) GROW Leaderboards related events. See [Events](/unity/grow/Grow_Leaderboards/#Events) for more information.

* Once the user logs into a social provider it will be possible to start retrieving his friends' states, as explained below.

## Events

Following is a list of all the events in GROW Leaderboards and an example of how to observe & handle them.

### OnFetchFriendsStatesStarted

This event is triggered when the leaderboards operation (friends states) starts with the provider ID which it queries.
Provides the social provider ID for which the leaderboards operation started.

``` cs
HighwayEvents.OnFetchFriendsStatesStarted += onFetchFriendsStatesStarted;

public void onFetchFriendsStatesStarted(int providerId) {
// ... your game specific implementation here ...
}
```

### OnFetchFriendsStatesFinished

This event is triggered when the leaderboards operation (friends states) ends.
Provides the social provider ID for which the leaderboards operation finished,
and a list of `FriendState`s with the friends' states in it.

``` cs
HighwayEvents.OnFetchFriendsStatesFinished += onFetchFriendsStatesFinished;

public void onFetchFriendsStatesFinished(int providerId, IList<FriendState> friendStates) {
// ... your game specific implementation here ...
}
```

### OnFetchFriendsStatesFailed

This event is triggered if the leaderboards operation (friends states) fails.
Provides the social provider ID for which the leaderboards operation failed
and an error message which is the reason for the failure.

``` cs
HighwayEvents.OnFetchFriendsStatesFailed += onFetchFriendsStatesFailed;

public void onFetchFriendsStatesFailed(int providerId, string failReason) {
// ... your game specific implementation here ...
}
```

## Main Classes & Methods

Here you can find descriptions of the main classes of GROW Leaderboards. These classes contain functionality for leaderboards-related operations such as querying friends state.

### GrowLeaderboards

`GrowLeaderboards` represents a manager class which is in charge of retrieving leaderboard information from the server.

#### Functions

**`FetchFriendsStates(providerId, friendsProfileIds)`**

Fetches the friends' state from the server. The friends' state contains relevant information on completed levels and highscores for the provided list of users.
Returns `false` if the operation cannot be started, `true` otherwise.
Params:

- providerId - The social provider ID for which to get the friends' state.
- friendsProfileIds - a List of friends' profile IDs in the network provided. Can be obtained by calling [`SoomlaProfile.GetContacts(providerId)`](/unity/profile/profile_mainclasses/#GetContacts).

The `OnFetchFriendsStatesStarted` event is triggered once the sending process is started, and one of `OnFetchFriendsStatesFinished` or `OnFetchFriendsStatesFailed` is triggered depending on the operation outcome.

### FriendState

`FriendState` represents a friend's state in the game. It contains all relevant information to create a leaderboard between a player and his friends.

#### Members

**`ProfileId`**

The profile ID of the user in the social network.

**`LastCompletedWorlds`**

A Map of worlds having levels completed in them by the user. It maps between the world ID and a completed inner world ID.

**`Records`**

A Map of records made by the user. It maps between score ID and the highest record done by the user.

## example

Coming soon
