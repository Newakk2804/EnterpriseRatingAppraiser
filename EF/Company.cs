//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnterpriseRatingAppraiser.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            this.CompanyPerfomance = new HashSet<CompanyPerfomance>();
        }
    
        public int Id { get; set; }
        public string NameCompany { get; set; }
        public System.DateTime DateOfFoundation { get; set; }
        public string CompanyDescription { get; set; }
        public int IdTypeOfEconomicActivity { get; set; }
        public int IdOwnershipForm { get; set; }
    
        public virtual TypeOfEconomicActivity TypeOfEconomicActivity { get; set; }
        public virtual OwnershipForm OwnershipForm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyPerfomance> CompanyPerfomance { get; set; }
    }
}
