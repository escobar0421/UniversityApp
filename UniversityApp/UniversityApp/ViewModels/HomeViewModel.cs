using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.Views;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
   public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            GoToStudentsCommand = new Command(async () => await GoToStudents());
            GoToInstructorCommand = new Command(async () => await GoToInstructor());
        }
        public Command GoToStudentsCommand { get; set; }
        public Command GoToInstructorCommand { get; set; }

        async Task GoToStudents()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new StudentsPage());
        }

        async Task GoToInstructor()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new InstructorPage());
        }
    }
}
