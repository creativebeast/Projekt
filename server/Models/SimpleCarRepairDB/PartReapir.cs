using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCarRepair.Models.SimpleCarRepairDb
{
  [Table("PartReapir", Schema = "dbo")]
  public partial class PartReapir
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }
    public int PartId
    {
      get;
      set;
    }
    public Part Part { get; set; }
    public int? RepairId
    {
      get;
      set;
    }
    public Repair Repair { get; set; }
    public int Quantity
    {
      get;
      set;
    }
  }
}
