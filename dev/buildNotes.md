## Customize Your Xamarin App Icon (Feb 2020)

### Xamarin.Forms
* use android studio's [image asset studio](https://developer.android.com/studio/write/image-asset-studio) to create a set of icons
* place them in the Resources folder replacing all the `icon.png` and `launcher_foreground.png` files that exist there

### Xamarin Native Android
* create 512x512 png and save in assets/drawings/icon.png
* in project settings, manifest, select icon as @drawable/icon
* _test icons with [icons-launcher.html](https://romannurik.github.io/AndroidAssetStudio/icons-launcher.html)_
  
## Deploy Your Xamarin App to the Google Play Store
* ensure the release build is selected (not debug)
* modify project (manifest) to ensure version code is unique
* modify project (options) to set package format to bundle
* right-click the project and hit archive
* after it successfully builds click the distribute button
  * on the signing identity screen create or select your key
  * save the `.aab` file somewhere (you'll need your secure key)
  * upload it through the google play console web interface