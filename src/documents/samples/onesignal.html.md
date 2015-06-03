---
layout: "sample"
image: "Events"
title: "Events"
text: "Learn how to deliver push notifications triggered by android-store events."
position: 5
theme: 'platforms'
collection: 'android_store'
module: 'store'
platform: 'android'
---

# OneSignal + SOOMLA Store

## Use Case: Send push notifications after store events

<a title="OneSignal Push Notification Service" href="https://onesignal.com">OneSignal</a> is a free, multi-platform, push notification delivery system for mobile apps. OneSignal supports platforms including iOS, Android, Windows Phone, and Amazon fire devices.

Follow the directions below to see a great use care for how OneSignal can be integrated with the SOOMLA Store to increase paying player conversion.

1) Sign up and follow the instructions on the <a title="OneSignal Push Notification Service" href="https://onesignal.com">OneSignal</a> website to set up OneSignal multi-platform push notifications for your app.

2) Add the SOOMLA store: Import, drag the prefab, initialize and setup your virtual goods. Here are the <a href="http://know.soom.la/unity/store/store_gettingstarted/" target="_blank">full instructions</a>.

3) Add the code below into your game.

<div role="tabpanel">

  <!-- Nav tabs -->
  <ul class="nav nav-tabs nav-tabs-use-case-code" role="tablist">
    <li role="presentation" class="active"><a href="#sample-unity" aria-controls="unity" role="tab" data-toggle="tab">Unity</a></li>
    <li role="presentation"><a href="#sample-cocos2dx" aria-controls="cocos2dx" role="tab" data-toggle="tab">Cocos2d-x v3</a></li>
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
using Soomla;
using Soomla.Store;

//Apply this script to a gameobject in the scene where the store is initialized
public class purchaseTracking : MonoBehaviour {

    void Start() {
        StoreEvents.OnMarketPurchaseStarted += onMarketPurchaseStarted;
    }

    public void onMarketPurchaseStarted(PurchasableVirtualItem pvi) {
        OneSignal.SendTag("StartedPurchase", "true");
    }

}
        </code>
      </pre>

    </div>
    <div role="tabpanel" class="tab-pane" id="sample-cocos2dx">
      <pre>
        <code class="hljs cpp">

Director::getInstance()->getEventDispatcher()->addCustomEventListener(CCStoreConsts::EVENT_MARKET_PURCHASE_STARTED, CC_CALLBACK_1(Example::onMarketPurchaseStarted, this));

void Example::onMarketPurchaseStarted(EventCustom *event) {
  // DICT_ELEMENT_PURCHASABLE - the PurchasableVirtualItem whose purchase
  //                            operation has just started

  // Read purchase data if you need to.
  // __Dictionary *eventData = (__Dictionary *)event->getUserData();
  // CCPurchasableVirtualItem *purchasable = dynamic_cast<CCPurchasableVirtualItem *>(eventData->objectForKey(CCStoreConsts::DICT_ELEMENT_PURCHASABLE));

  _push->sendTag("StartedPurchase", "true");
}
</code>
</pre>
    </div>
    <div role="tabpanel" class="tab-pane" id="sample-ios">
              <pre>
        <code class="hljs objectivec">
// observe the event:
[[NSNotificationCenter defaultCenter] addObserver:self
  selector:@selector(marketPurchaseStarted:) name:EVENT_MARKET_PURCHASE_STARTED object:nil];

// your handler:
- (void)marketPurchaseStarted:(NSNotification*)notification {
  // notification's userInfo contains the following keys:
  // DICT_ELEMENT_PURCHASABLE = The item whose purchase process has started

  [OneSignal.defaultClient sendTag:@"StartedPurchase" value:@"true"];
}
</code>
</pre>
</div>
    <div role="tabpanel" class="tab-pane" id="sample-android">
              <pre>
        <code class="hljs java">
@Subscribe
public void onMarketPurchaseStarted(MarketPurchaseStartedEvent marketPurchaseStartedEvent) {
  OneSignal.sendTag("StoreOpened", "1");
}
</code>
</pre>
    </div>
  </div>

</div>

4) On the OneSignal website, set up a segment that matches users with the tag "StartedPurchase", who have amount spent equal to zero, and who have been inactive for at least 3 hours. This works because OneSignal automatically tracks when a user spends money or hasn't used your app in a while. Next, create an automatic message to send to that segment of players. 

5) You're done! Your users will now get a notification after they close the app if they had started making a purchase but never completed it. Go ahead and try out different varieties of this example to boost your user conversion and retention even further.
