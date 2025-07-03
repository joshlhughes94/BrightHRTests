using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrightHRUITests.Services;
using Microsoft.Playwright;
using static System.Net.Mime.MediaTypeNames;

namespace BrightHRUITests.Models
{
    public class EmployeesPages
    {
        private readonly IPageDependencyService _pageDependencyService;

        private ILocator _employeesHubTitle => _pageDependencyService.Page.Result.Locator("//*[@class='text-3xl font-bold'][contains(.,'Employee hub')]");
        private ILocator _addEmployeeButton => _pageDependencyService.Page.Result.Locator("//*[@type='button'][contains(.,'Add employee')]");
        private ILocator _firstNameField => _pageDependencyService.Page.Result.Locator("#firstName");
        private ILocator _lastNameField => _pageDependencyService.Page.Result.Locator("#lastName");
        private ILocator _emailField => _pageDependencyService.Page.Result.Locator("#email");
        private ILocator _phoneNumberField => _pageDependencyService.Page.Result.Locator("#phoneNumber");
        private ILocator _datePickerStartDateField => _pageDependencyService.Page.Result.Locator("//*[@data-testid='input-selector']");
        private ILocator _jobTitleField => _pageDependencyService.Page.Result.Locator("#jobTitle");
        private ILocator _saveNewEmployeeButton => _pageDependencyService.Page.Result.Locator("//*[@type='submit'][1][contains(.,'Save new employee')]");
        private ILocator _addedEmployeeSuccessMessage => _pageDependencyService.Page.Result.Locator("//*[@class='text-lg text-white'][contains(.,'Success! New employee added')]");
        private ILocator _addAnotherEmployeeButton => _pageDependencyService.Page.Result.Locator("//*[@type='button'][contains(.,'Add another employee')]");
        private ILocator _closeModal => _pageDependencyService.Page.Result.Locator("(//*[@type='button'])[11]");
        private ILocator _searchEmployeeField => _pageDependencyService.Page.Result.Locator("(//*[@type='text'])");
        public EmployeesPages(IPageDependencyService pageDependencyService)
        {
            _pageDependencyService = pageDependencyService ?? throw new ArgumentNullException(nameof(pageDependencyService));  
        }

        public static class TestDataGenerator
        {
            private static readonly string[] FirstNames = { "Adam", "Simba", "Raya", "Moana", "Elsa", "Finley", "Luna", "Alicia" };
            private static readonly string[] LastNames = { "Jacob", "Grahams", "Lamar", "Dre", "Mathers", "Davis Jr", "Nunez", "Barker", "Jackson" };

            private static readonly Random Random = new Random();

            public static string GenerateFirstName()
            {
                return FirstNames[Random.Next(FirstNames.Length)];
            }

            public static string GenerateLastName()
            {
                return LastNames[Random.Next(LastNames.Length)];
            }
        }

        public List<string> CreatedEmployeeNames { get; } = new List<string>();

        public async Task EmployeeHubIsDisplayed()
        {
            await _employeesHubTitle.IsVisibleAsync();
        }

        public async Task SelectAddEmployeeButton()
        {
            await _addEmployeeButton.ClickAsync();
        }

        public async Task AddValidEmployee()
        {
            var firstName = TestDataGenerator.GenerateFirstName();
            var lastName = TestDataGenerator.GenerateLastName();
            var fullName = $"{firstName} {lastName}";
            CreatedEmployeeNames.Add(fullName);
            
            Random random = new Random();
            int number = random.Next(0,999);
            string phoneNumber = "07" + random.Next(100000000, 999999999).ToString();

            await _firstNameField.FillAsync(firstName);
            await _lastNameField.FillAsync(lastName);
            await _emailField.FillAsync($"testingemail{number}@test.com");
            await _phoneNumberField.FillAsync(phoneNumber);

            var tomorrow = DateTime.Today.AddDays(1);
            var dayToSelect = tomorrow.Day.ToString();
            await _datePickerStartDateField.ClickAsync();
            await _pageDependencyService.Page.Result.ClickAsync($".DayPicker-Day-Number:text('{dayToSelect}')");

            await _jobTitleField.FillAsync("QA");
            await _saveNewEmployeeButton.ClickAsync();
        }

        public async Task AddEmployeeSuccessMessageIsDisplayed()
        {
            await _addedEmployeeSuccessMessage.IsVisibleAsync();
        }

        public async Task SelectAddAnotherEmployeeButton()
        {
            await _addAnotherEmployeeButton.ClickAsync();
        }

        public async Task SearchForCreatedEmployees()
        {
            await _closeModal.ClickAsync();

            foreach (var fullName in CreatedEmployeeNames)
            {
                var firstName = fullName.Split(' ')[0];

                await _searchEmployeeField.FillAsync(firstName);

                var result = _pageDependencyService.Page.Result.Locator($"text={fullName}");
                await result.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
                Console.WriteLine($"Employee '{fullName}' is visible in the list.");
                await _searchEmployeeField.FillAsync("");
            }
        }
    }
}
