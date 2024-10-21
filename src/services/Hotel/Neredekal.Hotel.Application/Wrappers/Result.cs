using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Hotel.Application.Wrappers
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public Result(string message, bool isSuccess = true)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public static Result Success(string message)
        {
            return new(message);
        }

        public static Result Error(string message)
        {
            return new(message, false);
        }
    }

    public class Result<T> where T : class
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Result(T data, string message, bool isSuccess = true)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }

        public static Result<T> Success(T data, string message)
        {
            return new(data, message);
        }

        public static Result<T> Error(T data, string message)
        {
            return new(data, message, false);
        }
    }
}
