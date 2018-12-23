
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
    
public partial class Hand
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Hand()
    {

        this.Tricks = new HashSet<Trick>();

        this.Coins = new HashSet<Coin>();

        this.Points = new HashSet<Point>();

        this.ParticipantsRefusingPick = new HashSet<Participant>();

    }


    public int Id { get; set; }

    public string PartnerCardString { get; set; }

    public string BlindCards { get; set; }

    public string BuriedCards { get; set; }



    public virtual Game Game { get; set; }

    public virtual Participant StartingParticipant { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Trick> Tricks { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Coin> Coins { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Point> Points { get; set; }

    public virtual Participant PartnerParticipant { get; set; }

    public virtual Participant PickerParticipant { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Participant> ParticipantsRefusingPick { get; set; }

}

}
