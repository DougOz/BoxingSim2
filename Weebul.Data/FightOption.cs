//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Weebul.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class FightOption
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FightOption()
        {
            this.Fights = new HashSet<Fight>();
        }
    
        public int OptionsId { get; set; }
        public double Luck { get; set; }
        public double CutMultiplier { get; set; }
        public int NumberOfRounds { get; set; }
        public int WeightClass { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fight> Fights { get; set; }
    }
}
