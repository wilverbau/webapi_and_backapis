# webapi_and_backapis
Solution consists of 6 projects:
- 2 API projects (as backend api), and their respective test project
- a webApi project, calling and combining calls to both backend apis
- a Blazor project (as Server-side Blazor) for the front end, invoking endpoints of the webApi.

The BackendAPI2 simulates an API for weather forecasts information (date, temperature, zipcode).
The BackendAPI3 simulates an API for zip codes information (zipcode, city, county, state).

The webApi combines the calls to make a "full weather forecast" information, where are combined: ate, temperature, zipcode, city, county, state.
That webApi uses 2 HttpClients, each pointing to a backendApi.

Each backendApi has a 

  ![image](https://github.com/wilverbau/webapi_and_backapis/assets/105066089/44dcf7b1-ba42-46d8-bf1a-b4d1d115c357)

