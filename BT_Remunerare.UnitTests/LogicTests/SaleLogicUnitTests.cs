using BT_Remunerare.BL.Classes;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using FluentAssertions;
using Moq;
using Xunit;

namespace BT_Remunerare.UnitTests.LogicTests
{
    public class SaleLogicUnitTests
    {

        private readonly Mock<ISaleRepository> saleRepository;
        private readonly Mock<ISalesRemunerationRepository> salesRemunerationRepository;
        private readonly SaleLogic saleLogic;
        public SaleLogicUnitTests()
        {
            saleRepository = new Mock<ISaleRepository>();
            salesRemunerationRepository = new Mock<ISalesRemunerationRepository>();

            saleLogic = new SaleLogic(saleRepository.Object, salesRemunerationRepository.Object);
        }

        [Fact]
        public async Task AddSale_IsExecuted()
        {
            var response = new Response
            {
                IsSuccesful = true
            };

            saleRepository.Setup(s => s.AddSale(It.IsAny<SaleDTO>())).Returns(response);

            //Act
            var result = saleLogic.AddSale(It.IsAny<SaleDTO>());

            //Assert
            result.IsSuccesful.Should().Be(result.IsSuccesful);
            saleRepository.Verify(s => s.AddSale(It.IsAny<SaleDTO>()), Times.Once);
        }

        [Fact]
        public async Task AddSale_Return()
        {
            var response = new Response { IsSuccesful = false, ErrorMessage = "No period with period id 1 was found" };
            var sale = new SaleDTO
            {
                PeriodId = 1,
                NumberOfProducts = 1,
                SaleId = 1
            };
            saleRepository.Setup(s => s.AddSale(sale)).Returns(response);
            //Act
            var result = saleLogic.AddSale(sale);
            //Assert

            result.ErrorMessage.Should().Be(response.ErrorMessage);
            saleRepository.Verify(s => s.AddSale(sale), Times.Once);

        }

        [Fact]
        public async Task GetTotalSalesValueByPeriodId_IsExecuted()
        {


            IList<SaleDTO> saleDTOs = new List<SaleDTO> {
                new SaleDTO
                {
                    PeriodId = 1,
                    ProductId = 1
                },
                new SaleDTO
                {
                    PeriodId = 1,
                    ProductId = 2
                }
            };

            IList<SalesRemunerationRuleDTO> salesRemunerationRuleDTOs = new List<SalesRemunerationRuleDTO>
            {
                new SalesRemunerationRuleDTO
                {
                    PeriodId = 1,
                    ProductId = 1,
                    Remuneration = 1
                },
                new SalesRemunerationRuleDTO
                {
                    PeriodId = 1,
                    ProductId = 2,
                    Remuneration = 2
                }
            };
            var totalSales = new Dictionary<int, IList<VendorTotalSalesDTO>>();
            totalSales.Add(1,
                    new List<VendorTotalSalesDTO> {
                        new VendorTotalSalesDTO {
                            Product = new ProductDTO {
                                ProductId = 1,
                                ProductName = "Product1"
                            },TotalSalesValue = 2,
                            Vendor = new VendorDTO {
                                VendorId =1,
                                VendorName = "Vanzator1"
                            }
                        }
                    });
            totalSales.Add(2,
                    new List<VendorTotalSalesDTO> {
                        new VendorTotalSalesDTO {
                            Product = new ProductDTO {
                                ProductId = 1,
                                ProductName = "Product1"
                            },TotalSalesValue = 2,
                            Vendor = new VendorDTO {
                                VendorId =1,
                                VendorName = "Vanzator1"
                            }
                        }
                    });
            TotalSalesDTO totalSalesDTO = new TotalSalesDTO
            {
                TotalSales = totalSales
            };

            saleRepository.Setup(s => s.GetSalesByPeriodId(It.IsAny<int>())).Returns(saleDTOs);

            salesRemunerationRepository.Setup(s => s.GetSalesRemunerationByPeriodAndProductId(1, 1)).Returns(salesRemunerationRuleDTOs[0]);
            salesRemunerationRepository.Setup(s => s.GetSalesRemunerationByPeriodAndProductId(1, 2)).Returns(salesRemunerationRuleDTOs[1]);

            //Act
            var result = saleLogic.GetTotalSalesValueByPeriodId(1);

            //Assert
            saleRepository.Verify(s => s.GetSalesByPeriodId(1), Times.Once);
            salesRemunerationRepository.Verify(s => s.GetSalesRemunerationByPeriodAndProductId(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
        }
    }
}