using BT_Remunerare.BL.Classes;
using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.Controllers;
using BT_Remunerare.DAL.Repository.Classes;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.Helpers.Interfaces;
using BT_Remunerare.Models;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BT_Remunerare.UnitTests.ControllerTests
{
    public class PeriodControllerTests
    {
        private readonly Mock<IPeriodLogic> _periodLogic;
        private readonly Mock<IPeriodControllerHelper<PeriodViewModel, PeriodDTO>> _periodControllerHelper;
        private readonly PeriodController periodController;

        public PeriodControllerTests(Mock<IPeriodLogic> periodLogic, Mock<IPeriodControllerHelper<PeriodViewModel, PeriodDTO>> periodControllerHelper, PeriodController periodController)
        {
            _periodLogic = periodLogic;
            _periodControllerHelper = periodControllerHelper;
            periodController = new PeriodController(_periodLogic.Object, _periodControllerHelper.Object);
        }

        [Fact]
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
            StatusCodeResult result = periodController.AddPeriod(It.IsAny<PeriodViewModel>()) as StatusCodeResult;

            //Assert
            //result.IsSuccesful.Should().Be(result.IsSuccesful);
            //saleRepository.Verify(s => s.AddSale(It.IsAny<SaleDTO>()), Times.Once);
        }
    }
}
