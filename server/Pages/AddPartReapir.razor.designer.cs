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
    public partial class AddPartReapirComponent : ComponentBase
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

        [Parameter]
        public dynamic RepairId { get; set; }

        IEnumerable<SimpleCarRepair.Models.SimpleCarRepairDb.Part> _getPartsForPartIdResult;
        protected IEnumerable<SimpleCarRepair.Models.SimpleCarRepairDb.Part> getPartsForPartIdResult
        {
            get
            {
                return _getPartsForPartIdResult;
            }
            set
            {
                if (!object.Equals(_getPartsForPartIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getPartsForPartIdResult", NewValue = value, OldValue = _getPartsForPartIdResult };
                    _getPartsForPartIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir _partreapir;
        protected SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir partreapir
        {
            get
            {
                return _partreapir;
            }
            set
            {
                if (!object.Equals(_partreapir, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "partreapir", NewValue = value, OldValue = _partreapir };
                    _partreapir = value;
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
            getPartsForPartIdResult = simpleCarRepairDbGetPartsResult;

            partreapir = new SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir args)
        {
            partreapir.RepairId = int.Parse($"{RepairId}");

            try
            {
                var simpleCarRepairDbCreatePartReapirResult = await SimpleCarRepairDb.CreatePartReapir(partreapir);
                DialogService.Close(partreapir);
            }
            catch (System.Exception simpleCarRepairDbCreatePartReapirException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new PartReapir!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
