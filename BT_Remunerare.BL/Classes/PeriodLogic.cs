using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Classes
{
    public class PeriodLogic : IPeriodLogic
    {
        private readonly IPeriodRepository _periodRepository;
        public PeriodLogic(IPeriodRepository periodRepository)
        {
            _periodRepository = periodRepository;
        }

        public Response AddPeriod(PeriodDTO periodDTO)
        {
            return _periodRepository.AddPeriod(periodDTO);
        }

        public Response DeletePeriod(int periodId)
        {
            return _periodRepository.DeletePeriod(periodId);
        }

        public IList<PeriodDTO> GetAllPeriods()
        {
            return _periodRepository.GetAllPeriods();
        }

        public IList<PeriodDTO> GetAllPeriodsWithSalesAndRemuneration()
        {
            return _periodRepository.GetAllPeriodsWithSalesAndRemuneration();
        }

        public PeriodDTO? GetPeriodById(int periodId)
        {
            return periodId == 0 ? null : _periodRepository.GetPeriodById(periodId);
        }

        public Response UpdatePeriod(PeriodDTO periodDTO)
        {
            return _periodRepository.UpdatePeriod(periodDTO);
        }
    }
}
