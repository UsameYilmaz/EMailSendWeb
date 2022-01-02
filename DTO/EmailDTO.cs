using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMailSendWeb.DTO
{
    public class EmailDTO 
    {
           public string Baslik { get; set; }
            public string Icerik { get; set; }
            public string AdSoyad { get; set; }
            public List<string> EPosta { get; set; }//
            public DateTime SendDate { get; set; }
        
    }
}
