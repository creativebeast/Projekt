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
    public partial class PartsComponent : ComponentBase
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
        protected RadzenDataGrid<SimpleCarRepair.Models.SimpleCarRepairDb.Part> grid0;

        IEnumerable<SimpleCarRepair.Models.SimpleCarRepairDb.Part> _getPartsResult;
        protected IEnumerable<SimpleCarRepair.Models.SimpleCarRepairDb.Part> getPartsResult
        {
            get
            {
                return _getPartsResult;
            }
            set
            {
                if (!object.Equals(_getPartsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getPartsResult", NewValue = value, OldValue = _getPartsResult };
                    _getPartsResult = value;
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
            var simpleCarRepairDbGetPartsResult = await SimpleCarRepairDb.GetParts();
            getPartsResult = simpleCarRepairDbGetPartsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddParts>("Add Parts", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SimpleCarRepair.Models.SimpleCarRepairDb.Part args)
        {
            var dialogResult = await DialogService.OpenAsync<EditParts>("Edit Parts", new Dictionary<string, object>() { {"Id", args.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args, dynamic data)
        {
            await DialogService.OpenAsync<Image>("Image", new Dictionary<string, object>() { {"Id", data.Id} });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var simpleCarRepairDbDeletePartResult = await SimpleCarRepairDb.DeletePart(data.Id);
                    if (simpleCarRepairDbDeletePartResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception simpleCarRepairDbDeletePartException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Part" });
            }
        }
    }
}
