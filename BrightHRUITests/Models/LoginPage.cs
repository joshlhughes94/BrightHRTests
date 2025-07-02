using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrightHRUITests.Services;

namespace BrightHRUITests.Models
{
    public class LoginPage
    {
        private readonly IPageDependencyService _pageDependencyService;
        public LoginPage(IPageDependencyService pageDependencyService)
        {
            _pageDependencyService = pageDependencyService ?? throw new ArgumentNullException(nameof(pageDependencyService));
        }

        public async Task GoToLoginPage()
        {
            await _pageDependencyService.Page.Result.GotoAsync(_pageDependencyService.AppSettings.Value.BrightHRTestURL);
        }
    }
}
