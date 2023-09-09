﻿using TestWebApplication.Selenium;

namespace TestWebApplication.Background
{
    public class BackgroundWork : BackgroundService
    {
        readonly ILogger<BackgroundWork> _logger;

        public BackgroundWork(ILogger<BackgroundWork> logger)
        {
            _logger = logger;
        }
        
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) 
            { 
                WeatherFinder weatherFounder = new WeatherFinder();
                weatherFounder.FindWeather();
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

    }
}
