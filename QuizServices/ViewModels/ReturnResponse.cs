using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.ViewModels
{
    public class ReturnResponse
    {
        public object Result { get; set; }

        public ReturnStatus Status { get; set; }

        public static ReturnResponse GetSuccessStatus(Object obj)
        {
            ReturnResponse r = new ReturnResponse();
            ReturnStatus s = new ReturnStatus("1","Success");

            r.Result = obj;
            r.Status = s;

            return r;
        }

        public static ReturnResponse GetFailureStatus(Object obj)
        {
            ReturnResponse r = new ReturnResponse();
            ReturnStatus s = new ReturnStatus("-1", "Failure");

            r.Result = obj;
            r.Status = s;

            return r;
        }
    }

    public class ReturnStatus
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ReturnStatus(string code, string messsage)
        {
            this.Code = code;
            this.Message = messsage;

        }
    }

   
}
