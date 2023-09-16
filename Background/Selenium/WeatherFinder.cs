using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Xml.Linq;
using TestWebApplication.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting.Server;
//using Newtonsoft.Json;
using System.IO;
using OpenQA.Selenium.Firefox;
using Microsoft.Extensions.Options;

namespace TestWebApplication.Selenium
{
    public class WeatherFinder
    {
        
        public void FindWeather() 
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("--headless");
            firefoxOptions.AddArgument("no-sandbox");
            var driver = new FirefoxDriver(@"wwwroot/Drivers/geckodriver", firefoxOptions);

            driver.Navigate().GoToUrl("https://vclock.com/time/");
            var weatherDays = driver.FindElements(By.CssSelector(".panel.panel-default.panel-heading-fullwidth"));

            List<Weather> weatherData = new List<Weather>();
            foreach (var weatherDay in weatherDays)
            {
                Weather day = new Weather();
                try
                {
                    day.Day = weatherDay.FindElement(By.CssSelector(".title.text-ellipsis")).Text;
                    day.TempatureMax = weatherDay.FindElement(By.CssSelector(".colored.digit.text-nowrap.text-center.font-digit")).Text;
                    day.TempatureMin = weatherDay.FindElement(By.CssSelector(".colored.text-center")).Text;
                    weatherData.Add(day);
                }
                catch
                {

                }
                
            }

            driver.Close();
            string json = System.Text.Json.JsonSerializer.Serialize(weatherData);
            File.WriteAllText(@"wwwroot/Data/WeatherData.json", json);

            //https://stackoverflow.com/questions/16921652/how-to-write-a-json-file-in-c
        }

        public IEnumerable<Weather> GetWeather()
        {
            string json = File.ReadAllText(@"wwwroot/Data/WeatherData.json");
            IEnumerable<Weather> weatherForecast = JsonSerializer.Deserialize<IEnumerable<Weather>>(json);
            if (weatherForecast != null)
            {
                return weatherForecast;
            }
            else
                return Enumerable.Empty<Weather>();
        }

    }

}
