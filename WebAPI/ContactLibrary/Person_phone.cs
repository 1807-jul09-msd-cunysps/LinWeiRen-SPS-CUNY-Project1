namespace ContactLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Person_phone
    {
        [Key]
        public long PHid { get; set; }

        public long Pid { get; set; }

        public int? countryCode { get; set; }

        public int? areaCode { get; set; }

        public long number { get; set; }

        public int? ext { get; set; }

        public virtual Person Person { get; set; }
    }
}
