using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSiteUITests.Services;
using Microsoft.Playwright;

namespace TestSiteUITests.Models
{
    public class DashboardPage
    {
        private readonly IPageDependencyService _pageDependencyService;
        private ILocator _menuOption => _pageDependencyService.Page.Result.Locator("#mobileMenuIcon");
        private ILocator _employeesMenuOption => _pageDependencyService.Page.Result.Locator("//*[@data-e2e='employees']");

        public DashboardPage(IPageDependencyService pageDependencyService)
        {
            _pageDependencyService = pageDependencyService ?? throw new ArgumentOutOfRangeException(nameof(pageDependencyService)); 
        }

        public async Task SelectEmployeesFromTheMenu()
        {
            //await _menuOption.ClickAsync();
            await _employeesMenuOption.ClickAsync();
        }
    }
}
