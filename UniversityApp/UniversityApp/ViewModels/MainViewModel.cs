using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.Views;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
    public class MainViewModel
    {
        public CoursesViewModel courses { get; set; }

        public CourseInstructorsViewModel courseInstructors { get; set; }

        public DepartmentsViewModel Departments { get; set; }

        public EnrollmentIDViewModel EnrollmentID { get; set; }

        public OfficeAssignmentsViewModel OfficeAssignments { get; set; }

        public StudentsViewModel Students { get; set; }

        public InstructorViewModel Instructor { get; set; }


        public CreateCourseViewModel CreateCourse { get; set; }

        public CreateInstructorViewModel CreateInstructor { get; set; }

        public CreateStudentsViewModel CreateStudents { get; set; }


        public EditCourseViewModel EditCourse { get; set; }
        public EditInstructorViewModel EditInstructor { get; set; }
        public EditStudentViewModel EditStudent { get; set; }




        public HomeViewModel Home { get; set; }

        public LoginViewModel Login { get; set; }



        public MainViewModel()
        {
            instance = this;

            courses = new CoursesViewModel();

            courseInstructors = new CourseInstructorsViewModel();

            Login = new LoginViewModel();

            CreateInstructor = new CreateInstructorViewModel();

            CreateStudents = new CreateStudentsViewModel();

            CreateCourse = new CreateCourseViewModel();

            Departments = new DepartmentsViewModel();

            EnrollmentID = new EnrollmentIDViewModel();

            Instructor = new InstructorViewModel();

            OfficeAssignments = new OfficeAssignmentsViewModel();

            Students = new StudentsViewModel();

            Home = new HomeViewModel();

            CreateCourseCommand = new Command(async () => await GoToCreateCourse());

            CreateStudentCommand = new Command(async () => await GoToCreateStudent());

            CreateInstructorCommand = new Command(async () => await GoToCreateInstructor());
        }

        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();

            return instance;
        }

        public Command CreateCourseCommand { get; set; }
         async Task GoToCreateCourse()
         {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateCoursePage());
         }

        public Command CreateStudentCommand { get; set; }
        async Task GoToCreateStudent()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateStudentsPage());
        }

        public Command CreateInstructorCommand { get; set; }
        async Task GoToCreateInstructor()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateInstructorPage());
        }

    }
}
