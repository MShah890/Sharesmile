//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SS_DB_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NGO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGO()
        {
            this.NGO_DRIVER = new HashSet<NGO_DRIVER>();
        }
    
        public int NGO_Id { get; set; }
        public string Name { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string Mobile_Number { get; set; }
        public string Mobile_Alternate { get; set; }
        public int Area_Id { get; set; }
        public string NGO_Logo_File { get; set; }
        public string Email { get; set; }
        public string Website_URL { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }
        public string Account_Status { get; set; }
    
        public virtual Area Area { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NGO_DRIVER> NGO_DRIVER { get; set; }
    }
}