using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSiteUITests.Models;

namespace TestSiteUITests.Services
{
        public interface IPageService
        {
            LoginPage LoginPage { get; }
            DashboardPage DashboardPage { get; }
            EmployeesPages EmployeesPages { get; }
        }

        public class PagesService : IPageService
        {
            public PagesService(LoginPage loginPage, DashboardPage dashboardPage, 
                EmployeesPages employeesPages)
            {
                LoginPage = loginPage ?? throw new ArgumentNullException(nameof(loginPage)); 
                DashboardPage = dashboardPage ?? throw new ArgumentNullException(nameof(dashboardPage));
                EmployeesPages = employeesPages ?? throw new ArgumentNullException(nameof(employeesPages)); 
            }

        public LoginPage LoginPage { get; }
        public DashboardPage DashboardPage { get; }
        public EmployeesPages EmployeesPages { get; }
        }
}