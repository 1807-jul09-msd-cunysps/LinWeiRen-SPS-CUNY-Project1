namespace PhoneAppAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact_message
    {
        [Key]
        public int message_id { get; set; }

        [StringLength(50)]
        public string full_name { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [Column(TypeName = "text")]
        public string full_message { get; set; }
    }
}
