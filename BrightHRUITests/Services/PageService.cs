using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrightHRUITests.Models;

namespace BrightHRUITests.Services
{
        public interface IPageService
        {
            LoginPage LoginPage { get; }
        }

        public class PagesService : IPageService
        {
            public PagesService(LoginPage loginPage)
            {
                LoginPage = loginPage ?? throw new ArgumentNullException(nameof(loginPage)); 
            }

        public LoginPage LoginPage { get; }
        }
}