# 🌤️ LiveWeather

> A clean and minimal **.NET 8 Web API** that fetches live **weather** and **air quality** data using the **OpenWeather API**.  
> Designed with **Clean Architecture**, dependency injection, and unit testing.

---

## 🧩 Overview
**LiveWeather** allows you to query a city name and get real-time:
- 🌡️ Temperature (°C)  
- 💧 Humidity (%)  
- 🌬️ Wind Speed (m/s)  
- 🌫️ Air Quality Index (AQI)  
- 🧪 Pollutants (CO, NO₂, PM2.5, etc.)  
- 📍 Coordinates (Latitude / Longitude)

---

## ⚙️ Tech Stack

| Layer | Description |
|-------|--------------|
| **Domain** | Configuration models (`OpenWeatherConfig`) |
| **Application** | DTOs, Interfaces, and Business logic (`WeatherService`) |
| **Infrastructure** | API integration (`WeatherCrawller`), Retry & Logging |
| **Endpoint** | Web API controllers (`WeatherController`) |
| **Tests** | xUnit test for `Infrastructor(GetByCityName)` |

🛠 Built with  
- **.NET 8 Web API**  
- **HttpClient**  
- **ILogger / IOptions pattern**  
- **xUnit + FluentAssertions**  
- **Clean Architecture layering**

---
## 📡 Example Response
```json
{
  "success": true,
  "data": {
    "city": "Tehran",
    "temperature": 24.8,
    "humidity": 13,
    "windSpeed": 8.2,
    "aqi": 3,
    "pollutants": {
      "co": 124.3,
      "no": 0.5,
      "no2": 7.9,
      "o3": 103.2,
      "so2": 2.9,
      "pm2_5": 13.6,
      "pm10": 33.6,
      "nh3": 0.07
    },
    "latitude": 35.6944,
    "longitude": 51.4215
  }
}
