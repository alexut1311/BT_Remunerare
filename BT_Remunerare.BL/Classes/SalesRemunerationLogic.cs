using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Classes
{
    public class SalesRemunerationLogic : ISalesRemunerationLogic
    {
        private readonly ISalesRemunerationRepository _salesRemunerationRepository;

        public SalesRemunerationLogic(ISalesRemunerationRepository salesRemunerationRepository)
        {
            _salesRemunerationRepository = salesRemunerationRepository;
        }

        public void AddSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationDTO)
        {
            _salesRemunerationRepository.AddSalesRemuneration(salesRemunerationDTO);
        }

        public void DeleteSalesRemuneration(int salesRemunerationId)
        {
            _salesRemunerationRepository.DeleteSalesRemuneration(salesRemunerationId);
        }

        public IList<SalesRemunerationRuleDTO> GetAllSalesRemunerationRules()
        {
            return _salesRemunerationRepository.GetAllSalesRemunerationRules();
        }

        public SalesRemunerationRuleDTO GetSalesRemunerationById(int salesRemunerationId)
        {
            return _salesRemunerationRepository.GetSalesRemunerationById(salesRemunerationId);
        }

        public void UpdateSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationDTO)
        {
            _salesRemunerationRepository.UpdateSalesRemuneration(salesRemunerationDTO);
        }
    }
}
