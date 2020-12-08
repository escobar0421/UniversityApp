using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using UniversityApp.ViewModels;

namespace UniversityApp.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator ()
            {
            this.Main = new MainViewModel();
            }
    }
}
