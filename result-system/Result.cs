#region

using System;
using System.Threading.Tasks;

#endregion

namespace result_system
{
    public class Result<T> : Result, IResult<T>
    {
        public T Value { get; }

        public Result(T Value, int StatusCode, string ErrorMessage) : base(StatusCode, ErrorMessage)
        {
            this.Value = Value;
        }
    }
    
    [Serializable]
    public class Result : IResult
    {
        public int StatusCode { get; }
        public string ErrorMessage { get; }
        public bool IsSuccess => StatusCode is > 199 and <299;


        public Result(int StatusCode, string ErrorMessage)
        {
            this.StatusCode = StatusCode;
            this.ErrorMessage = ErrorMessage;
        }

        public static IResult Success()
        {
            return new Result(200, string.Empty);
        }

        public static IResult<T> Success<T>(T value)
        {
            return new Result<T>(value, 200, string.Empty);
        }

        public static IResult Failure(int statusCode, string errorMessage)
        {
            return new Result(statusCode, errorMessage);
        }

        public static IResult Failure(int statusCode, string errorMessage, params object[] args)
        {
            return Failure(statusCode, string.Format(errorMessage, args));
        }

        public static IResult<T> Failure<T>(int statusCode, string errorMessage)
        {
            return new Result<T>(default, statusCode, errorMessage);
        }

        public static IResult<T> Failure<T>(int statusCode, string errorMessage, params object[] args)
        {
            return Failure<T>(statusCode, string.Format(errorMessage, args));
        }

        public static async Task<IResult<T>> From<T>(Func<Task<T>> operation)
        {
            try
            {
                T value = await operation.Invoke();
                return Success(value);
            }
            catch (Exception e)
            {
                return Failure<T>(500, e.Message);
            }
        }

        public static async Task<IResult> From(Func<Task> operation)
        {
            try
            {
                await operation.Invoke();
                return Success();
            }
            catch (Exception e)
            {
                return Failure(500, e.Message);
            }
        }

        public static IResult<T> From<T>(Func<T> operation)
        {
            try
            {
                T value = operation.Invoke();
                return Success(value);
            }
            catch (Exception e)
            {
                return Failure<T>(500, e.Message);
            }
        }
    }
}