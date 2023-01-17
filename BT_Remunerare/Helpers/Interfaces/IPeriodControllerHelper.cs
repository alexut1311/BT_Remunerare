namespace BT_Remunerare.Helpers.Interfaces
{
    public interface IPeriodControllerHelper<ViewModel, DTO> : IControllerHelper<ViewModel, DTO>
    {
        IList<ViewModel> BuildListViewModelWithSalesAndRemuneration(IList<DTO> dTOs);
    }
}
