using BarNone.Shared.Core;

namespace BarNone.Shared.DataTransfer.Core
{
    public class BaseDetailDTO<TDetailDTO> : IUntrackableDTO<TDetailDTO>
        where TDetailDTO : IUntrackableDTO<TDetailDTO>, new()
    {
    }
}
