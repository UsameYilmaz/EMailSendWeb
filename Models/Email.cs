using System;
using System.Collections.Generic;

namespace EMailSendWeb.Models
{
    public class Email //veritabanına gönderilecek model.
    {
        public int ID { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string AdSoyad { get; set; }
        public string EPosta { get; set; }
        public DateTime SendDate { get; set; }

    }

}

