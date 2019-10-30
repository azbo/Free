namespace Fas
{
    public interface ISql
    {
        int update<T>(T data, string xmlId);

        object insert<T>(T data, string xmlId);

        int delete<T>(T data, string xmlId);

        T select<T>(T data, string xmlId);
    }
}
