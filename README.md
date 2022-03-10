# Weather App API - Backend service

This project was generated with Minimal APIs in .NET 6.

## About API:
  This API is designed to fetch the weather details from an External API - `openweathermap`

## Solution Structure:
1. WeatherAppApi - API end point
2. WeatherApp.Domain - Contains domain logics

## Config Information:
appsettings.json contains the config information.
1. Logging - Contains the logging configuration
2. ForecastDays - Number of Forecast days to be displayed in UI
3. NoOfHours - Number of hourly weather graph report to be displayed in UI  
4. ApiKey - key to be attached when access `openweathermap` API
5. Serilog - Serilog config details

## Concepts implemented
1. Minimal API
2. Serilog
3. Builder Pattern - It is used to construct a Weather object step by step and the final step returns the Weather object. Also, the process of constructing an object should be generic so that it can be used to create different representations of the same object. This will help to extend the implementation. If we want to use another external API then we can change the builder and construct Weather object with the same pattern.
4. Factory Pattern - We can create different instance based on the requirement. It supports the extension of the implementation
5. Caching - Implemented InMemory caching to speed up the API response time
6. Logging - Created separate service to handle logging mechanism - single responsibility
