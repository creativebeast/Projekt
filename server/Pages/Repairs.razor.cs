using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using SimpleCarRepair.Models.SimpleCarRepairDb;

namespace SimpleCarRepair.Pages
{
    public partial class RepairsComponent
    {
        public async Task<IQueryable<Repair>> GetRepairs()
        {
            var simpleCarRepairDbGetRepairsResult = await SimpleCarRepairDb.GetRepairs(new Query() { Filter = $@"i => i.MechanicId == @0 && i.Date.Date == @1.Date && i.Deactivated == @2", FilterParameters = new object[] { Security.User.Id, Date, false }, Expand = "Client" });
            return simpleCarRepairDbGetRepairsResult;
        }
    }
}
