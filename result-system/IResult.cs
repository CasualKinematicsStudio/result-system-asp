namespace result_system
{
    public interface IResult<out T> : IResult
    {
        T Value { get; }
    }

    public interface IResult
    {
        int StatusCode { get; }

        string ErrorMessage { get; }

        bool IsSuccess { get; }
    }
}