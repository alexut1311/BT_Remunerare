using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.DAL.Repository.Interfaces
{
    public interface IPeriodRepository
    {
        Response AddPeriod(PeriodDTO saleDTO);
        PeriodDTO? GetPeriodById(int saleId);
        Response UpdatePeriod(PeriodDTO saleDTO);
        Response DeletePeriod(int saleId);
        IList<PeriodDTO> GetAllPeriods();
        IList<PeriodDTO> GetAllPeriodsWithSalesAndRemuneration();
    }
}
