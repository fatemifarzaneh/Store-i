using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Common.Dto
{
    public class ResultDto
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ResultDto<T>
    {
        public bool IsSucsess { set; get; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
