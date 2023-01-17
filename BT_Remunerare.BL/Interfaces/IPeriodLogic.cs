using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Interfaces
{
    public interface IPeriodLogic
    {
        Response AddPeriod(PeriodDTO periodDTO);
        PeriodDTO? GetPeriodById(int periodId);
        Response UpdatePeriod(PeriodDTO periodDTO);
        Response DeletePeriod(int periodId);
        IList<PeriodDTO> GetAllPeriods();
        IList<PeriodDTO> GetAllPeriodsWithSalesAndRemuneration();
    }
}
