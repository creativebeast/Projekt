using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using SimpleCarRepair.Data;

namespace SimpleCarRepair
{
    public partial class SimpleCarRepairDbService
    {
        SimpleCarRepairDbContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly SimpleCarRepairDbContext context;
        private readonly NavigationManager navigationManager;

        public SimpleCarRepairDbService(SimpleCarRepairDbContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportClientsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/simplecarrepairdb/clients/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/simplecarrepairdb/clients/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportClientsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/simplecarrepairdb/clients/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/simplecarrepairdb/clients/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnClientsRead(ref IQueryable<Models.SimpleCarRepairDb.Client> items);

        public async Task<IQueryable<Models.SimpleCarRepairDb.Client>> GetClients(Query query = null)
        {
            var items = Context.Clients.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnClientsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnClientCreated(Models.SimpleCarRepairDb.Client item);
        partial void OnAfterClientCreated(Models.SimpleCarRepairDb.Client item);

        public async Task<Models.SimpleCarRepairDb.Client> CreateClient(Models.SimpleCarRepairDb.Client client)
        {
            OnClientCreated(client);

            var existingItem = Context.Clients
                              .Where(i => i.Id == client.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Clients.Add(client);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(client).State = EntityState.Detached;
                throw;
            }

            OnAfterClientCreated(client);

            return client;
        }
        public async Task ExportPartsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/simplecarrepairdb/parts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/simplecarrepairdb/parts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPartsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/simplecarrepairdb/parts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/simplecarrepairdb/parts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPartsRead(ref IQueryable<Models.SimpleCarRepairDb.Part> items);

        public async Task<IQueryable<Models.SimpleCarRepairDb.Part>> GetParts(Query query = null)
        {
            var items = Context.Parts.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPartsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPartCreated(Models.SimpleCarRepairDb.Part item);
        partial void OnAfterPartCreated(Models.SimpleCarRepairDb.Part item);

        public async Task<Models.SimpleCarRepairDb.Part> CreatePart(Models.SimpleCarRepairDb.Part part)
        {
            OnPartCreated(part);

            var existingItem = Context.Parts
                              .Where(i => i.Id == part.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Parts.Add(part);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(part).State = EntityState.Detached;
                throw;
            }

            OnAfterPartCreated(part);

            return part;
        }
        public async Task ExportPartReapirsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/simplecarrepairdb/partreapirs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/simplecarrepairdb/partreapirs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPartReapirsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/simplecarrepairdb/partreapirs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/simplecarrepairdb/partreapirs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPartReapirsRead(ref IQueryable<Models.SimpleCarRepairDb.PartReapir> items);

        public async Task<IQueryable<Models.SimpleCarRepairDb.PartReapir>> GetPartReapirs(Query query = null)
        {
            var items = Context.PartReapirs.AsQueryable();

            items = items.Include(i => i.Part);

            items = items.Include(i => i.Repair);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPartReapirsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPartReapirCreated(Models.SimpleCarRepairDb.PartReapir item);
        partial void OnAfterPartReapirCreated(Models.SimpleCarRepairDb.PartReapir item);

        public async Task<Models.SimpleCarRepairDb.PartReapir> CreatePartReapir(Models.SimpleCarRepairDb.PartReapir partReapir)
        {
            OnPartReapirCreated(partReapir);

            var existingItem = Context.PartReapirs
                              .Where(i => i.Id == partReapir.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.PartReapirs.Add(partReapir);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(partReapir).State = EntityState.Detached;
                partReapir.Part = null;
                partReapir.Repair = null;
                throw;
            }

            OnAfterPartReapirCreated(partReapir);

            return partReapir;
        }
        public async Task ExportRepairsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/simplecarrepairdb/repairs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/simplecarrepairdb/repairs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRepairsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/simplecarrepairdb/repairs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/simplecarrepairdb/repairs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRepairsRead(ref IQueryable<Models.SimpleCarRepairDb.Repair> items);

        public async Task<IQueryable<Models.SimpleCarRepairDb.Repair>> GetRepairs(Query query = null)
        {
            var items = Context.Repairs.AsQueryable();

            items = items.Include(i => i.Client);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnRepairsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRepairCreated(Models.SimpleCarRepairDb.Repair item);
        partial void OnAfterRepairCreated(Models.SimpleCarRepairDb.Repair item);

        public async Task<Models.SimpleCarRepairDb.Repair> CreateRepair(Models.SimpleCarRepairDb.Repair repair)
        {
            OnRepairCreated(repair);

            var existingItem = Context.Repairs
                              .Where(i => i.Id == repair.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Repairs.Add(repair);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(repair).State = EntityState.Detached;
                repair.Client = null;
                throw;
            }

            OnAfterRepairCreated(repair);

            return repair;
        }

        partial void OnClientDeleted(Models.SimpleCarRepairDb.Client item);
        partial void OnAfterClientDeleted(Models.SimpleCarRepairDb.Client item);

        public async Task<Models.SimpleCarRepairDb.Client> DeleteClient(int? id)
        {
            var itemToDelete = Context.Clients
                              .Where(i => i.Id == id)
                              .Include(i => i.Repairs)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnClientDeleted(itemToDelete);

            Context.Clients.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterClientDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnClientGet(Models.SimpleCarRepairDb.Client item);

        public async Task<Models.SimpleCarRepairDb.Client> GetClientById(int? id)
        {
            var items = Context.Clients
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnClientGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.SimpleCarRepairDb.Client> CancelClientChanges(Models.SimpleCarRepairDb.Client item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnClientUpdated(Models.SimpleCarRepairDb.Client item);
        partial void OnAfterClientUpdated(Models.SimpleCarRepairDb.Client item);

        public async Task<Models.SimpleCarRepairDb.Client> UpdateClient(int? id, Models.SimpleCarRepairDb.Client client)
        {
            OnClientUpdated(client);

            var itemToUpdate = Context.Clients
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(client);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterClientUpdated(client);

            return client;
        }

        partial void OnPartDeleted(Models.SimpleCarRepairDb.Part item);
        partial void OnAfterPartDeleted(Models.SimpleCarRepairDb.Part item);

        public async Task<Models.SimpleCarRepairDb.Part> DeletePart(int? id)
        {
            var itemToDelete = Context.Parts
                              .Where(i => i.Id == id)
                              .Include(i => i.PartReapirs)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPartDeleted(itemToDelete);

            Context.Parts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPartDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnPartGet(Models.SimpleCarRepairDb.Part item);

        public async Task<Models.SimpleCarRepairDb.Part> GetPartById(int? id)
        {
            var items = Context.Parts
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnPartGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.SimpleCarRepairDb.Part> CancelPartChanges(Models.SimpleCarRepairDb.Part item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPartUpdated(Models.SimpleCarRepairDb.Part item);
        partial void OnAfterPartUpdated(Models.SimpleCarRepairDb.Part item);

        public async Task<Models.SimpleCarRepairDb.Part> UpdatePart(int? id, Models.SimpleCarRepairDb.Part part)
        {
            OnPartUpdated(part);

            var itemToUpdate = Context.Parts
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(part);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterPartUpdated(part);

            return part;
        }

        partial void OnPartReapirDeleted(Models.SimpleCarRepairDb.PartReapir item);
        partial void OnAfterPartReapirDeleted(Models.SimpleCarRepairDb.PartReapir item);

        public async Task<Models.SimpleCarRepairDb.PartReapir> DeletePartReapir(int? id)
        {
            var itemToDelete = Context.PartReapirs
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPartReapirDeleted(itemToDelete);

            Context.PartReapirs.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPartReapirDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnPartReapirGet(Models.SimpleCarRepairDb.PartReapir item);

        public async Task<Models.SimpleCarRepairDb.PartReapir> GetPartReapirById(int? id)
        {
            var items = Context.PartReapirs
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Part);

            items = items.Include(i => i.Repair);

            var itemToReturn = items.FirstOrDefault();

            OnPartReapirGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.SimpleCarRepairDb.PartReapir> CancelPartReapirChanges(Models.SimpleCarRepairDb.PartReapir item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPartReapirUpdated(Models.SimpleCarRepairDb.PartReapir item);
        partial void OnAfterPartReapirUpdated(Models.SimpleCarRepairDb.PartReapir item);

        public async Task<Models.SimpleCarRepairDb.PartReapir> UpdatePartReapir(int? id, Models.SimpleCarRepairDb.PartReapir partReapir)
        {
            OnPartReapirUpdated(partReapir);

            var itemToUpdate = Context.PartReapirs
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(partReapir);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterPartReapirUpdated(partReapir);

            return partReapir;
        }

        partial void OnRepairDeleted(Models.SimpleCarRepairDb.Repair item);
        partial void OnAfterRepairDeleted(Models.SimpleCarRepairDb.Repair item);

        public async Task<Models.SimpleCarRepairDb.Repair> DeleteRepair(int? id)
        {
            var itemToDelete = Context.Repairs
                              .Where(i => i.Id == id)
                              .Include(i => i.PartReapirs)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRepairDeleted(itemToDelete);

            Context.Repairs.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRepairDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnRepairGet(Models.SimpleCarRepairDb.Repair item);

        public async Task<Models.SimpleCarRepairDb.Repair> GetRepairById(int? id)
        {
            var items = Context.Repairs
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Client);

            var itemToReturn = items.FirstOrDefault();

            OnRepairGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.SimpleCarRepairDb.Repair> CancelRepairChanges(Models.SimpleCarRepairDb.Repair item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRepairUpdated(Models.SimpleCarRepairDb.Repair item);
        partial void OnAfterRepairUpdated(Models.SimpleCarRepairDb.Repair item);

        public async Task<Models.SimpleCarRepairDb.Repair> UpdateRepair(int? id, Models.SimpleCarRepairDb.Repair repair)
        {
            OnRepairUpdated(repair);

            var itemToUpdate = Context.Repairs
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(repair);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterRepairUpdated(repair);

            return repair;
        }
    }
}
