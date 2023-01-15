using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.DAL.Repository.Interfaces
{
    public interface ISalesRemunerationRepository
    {
        void AddSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationDTO);
        SalesRemunerationRuleDTO GetSalesRemunerationById(int salesRemunerationId);
        void UpdateSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationDTO);
        void DeleteSalesRemuneration(int salesRemunerationId);
        IList<SalesRemunerationRuleDTO> GetAllSalesRemunerationRules();
    }
}
