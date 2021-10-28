using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewis.WebApp.MVC.Models.Response
{
    public class ResponseResult<TEntity>
    {
        public ResponseResult()
        {
            ResponseResultError = new ResponseResult();
        }

        public object Result { get; set; }
        public TEntity Value { get; set; }
        public ResponseResult ResponseResultError { get; set; }
    }

    public class ResponseResult
    {
        public ResponseResult()
        {
            Errors = new ResponseErrorMessages();
        }

        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }
        
    }

    public class ResponseErrorMessages
    {
        public ResponseErrorMessages()
        {
            Messages = new List<string>();
        }

        public List<string> Messages { get; set; }
    }
}
