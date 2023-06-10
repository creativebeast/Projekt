using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCarRepair.Data;

namespace SimpleCarRepair
{
    public partial class ExportSimpleCarRepairDbController : ExportController
    {
        private readonly SimpleCarRepairDbContext context;
        private readonly SimpleCarRepairDbService service;
        public ExportSimpleCarRepairDbController(SimpleCarRepairDbContext context, SimpleCarRepairDbService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/SimpleCarRepairDb/clients/csv")]
        [HttpGet("/export/SimpleCarRepairDb/clients/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportClientsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetClients(), Request.Query), fileName);
        }

        [HttpGet("/export/SimpleCarRepairDb/clients/excel")]
        [HttpGet("/export/SimpleCarRepairDb/clients/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportClientsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetClients(), Request.Query), fileName);
        }
        [HttpGet("/export/SimpleCarRepairDb/parts/csv")]
        [HttpGet("/export/SimpleCarRepairDb/parts/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportPartsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetParts(), Request.Query), fileName);
        }

        [HttpGet("/export/SimpleCarRepairDb/parts/excel")]
        [HttpGet("/export/SimpleCarRepairDb/parts/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportPartsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetParts(), Request.Query), fileName);
        }
        [HttpGet("/export/SimpleCarRepairDb/partreapirs/csv")]
        [HttpGet("/export/SimpleCarRepairDb/partreapirs/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportPartReapirsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetPartReapirs(), Request.Query), fileName);
        }

        [HttpGet("/export/SimpleCarRepairDb/partreapirs/excel")]
        [HttpGet("/export/SimpleCarRepairDb/partreapirs/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportPartReapirsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetPartReapirs(), Request.Query), fileName);
        }
        [HttpGet("/export/SimpleCarRepairDb/repairs/csv")]
        [HttpGet("/export/SimpleCarRepairDb/repairs/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportRepairsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRepairs(), Request.Query), fileName);
        }

        [HttpGet("/export/SimpleCarRepairDb/repairs/excel")]
        [HttpGet("/export/SimpleCarRepairDb/repairs/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportRepairsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRepairs(), Request.Query), fileName);
        }
    }
}
