using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EmailServiceLibrary
{
    public class Mail
    {
        
       
    
            public string FromAddress { get; set; }

            public string ToAddress { get; set; }
        
            public string Bcc { get; set; }
         
            public string Cc { get; set; }
          
            public string Subject { get; set; }
        
            public string Body { get; set; }
           
            public string Attachment { get; set; }
        
    }
}
