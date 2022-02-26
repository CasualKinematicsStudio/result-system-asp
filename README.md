# Result System By Casual Kinematic Studio

Complex tool to resolve problems with response object in APIs.

In main implementation there are two types of result object with response object and without.


###With response body ###
```
    public class Result<T> : Result, IResult<T>
    {
        public T Value { get; }

        public Result(T Value, int StatusCode, string ErrorMessage) : base(StatusCode, ErrorMessage)
        {
            this.Value = Value;
        }
    }
```

###Without response body ###
```
    public class Result : IResult
    {
        public int StatusCode { get; }
        public string ErrorMessage { get; }
        public bool IsSuccess => StatusCode is > 199 and <299 
    }
```

Also there are some extensions like **Result.From** which is very useful in case that you
need to make some operation which can throw exception.**Result.From** method use try catch block to 
handle all exceptions and make all operations safety. This function have few overloads with 

* Asynchronous operation with return type of **T**
* Synchronous operation with return type of **T**
* Asynchronous operation without predefined return type (by default it is a Result object without body type)

If some exception handled in **Result.From** method it return **Result** object with status code 500 and error message.

You can use also **Result.Success** and **Result.Failure** with status code and error message and als same with **T** type.
**IsSuccess** property depend on **StatusCode** property (**IsSuccess** true when  StatusCode is > 199 and <299)  

You can also inherit from **Result** and write your custom logic to complement the base implementation.  