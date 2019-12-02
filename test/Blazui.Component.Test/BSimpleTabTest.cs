using System;
using System.Threading.Tasks;
using Blazui.ServerRender;
using PuppeteerSharp;
using Xunit;

namespace Blazui.Component.Test
{
    public class BSimpleTabTest : SetupTest
    {
        [Fact]
        public async Task Test1Async()
        {
            await InitilizeAsync();
            Browser browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false,
                Devtools = true
            });

            // Create a new page and go to Bing Maps
            Page page = await browser.NewPageAsync();
            await page.GoToAsync("https://localhost:5001");
            await Task.Delay(100000);
        }
    }
}
