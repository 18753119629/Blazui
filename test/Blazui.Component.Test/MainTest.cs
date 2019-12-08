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
    public class MainTest : SetupTest
    {
        public MainTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task TestTab1Async()
        {
            await InitilizeAsync();
            Page page = await Browser.NewPageAsync();
            await page.GoToAsync("https://localhost:5001");
            await NavigateToMenuAsync(page, "Tabs ��ǩҳ");
            var demoCards = await WaitForDemoCardsAsync(page);
            await TestAsync("Tabs ��ǩҳ", demoCards.FirstOrDefault(x => x.Title == "�����ġ����ı�ǩҳ"));
        }
        [Fact]
        public async Task TestTab2Async()
        {
            await InitilizeAsync();
            Page page = await Browser.NewPageAsync();
            await page.GoToAsync("https://localhost:5001");
            await NavigateToMenuAsync(page, "Tabs ��ǩҳ");
            var demoCards = await WaitForDemoCardsAsync(page);
            await TestAsync("Tabs ��ǩҳ", demoCards.FirstOrDefault(x => x.Title == "ѡ���ʽ�ı�ǩҳ"));
        }
        [Fact]
        public async Task TestTab3Async()
        {
            await InitilizeAsync();
            Page page = await Browser.NewPageAsync();
            await page.GoToAsync("https://localhost:5001");
            await NavigateToMenuAsync(page, "Tabs ��ǩҳ");
            var demoCards = await WaitForDemoCardsAsync(page);
            await TestAsync("Tabs ��ǩҳ", demoCards.FirstOrDefault(x => x.Title == "��Ƭ���ı�ǩҳ"));
        }
        [Fact]
        public async Task TestTab4Async()
        {
            await InitilizeAsync();
            Page page = await Browser.NewPageAsync();
            await page.GoToAsync("https://localhost:5001");
            await NavigateToMenuAsync(page, "Tabs ��ǩҳ");
            var demoCards = await WaitForDemoCardsAsync(page);
            await TestAsync("Tabs ��ǩҳ", demoCards.FirstOrDefault(x => x.Title == "����ߵı�ǩҳ"));
        }
        [Fact]
        public async Task TestTab5Async()
        {
            await InitilizeAsync();
            Page page = await Browser.NewPageAsync();
            await page.GoToAsync("https://localhost:5001");
            await NavigateToMenuAsync(page, "Tabs ��ǩҳ");
            var demoCards = await WaitForDemoCardsAsync(page);
            await TestAsync("Tabs ��ǩҳ", demoCards.FirstOrDefault(x => x.Title == "�����¼�APIʵ�ֿɱ༭�ı�ǩҳ"));
        }
        [Fact]
        public async Task TestTab6Async()
        {
            await InitilizeAsync();
            Page page = await Browser.NewPageAsync();
            await page.GoToAsync("https://localhost:5001");
            await NavigateToMenuAsync(page, "Tabs ��ǩҳ");
            var demoCards = await WaitForDemoCardsAsync(page);
            await TestAsync("Tabs ��ǩҳ", demoCards.FirstOrDefault(x => x.Title == "˫���ʵ�ֿɱ༭�ı�ǩҳ"));
        }
    }
}
