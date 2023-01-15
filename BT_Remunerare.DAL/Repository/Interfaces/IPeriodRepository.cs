using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.DAL.Repository.Interfaces
{
    public interface IPeriodRepository
    {
        void AddPeriod(PeriodDTO saleDTO);
        PeriodDTO? GetPeriodById(int saleId);
        void UpdatePeriod(PeriodDTO saleDTO);
        void DeletePeriod(int saleId);
        IList<PeriodDTO> GetAllPeriods();
        IList<PeriodDTO> GetAllPeriodsWithSalesAndRemuneration();
    }
}
