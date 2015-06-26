---
layout: "sample"
image: "Events"
title: "Events"
text: "Learn how to observe and handle economy events triggered by android-store to customize your game-specific behavior."
position: 5
theme: 'platforms'
collection: 'android_store'
module: 'store'
platform: 'android'
---

# TUNE + SOOMLA LevelUp

## Use Case: Measure SOOMLA LevelUp

The biggest hurdle for marketing a mobile app is integrating SDKs for every ad network and publisher you want to work with. With [TUNE](http://www.tune.com), you never have to integrate another advertising SDK. Not only have we integrated with mobile ad networks and publishers, but you can easily pass conversion information to any third party partner you choose.

The [TUNE SDK](https://developers.mobileapptracking.com/mobile-sdks/) provides application session and event logging functionality. To begin measuring sessions and installs, initiate the “measureSession” method. You can then rely on TUNE to log in-app events (such as purchases, game levels, and any other user engagement).

This document will show you how to measure level events from SOOMLA LevelUp, so you can see which ad networks and publishers are sending you more engaged users getting further in your game.

<div role="tabpanel">

  <!-- Nav tabs -->
  <ul class="nav nav-tabs nav-tabs-use-case-code" role="tablist">
    <li role="presentation" class="active"><a href="#sample-unity" aria-controls="unity" role="tab" data-toggle="tab">Unity</a></li>
    <li role="presentation"><a href="#sample-cocos2dx" aria-controls="cocos2dx" role="tab" data-toggle="tab">Cocos2d-x</a></li>
    <li role="presentation"><a href="#sample-ios" aria-controls="iod" role="tab" data-toggle="tab">iOS</a></li>
    <li role="presentation"><a href="#sample-android" aria-controls="android" role="tab" data-toggle="tab">Android</a></li>
  </ul>

  <!-- Tab panes -->
  <div class="tab-content tab-content-use-case-code">
    <div role="tabpanel" class="tab-pane active" id="sample-unity">
      <pre>
        <code class="cs">
using UnityEngine;
using System.Collections;
using MATSDK;
using Soomla.Levelup;

public class TuneSoomlaLevelUpScript : MonoBehaviour {
    void Start () {
        // Initialize TUNE SDK
        MATBinding.Init("tune_advertiser_id", "tune_conversion_key");
        // Measure initial app open
        MATBinding.MeasureSession();

        // Listen for Soomla OnLevelStarted event
        LevelUpEvents.OnLevelStarted += onLevelStarted;

        // Create an example World with Level
        World world = new World("exampleWorld");
        Level lvl1 = new Level("Level 1");
        world.AddInnerWorld(lvl1);

        // Initialize Soomla LevelUp with the example world containing a level
        SoomlaLevelUp.Initialize(world);

        // Start the level, will trigger OnLevelStarted event
        lvl1.Start();
    }

    void OnApplicationPause(bool pauseStatus) {
        if (!pauseStatus) {
            // Measure app resumes from background
            MATBinding.MeasureSession();
        }
    }

    // Set level ID and measure a level achieved event upon level start
    public void onLevelStarted(Level level) {
        // Create a MATEvent for level_achieved with the level ID
        MATEvent levelEvent = new MATEvent("level_achieved");
        matEvent.contentId = level.ID;
        // Measure "level_achieved" event for this level ID
        MATBinding.MeasureEvent (levelEvent);
    }
}
        </code>
      </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-cocos2dx">...</div>
    <div role="tabpanel" class="tab-pane" id="sample-ios">...</div>
    <div role="tabpanel" class="tab-pane" id="sample-android">...</div>
  </div>

</div>

## Getting started with the two SDKs

1. [Sign up](https://platform.mobileapptracking.com/#!/advertiser) with TUNE to get started on your home for attribution and analytics.

2. Download and integrate the [TUNE SDK](https://developers.mobileapptracking.com/mobile-sdks/).

3. Download and integrate [SOOMLA LevelUp](http://know.soom.la/).

## Support

For more information on SDK implementation and events to measure, please visit our [documentation](https://developers.mobileapptracking.com/mobile-sdks/).

If you have further questions, please feel free to contact us at [support@mobileapptracking.com](mailto:support@mobileapptracking.com).
