using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCarRepair.Models.SimpleCarRepairDb
{
  [Table("Clients", Schema = "dbo")]
  public partial class Client
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public ICollection<Repair> Repairs { get; set; }
    public string Name
    {
      get;
      set;
    }
    public string LastName
    {
      get;
      set;
    }
    public string Phone
    {
      get;
      set;
    }
    public string Email
    {
      get;
      set;
    }
    public string NIP
    {
      get;
      set;
    }
  }
}
