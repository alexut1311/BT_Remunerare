using BT_Remunerare.BL.Classes;
using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.Controllers;
using BT_Remunerare.Helpers.Classes;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Remunerare.Tests.ControllerTests
{
    public class PeriodControllerTests
    {
        private Mock<IPeriodLogic> _periodLogic;
        private Mock<IPeriodControllerHelper<PeriodViewModel, PeriodDTO>> _periodControllerHelper;
        private PeriodController _periodController;


        [SetUp]
        public void Setup()
        {
            _periodLogic = new Mock<IPeriodLogic>();
            _periodControllerHelper = new Mock<IPeriodControllerHelper<PeriodViewModel, PeriodDTO>>();
            _periodController = new PeriodController(_periodLogic.Object, _periodControllerHelper.Object);
        }

        [Test]
        public async Task AddPeriod_IsExecuted()
        {
            var response = new Response
            {
                IsSuccesful = true
            };

            var periodDTO = new PeriodDTO
            {
                Year = 1,
                Month = 1
            };

            _periodControllerHelper.Setup(s => s.BuildDTO(It.IsAny<PeriodViewModel>())).Returns(periodDTO);
            _periodLogic.Setup(s => s.AddPeriod(It.IsAny<PeriodDTO>())).Returns(response);

            //Act
            StatusCodeResult result = _periodController.AddPeriod(It.IsAny<PeriodViewModel>()) as StatusCodeResult;
            Assert.That(result.StatusCode, Is.EqualTo(200));
            
            //Assert
            //result.IsSuccesful.Should().Be(result.IsSuccesful);
            //saleRepository.Verify(s => s.AddSale(It.IsAny<SaleDTO>()), Times.Once);
        }
    }
}
