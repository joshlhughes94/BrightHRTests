using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrightHRUITests.Services;
using Microsoft.Playwright;

namespace BrightHRUITests.Models
{
    public class LoginPage
    {
        private readonly IPageDependencyService _pageDependencyService;
        private ILocator _username => _pageDependencyService.Page.Result.Locator("#username");
        private ILocator _password => _pageDependencyService.Page.Result.Locator("#password");
        private ILocator _loginButton => _pageDependencyService.Page.Result.Locator("//*[@type='submit']");
        public LoginPage(IPageDependencyService pageDependencyService)
        {
            _pageDependencyService = pageDependencyService ?? throw new ArgumentNullException(nameof(pageDependencyService));
        }

        public async Task GoToLoginPage()
        {
            await _pageDependencyService.Page.Result.GotoAsync(_pageDependencyService.AppSettings.Value.BrightHRTestURL);
        }

        public async Task LoginSuccessfully(string username, string password)
        {
            await _username.FillAsync(username);
            await _password.FillAsync(password);
            await _loginButton.ClickAsync();
        }
    }
}
