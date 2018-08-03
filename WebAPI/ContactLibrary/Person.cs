namespace ContactLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person")]
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            Person_phone = new HashSet<Person_phone>();
            Person_address = new HashSet<Person_address>();
        }

        [Key]
        public long Pid { get; set; }

        [StringLength(50)]
        public string firstName { get; set; }

        [StringLength(50)]
        public string lastName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person_phone> Person_phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person_address> Person_address { get; set; }
    }
    public class SerializablePerson
    {
        public long Pid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class DeserializedPerson
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string houseNum { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public int zipcode { get; set; }
        public int countryCode { get; set; }
        public int areaCode { get; set; }
        public int number { get; set; }
        public int ext { get; set; }
    }
}
