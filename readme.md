### Requirements

- Need free Syncfusion Community license for Xamarin Forms
- Need free Auth0 account, configure an application and API

Create settings file ```settings.json``` in gametrove.core containing;
```
{
  "syncfusion": "<syncfusion license here>",
  "api": {
    "url": "<server url here>"
  },
  "auth": {
    "domain": "",
    "clientid": "",
    "audience": "" 
  }
}
```

Copy the settings file as settings.Debug.json, this version will be included when you are in Debug mode. 