using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace BarNone.TheRack.FlexEngine
{
    public class FEP_Load
    {
        public List<FlexResposeEntityDTO> Execute(DomainContext context, FlexRequestEntityDTO entityDTO)
        {
            var repo = FlexMap.Map[entityDTO.Type](context);

            var elements = repo.Get();

            return elements.Select(e =>
            {
                return new FlexResposeEntityDTO
                {
                    Result = e.CreateDTO(new ConvertConfig()),
                    Details = entityDTO.Details?.Aggregate(new Dictionary<string, dynamic>(), (result, detail) =>
                    {
                        var name = detail.Name ?? detail.Type;
                        result[name] = LoadDetails(context, detail);
                        return result;
                    })
                };
            }).ToList();
        }

        private List<FlexResposeEntityDTO> LoadDetails(DomainContext context, FlexRequestEntityDTO detailDTO)
        {
            var load = new FEP_Load();

            var response = load.Execute(context, detailDTO);

            return response;
        }
    }
}
