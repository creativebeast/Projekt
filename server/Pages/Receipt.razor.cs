using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using SimpleCarRepair.Models.SimpleCarRepairDb;

namespace SimpleCarRepair.Pages
{
    public partial class ReceiptComponent
    {
        public async Task<Repair> GetRepair(int id)
        {
            var simpleCarRepairDbGetRepairsResult = await SimpleCarRepairDb.GetRepairs(new Query() { Filter = $@"i => i.Id == @0", FilterParameters = new object[] { id }, Expand = "Client, PartReapirs" });
            return simpleCarRepairDbGetRepairsResult.FirstOrDefault();
        }
    }
}
