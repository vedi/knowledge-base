---
layout: "content"
image: "Tutorial"
title: "Getting Started (SDKBOX)"
text: "Get started with GROW open analytics (and Whales Report) for Cocos2d-x with SDKBOX. Doesn't Include any of SOOMLA's opensource modules, only Highway and SoomlaInsights."
position: 30
theme: 'platforms'
collection: 'cocos2dx_grow'
module: 'grow'
lang: 'cpp'
platform: 'cocos2dx'
---

#Getting Started

##Overview

Soomla GROW is our flagship, community driven, data network. Developers using GROW can gain valuable insights about their games' performance and compare the data to benchmarks of other games in the GROW community.

Whales Report and SoomlaInsights are also provided to users of the GROW network. SoomlaInsights is an SDK provided with the GROW packages and Whales Report are automatically sent to the emails of users with live & active games.

**Note:** This document is for users of SDKBOX who don't want to integrate SOOMLA's opensource modules into their game.

##Getting Started

Go to the [GROW dashboard](http://dashboard.soom.la) and sign up \ login. Upon logging in, you will be directed to the main page of the dashboard. On the left side panel, you can click on "Demo Game" in order to know what to expect to see once you start using Grow.

1. Click on the right pointing arrow next to "Demo Game" > "Add New App" and fill in the required fields.

  ![alt text](/img/tutorial_img/unity_grow/addNewApp.png "Add new app")

> Once you created a new App, you'll see your game and environment keys. Use them in the following instructions. (You can ignore other installation instructions on the dashboard as you're only using SDKBOX).

2. Install `soomla-grow` package on SDKBOX as explained [here](http://soom.la).

3. Once you finished installation, add "Game Key" and "Env Key" from the GROW dashboard to SDKBOX's configuration json:

  ![alt text](/img/tutorial_img/cocos_grow/dashboardKeys.png "Keys")

[ADD JSON EXAMPLE HERE]

> you're probably testing your integration and you want to use the **Sandbox** environment key for starters.

  <div class="info-box">The *game* and *environment* keys allow for your game to distinguish multiple environments for the same game. The dashboard pre-generates two fixed environments for your game: **Production** and **Sandbox**. When you decide to publish your game, make sure to switch the env key to **Production**.  You can always generate more environments.  For example - you can choose to have a playground environment for your game's beta testers which will be isolated from your production environment and will thus prevent analytics data from being mixed between the two.  Another best practice is to have a separate environment for each version of your game.</div>



###**Instructions for iOS**

In your XCode project, perform the following steps:

1. Add `jansson` (**external/jansson/**) to your project (just add it as a source folder, make sure to check "create group").

2. For each of the following XCode projects:

  - `Cocos2dXHighway.xcodeproj` (**extensions/cocos2dx-highway/**)

  perform the following:

  - Drag the project into your project
  - Add its targets to your **Build Phases->Target Dependencies**
  - Add the Products (\*.a) of the project to **Build Phases->Link Binary With Libraries**.

  ![alt text](/img/tutorial_img/cocos_grow/iosStep2.png "iOS Integration")

3. Add the following directories to **Build Settings->Header Search Paths** (with the `recursive` option):

  NOTE: This article assumes you have a `cocos2d` folder under your project folder which either contains the Cocos2d-x framework, or links to to its root folder.

 - `$(SRCROOT)/../cocos2d/extensions/cocos2dx-highway/Soomla`

  ![alt text](/img/tutorial_img/cocos_grow/headerSP.png "Header search paths")

4. Add the AFNetworking dependency to your project:
  - Add the static library (from `extensions/cocos2dx-highway/build/ios/libAFNetworking.a`) to **Build Phases->Link Binary With Libraries**.  Achieve this by clicking the + icon, and then "Add Other", and browse for the file.
  - Add `$(SRCROOT)/../cocos2d/extensions/cocos2dx-highway/build/ios/` to **Build Settings->Library Search Paths** (non-recursive)

5. Add the `-ObjC` to the **Build Setting->Other Linker Flags**

  ![alt text](/img/tutorial_img/ios_getting_started/linkerFlags.png "Linker Flags")

6. Make sure you have these 7 Frameworks linked to your XCode project:

  - Security
  - libsqlite3.0.dylib
  - StoreKit
  - CFNetwork
  - libicucore
  - SystemConfguration
  - AdSupport

That's it! Now all you have to do is build your XCode project and run your game.

###**Instructions for Android**

1. Import cocos2dx-highway module into your project's Android.mk by adding the following:

    ```
    LOCAL_WHOLE_STATIC_LIBRARIES += cocos2dx_highway_static
    # add this line along with your other LOCAL_WHOLE_STATIC_LIBRARIES

    $(call import-module, extensions/cocos2dx-highway)
    # add this line at the end of the file, along with the other import-module calls
    ```

2. Add the following jars to your android project's classpath:

  From `extensions/cocos2dx-highway/build/android`

    - AndroidViper.jar???

    - Cocos2dxAndroidHighway.jar???

    - google-play-services.jar???

4. Update your `AndroidManifest.xml`:

  ``` xml
  <uses-permission android:name="android.permission.INTERNET"/>
  <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
  ```

That's it! Don't forget to run the **build_native.py** script so that SOOMLA sources will be built with cocos2d-x.



##Back to the Dashboard

Once your app is running, you can go back to the [GROW dashboard](http://dashboard.soom.la) to verify the integration. Just refresh the page, and the environments tab should appear (be patient, this step can take a few minutes).

![alt text](/img/tutorial_img/unity_grow/verifyIntegration.png "Verify Integration")

And that's it! Whales Report, SoomlaInsights and GROW Analytics.
