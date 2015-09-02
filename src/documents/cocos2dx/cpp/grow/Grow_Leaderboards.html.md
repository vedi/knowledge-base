---
layout: "content"
image: "Leaderboards"
title: "Social Leaderboards"
text: "Get started with GROW Social Leaderboards for Unity. Here you can find initialization instructions, event handling and usage examples."
position: 9
theme: 'platforms'
collection: 'cocos2dx_grow'
module: 'grow'
lang: 'cpp'
platform: 'cocos2dx'
---

# Social Leaderboards

## Overview

GROW Leaderboards lets you turn your game into a competition, by showing users their friends' progress. This will keep your users engaged and informed of where they stand compared to others. Your users can use their favorite social network to login into Leaderboards.
With Leaderboards you can:

- Create a leaderboard of all the user's friends playing your game.
- Pop up a friend's better score in a level just completed by the user.
- A lot more ... you can create your own use cases and even share them with others on SOOMLA's [Forums](http://answers.soom.la).

## Integration

<div class="info-box">GROW Leaderboards is included in [GrowCompete](/cocos2dx/cpp/grow/GrowCompete_GettingStarted#SetupGrowCompete) and [GrowUltimate](/cocos2dx/cpp/grow/GrowUltimate_GettingStarted#SetupGrowUltimate) bundles. Please follow the relevant bundle instruction first to properly integrate and initialize GROW Leaderboards.</div>


1. Initialize all necessary modules according to the instructions of your relevant bundle.

* Create event handler functions in order to be notified about (and handle) GROW Leaderboards related events. See [Events](/cocos2dx/cpp/grow/Grow_Leaderboards/#Events) for more information.

* Once the user logs into a social provider it will be possible to start retrieving his friends' states, as explained below.

## Observing & Handling Events

GROW Insights uses the Cocos2d-x facilities to dispatch its own custom events.
The names of such events are defined in `CCHighwayConsts`.

### Cocos2d-x v3 Events

** Subscribing **

Subscribe to events through the Cocos2d-x  [`EventDispatcher`](http://www.cocos2d-x.org/wiki/EventDispatcher_Mechanism):

```cpp
cocos2d::Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_FETCH_FRIENDS_STATES_STARTED, CC_CALLBACK_1(ExampleScene::onFetchFriendsStatesStarted, this));
```

** Handling **

Handle the event through your own custom function:

```cpp
void ExampleScene::onFetchFriendsStatesStarted(cocos2d::EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __Integer *providerId = dynamic_cast<__Integer *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_PROVIDER_ID));
	// ... your game specific implementation here ...
}
```


### Cocos2d-x v2 Events

** Subscribing **

Subscribe to events through the Cocos2d-x `CCNotificationCenter`:

```cpp
cocos2d::CCNotificationCenter::sharedNotificationCenter()->addObserver(this, callfuncO_selector(ExampleScene::onFetchFriendsStatesStarted), CCHighwayConsts::EVENT_FETCH_FRIENDS_STATES_STARTED, NULL);
```

** Handling **

Handle the event through your own custom function:

```cpp
void ExampleScene::onFetchFriendsStatesStarted(cocos2d::CCDictionary *eventData) {
	__Integer *providerId = dynamic_cast<__Integer *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_PROVIDER_ID));
	// ... your game specific implementation here ...
}
```

## Events

Following is a list of all the events in GROW Leaderboards and an example of how to observe & handle them.

### EVENT_FETCH_FRIENDS_STATES_STARTED

This event is triggered when the leaderboards operation (friends states) starts with the provider ID which it queries.
Provides the social provider ID for which the leaderboards operation started.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_FETCH_FRIENDS_STATES_STARTED, CC_CALLBACK_1(Example::onFetchFriendsStatesStarted, this));

void Example::onFetchFriendsStatesStarted(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __Integer *providerId = dynamic_cast<__Integer *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_PROVIDER_ID));
	// ... your game specific implementation here ...
}
```

### EVENT_FETCH_FRIENDS_STATES_FINISHED

This event is triggered when the leaderboards operation (friends states) ends.
Provides the social provider ID for which the leaderboards operation finished,
and a list of `FriendState`s with the friends' states in it.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_FETCH_FRIENDS_STATES_FINISHED, CC_CALLBACK_1(Example::onFetchFriendsStatesFinished, this));

void Example::onFetchFriendsStatesFinished(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __Integer *providerId = dynamic_cast<__Integer *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_PROVIDER_ID));
    __Array *friendsStates = dynamic_cast<__Array *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_FRIENDS_STATES));
	// ... your game specific implementation here ...
}
```

### EVENT_FETCH_FRIENDS_STATES_FAILED

This event is triggered if the leaderboards operation (friends states) fails.
Provides the social provider ID for which the leaderboards operation failed
and an error message which is the reason for the failure.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_FETCH_FRIENDS_STATES_FAILED, CC_CALLBACK_1(Example::onFetchFriendsStatesFailed, this));

void Example::onFetchFriendsStatesFailed(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __Integer *providerId = dynamic_cast<__Integer *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_PROVIDER_ID));
    __String *errorMessage = dynamic_cast<__String *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_ERROR_MESSAGE));
	// ... your game specific implementation here ...
}
```

## Main Classes & Methods

Here you can find descriptions of the main classes of GROW Leaderboards. These classes contain functionality for leaderboards-related operations such as fetching friends state.

### CCGrowLeaderboards

`CCGrowLeaderboards` represents a manager class which is in charge of retrieving leaderboard information from the server.

#### Functions

**`CCGrowLeaderboards::fetchFriendsStates(providerId, friendsProfileIds)`**

Fetches the friends' state from the server. The friends' state contains relevant information on completed levels and highscores for the provided list of users.
Returns `false` if the operation cannot be started, `true` otherwise.
Params:

- providerId - The social provider ID for which to get the friends' state.
- friendsProfileIds - a List of friends' profile IDs in the network provided. Can be obtained by calling [`CCSoomlaProfile::getContacts()`](/cocos2dx/cpp/profile/Profile_MainClasses#getContacts).

The `EVENT_FETCH_FRIENDS_STATES_STARTED` event is triggered once the sending process is started, and one of `EVENT_FETCH_FRIENDS_STATES_FINISHED` or `EVENT_FETCH_FRIENDS_STATES_FAILED` is triggered depending on the operation outcome.

### CCFriendState

`CCFriendState` represents a friend's state in the game. It contains all relevant information to create a leaderboard between a player and his friends.

#### Functions

**`CCFriendState::getProfileId()`**

Returns the profile ID of the user in the social network.

**`CCFriendState::getLastCompletedWorlds()`**

Returns a Map of worlds having levels completed in them by the user. It maps between the world ID and a completed inner world ID.

**`CCFriendState::getRecords()`**

Returns a Map of records made by the user. It maps between score ID and the highest record done by the user.

## Example

```cpp
using namespace grow;

bool AppDelegate::applicationDidFinishLaunching() {

	// Add event listeners - Make sure to set the event handlers before you initialize
	Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_FETCH_FRIENDS_STATES_FINISHED, CC_CALLBACK_1(AppDelegate::onFetchFriendsStatesFinished, this));
	Director::getInstance()->getEventDispatcher()->addCustomEventListener(soomla::CCProfileConsts::EVENT_GET_CONTACTS_FINISHED,                                                          CC_CALLBACK_1(AppDelegate::onGetContactsFinished, this));

	soomla::CCSoomla::initialize("ExampleCustomSecret");

	// Make sure to make this call in your AppDelegate's
	// applicationDidFinishLaunching method, and before
	// initializing any other SOOMLA/GROW components
	// i.e. before CCSoomlaStore::initialize(...)
	CCGrowHighway::initShared(__String::create("yourGameKey"),
							  __String::create("yourEnvKey"));

	// Make sure to make this call AFTER initializing HIGHWAY,
	// and BEFORE initializing STORE/PROFILE/LEVELUP
	grow::CCGrowSync::initShared(false, true); // Sync only state

	// LEADERBOARDS requires no initialization,
	// but it depends on SYNC initialization with stateSync=true

	/** Set up and initialize Store, Profile, and LevelUp **/
	ExampleAssets *assets = ExampleAssets::create();

	__Dictionary *storeParams = __Dictionary::create();
	storeParams->setObject(__String::create("ExamplePublicKey"), "androidPublicKey");

	soomla::CCSoomlaStore::initialize(assets, storeParams);

	__Dictionary *profileParams = __Dictionary::create();
	soomla::CCSoomlaProfile::initialize(profileParams);

	soomla::CCSoomlaLevelUp::getInstance()->initialize(mainWorld);
}

void AppDelegate::onFetchFriendsStatesFinished(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __Integer *providerId = dynamic_cast<__Integer *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_PROVIDER_ID));
    __Array *friendsStates = dynamic_cast<__Array *>(eventData->objectForKey(CCHighwayConsts::DICT_ELEMENT_LEADERBOARDS_FRIENDS_STATES));
	cocos2d::log("Finished fetching friends states.");
    // ... Display leaderboards to the user ...
}

void AppDelegate::onGetContactsFinished(EventCustom *event) {
    __Dictionary *eventData = (__Dictionary *)event->getUserData();
    __Integer *providerId = dynamic_cast<__Integer *>(eventData->objectForKey(CCProfileConsts::DICT_ELEMENT_PROVIDER));
    __Bool *hasMore = dynamic_cast<__Bool *>(eventData->objectForKey(CCProfileConsts::DICT_ELEMENT_HAS_MORE));
    __Array *contactsArray = dynamic_cast<__Array *>(eventData->objectForKey(CCProfileConsts::DICT_ELEMENT_CONTACTS));


    // ... handle page results ...
    __Array *friendsProfileIds = CCArray::create();
    for (Ref* userProfileRef : *contactsArray) {
        CCUserProfile *userProfile = (CCUserProfile*)userProfileRef;
        friendsProfileIds->addObject(userProfile->getProfileId());
    }

    CCGrowLeaderboards::fetchFriendsStates(providerId->getValue(), friendsProfileIds);

    if (hasMore != nullptr && hasMore->getValue()) {
        soomla::CCSoomlaProfile::getInstance()->getContacts(
                                                            soomla::FACEBOOK,
                                                            false,                              // going on with the pagination
                                                            NULL,                               // no reward
                                                            NULL                                // no error handling, to keep example simple
                                                            );
    } else {
        // no pages anymore
    }

}
```
