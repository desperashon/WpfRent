//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfRent.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Characteristics
    {
        public Characteristics()
        {
            this.Announcement_Characteristis = new HashSet<Announcement_Characteristis>();
        }
    
        public int characteristic_id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<Announcement_Characteristis> Announcement_Characteristis { get; set; }
    }
}
