namespace BT_Remunerare.Helpers.Interfaces
{
    public interface ISalesRemunerationRuleControllerHelper<ViewModel, DTO> : IControllerHelper<ViewModel, DTO>
    {
        IList<ViewModel> BuildListViewModelWithProductAndPeriod(IList<DTO> dTOs);
    }
}
