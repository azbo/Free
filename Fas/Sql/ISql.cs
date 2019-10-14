namespace Fas
{
    public interface ISql
    {
        int Update<T>(T data, string xmlId);

        object Insert<T>(T data, string xmlId);

        int Delete<T>(T data, string xmlId);

        T Select<T>(T data, string xmlId);
    }
}
