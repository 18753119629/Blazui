using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blazui.ServerRender;
using PuppeteerSharp;
using Xunit;
using Xunit.Abstractions;

namespace Blazui.Component.Test
{
    public class BTabTest : SetupTest
    {
        public BTabTest(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("�����ġ����ı�ǩҳ")]
        [InlineData("ѡ���ʽ�ı�ǩҳ")]
        [InlineData("��Ƭ���ı�ǩҳ")]
        [InlineData("����ߵı�ǩҳ")]
        [InlineData("�ɱ༭�ı�ǩҳ")]
        public async Task Test1Async(string title)
        {
            await InitilizeAsync();
            Page page = await Browser.NewPageAsync();
            await page.GoToAsync("https://localhost:5001");
            await NavigateToMenuAsync(page, "Tabs ��ǩҳ");
            var demoCards = await WaitForDemoCardsAsync(page);
            await TestAsync("Tabs ��ǩҳ", demoCards.FirstOrDefault(x => x.Title == title));
        }
    }
}
