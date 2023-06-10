using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCarRepair.Models.SimpleCarRepairDb
{
  [Table("Repairs", Schema = "dbo")]
  public partial class Repair
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public ICollection<PartReapir> PartReapirs { get; set; }
    public DateTime Date
    {
      get;
      set;
    }
    public string CarImage
    {
      get;
      set;
    }
    public string DamageImage
    {
      get;
      set;
    }
    public int ClientId
    {
      get;
      set;
    }
    public Client Client { get; set; }
    public string MechanicId
    {
      get;
      set;
    }
    public string Description
    {
      get;
      set;
    }
    public bool Finished
    {
      get;
      set;
    }
    public bool Deactivated
    {
      get;
      set;
    }
  }
}
