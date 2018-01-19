using BarNone.Shared.Core;

namespace BarNone.Shared.DataTransfer.Core
{
    /// <summary>
    /// Base detail dto.
    /// </summary>
    /// <typeparam name="TDetailDTO">The type of the detail dto.</typeparam>
    /// <seealso cref="BarNone.Shared.Core.IUntrackableDTO{TDetailDTO}" />
    public class BaseDetailDTO<TDetailDTO> : IUntrackableDTO<TDetailDTO>
        where TDetailDTO : IUntrackableDTO<TDetailDTO>, new()
    {
    }
}
