using Microsoft.AspNetCore.Mvc;
using Moq;
using TennisBookings.Web.Controllers;
using TennisBookings.Web.External.Models;
using TennisBookings.Web.Services;
using TennisBookings.Web.ViewModels;
using Xunit;

namespace TennisBookings.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsSun()
        {
            var mockWeatherForcaster = new Mock<IWeatherForecaster>();
            mockWeatherForcaster.Setup(w => w.GetCurrentWeather()).Returns(new WeatherResult() { WeatherCondition = WeatherCondition.Sun });

            var sut = new HomeController(mockWeatherForcaster.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("It's sunny right now", model.WeatherDescription);
        }

        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsRain()
        {
            var mockWeatherForcaster = new Mock<IWeatherForecaster>();
            mockWeatherForcaster.Setup(w => w.GetCurrentWeather()).Returns(new WeatherResult() { WeatherCondition = WeatherCondition.Rain });

            var sut = new HomeController(mockWeatherForcaster.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("We're sorry but it's raining here.", model.WeatherDescription);
        }
    }
}
