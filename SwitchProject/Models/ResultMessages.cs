using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwitchProject.Models
{
    public class ResultMessages<T> where T:class
    {
       
        public ResultMessages():this(true,"The operation was successful.")
        {

        }
        public ResultMessages(bool dstatus, string error)
        {

            status = dstatus;
            Message = error;
        }
            
        public string StatusMessage
        {
            get
            {
                
                return Message;
            }
        }
        public string Failed
        {
            get
            {
                return "The operation failed.</br> " + Message;
            }
        }
        public string Message { get; set; }
        public bool status { get; set; }
        public T Data { get; set; }
    }
}