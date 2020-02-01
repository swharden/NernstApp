### To Set the Icon
* create 512x512 png and save in assets/drawings/icon.png
* in project settings, manifest, select icon as @drawable/icon
* _test icons with [icons-launcher.html](https://romannurik.github.io/AndroidAssetStudio/icons-launcher.html)_
  
### To Deploy to Google Play Store
* modify project options to set package format to bundle
* modify project to ensure version code is unique
* right-click the project and hit archive
* after it successfully builds click the distribute button
  * save it somewhere
  * upload it through the google play console web interface