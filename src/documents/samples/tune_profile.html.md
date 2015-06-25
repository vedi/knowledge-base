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

# TUNE + SOOMLA Profile

## Use Case: Measure SOOMLA Profile logins


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
using Soomla.Profile;

public class TuneSoomlaProfileScript : MonoBehaviour {
    void Start () {
        // Listen for Soomla OnLoginFinished event
        ProfileEvents.OnLoginFinished += onLoginFinished;
        // Initialize Soomla Profile
        SoomlaProfile.Initialize();

        // Initialize TUNE SDK
        MATBinding.Init("tune_advertiser_id", "tune_conversion_key");
        // Measure initial app open
        MATBinding.MeasureSession();
    }

    void OnApplicationPause(bool pauseStatus) {
        if (!pauseStatus) {
            // Measure app resumes from background
            MATBinding.MeasureSession();
        }
    }

    // Set user ID and measure login event upon login finished
    public void onLoginFinished(UserProfile userProfileJson, string payload) {
        Provider provider = userProfileJson.Provider;
        string userId = userProfileJson.ProfileId;
        // Set different user ids in TUNE SDK based on provider
        if (provider == Provider.FACEBOOK) {
            MATBinding.SetFacebookUserId(userId);
        } else if (provider == Provider.GOOGLE) {
            MATBinding.SetGoogleUserId(userId);
        } else if (provider == Provider.TWITTER) {
            MATBinding.SetTwitterUserId(userId);
        } else {
            MATBinding.SetUserId(userId);
        }
        // Measure a login event for this user id
        MATBinding.MeasureEvent("login");
    }
}
        </code>
      </pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-cocos2dx">...</div>
    <div role="tabpanel" class="tab-pane" id="sample-ios">...</div>
    <div role="tabpanel" class="tab-pane" id="sample-android">
      <pre>
        <code class="java">
import android.app.Activity;
import android.os.Bundle;

import com.mobileapptracker.MATEvent;
import com.mobileapptracker.MobileAppTracker;
import com.soomla.BusProvider;
import com.soomla.Soomla;
import com.soomla.SoomlaConfig;
import com.soomla.profile.SoomlaProfile;
import com.soomla.profile.domain.IProvider;
import com.soomla.profile.domain.UserProfile;
import com.soomla.profile.events.auth.LoginFinishedEvent;
import com.squareup.otto.Subscribe;

public class MainActivity extends Activity {
    private MobileAppTracker mobileAppTracker;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Initialize the TUNE SDK
        mobileAppTracker = MobileAppTracker.init(
                getApplicationContext(),
                "tune_advertiser_id",
                "tune_conversion_key");

        // Initialize Soomla and Soomla Profile
        Soomla.initialize("[YOUR CUSTOM GAME SECRET HERE]");
        SoomlaProfile.getInstance().initialize();
    }

    @Override
    protected void onResume() {
        super.onResume();
        // Register to receive Soomla events
        BusProvider.getInstance().register(this);
        // Measure an app open in TUNE
        mobileAppTracker.setReferralSources(this);
        mobileAppTracker.measureSession();
    }

    @Override
    protected void onPause() {
        // Unregister from Soomla events
        BusProvider.getInstance().unregister(this);
        super.onPause();
    }

    @Subscribe
    public void onLoginFinished(LoginFinishedEvent loginFinishedEvent) {
        // On login finished, set the user ID in TUNE based on provider, and measure a login
        UserProfile user = loginFinishedEvent.UserProfile;
        if (user != null) {
            IProvider.Provider provider = user.getProvider();
            String userId = user.getProfileId();
            // Set different user ids in TUNE SDK based on provider
            if (provider == IProvider.Provider.FACEBOOK) {
                mobileAppTracker.setFacebookUserId(userId);
            } else if (provider == IProvider.Provider.GOOGLE) {
                mobileAppTracker.setGoogleUserId(userId);
            } else if (provider == IProvider.Provider.TWITTER) {
                mobileAppTracker.setTwitterUserId(userId);
            } else {
                mobileAppTracker.setUserId(userId);
            }
            // Measure a login event for this user id
            mobileAppTracker.measureEvent(MATEvent.LOGIN);
        }
    }
}
        </code>
      </pre>
    </div>
  </div>

</div>


## Support

For more information on SDK implementation and events to measure, please visit our [documentation](https://developers.mobileapptracking.com/mobile-sdks/).

If you have further questions, please feel free to contact us at [support@mobileapptracking.com](mailto:support@mobileapptracking.com).
