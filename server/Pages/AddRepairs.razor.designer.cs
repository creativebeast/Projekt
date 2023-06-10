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
    public partial class AddRepairsComponent : ComponentBase
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

        IEnumerable<SimpleCarRepair.Models.SimpleCarRepairDb.Client> _getClientsForClientIdResult;
        protected IEnumerable<SimpleCarRepair.Models.SimpleCarRepairDb.Client> getClientsForClientIdResult
        {
            get
            {
                return _getClientsForClientIdResult;
            }
            set
            {
                if (!object.Equals(_getClientsForClientIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getClientsForClientIdResult", NewValue = value, OldValue = _getClientsForClientIdResult };
                    _getClientsForClientIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        SimpleCarRepair.Models.SimpleCarRepairDb.Repair _repair;
        protected SimpleCarRepair.Models.SimpleCarRepairDb.Repair repair
        {
            get
            {
                return _repair;
            }
            set
            {
                if (!object.Equals(_repair, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "repair", NewValue = value, OldValue = _repair };
                    _repair = value;
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
            var simpleCarRepairDbGetClientsResult = await SimpleCarRepairDb.GetClients();
            getClientsForClientIdResult = simpleCarRepairDbGetClientsResult;

            repair = new SimpleCarRepair.Models.SimpleCarRepairDb.Repair(){};

            repair.MechanicId = Security.User.Id;

            repair.Date = DateTime.Now;
        }

        protected async System.Threading.Tasks.Task Form0Submit(SimpleCarRepair.Models.SimpleCarRepairDb.Repair args)
        {
            try
            {
                var simpleCarRepairDbCreateRepairResult = await SimpleCarRepairDb.CreateRepair(repair);
                DialogService.Close(repair);
            }
            catch (System.Exception simpleCarRepairDbCreateRepairException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Repair!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
