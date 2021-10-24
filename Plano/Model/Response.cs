using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plano.Model
{
    /// <summary>
    /// 转换结果返回内容
    /// </summary>
    public class Response
    {
        private bool status = false;
        /// <summary>
        /// 转化状态
        /// </summary>
        [JsonProperty("Status")]
        public bool Status {
            get { return status; }
            set { status = value; } 
        }

        private string message = "成功";
        /// <summary>
        /// 说明
        /// </summary>
        [JsonProperty("Message")]
        public string Message {
            get { return message; }
            set { message = value; } 
        }

        private string result = "";
        /// <summary>
        /// 结果
        /// </summary>
        [JsonProperty("errorCode")]
        public string Result {
            get { return result; }
            set { result = value; } 
        }
    }
}
