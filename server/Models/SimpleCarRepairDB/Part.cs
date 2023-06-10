using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCarRepair.Models.SimpleCarRepairDb
{
  [Table("Parts", Schema = "dbo")]
  public partial class Part
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public ICollection<PartReapir> PartReapirs { get; set; }
    public string Name
    {
      get;
      set;
    }
    public string EAN
    {
      get;
      set;
    }
    public string Image
    {
      get;
      set;
    }
    public decimal Cost
    {
      get;
      set;
    }
  }
}
