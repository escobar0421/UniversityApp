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
   public class DepartmentsViewModel : BaseViewModel
    {
        private BL.Services.IDepartmentsService departmentsService;
        public ObservableCollection<DepartmentsDTO> departments;
        private bool isRefreshing;
        public ObservableCollection<DepartmentsDTO> Departments
        {
            get { return this.departments; }
            set { this.SetValue(ref this.departments, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public DepartmentsViewModel()
        {
            this.departmentsService = new DepartmentsService();
            this.RefresCommand = new Command(async () => await GetDepartments());
            this.RefresCommand.Execute(null);
        }
        public Command RefresCommand { get; set; }

        async Task GetDepartments()
        {
            try
            {
                this.IsRefreshing = true;

                var connection = await departmentsService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                    return;
                }
                var listDepartments = await departmentsService.GetAll(Endpoints.GET_DEPARTMENTS);
                this.Departments = new ObservableCollection<DepartmentsDTO>(listDepartments);
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
