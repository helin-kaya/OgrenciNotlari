using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciNotlariEL.ResultModels
{
    public class DataResult<T> : IDataResult<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public DataResult()
        {

        }

        public DataResult(bool success, string msg, T data)
        {
            IsSuccess = success;
            Message = msg;
            Data = data;
        }
    }
}
