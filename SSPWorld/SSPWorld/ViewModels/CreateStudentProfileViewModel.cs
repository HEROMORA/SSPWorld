﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Acr.UserDialogs;
using SSPWorld.Models;
using SSPWorld.Repositories;
using SSPWorld.Utilities;
using SSPWorld.Views;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class CreateStudentProfileViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IStudentRepository _studentRepository = new StudentRepository();
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private Department _selectedDepartment;

        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                if (_selectedDepartment == value) return;
                _selectedDepartment = value;
                OnPropertyChanged();
            }
        }

        private Year _selectedYear;
        public Year SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear == value) return;
                _selectedYear = value;
                OnPropertyChanged();
            }
        }


        public List<string> YearsNames => Enum.GetNames(typeof(Year)).Select(b => b.SplitCamelCase()).ToList();
        public List<string> DepartmentsNames => Enum.GetNames(typeof(Department)).Select(b => b.SplitCamelCase()).ToList();

        public ICommand SubmitCommand { get; private set; }

        public CreateStudentProfileViewModel(INavigation navigation)
        {
            _navigation = navigation;
            BindCommands();
        }

        private void BindCommands()
        {
            SubmitCommand = new Command(async c => await Submit());
        }

        private async Task Submit()
        {
            //TODO: Image to be added
            var student = new Student()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Department = SelectedDepartment,
                Year = SelectedYear,
                Image = ""
            };

            var result = await _studentRepository.CreateStudentProfile(student);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "Success")
                    App.Current.MainPage = new NavigationPage(new HomePage());
                else
                {
                    UserDialogs.Instance.Alert("Error: " + result);
                }
            }
            else
            {
                UserDialogs.Instance.Alert("Server Error");
            }
        }
    }
}
