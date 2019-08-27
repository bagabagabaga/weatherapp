
# An Average WeatherApp

An Average WeatherApp is, surprisingly, a web app that returns averaged weather for a lot of places in Germany.
It's comprised of:
  - Vue in the fronted
  - ASP.NET Core 2.2 Web API in backend
  - SQLite for some light DB work

### Libraries used
- Frontend
    - Axios
    - Apexcharts
    - Vue-multiselect
    - Bootstrap
- Backend
    - EF Core
    - MoreLinq
    - Nlog
    - NSwag
 - Testing
	 - XUnit
    

### How to run?

Go to the weatherapp.api folder and run this command to start the API.
```sh
$ dotnet run
```
Go to the weatherapp.client folder and run these commands to run the Vue app.
```sh
$ npm install
$ npm run serve
```

### Notes

I've deleted the JSON data I've got from Openweathermap API page for database seeding. The numerous cities have been left in the sqlite .db file because the seeding process took a long time.

### Todos

 - Write additonal tests
 - Deploy the app somewhere

