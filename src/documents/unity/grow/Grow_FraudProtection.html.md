---
layout: "content"
image: "FraudProtection"
title: "IAP Fraud Protection"
text: "Get started with Grow IAP Fraud Protection."
position: 4
theme: 'platforms'
collection: 'unity_grow'
module: 'grow'
platform: 'unity'
---

# IAP Fraud Protection

## Introduction

There are dozens ways to hack the games. In general we can divide them into 2 categories: 

* File Overwriting - Hackers search games for important files and variables containing the current game score, currency 
  balance, and level progression. And change values to their benefit.
* Fake in game purchases - Special hacking applications perform the faking communications with the game server. 

## Encrypted Storage

Very often the developers save game data in plain text formats that makes it possible to hackers to find and to recognize these data.
After that they can replace values and get benefits in the game.

There is a robust solution for this issue. It's an encrypting. If you use strong algorithms and keys to encrypt your data,
it makes almost impossible to hackers to decrypt them.

Soomla's solutions use the encrypted storage and do the best to prevent your games from such kind of hacking. 
Definitely you already noticed the "Soomla Secret" setting, when you set up Soomla libraries. It's up to you to define 
as strong key as it's possible, and keep it in secret to prevent hacking of your games.

## Server Side Verification

When your games perform in-app purchasing, they communicate with App Store or Google Play server asking whether a transaction
was completed successfully, and after that your game gives purchased virtual goods to the user. 

Hacking software intercepts requests to the App Store or Google Play and emulates their behaviour. As result your games
will give virtual goods to a hacker for free.
 
The best way to prevent it to use a private server to do the verifications. When an user buys something from an app 
they are sent an electronic receipt. The game sends the receipt to your server. And the server performs a request to 
Google or Apple server to prove the receipt is valid. After that the server sends a respond to your game with result of 
verification. And only after that your game gives virtual goods to the user.

The most games do not have a server, because the gameplay happens on the client side. Implementing and maintenance of 
a server it's just additional article in your budget, and it's not evident for your investors to pay for it. That's why 
 some games skip the server side verification, and this hole in security extensively used by the hackers.

Good news! Today you do not need such server at all. Soomla provides this receipt validation server. After verifying a 
receipt, it performs an extra step and checks for suspicious activities.

## Fraud protection in Unity3d

In order to turn on Fraud Protection in Unity3d, you just need to click on `Fraud Protection` check box in Soomla Settings.
Optionally, you can turn on `Verify On Server Failure` if you want to get purchases automatically verified in case of 
network failures during the verification process.

Additionally you need to provide specific params needed for your billing service provider.

* For Google Play go over [Google Play Purchase Verification](/android/store/Store_GooglePlayVerification).
