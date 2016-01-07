---
layout: "content"
image: "Tutorial"
title: "Grow with Facebook"
text: "Add GROW to your existing project with Facebook integration"
position: 15
theme: 'platforms'
collection: 'unity_grow'
module: 'grow'
platform: 'unity'
---

# Integrating Grow with Facebook

## Overview

 GROW is SOOMLA's flagship - a community-driven data network. Mobile game studios can take advantage of the different GROW products in order to get valuable insights about their games' performance and increase retention and monetization. [Read more...](/university/articles/Grow_About)

GROW:

- [Analytics](/university/articles/Grow_Analytics)

- [Whales Report](/university/articles/Grow_WhalesReport)

- [Insights](/unity/grow/Grow_Insights)

## Integrating Grow with Facebook

### New Game & Configurations

Go to the [GROW dashboard](http://dashboard.soom.la) and sign up \ login. Upon logging in, you will be directed to the main page of the dashboard. You will need to create a new game in order to start your journey with GROW.

1. In the games screen click on the "+" button to add a new game. If it's your first time in the dashboard, just click on the "+" button underneath the "Create your first game" label in the middle of the screen.

	  ![alt text](/img/tutorial_img/unity_grow/addNewApp.png "Add new app")

	Once you created your game, you'll be redirected to a quick start process to download any of the GROW bundles (You can also click "Downloads" on the top right corner of the screen). Click on **Facebook**. You'll see an instructions screen, you can continue with that or stay here for the extended version.  

2. Double-click on the downloaded Unity package, it'll import all the necessary files into your Unity project.

	![alt text](/img/tutorial_img/unity_grow/importHighway.png "import")

3. In the menu bar go to **Window > GROW Settings**:

	![alt text](/img/tutorial_img/unity_grow/soomlaSettingsStoreAndHighway.png "SOOMLA Settings")

	a. **Copy the "Game Key" and "Environment Key"** given to you from the [dashboard](http://dashboard.soom.la) into the fields in the settings pane of the Unity Editor. At this point, you're probably testing your integration and you want to use the **Sandbox** environment key.

	<div class="info-box">The "game" and "environment" keys allow GROW to distinguish between multiple environments of your games. The dashboard pre-generates two fixed environments for your game: **Production** and **Sandbox**. When you decide to publish your game, **make sure to switch the environment key to <u>Production</u>**.  You can always generate more environments:  For example - you can choose to have a playground environment for your game's beta testers which will be isolated from your production environment and will thus prevent analytics data from being mixed between the two.  Another best practice might be to have a separate environment for each version of your game.</div>

	<img src="/img/tutorial_img/unity_grow/dashboardKeys.png" alt="Game key and Env key" style="border:0;">

	b. Click on the **Add Prefab** button and the prefab will be added to the **current scene**.
	<div class="info-box"> Make sure that you are in your Main Scene when adding the Prefab </div>
