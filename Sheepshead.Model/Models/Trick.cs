
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Sheepshead.Model.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Trick
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Trick()
    {

        this.CardsPlayed = new HashSet<CardsPlayed>();

    }


    public int Id { get; set; }

    public int HandId { get; set; }

    public int StartingPlayerId { get; set; }



    public virtual Hand Hand { get; set; }

    public virtual Player StartingPlayer { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CardsPlayed> CardsPlayed { get; set; }

}

}