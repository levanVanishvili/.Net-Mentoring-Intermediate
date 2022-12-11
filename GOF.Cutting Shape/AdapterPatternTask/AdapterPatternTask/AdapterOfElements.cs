namespace AdapterPatternTask
{
    public class AdapterOfElements<T> : IContainer<T>
    {
        private readonly IElements<T> _elements;

        public AdapterOfElements(IElements<T> elements)
        {
            _elements= elements;
        }

        public IEnumerable<T> Items => _elements.GetElements();

        public int Count { get; }
    }
}
