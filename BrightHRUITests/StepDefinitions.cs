using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSiteUITests.Services;
using Microsoft.Playwright;
using Reqnroll;

namespace TestSiteUITests
{
    [Binding]
    public class StepDefinitions
    {
        private readonly IPageService _pageService;
        private readonly IPageDependencyService _pageDependencyService;
        private IBrowser _browser;
        private IPage _page;

        public StepDefinitions(IPageService pageService, IPageDependencyService pageDependencyService)
        {
            _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
            _pageDependencyService = pageDependencyService ?? throw new ArgumentNullException(nameof(pageDependencyService));
        }

        private async Task<IBrowser> InitializeBrowser(string browserName)
        {
            var playwright = await Playwright.CreateAsync();
            bool isHeadless = true;
            return browserName.ToLower() switch
            {
                "chromium" => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = isHeadless }),
                "firefox" => await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = isHeadless }),
                "webkit" => await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = isHeadless }),
                _ => throw new ArgumentException($"Browser '{browserName}' is not supported"),
            };
        }

        [Given(@"I run the test on ""(.*)""")]
        public async Task GivenIRunTheTestOn(string browser)
        {
            var browserName = browser.Replace("browser-", "").ToLower();
            _browser = await InitializeBrowser(browserName);
            _page = await _browser.NewPageAsync();
            _pageDependencyService.SetPage(_page);
        }
        
        [Given("I have navigated to the TestSite login page")]
        public async Task GivenIHaveNavigatedToTheTestSiteLoginPage()
        {
            var loginPage = _pageService.LoginPage;
            await loginPage.GoToLoginPage();
        }
        
        

        [When("I enter valid login credentials")]
        public async Task WhenIEnterValidLoginCredentials()
        {
            var username = _pageDependencyService.AppSettings.Value.ValidUsername;
            var password = _pageDependencyService.AppSettings.Value.ValidPassword;

            var loginPage = _pageService.LoginPage;
            await loginPage.LoginSuccessfully(username, password);
        }

        [When("I have selected Employees from the menu")]
        public async Task WhenIHaveSelectedEmployeesFromTheMenu()
        {
            var dashboardPage = _pageService.DashboardPage;
            await dashboardPage.SelectEmployeesFromTheMenu();
        }

        [Then("I am displayed the employees page")]
        public async Task ThenIAmDisplayedTheEmployeesPage()
        {
            var employeesPage = _pageService.EmployeesPages;
            await employeesPage.EmployeeHubIsDisplayed();
        }

        [When("I have selected the Add Employee Button")]
        public async Task WhenIHaveSelectedTheAddEmployeeButton()
        {
            var employeesPage = _pageService.EmployeesPages;
            await employeesPage.SelectAddEmployeeButton();
        }

        [When("I have created a valid Employee")]
        public async Task WhenIHaveCreatedAValidEmployee()
        {
            var employeesPage = _pageService.EmployeesPages;
            await employeesPage.AddValidEmployee();
        }

        [Then("I am displayed the employee added success message")]
        public async Task ThenIAmDisplayedTheEmployeeAddedSuccessMessage()
        {
            var employeesPage = _pageService.EmployeesPages;
            await employeesPage.AddEmployeeSuccessMessageIsDisplayed();
        }

        [Then("I select the add another employee button")]
        public async Task ThenISelectTheAddAnotherEmployeeButton()
        {
            var employeesPage = _pageService.EmployeesPages;
            await employeesPage.SelectAddAnotherEmployeeButton();
        }

        [Then("I create a second employee")]
        public async Task ThenICreateASecondEmployee()
        {
            var employeesPage = _pageService.EmployeesPages;
            await employeesPage.AddValidEmployee();
        }

        [Then("I am displayed both employees in the employees list")]
        public async Task ThenIAmDisplayedBothEmployeesInTheEmployeesList()
        {
            var employeesPage = _pageService.EmployeesPages;
            await employeesPage.SearchForCreatedEmployees();
        }
    }
}
