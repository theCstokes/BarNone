namespace BarNone.Shared.DataTransfer.Core
{
    public abstract class BaseTypeDTO<TDTO> : BaseDTO<TDTO>
        where TDTO : new()
    {
        public abstract int Value { get; set; }

        public abstract string Name { get; set; }
    }
}
