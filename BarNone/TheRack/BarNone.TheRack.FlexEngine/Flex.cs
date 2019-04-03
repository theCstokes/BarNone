using BarNone.TheRack.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace BarNone.TheRack.FlexEngine
{
    public class FlexRunner
    {
        public static FlexResponseDTO Execute(FlexRequestDTO requestDTO)
        {
            var context = new DomainContext();

            return new FlexResponseDTO
            {
                Results = requestDTO.Requests.Aggregate(new Dictionary<string, dynamic>(), (result, request) =>
                {
                    var name = request.Name ?? request.Type;
                    var load = new FEP_Load();

                    result[name] = load.Execute(context, request);
                    return result;
                })
            };
        }
    }
}
