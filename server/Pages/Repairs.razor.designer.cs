using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using SimpleCarRepair.Models.SimpleCarRepairDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SimpleCarRepair.Models;

namespace SimpleCarRepair.Pages
{
    public partial class RepairsComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected SimpleCarRepairDbService SimpleCarRepairDb { get; set; }
        protected RadzenDataGrid<SimpleCarRepair.Models.SimpleCarRepairDb.Repair> grid0;
        protected RadzenDataGrid<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir> grid1;

        DateTime _Date;
        protected DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                if (!object.Equals(_Date, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "Date", NewValue = value, OldValue = _Date };
                    _Date = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<SimpleCarRepair.Models.SimpleCarRepairDb.Repair> _Repairs;
        protected IEnumerable<SimpleCarRepair.Models.SimpleCarRepairDb.Repair> Repairs
        {
            get
            {
                return _Repairs;
            }
            set
            {
                if (!object.Equals(_Repairs, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "Repairs", NewValue = value, OldValue = _Repairs };
                    _Repairs = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir> _PartReapirs;
        protected IEnumerable<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir> PartReapirs
        {
            get
            {
                return _PartReapirs;
            }
            set
            {
                if (!object.Equals(_PartReapirs, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "PartReapirs", NewValue = value, OldValue = _PartReapirs };
                    _PartReapirs = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        SimpleCarRepair.Models.SimpleCarRepairDb.Repair _master;
        protected SimpleCarRepair.Models.SimpleCarRepairDb.Repair master
        {
            get
            {
                return _master;
            }
            set
            {
                if (!object.Equals(_master, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "master", NewValue = value, OldValue = _master };
                    _master = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        string _lastFilter;
        protected string lastFilter
        {
            get
            {
                return _lastFilter;
            }
            set
            {
                if (!object.Equals(_lastFilter, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "lastFilter", NewValue = value, OldValue = _lastFilter };
                    _lastFilter = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {
            Date = DateTime.Now;

            var getRepairsResult = await GetRepairs();
            Repairs = getRepairsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddRepairs>("Add Repairs", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Datepicker0Change(DateTime? args)
        {
            var getRepairsResult = await GetRepairs();
            Repairs = getRepairsResult;

            PartReapirs = new List<PartReapir>();
        }

        protected async void Grid0Render(DataGridRenderEventArgs<SimpleCarRepair.Models.SimpleCarRepairDb.Repair> args)
        {
            if (grid0.Query.Filter != lastFilter) {
                master = grid0.View.FirstOrDefault();
            }

            if (grid0.Query.Filter != lastFilter)
            {
                await grid0.SelectRow(master);
            }

            lastFilter = grid0.Query.Filter;
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<SimpleCarRepair.Models.SimpleCarRepairDb.Repair> args)
        {
            await DialogService.OpenAsync<EditRepairs>("Edit Repairs", new Dictionary<string, object>() { {"Id", args.Data.Id} });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SimpleCarRepair.Models.SimpleCarRepairDb.Repair args)
        {
            master = args;

            if (args == null) {
                PartReapirs = null;
            }

            if (args != null)
            {
                var simpleCarRepairDbGetPartReapirsResult = await SimpleCarRepairDb.GetPartReapirs(new Query() { Filter = $@"i => i.RepairId == {args.Id}" });
                PartReapirs = simpleCarRepairDbGetPartReapirsResult;
            }
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args, dynamic data)
        {
            await DialogService.OpenAsync<ImageReapirCar>($"Repair Car", new Dictionary<string, object>() { {"id", data.Id} });
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args, dynamic data)
        {
            await DialogService.OpenAsync<ImageReapirCarDamage>($"Car Damage", new Dictionary<string, object>() { {"id", data.Id} });
        }

        protected async System.Threading.Tasks.Task Button3Click(MouseEventArgs args, dynamic data)
        {
            await DialogService.OpenAsync<ClinetInfo>($"Client Info", new Dictionary<string, object>() { {"id", data.Client.Id} });
        }

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args, dynamic data)
        {
            await DialogService.OpenAsync<Receipt>("Receipt", new Dictionary<string, object>() { {"id", data.Id} });
        }

        protected async System.Threading.Tasks.Task PartReapirAddButtonClick(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddPartReapir>("Add Part Reapir", new Dictionary<string, object>() { {"RepairId", master.Id} });
            await grid1.Reload();
        }

        protected async System.Threading.Tasks.Task Grid1RowSelect(SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir args)
        {
            var dialogResult = await DialogService.OpenAsync<EditPartReapir>("Edit Part Reapir", new Dictionary<string, object>() { {"Id", args.Id} });
            await grid1.Reload();
        }

        protected async System.Threading.Tasks.Task PartReapirDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var simpleCarRepairDbDeletePartReapirResult = await SimpleCarRepairDb.DeletePartReapir(data.Id);
                    if (simpleCarRepairDbDeletePartReapirResult != null)
                    {
                        await grid1.Reload();
                    }
                }
            }
            catch (System.Exception simpleCarRepairDbDeletePartReapirException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Repair" });
            }
        }
    }
}
