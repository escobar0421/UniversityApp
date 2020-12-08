﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using UniversityApp.Helpers;
using UniversityApp.Views;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
   public class StudentItemViewModel : StudentDTO
    {
        private BL.Services.IStudentService studentService;

        public StudentItemViewModel()
        {
            this.studentService = new StudentService();

            EditStudentCommand = new Command(async () => await EditStudent());
            DeleteStudentCommand = new Command(async () => await DeleteStudent());
        }

        public Command EditStudentCommand { get; set; }
        public Command DeleteStudentCommand { get; set; }
        async Task EditStudent()
        {
            MainViewModel.GetInstance().EditStudent = new EditStudentViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditStudentsPage());
        }

        async Task DeleteStudent()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Confirm", "Delete Confirm", "Yes", "No");
            if (!answer)
                return;

            var connection = await studentService.CheckConnection();
            if (!connection)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                return;
            }

            await studentService.Delete(Endpoints.DELETE_STUDENTS, this.ID);

            await Application.Current.MainPage.DisplayAlert("Message", "The process is successful", "Cancel");
            var studentsViewModel = StudentsViewModel.GetInstance();
            var studentDeleted = studentsViewModel.AllStudents.FirstOrDefault(x => x.LastName == this.LastName);
            studentsViewModel.AllStudents.Remove(studentDeleted);
            studentsViewModel.GetStudentByName();
        }
    }
}
