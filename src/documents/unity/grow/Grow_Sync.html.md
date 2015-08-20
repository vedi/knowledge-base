---
layout: "content"
image: "Tutorial"
title: "Sync"
text: "Get started with GROW Sync for Unity. Here you can find initialization instructions, event handling and usage examples."
position: 5
theme: 'platforms'
collection: 'unity_grow'
module: 'grow'
platform: 'unity'
---

# Sync

## Overview

GROW Sync brings you a complete solution for saving your game state in the cloud, cross device synching and remote economy (metadata) management.
With SYNC you can:

- Save game progress & virtual items on the server.
- Restore the state upon uninstalling and reinstalling the game.
- Cross device synching - allow the player to have the same game state across all of his/her devices.
- Remote Economy (MetaData) Management - manage your economy without changing any code in your game.

## Integration

<div class="info-box">GROW Sync is included in [GrowSpend](/unity/grow/GrowSpend_GettingStarted#SetupGrowSpend), [GrowCompete](/unity/grow/GrowCompete_GettingStarted#SetupGrowCompete) and [GrowUltimate](/unity/grow/GrowUltimate_GettingStarted#SetupGrowUltimate) bundles. Please refer to the relevant bundle for initialization instructions.</div>


1. Initialize `SoomlaSync` according to the instructions of your relevant bundle.

* Create event handler functions in order to be notified about (and handle) GROW Sync related events. See [Events](/unity/grow/Grow_Sync/#Events) for more information.

* Once initialized, GROW Sync will start syncing the player's state and game meta-data, depending on the integrated modules. Synchronization is done with a unique identifier for every device.

* If PROFILE is integrated, once the player logs into a social network the game state will be synched across all of the player's devices, on which he/she is logged in with the same profile for that social provider.

<div class="warning-box">Make sure to start gameplay or change local state only **after** [`OnStateSyncFinished`](/unity/grow/Grow_Sync#OnStateSyncFinished) event is triggered. </div>

## Events

Following is a list of all the events in GROW Sync and an example of how to observe & handle them.

### OnSoomlaSyncInitialized

This event is triggered when the GROW Sync feature is initialized and ready.

``` cs
HighwayEvents.OnSoomlaSyncInitialized += onSoomlaSyncInitialized;

public void onSoomlaSyncInitialized() {
// ... your game specific implementation here ...
}
```

### OnMetaDataSyncStarted

This event is triggered when metadata sync has started.

``` cs
HighwayEvents.OnMetaDataSyncStarted += onMetaDataSyncStarted;

public void onMetaDataSyncStarted() {
// ... your game specific implementation here ...
}
```

### OnMetaDataSyncFinished

This event is triggered when metadata sync has finished.
Provides a list of modules which were synced.

``` cs
HighwayEvents.OnMetaDataSyncFinished += onMetaDataSyncFinished;

public void onMetaDataSyncFinished(IList<string> modules) {
// ... your game specific implementation here ...
}
```

### OnMetaDataSyncFailed

This event is triggered when metadata sync has failed.
Provides the error code and reason of the failure.

``` cs
HighwayEvents.OnMetaDataSyncFailed += onMetaDataSyncFailed;

public void onMetaDataSyncFailed(MetaDataSyncErrorCode errorCode, string failReason) {
// ... your game specific implementation here ...
}
```

### OnStateSyncStarted

This event is triggered when state sync has started.

``` cs
HighwayEvents.OnStateSyncStarted += onStateSyncStarted;

public void onStateSyncStarted() {
// ... your game specific implementation here ...
}
```

### OnStateSyncFinished

This event is triggered when metadata sync has finished.
Provides a list of modules which had their state updated, and a list of modules which failed to update.

``` cs
HighwayEvents.OnStateSyncFinished += onStateSyncFinished;

public void onStateSyncFinished(IList<string> changedComponents, IList<string> failedComponents) {
// ... your game specific implementation here ...
}
```

<div class="info-box">`OnStateSyncFinished` event may be triggered not only when the game starts, but on other occasions as well. A sync process will start also after user login to a social network, or if the game goes back to foreground after being in the background for a while.</div>

### OnStateSyncFailed

This event is triggered when metadata sync has failed.
Provides the error code and reason of failure.

``` cs
HighwayEvents.OnStateSyncFailed += onStateSyncFailed;

public void onStateSyncFailed(StateSyncErrorCode errorCode, string failReason) {
// ... your game specific implementation here ...
}
```

## Main Classes & Methods

Here you can find descriptions of the main classes of GROW Sync.

### SoomlaSync

`SoomlaGifting` sepresents a class which is in charge of syncing meta-data and state between the client and the server.

#### Functions

**`Initialize(metaDataSync, stateSync)`**

Initializes the GROW Sync feature. Once initialized, the `OnSoomlaSyncInitialized` event is triggered.
Params:

- metaDataSync - should GROW Sync synchronize meta-data for integrated modules.
- stateSync - should GROW Sync synchronize state for integrated modules.

## example

Coming soon
