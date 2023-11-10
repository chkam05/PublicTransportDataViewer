# PublicTransportDataViewer
Application and libraries to download and view Public Transport Data in Poland.

## Application overview:

![Main Window (Doc/Images/main_page.jpg)](Doc/Images/main_page.png)

The application is divided into several main elements/libraries:
- __PublicTransportDataViewer__ - main application that allows to display public transport data.
- __DownloaderCore__ - the main library containing tools for downloading data (downloader, serializer, request and response data models).
- __MpkCzestochowaDownloader__ - a library that allows you to download MPK Częstochowa public transport data.
- __ZtmDataDownloader__ - a library enabling downloading public transport data of the Upper Silesian-Zagłębie Metropolis.
- __DataTester__ - unit tests of data retrieval libraries.

From the main application window, you can select the city whose public transport data you want to download and configure the application (appearance, language).

## Sections:

- [MpkCzestochowa](Doc/MpkCzestochowa.md)
- [Ztm](Doc/Ztm.md)
- [Settings](Doc/Settings.md)

## Data Sources:

- [MpkCzestochowa](https://www.czestochowa.pl/rozklady-jazdy)
- [ZTM](https://rj.metropoliaztm.pl/)

## Used additional libraries:

- [chkam05.Tools.ControlsEx 2.4.4](https://github.com/chkam05/Tools.ControlsEx)
- [MaterialDesignThemes 4.5.0](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
- [Newtonsoft.Json 13.0.3](https://www.newtonsoft.com/json)
