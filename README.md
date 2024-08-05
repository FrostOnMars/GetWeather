# GetWeather

Hello, world!

PURPOSE OF THIS APP:
This app is a practice application using C#, Api, and (Postgres) SQL to learn new skills and demonstrate what I can do for future employers. It searches the names of 5 hard-coded cities, finds the 5 closest matches, and returns weather conditions into a SQL database. Cities are hard-coded here for demonstration, but future extensibility may include ways to read input from a variety of sources (tables, user input, etc) and search by city name, lat/lon, and zip code. 

I have added methods for converting units of measurement and translating weather data into user-friendly language. Examples of these conversions include:
  - converting wind speed degrees into directions on a compass
  - describing wind speed integers to the Beaufort Scale
  - converting cloud cover from percentages into descriptive English language
  - converting Kelvin and metric units into Imperial 

GetWeather relies on the free version of OpenWeather API, available at https://openweathermap.org/. The API call consists of two important parts:
- first I call for Location, returning a list of 5 cities that are close to the city name input as well as their geodata (lat/lon, names in other languages, etc)
- second, the API accesses weather data for each city in that list

CODE FLOW
  1. First, the UrlController class determines how to build the API call. Geo and weather data use different urls. The abstract class UrlController writes the instructions for how to join BaseUrl with UrlParameters. The children classes (WeatherUrlController and GeoUrlController) override the specifics required for each unique url call. 
  2. The FullUrl is passed to GeoDataController, which returns with location information, such as city names and lat/lon coordingates, which are needed to build the weather URL call.
  3. For each city in the GeoData list, the UrlController creates a unique call using lattitude and longitude coordinates.
  4. Because Json is notoriously unreliable about providing arrays or single objects, I use the class JsonHelper to accept either an array or a single object when deserializing the Json response. 
  5. The deserialized responses return weather conditions data that gets populated in WeatherData models.
  6. For each WeatherData model, the SqlController uses a stored procedure to populate tables with weather data, which is converted into user-friendly language by methods in the WeatherDescriber class.

Database final product:
![Screenshot 2024-08-05 135918](https://github.com/user-attachments/assets/bc87472c-83c3-43b5-a842-719a59a7a8eb)

Examples of logic that explain weather conditions in a more user-friendly way:
![Screenshot 2024-08-05 151128](https://github.com/user-attachments/assets/fe6f6dfe-6e80-48f7-894d-9447f280bb3a)
![Screenshot 2024-08-05 151208](https://github.com/user-attachments/assets/e3e028eb-f357-47ee-bcc8-be704afd17db)

