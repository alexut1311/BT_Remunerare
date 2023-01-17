using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Interfaces
{
    public interface ISalesRemunerationLogic
    {
        void AddSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationDTO);
        SalesRemunerationRuleDTO GetSalesRemunerationById(int salesRemunerationId);
        void UpdateSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationDTO);
        void DeleteSalesRemuneration(int salesRemunerationId);
        IList<SalesRemunerationRuleDTO> GetAllSalesRemunerationRules();
    }
}
