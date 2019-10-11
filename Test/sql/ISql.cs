namespace Test.query
{
    interface ISql
    {
        int modify<T>(T data, string xmlId);

        object insert<T>(T data, string xmlId);

        int remove<T>(T data, string xmlId);

        T query<T>(T data, string xmlId);
    }
}
