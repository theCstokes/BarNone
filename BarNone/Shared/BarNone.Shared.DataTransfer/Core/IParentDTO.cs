namespace BarNone.Shared.DataTransfer.Core
{
    /// <summary>
    /// Parent dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.IDTO" />
    public interface IParentDTO : IDTO
    {
        dynamic Details { get; set; }
    }

    /// <summary>
    /// Parent dto.
    /// </summary>
    /// <typeparam name="TDetailDTO">The type of the detail dto.</typeparam>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.IDTO" />
    public interface IParentDTO<TDetailDTO> : IParentDTO
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
        new TDetailDTO Details { get; set; }
    }
}
