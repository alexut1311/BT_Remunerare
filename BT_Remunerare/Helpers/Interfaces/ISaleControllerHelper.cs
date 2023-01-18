using BT_Remunerare.Models;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.Helpers.Interfaces
{
    public interface ISaleControllerHelper<ViewModel, DTO> : IControllerHelper<ViewModel, DTO>
    {
        IList<ViewModel> BuildListViewModelWithVendorAndProductAndPeriod(IList<DTO> dTOs);
        TotalSalesViewModel BuildTotalSalesViewModel(TotalSalesDTO totalSalesValueDTO);
    }
}
