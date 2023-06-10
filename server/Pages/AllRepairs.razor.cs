using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using SimpleCarRepair.Models.SimpleCarRepairDb;

namespace SimpleCarRepair.Pages
{
    public partial class AllRepairsComponent
    {
        public async Task<IQueryable<Repair>> GetRepairs()
        {
            var simpleCarRepairDbGetRepairsResult = await SimpleCarRepairDb.GetRepairs(new Query() { Filter = $@"i => i.Date.Date == @0.Date", FilterParameters = new object[] { Date }, Expand = "Client" });
            return simpleCarRepairDbGetRepairsResult;
        }
    }
}
