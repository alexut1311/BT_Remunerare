using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Interfaces
{
    public interface IPeriodLogic
    {
        void AddPeriod(PeriodDTO periodDTO);
        PeriodDTO? GetPeriodById(int periodId);
        void UpdatePeriod(PeriodDTO periodDTO);
        void DeletePeriod(int periodId);
        IList<PeriodDTO> GetAllPeriods();
        IList<PeriodDTO> GetAllPeriodsWithSalesAndRemuneration();
    }
}
