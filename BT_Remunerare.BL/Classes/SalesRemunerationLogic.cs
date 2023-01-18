using BT_Remunerare.BL.Interfaces;
using BT_Remunerare.DAL.Repository.Interfaces;
using BT_Remunerare.TL.Common;
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

        public Response AddSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationDTO)
        {
            return _salesRemunerationRepository.AddSalesRemuneration(salesRemunerationDTO);
        }

        public Response DeleteSalesRemuneration(int salesRemunerationId)
        {
            return _salesRemunerationRepository.DeleteSalesRemuneration(salesRemunerationId);
        }

        public IList<SalesRemunerationRuleDTO> GetAllSalesRemunerationRules()
        {
            return _salesRemunerationRepository.GetAllSalesRemunerationRules();
        }

        public SalesRemunerationRuleDTO? GetSalesRemunerationById(int salesRemunerationId)
        {
            return salesRemunerationId == 0 ? null : _salesRemunerationRepository.GetSalesRemunerationById(salesRemunerationId);
        }

        public Response UpdateSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationDTO)
        {
            return _salesRemunerationRepository.UpdateSalesRemuneration(salesRemunerationDTO);
        }
    }
}
