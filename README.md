# webapi_and_backapis
Solution consists of 6 projects:
- 2 API projects (as backend api), and their respective test project
- a webApi project, calling and combining calls to both backend apis
- a Blazor project (as Server-side Blazor) for the front end, invoking endpoints of the webApi.

The BackendAPI2 simulates an API for weather forecasts information (date, temperature, zipcode).
The BackendAPI3 simulates an API for zip codes information (zipcode, city, county, state).

The webApi combines the calls to make a "full weather forecast" information, where are combined: ate, temperature, zipcode, city, county, state.
That webApi uses 2 HttpClients, each pointing to a backendApi.

Each backendApi has a Properties>launchSettings.json file, where a port is assigned for the localhost Kestrel webserver.
The webApi references those values in its appsettings.json file.

Similarly, the Blazor application stores the URL exposed for the webApi (in its launchSettings.json) in its appsettings.json file, and populates an HttpClient.

In order to test locally, it is required to have multiple projects started, as follows:

  ![image](https://github.com/wilverbau/webapi_and_backapis/assets/105066089/44dcf7b1-ba42-46d8-bf1a-b4d1d115c357)

In the end, we get 4 web tabs being launched, 1 for each backend api, 1 for the webapi and 1 for the Blazor app.
![image](https://github.com/wilverbau/webapi_and_backapis/assets/105066089/32019c01-35fb-4f1b-b448-7b827347ab05)
