﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SSPWorld.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateStudentProfilePage : ContentPage
    {
        public CreateStudentProfilePage()
        {
            InitializeComponent();
            BindingContext = new CreateStudentProfileViewModel(Navigation);
        }
    }
}