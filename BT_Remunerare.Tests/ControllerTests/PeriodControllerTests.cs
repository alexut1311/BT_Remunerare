using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.Controllers;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BT_Remunerare.Tests.ControllerTests
{
    public class PeriodControllerTests
    {
        private Mock<IPeriodLogic>? _periodLogic;
        private Mock<IPeriodControllerHelper<PeriodViewModel, PeriodDTO>>? _periodControllerHelper;
        private PeriodController? _periodController;


        [SetUp]
        public void Setup()
        {
            _periodLogic = new Mock<IPeriodLogic>();
            _periodControllerHelper = new Mock<IPeriodControllerHelper<PeriodViewModel, PeriodDTO>>();
            _periodController = new PeriodController(_periodLogic.Object, _periodControllerHelper.Object);
        }

        [Test]
        public void AddPeriod_OkResult()
        {
            Response response = new()
            {
                IsSuccesful = true
            };

            PeriodDTO periodDTO = new()
            {
                Year = 1,
                Month = 1
            };

            _ = _periodControllerHelper.Setup(s => s.BuildDTO(It.IsAny<PeriodViewModel>())).Returns(periodDTO);
            _ = _periodLogic.Setup(s => s.AddPeriod(It.IsAny<PeriodDTO>())).Returns(response);

            StatusCodeResult? result = _periodController.AddPeriod(It.IsAny<PeriodViewModel>()) as StatusCodeResult;
            Assert.That(result.StatusCode, Is.EqualTo(200));

            _periodControllerHelper.Verify(s => s.BuildDTO(It.IsAny<PeriodViewModel>()), Times.Once);
            _periodLogic.Verify(s => s.AddPeriod(It.IsAny<PeriodDTO>()), Times.Once);
        }

        [Test]
        public void AddPeriod_BadResult()
        {
            string errorMessage = "Error";
            Response response = new()
            {
                IsSuccesful = false,
                ErrorMessage= errorMessage
            };

            PeriodDTO periodDTO = new()
            {
                Year = 1,
                Month = 1
            };

            _ = _periodControllerHelper.Setup(s => s.BuildDTO(It.IsAny<PeriodViewModel>())).Returns(periodDTO);
            _ = _periodLogic.Setup(s => s.AddPeriod(It.IsAny<PeriodDTO>())).Returns(response);

            ObjectResult result = _periodController.AddPeriod(It.IsAny<PeriodViewModel>()) as ObjectResult;
            Assert.That(result.StatusCode, Is.EqualTo(500));

            var resultValue = result.Value as Response;
            Assert.That(resultValue.IsSuccesful, Is.EqualTo(false));
            Assert.That(resultValue.ErrorMessage, Is.EqualTo(errorMessage));


            _periodControllerHelper.Verify(s => s.BuildDTO(It.IsAny<PeriodViewModel>()), Times.Once);
            _periodLogic.Verify(s => s.AddPeriod(It.IsAny<PeriodDTO>()), Times.Once);
        }

        [Test]
        public void AddPeriod_ThrowsException()
        {
            _ = _periodLogic.Setup(s => s.AddPeriod(It.IsAny<PeriodDTO>())).Throws<NullReferenceException>();

            ObjectResult result = _periodController.AddPeriod(It.IsAny<PeriodViewModel>()) as ObjectResult;
            Assert.That(result.StatusCode, Is.EqualTo(500));

            var resultValue = result.Value as Response;
            Assert.That(resultValue.IsSuccesful, Is.EqualTo(false));
            Assert.That(resultValue.ErrorMessage, Is.EqualTo("Object reference not set to an instance of an object."));

            _periodControllerHelper.Verify(s => s.BuildDTO(It.IsAny<PeriodViewModel>()), Times.Once);
            _periodLogic.Verify(s => s.AddPeriod(It.IsAny<PeriodDTO>()), Times.Once);
        }

        [Test]
        public void DeletePeriod_OkResult()
        {
            Response response = new()
            {
                IsSuccesful = true
            };

            _ = _periodLogic.Setup(s => s.DeletePeriod(It.IsAny<int>())).Returns(response);

            StatusCodeResult? result = _periodController.DeletePeriod(It.IsAny<int>()) as StatusCodeResult;
            Assert.That(result.StatusCode, Is.EqualTo(200));

            _periodLogic.Verify(s => s.DeletePeriod(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void DeletePeriod_BadResult()
        {
            string errorMessage = "Error";
            Response response = new()
            {
                IsSuccesful = false,
                ErrorMessage = errorMessage
            };

            _ = _periodLogic.Setup(s => s.DeletePeriod(It.IsAny<int>())).Returns(response);

            ObjectResult result = _periodController.DeletePeriod(It.IsAny<int>()) as ObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(500));

            var resultValue = result.Value as Response;
            Assert.That(resultValue.IsSuccesful, Is.EqualTo(false));
            Assert.That(resultValue.ErrorMessage, Is.EqualTo(errorMessage));


            _periodLogic.Verify(s => s.DeletePeriod(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void DeletePeriod_ThrowsException()
        {
            _ = _periodLogic.Setup(s => s.DeletePeriod(It.IsAny<int>())).Throws<NullReferenceException>();

            ObjectResult result = _periodController.AddPeriod(It.IsAny<PeriodViewModel>()) as ObjectResult;
            Assert.That(result.StatusCode, Is.EqualTo(500));

            var resultValue = result.Value as Response;
            Assert.That(resultValue.IsSuccesful, Is.EqualTo(false));
            Assert.That(resultValue.ErrorMessage, Is.EqualTo("Object reference not set to an instance of an object."));

            _periodLogic.Verify(s => s.DeletePeriod(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void GetAllPeriods_OkResult()
        {
            IList<PeriodViewModel> periodViewModels = new List<PeriodViewModel>()
            {
                new PeriodViewModel()
                {
                Year = 1,
                Month = 1
                }
            };

            _ = _periodLogic.Setup(s => s.GetAllPeriods());
            _ = _periodControllerHelper.Setup(s => s.BuildListViewModel(It.IsAny<IList<PeriodDTO>>())).Returns(periodViewModels);

            ObjectResult? result = _periodController.GetAllPeriods() as ObjectResult;
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var resultValue = result.Value as List<PeriodViewModel>;

            Assert.That(resultValue[0].Year, Is.EqualTo(periodViewModels[0].Year));
            Assert.That(resultValue[0].Month, Is.EqualTo(periodViewModels[0].Month));

            _periodLogic.Verify(s => s.GetAllPeriods(), Times.Once);
            _periodControllerHelper.Verify(s => s.BuildListViewModel(It.IsAny<IList<PeriodDTO>>()), Times.Once);
        }

        [Test]
        public void GetAllPeriods_ThrowsException()
        {
            _ = _periodLogic.Setup(s => s.GetAllPeriods()).Throws<NullReferenceException>();

            ObjectResult? result = _periodController.GetAllPeriods() as ObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(500));

            var resultValue = result.Value as Response;
            Assert.That(resultValue.IsSuccesful, Is.EqualTo(false));
            Assert.That(resultValue.ErrorMessage, Is.EqualTo("Object reference not set to an instance of an object."));

            _periodLogic.Verify(s => s.GetAllPeriods(), Times.Once);
            _periodControllerHelper.Verify(s => s.BuildListViewModel(It.IsAny<IList<PeriodDTO>>()), Times.Never);
        }

        [Test]
        public void GetAllPeriodsWithSalesAndRemuneration_OkResult()
        {
            IList<PeriodViewModel> periodViewModels = new List<PeriodViewModel>()
            {
                new PeriodViewModel()
                {
                Year = 1,
                Month = 1,
                }
            };

            _ = _periodLogic.Setup(s => s.GetAllPeriodsWithSalesAndRemuneration());
            _ = _periodControllerHelper.Setup(s => s.BuildListViewModelWithSalesAndRemuneration(It.IsAny<IList<PeriodDTO>>())).Returns(periodViewModels);

            ObjectResult? result = _periodController.GetAllPeriodsWithSalesAndRemuneration() as ObjectResult;
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var resultValue = result.Value as List<PeriodViewModel>;

            Assert.That(resultValue[0].Year, Is.EqualTo(periodViewModels[0].Year));
            Assert.That(resultValue[0].Month, Is.EqualTo(periodViewModels[0].Month));

            _periodLogic.Verify(s => s.GetAllPeriodsWithSalesAndRemuneration(), Times.Once);
            _periodControllerHelper.Verify(s => s.BuildListViewModelWithSalesAndRemuneration(It.IsAny<IList<PeriodDTO>>()), Times.Once);
        }

        [Test]
        public void GetAllPeriodsWithSalesAndRemuneration_ThrowsException()
        {
            _ = _periodLogic.Setup(s => s.GetAllPeriodsWithSalesAndRemuneration()).Throws<NullReferenceException>();

            ObjectResult? result = _periodController.GetAllPeriodsWithSalesAndRemuneration() as ObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(500));

            var resultValue = result.Value as Response;
            Assert.That(resultValue.IsSuccesful, Is.EqualTo(false));
            Assert.That(resultValue.ErrorMessage, Is.EqualTo("Object reference not set to an instance of an object."));

            _periodLogic.Verify(s => s.GetAllPeriodsWithSalesAndRemuneration(), Times.Once);
            _periodControllerHelper.Verify(s => s.BuildListViewModelWithSalesAndRemuneration(It.IsAny<IList<PeriodDTO>>()), Times.Never);
        }

        [Test]
        public void GetPeriodById_OkResult()
        {
            PeriodViewModel periodViewModel = new PeriodViewModel()
            {
                Year = 1,
                Month = 1,
            };

            _ = _periodLogic.Setup(s => s.GetPeriodById(It.IsAny<int>()));
            _ = _periodControllerHelper.Setup(s => s.BuildViewModel(It.IsAny<PeriodDTO>())).Returns(periodViewModel);

            ObjectResult? result = _periodController.GetPeriodById(It.IsAny<int>()) as ObjectResult;
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var resultValue = result.Value as PeriodViewModel;

            Assert.That(resultValue.Year, Is.EqualTo(periodViewModel.Year));
            Assert.That(resultValue.Month, Is.EqualTo(periodViewModel.Month));

            _periodLogic.Verify(s => s.GetPeriodById(It.IsAny<int>()), Times.Once);
            _periodControllerHelper.Verify(s => s.BuildViewModel(It.IsAny<PeriodDTO>()), Times.Once);
        }

        [Test]
        public void GetPeriodById_ThrowsException()
        {
            _ = _periodLogic.Setup(s => s.GetPeriodById(It.IsAny<int>())).Throws<NullReferenceException>();

            ObjectResult? result = _periodController.GetPeriodById(It.IsAny<int>()) as ObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(500));

            var resultValue = result.Value as Response;
            Assert.That(resultValue.IsSuccesful, Is.EqualTo(false));
            Assert.That(resultValue.ErrorMessage, Is.EqualTo("Object reference not set to an instance of an object."));

            _periodLogic.Verify(s => s.GetPeriodById(It.IsAny<int>()), Times.Once);
            _periodControllerHelper.Verify(s => s.BuildViewModel(It.IsAny<PeriodDTO>()), Times.Never);
        }

        [Test]
        public void UpdatePeriod_OkResult()
        {
            Response response = new()
            {
                IsSuccesful = true
            };

            PeriodDTO periodDTO = new()
            {
                Year = 1,
                Month = 1
            };

            _ = _periodControllerHelper.Setup(s => s.BuildDTO(It.IsAny<PeriodViewModel>())).Returns(periodDTO);
            _ = _periodLogic.Setup(s => s.UpdatePeriod(It.IsAny<PeriodDTO>())).Returns(response);

            StatusCodeResult? result = _periodController.UpdatePeriod(It.IsAny<PeriodViewModel>()) as StatusCodeResult;
            Assert.That(result.StatusCode, Is.EqualTo(200));

            _periodControllerHelper.Verify(s => s.BuildDTO(It.IsAny<PeriodViewModel>()), Times.Once);
            _periodLogic.Verify(s => s.UpdatePeriod(It.IsAny<PeriodDTO>()), Times.Once);
        }

        [Test]
        public void UpdatePeriod_BadResult()
        {
            string errorMessage = "Error";
            Response response = new()
            {
                IsSuccesful = false,
                ErrorMessage = errorMessage
            };

            PeriodDTO periodDTO = new()
            {
                Year = 1,
                Month = 1
            };

            _ = _periodControllerHelper.Setup(s => s.BuildDTO(It.IsAny<PeriodViewModel>())).Returns(periodDTO);
            _ = _periodLogic.Setup(s => s.UpdatePeriod(It.IsAny<PeriodDTO>())).Returns(response);

            ObjectResult result = _periodController.UpdatePeriod(It.IsAny<PeriodViewModel>()) as ObjectResult;
            Assert.That(result.StatusCode, Is.EqualTo(500));

            var resultValue = result.Value as Response;
            Assert.That(resultValue.IsSuccesful, Is.EqualTo(false));
            Assert.That(resultValue.ErrorMessage, Is.EqualTo(errorMessage));


            _periodControllerHelper.Verify(s => s.BuildDTO(It.IsAny<PeriodViewModel>()), Times.Once);
            _periodLogic.Verify(s => s.UpdatePeriod(It.IsAny<PeriodDTO>()), Times.Once);
        }

        [Test]
        public void UpdatePeriod_ThrowsException()
        {
            _ = _periodLogic.Setup(s => s.UpdatePeriod(It.IsAny<PeriodDTO>())).Throws<NullReferenceException>();

            ObjectResult result = _periodController.UpdatePeriod(It.IsAny<PeriodViewModel>()) as ObjectResult;
            Assert.That(result.StatusCode, Is.EqualTo(500));

            var resultValue = result.Value as Response;
            Assert.That(resultValue.IsSuccesful, Is.EqualTo(false));
            Assert.That(resultValue.ErrorMessage, Is.EqualTo("Object reference not set to an instance of an object."));

            _periodControllerHelper.Verify(s => s.BuildDTO(It.IsAny<PeriodViewModel>()), Times.Once);
            _periodLogic.Verify(s => s.UpdatePeriod(It.IsAny<PeriodDTO>()), Times.Once);
        }
    }
}
