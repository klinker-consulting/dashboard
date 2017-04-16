using System;
using Dashboard.Api.General.Actions;
using Dashboard.Api.Timers.Actions;
using Dashboard.Api.Weather.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Dashboard.Api.Weather.Service
{
    public interface IWeatherUpdaterService
    {
        void Start();
        void Stop();
    }

    public class WeatherUpdaterService : IWeatherUpdaterService
    {
        private readonly IActionSource _actionSource;
        private readonly IWeatherHub _weatherHub;
        private IDisposable _timerElapsedListener;

        public WeatherUpdaterService(IActionSource actionSource, IWeatherHub weatherHub)
        {
            _actionSource = actionSource;
            _weatherHub = weatherHub;
        }

        public void Start()
        {
            _timerElapsedListener = _actionSource.Listen<TimerElapsedAction>(OnTimerElapsed);
        }

        public void Stop()
        {
            _timerElapsedListener.Dispose();
        }

        private void OnTimerElapsed(TimerElapsedAction action)
        {
            _weatherHub.Update("50035", new WeatherDto());
        }
    }
}
