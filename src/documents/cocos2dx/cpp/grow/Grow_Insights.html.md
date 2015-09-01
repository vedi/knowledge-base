---
layout: "content"
image: "Tutorial"
title: "Soomla Insights"
text: "Get started with Soomla Insights for Cocos2d-x. Here you can find initialization instructions, event handling and usage examples."
position: 5
theme: 'platforms'
collection: 'cocos2dx_grow'
module: 'grow'
lang: 'cpp'
platform: 'cocos2dx'
---

# Soomla Insights

## Overview

Soomla Insights brings you priceless insights about your users. You can use the provided insights to take actions on your users at real time during gameplay or when your users arrive in the game. Things you can do may include:  

- Create special prices for paying users in your genre.
- Adapt the game difficulty for the specific user.
- Create push campaigns.
- A lot more ... you can create your own personalization features and even share them with others on http://answers.soom.la.

Currently, Insights supports PayInsights which categorizes users according to their pay-rank. More about this below.

## Integration

<div class="info-box">Soomla-Insights depends on Soomla-Highway, so make sure you follow the [Setup GROW](/cocos2dx/cpp/grow/grow_gettingstarted/#GettingStarted2) instructions before integrating Soomla-Insights.</div>

1. Initialize `SoomlaInsights` **after** initializing `SoomlaHighway`:

    ``` cpp
    CCSoomlaInsights::initShared();
    ```

2. Create event handler functions in order to be notified about (and handle) Soomla-Insights related events. See [Events](/cocos2dx/cpp/grow/Grow_Insights/#SoomlaInsightsEvents) for more information.

3. Once initialized, Soomla-Insights will automatically retrieve relevant insights from the server. Once the insights are ready (see [`OnInsightsRefreshFinished`](/cocos2dx/cpp/grow/Grow_Insights/#EVENT_INSIGHTS_REFRESH_FINISHED)) you can access them as explained below.

## Observing & Handling Events

SOOMLA uses the Cocos2d-x facilities to dispatch its own custom events.
The names of such events are defined in `CCHighwayConsts`.

### Cocos2d-x v3 Events

** Subscribing **

Subscribe to events through the Cocos2d-x  [`EventDispatcher`](http://www.cocos2d-x.org/wiki/EventDispatcher_Mechanism):

```cpp
cocos2d::Director::getInstance()->getEventDispatcher()->addCustomEventListener(soomla::CCHighwayConsts::EVENT_INSIGHTS_REFRESH_FINISHED, CC_CALLBACK_1(ExampleScene::onInsightsRefreshFinished, this));
```

** Handling **

Handle the event through your own custom function:

```cpp
void ExampleScene::onInsightsRefreshFinished(cocos2d::EventCustom *event) {
    // No meta-data information to get from eventData for Soomla Insights events
}
```


### Cocos2d-x v2 Events

** Subscribing **

Subscribe to events through the Cocos2d-x `CCNotificationCenter`:

```cpp
cocos2d::CCNotificationCenter::sharedNotificationCenter()->addObserver(this, callfuncO_selector(ExampleScene::onInsightsRefreshFinished), soomla::CCHighwayConsts::EVENT_INSIGHTS_REFRESH_FINISHED, NULL);
```

** Handling **

Handle the event through your own custom function:

```cpp
void ExampleScene::onInsightsRefreshFinished(cocos2d::CCDictionary *eventData) {
    // No meta-data information to get from eventData for Soomla Insights events
}
```

## Soomla Insights Events

Following is a list of all the events in Soomla-Insights and an example of how to observe & handle them. The events' handling examples are written for v3, but it's easy to convert them to v2 dialect, see how above.

### EVENT_SOOMLA_INSIGHTS_INITIALIZED

This event is triggered when the Soomla-Insights feature is initialized and ready.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_SOOMLA_INSIGHTS_INITIALIZED, CC_CALLBACK_1(Example::onSoomlaInsightsInitialized, this));

void Example::onSoomlaInsightsInitialized(EventCustom *event) {
    // ... your game specific implementation here ...
}
```

### EVENT_INSIGHTS_REFRESH_STARTED

This event is triggered when fetching insights from the server has started.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_INSIGHTS_REFRESH_STARTED, CC_CALLBACK_1(Example::onInsightsRefreshStarted, this));

void Example::onInsightsRefreshStarted(EventCustom *event) {
    // ... your game specific implementation here ...
}
```

### EVENT_INSIGHTS_REFRESH_FINISHED

This event is triggered when fetching insights from the server has finished.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_INSIGHTS_REFRESH_FINISHED, CC_CALLBACK_1(Example::onInsightsRefreshFinished, this));

void Example::onInsightsRefreshFinished(EventCustom *event) {
    // ... your game specific implementation here ...
}
```

### EVENT_INSIGHTS_REFRESH_FAILED

This event is triggered when fetching insights from the server has failed.

``` cpp
Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCHighwayConsts::EVENT_INSIGHTS_REFRESH_FAILED, CC_CALLBACK_1(Example::onInsightsRefreshFailed, this));

void Example::onInsightsRefreshFailed(EventCustom *event) {
    // ... your game specific implementation here ...
}
```

## Main Classes & Methods

Here you can find descriptions of the main classes of Soomla-Insights. These classes contain functionality for insights-related operations such as refreshing insights, retrieving and using them.

### CCSoomlaInsights

`CCSoomlaInsights` is the main class of Soomla-Insights which is in charge of fetching insights.

#### Functions

**`CCSoomlaInsights::refreshInsights()`**

Manually refresh the insights. The `OnInsightsRefreshStarted` event is triggered once the refresh process is started, and one of `OnInsightsRefreshFinished` or `OnInsightsRefreshFailed` is triggered depending on the refresh outcome.

**`CCSoomlaInsights::getUserInsights()`**

Returns the [User-Insights](/cocos2dx/cpp/grow/Grow_Insights/#CCUserInsights) received from the server.

<div class="info-box">Soomla-Insights caches its data on the device so that it's accessible even when there is no internet connection.</div>

### CCUserInsights

`CCUserInsights` holds insights related to the user currently playing the game.
Located in `CCSoomlaInsights` and can be accessed using `CCSoomlaInsights::getUserInsights()`.

#### Functions

**`CCUserInsights::getPayInsights()`**

Returns the [Pay-Insights](/cocos2dx/cpp/grow/Grow_Insights/#CCPayInsights) received from the server.

### CCPayInsights

`CCPayInsights` holds insights related to the user's payments.
Located in `CCUserInsights` and can be accessed using `CCUserInsights::getPayInsights()`.

#### Functions

**`CCPayInsights::getPayRankForGenre(soomla::Genre genre)`**

Returns the user's pay-rank for the given [genre](/cocos2dx/cpp/grow/Grow_Insights/#Genre)

##### Possible return values

- -1: No insights for selected genre
- 0: The user has paid 0$ in total
- 1: The user has paid up to 1$
- 2: The user has paid up to 5$
- 3: The user has paid up to 10$
- 4: The user has paid up to 50$
- 5: The user has paid up to 100$
- 6: The user has paid more than 100$

<div class="info-box">NOTE: Pay rank is calculated according to the user's total revenue from ALL games using Soomla.</div>

### Genre

`Genre` represents a game genre.

#### Usage

For example, in order to access a user's pay rank by the `Action` genre use `CCSoomlaInsights::getInstance()->getUserInsights()->getPayInsights()->getPayRankForGenre(Genre::Action)`

### Example

``` cpp

// Add event listeners - Make sure to set the event handlers before you initialize
Director::getInstance()->getEventDispatcher()->addCustomEventListener(
            CCHighwayConsts::EVENT_SOOMLA_INSIGHTS_INITIALIZED, CC_CALLBACK_1(Example::onSoomlaInsightsInitialized, this));
Director::getInstance()->getEventDispatcher()->addCustomEventListener(
            CCHighwayConsts::EVENT_INSIGHTS_REFRESH_FINISHED, CC_CALLBACK_1(Example::onInsightsRefreshFinished, this));

// Initialize SoomlaHighway
CCSoomlaHighway::initShared(
     __String::create("yourGameKey"),
     __String::create("yourEnvKey"));

// Initialize SoomlaInsights
CCSoomlaInsights::initShared();


void ExampleScene::onSoomlaInsightsInitialized(cocos2d::CCDictionary *eventData) {
    cocos2d::log("Soomla insights has been initialized.");
}

void ExampleScene::onInsightsRefreshFinished(cocos2d::CCDictionary *eventData) {
    if (CCSoomlaInsights::getInstance()->getUserInsights()->
                getPayInsights()->getPayRankForGenre(Genre::Educational) > 3) {
        // ... Do stuff according to your business plan ...
    }
}

```
