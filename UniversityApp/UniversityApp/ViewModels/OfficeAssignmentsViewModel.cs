using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using UniversityApp.Helpers;
using Xamarin.Forms;
namespace UniversityApp.ViewModels
{
  public  class OfficeAssignmentsViewModel : BaseViewModel
    {
        private BL.Services.IOfficeAssignmentsService officeAssignmentsService;
        public ObservableCollection<OfficeAssignmentsDTO> officeAssignments;
        private bool isRefreshing;
        public ObservableCollection<OfficeAssignmentsDTO> OfficeAssignments
        {
            get { return this.officeAssignments; }
            set { this.SetValue(ref this.officeAssignments, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public OfficeAssignmentsViewModel()
        {
            this.officeAssignmentsService = new OfficeAssignmentsService();
            this.RefresCommand = new Command(async () => await GetOfficeAssignments());
            this.RefresCommand.Execute(null);
        }
        public Command RefresCommand { get; set; }

        async Task GetOfficeAssignments()
        {
            try
            {
                this.IsRefreshing = true;

                var connection = await officeAssignmentsService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                    return;
                }
                var listOfficeAssignments = await officeAssignmentsService.GetAll(Endpoints.GET_OFFICEASSIGNMENTS);
                this.OfficeAssignments = new ObservableCollection<OfficeAssignmentsDTO>(listOfficeAssignments);
                this.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Cancel");
            }
        }
    }
}
