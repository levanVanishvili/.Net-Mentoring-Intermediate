namespace AdapterPatternTask
{
    public interface IElements<T>
    {
        IEnumerable<T> GetElements();
    }
}
