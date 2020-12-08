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
   public class EnrollmentIDViewModel : BaseViewModel
    {
        private BL.Services.IEnrollmentIDService enrollmentIDService;
        public ObservableCollection<EnrollmentIDDTO> enrollments;
        private bool isRefreshing;
        public ObservableCollection<EnrollmentIDDTO> Enrollments
        {
            get { return this.enrollments; }
            set { this.SetValue(ref this.enrollments, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public EnrollmentIDViewModel()
        {
            this.enrollmentIDService = new EnrollmentIDService();
            this.RefresCommand = new Command(async () => await GetEnrollments());
            this.RefresCommand.Execute(null);
        }
        public Command RefresCommand { get; set; }

        async Task GetEnrollments()
        {
            try
            {
                this.IsRefreshing = true;

                var connection = await enrollmentIDService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                    return;
                }
                var listEnrollments = await enrollmentIDService.GetAll(Endpoints.GET_ENROLLMENTS);
                this.Enrollments = new ObservableCollection<EnrollmentIDDTO>(listEnrollments);
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
