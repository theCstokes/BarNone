namespace BarNone.Shared.DataTransfer.Core.Filter
{
    public class WhereFilter
    {
        public string Property { get; set; }

        public FilterOpType Op { get; set; }

        public object Value { get; set; }
    }
}
