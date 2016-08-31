using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Message
{
    public class ValidateMessage
    {
        public ValidateMessage()
        {
            this.Success = false;
            this.Contents = new List<string>();
        }



        public bool Success { get; set; }

        // public string Content { get; set; }

        public List<string> Contents { get; set; }

        public override string ToString()
        {
            if (this.Contents == null || this.Contents.Count() == 0)
            {
                return string.Empty;
            }
            else
            {
                return string.Join("/", this.Contents.ToArray());
            }
        }
    }
}
