using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;

namespace BT_Remunerare.BL.Interfaces
{
    public interface ISalesRemunerationLogic
    {
        Response AddSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationDTO);
        SalesRemunerationRuleDTO GetSalesRemunerationById(int salesRemunerationId);
        Response UpdateSalesRemuneration(SalesRemunerationRuleDTO salesRemunerationDTO);
        Response DeleteSalesRemuneration(int salesRemunerationId);
        IList<SalesRemunerationRuleDTO> GetAllSalesRemunerationRules();
    }
}
