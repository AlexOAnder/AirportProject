//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class TechPark
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TechPark()
        {
            this.TechOnRepair = new HashSet<TechOnRepair>();
        }
    
        public int Id { get; set; }
        public int TechTypeId { get; set; }
        public string Name { get; set; }
        public System.DateTime DateOfPurchase { get; set; }
        public System.DateTime LastRepairmentDate { get; set; }
        public byte IsActive { get; set; }
        public string ModelName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TechOnRepair> TechOnRepair { get; set; }
        public virtual TechType TechType { get; set; }
    }
}
