namespace Application.Common
{
    // Generic class to encapsulate the result of the operations used in the services.
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public T Value { get; }

        private Result(bool isSuccess, string error, T value)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public static Result<T> Success(T value) => new (true, null, value);
        public static Result<T> Failure(string error) => new (false, error, default);
    }
}