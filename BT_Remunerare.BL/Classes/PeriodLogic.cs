using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Repository.Interfaces;
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

        public void AddPeriod(PeriodDTO periodDTO)
        {
            _periodRepository.AddPeriod(periodDTO);
        }

        public void DeletePeriod(int periodId)
        {
            _periodRepository.DeletePeriod(periodId);
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
            return _periodRepository.GetPeriodById(periodId);
        }

        public void UpdatePeriod(PeriodDTO periodDTO)
        {
            _periodRepository.UpdatePeriod(periodDTO);
        }
    }
}
