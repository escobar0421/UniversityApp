using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using UniversityApp.Helpers;
using Xamarin.Forms;

namespace UniversityApp.ViewModels 
{
  public  class StudentsViewModel : BaseViewModel
    {
        private BL.Services.IStudentService studentService;
        public ObservableCollection<StudentItemViewModel> students;
        private bool isRefreshing;
        private string filter;

        public List<StudentItemViewModel> AllStudents { get; set; }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.SetValue(ref this.filter, value);
                this.GetStudentByName();
            }
        }
        public ObservableCollection<StudentItemViewModel> Students
        {
            get { return this.students; }
            set { this.SetValue(ref this.students, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public StudentsViewModel()
        {
            instance = this;

            this.studentService = new StudentService();
            this.RefresCommand = new Command(async () => await GetStudents());
            this.RefresCommand.Execute(null);
        }

        private static StudentsViewModel instance;
        public static StudentsViewModel GetInstance()
        {
            if (instance == null)
                return new StudentsViewModel();

            return instance;
        }
        public Command RefresCommand { get; set; }

        async Task GetStudents()
        {
            try
            {
                this.IsRefreshing = true;

                var connection = await studentService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                    return;
                }
                var listStudents = (await studentService.GetAll(Endpoints.GET_STUDENTS)).Select(x => ToStudentItemViewModel(x));
                this.AllStudents = listStudents.ToList();
                this.Students = new ObservableCollection<StudentItemViewModel>(listStudents);
                this.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private StudentItemViewModel ToStudentItemViewModel(StudentDTO studentDTO) => new StudentItemViewModel
        {
            ID = studentDTO.ID,
            LastName = studentDTO.LastName,
            FirstMidName = studentDTO.FirstMidName,
            EnrollmentDate = studentDTO.EnrollmentDate
        };


       public void GetStudentByName()
        {
            var listStudents = this.AllStudents;
            if (!string.IsNullOrEmpty(this.Filter))
                listStudents = listStudents.Where(x => x.LastName.ToLower().Contains(this.Filter.ToLower()) ||
                                                       x.FirstMidName.ToLower().Contains(this.Filter.ToLower())).ToList();

            this.Students = new ObservableCollection<StudentItemViewModel>(listStudents);
        }
    }
}
