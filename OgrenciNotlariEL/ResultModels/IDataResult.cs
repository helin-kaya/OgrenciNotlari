﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciNotlariEL.ResultModels
{
    public interface IDataResult<T> : IResult
    {
        public T Data { get; set; }
    }
}
