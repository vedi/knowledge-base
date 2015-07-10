---
layout: "content"
image: "InAppPurchase"
title: "Google Play In-app Verification"
text: "Google Play in-app-purchase setup and integration with SOOMLA - set up verification."
position: 8
theme: 'platforms'
collection: 'android_store'
module: 'store'
platform: 'android'
---

#Google Play In-app Verification

##Getting Started

1. Link your API Project to your game, following [this](https://developers.google.com/android-publisher/getting_started#linking_your_api_project) section 

2. Create Web Application account in the Google APIs Console. Fill values of: "Redirect URIs" and "JavaScript origins".
  You can use default values, because we will not use them. The new record for "Client ID for web application" will be created.
      
  From this step you can get `clientId` and clientSecret`. 

  ![alt text](/img/tutorial_img/google_play_verification/create_web_application.png "Creating Web App")

3. Get `refreshToken`:

  1. Put the following URL in a browser on your machine: `https://accounts.google.com/o/oauth2/auth?scope=https://www.googleapis.com/auth/androidpublisher&response_type=code&access_type=offline&redirect_uri=<YOUR_REDIRECT_URI>&client_id=<YOUR_CLIENT_ID>`  
  and allow requested rights.
  
  2. The browser will be redirected to `redirect_uri` you've provided. Have a look at the address bar. There is `code` param
    with a value we need for the next step, looking like: "4/...#"
  
  3. Exchange "code" to "refresh token" performing the POST request to URL `https://www.googleapis.com/oauth2/v3/token`
  with the following params:
   ```
    grant_type=authorization_code 
    client_id=<YOUR_CLIENT_ID>
    client_secret=<YOUR_CLIENT_SECRET> 
    redirect_uri=<YOUR_REDIRECT_URI>
    code=<CODE_YOU_GOT_BEFORE> 
   ```
   
     You should get the following JSON-response:
   ```
   {
       "refresh_token": <YOUR_REFRESH_TOKEN>, 
       "access_token": <WE_DO_NOT_NEED_THIS>, 
       "expires_in": <WE_DO_NOT_NEED_THIS>, 
       "token_type": "Bearer"
   }
   ```
   
  **NOTE:**
   If you got a success response, but there is no refresh token. You can try to force getting it. Just add 
   `approval_prompt=force` to the URL, when you request code.
  

##Useful links

- [Quick definition of Google Play Developer API](http://developer.android.com/google/play/developer-api.html#subscriptions_api_overview)

- [Google Play Developer API Getting Started](https://developers.google.com/android-publisher/getting_started)

- [How to get tokens](https://developers.google.com/identity/protocols/OAuth2WebServer)

- [Google Play Developer API: Purchases](https://developers.google.com/android-publisher/api-ref/purchases/products)

