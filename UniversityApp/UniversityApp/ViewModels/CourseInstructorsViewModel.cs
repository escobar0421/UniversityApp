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
   public class CourseInstructorsViewModel : BaseViewModel
    {
        private BL.Services.ICourseInstructorService courseInstructorService;
        public ObservableCollection<CourseInstructorDTO> courseInstructor;
        private bool isRefreshing;
        private string filter;
        private List<CourseInstructorDTO> AllCourseInstructor { get; set; }

        public string Filter
        {
            get { return this.Filter; }
            set
            {
                this.SetValue(ref this.filter, value);
                this.GetInstructorsByName();
            }
        }
        public ObservableCollection<CourseInstructorDTO> CourseInstructor
        {
            get { return this.courseInstructor; }
            set { this.SetValue(ref this.courseInstructor, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public CourseInstructorsViewModel()
        {
            this.courseInstructorService = new CourseInstructorService();
            this.RefresCommand = new Command(async () => await GetCourseInstructor());
            this.RefresCommand.Execute(null);
        }
        public Command RefresCommand { get; set; }

        async Task GetCourseInstructor()
        {
            try
            {
                this.IsRefreshing = true;

                var connection = await courseInstructorService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                    return;
                }
                var listCourseInstructors = await courseInstructorService.GetAll(Endpoints.GET_COURSES_INSTRUCTORS);
                this.AllCourseInstructor = listCourseInstructors.ToList();
                this.CourseInstructor = new ObservableCollection<CourseInstructorDTO>(listCourseInstructors);
                this.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        void GetInstructorsByName()
        {
            var listCourseInstructor = this.AllCourseInstructor;
            if (string.IsNullOrEmpty(this.Filter))
                listCourseInstructor = listCourseInstructor.Where(x => x.instructor.LastName.ToLower().Contains(this.Filter.ToLower()) ||
                                                                       x.instructor.FirstMidName.ToLower().Contains(this.Filter.ToLower())).ToList();
            this.CourseInstructor = new ObservableCollection<CourseInstructorDTO>(listCourseInstructor);
        }
    }
}
