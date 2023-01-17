namespace BT_Remunerare.Helpers.Interfaces
{
    public interface IControllerHelper<ViewModel, DTO>
    {
        DTO BuildDTO(ViewModel viewModel);
        IList<ViewModel> BuildListViewModel(IList<DTO> dTOs);
        ViewModel BuildViewModel(DTO? dTO);
    }
}
