﻿using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blazui.Component.Test.FormTests
{
    [TestName("Form 表单", "基础用法")]
    public class Test1 : IDemoTester
    {
        private ElementHandle submitButton;
        private ElementHandle resetButton;
        private ElementHandle[] formItems;

        public async Task TestAsync(DemoCard demoCard)
        {
            var filledIndexes = new List<int>();
            await AssertFormAsync(demoCard, false, filledIndexes);
            await submitButton.ClickAsync();
            await Task.Delay(50);
            await AssertFormAsync(demoCard, true, filledIndexes);

            var formItem1 = formItems.FirstOrDefault();
            var formItem1Input = await formItem1.QuerySelectorAsync("div.el-input > input");
            var label1 = await formItem1.QuerySelectorAsync("label");
            await formItem1Input.TypeAsync("测试活动");
            await label1.ClickAsync();
            await Task.Delay(50);
            filledIndexes.Add(0);
            await AssertFormAsync(demoCard, true, filledIndexes);

            //跳过活动区域的测试
            var formItem3 = formItems.Skip(2).FirstOrDefault();
            var content3 = await formItem3.QuerySelectorAsync("div.el-form-item__content");
            var selector = await content3.QuerySelectorAsync("div.el-select > div.el-input.el-input--suffix");
            await selector.ClickAsync();
            await Task.Delay(500);
            var firstItem = await demoCard.Page.QuerySelectorAsync("div.el-select-dropdown.el-popper > div.el-scrollbar > div.el-select-dropdown__wrap.el-scrollbar__wrap > ul.el-scrollbar__view.el-select-dropdown__list > li.el-select-dropdown__item");
            await firstItem.ClickAsync();
            await Task.Delay(500);
            var itemText = await firstItem.EvaluateFunctionAsync<string>("x=>x.innerText");
            var input = await content3.QuerySelectorAsync("div.el-select > div.el-input.el-input--suffix > input[type='text'][placeholder='请选择活动区域'].el-input__inner");
            var selectedItemText = await input.EvaluateFunctionAsync<string>("x=>x.value");
            Assert.Equal(itemText?.Trim(), selectedItemText?.Trim());
            firstItem = await demoCard.Page.QuerySelectorAsync("div.el-select-dropdown.el-popper > div.el-scrollbar > div.el-select-dropdown__wrap.el-scrollbar__wrap > ul.el-scrollbar__view.el-select-dropdown__list > li.el-select-dropdown__item");
            Assert.Null(firstItem);
            filledIndexes.Add(2);
            await AssertFormAsync(demoCard, true, filledIndexes);
        }

        private async Task AssertFormAsync(DemoCard demoCard, bool showRequired, List<int> filledIndexes)
        {
            var form = await demoCard.Body.QuerySelectorAsync("form.el-form--label-left.el-form");
            Assert.NotNull(form);
            formItems = await form.QuerySelectorAllAsync("div.el-form-item");
            Assert.Equal(9, formItems.Length);
            foreach (var formItem in formItems)
            {
                var index = Array.IndexOf(formItems, formItem);
                var label = await formItem.QuerySelectorAsync("label[for='name'].el-form-item__label");
                var content = await formItem.QuerySelectorAsync("div.el-form-item__content");
                Assert.NotNull(content);
                var contentMarginLeft = await content.EvaluateFunctionAsync<string>("x=>x.style.marginLeft");
                var classList = await formItem.EvaluateFunctionAsync<string>("x=>x.classList.toString()");
                var clsList = classList.Split(' ').Select(x => x.Trim()).ToArray() ?? new string[0];
                string labelWidth = string.Empty;
                string labelText = string.Empty;
                var error = await content.QuerySelectorAsync("div.el-form-item__error");
                if (index < 8)
                {
                    Assert.NotNull(label);
                    labelWidth = await label.EvaluateFunctionAsync<string>("x=>x.style.width");
                    labelText = await label.EvaluateFunctionAsync<string>("x=>x.innerText");
                    Assert.Equal("100px", contentMarginLeft);
                    Assert.Equal("100px", labelWidth);
                    if (index == 2)
                    {
                        Assert.DoesNotContain("is-required", clsList);
                    }
                    else
                    {
                        Assert.Contains("is-required", clsList);
                    }
                }
                else
                {
                    Assert.Null(label);
                    Assert.True(string.IsNullOrWhiteSpace(contentMarginLeft));
                    Assert.True(string.IsNullOrWhiteSpace(labelWidth));
                    Assert.DoesNotContain("is-required", clsList);
                }
                if (index == 0)
                {
                    Assert.Equal("活动名称", labelText?.Trim());
                    var input = await content.QuerySelectorAsync("div.el-input > input[type='text'][name='Name'][placeholder='请输入内容'].el-input__inner");
                    Assert.NotNull(input);
                    var inputValue = await input.EvaluateFunctionAsync<string>("x=>x.value");
                    if (filledIndexes.Contains(0))
                    {
                        Assert.Null(error);
                        Assert.Equal("测试活动", inputValue);
                    }
                    else
                    {
                        await AssertErrorAsync(showRequired, error, "请确认活动名称");
                        Assert.Equal(string.Empty, inputValue);
                    }
                    continue;
                }

                if (index == 1)
                {
                    Assert.Equal("活动区域", labelText?.Trim());
                    var input = await content.QuerySelectorAsync("div.el-select > div.el-input.el-input--suffix > input[type='text'][placeholder='请选择活动区域'].el-input__inner");
                    Assert.NotNull(input);
                    var inputValue = await input.EvaluateFunctionAsync<string>("x=>x.value");
                    Assert.Equal("北京", inputValue);
                    var icon = await content.QuerySelectorAsync("div.el-select > div.el-input.el-input--suffix > span.el-input__suffix > span.el-input__suffix-inner > i.el-input__icon.el-select__caret.el-icon-arrow-up");
                    Assert.NotNull(icon);
                    Assert.Null(error);
                    continue;
                }

                if (index == 2)
                {
                    Assert.Equal("活动区域2", labelText?.Trim());
                    var input = await content.QuerySelectorAsync("div.el-select > div.el-input.el-input--suffix > input[type='text'][placeholder='请选择活动区域'].el-input__inner");
                    var inputValue = await input.EvaluateFunctionAsync<string>("x=>x.value");
                    Assert.NotNull(input);
                    var icon = await content.QuerySelectorAsync("div.el-select > div.el-input.el-input--suffix > span.el-input__suffix > span.el-input__suffix-inner > i.el-input__icon.el-select__caret.el-icon-arrow-up");
                    Assert.NotNull(icon);
                    Assert.Null(error);
                    if (filledIndexes.Contains(2))
                    {
                        Assert.Equal("北京", inputValue);
                    }
                    else
                    {
                        Assert.Equal(string.Empty, inputValue);
                    }
                    continue;
                }

                if (index == 3)
                {
                    Assert.Equal("活动时间", labelText?.Trim());
                    var input = await content.QuerySelectorAsync("div.el-input.el-date-editor.el-input--prefix.el-input--suffix.el-date-editor--date > input[type='text'][placeholder='请选择日期'][name='Time'].el-input__inner");
                    Assert.NotNull(input);
                    var inputValue = await input.EvaluateFunctionAsync<string>("x=>x.value");
                    Assert.Equal(string.Empty, inputValue);
                    var icon = await content.QuerySelectorAsync("div.el-input.el-date-editor.el-input--prefix.el-input--suffix.el-date-editor--date > span.el-input__prefix > i.el-input__icon.el-icon-date");
                    Assert.NotNull(icon);
                    await AssertErrorAsync(showRequired, error, "请确认活动时间");
                    continue;
                }

                if (index == 4)
                {
                    Assert.Equal("即时配送", labelText?.Trim());
                    var input = await content.QuerySelectorAsync("div.el-switch > input[type='checkbox'].el-switch__input");
                    Assert.NotNull(input);
                    var icon = await content.QuerySelectorAsync("div.el-switch > span.el-switch__core");
                    Assert.NotNull(icon);
                    var borderColor = await icon.EvaluateFunctionAsync<string>("x=>x.style.borderColor");
                    Assert.Equal("rgb(19, 206, 102)", borderColor);
                    var backgroundColor = await icon.EvaluateFunctionAsync<string>("x=>x.style.backgroundColor");
                    Assert.Equal("rgb(192, 204, 218)", backgroundColor);
                    Assert.Null(error);
                    continue;
                }

                if (index == 5)
                {
                    Assert.Equal("活动性质", labelText?.Trim());
                    var checkboxes = await content.QuerySelectorAllAsync("div.el-checkbox-group > label.el-checkbox");
                    Assert.Equal(2, checkboxes.Length);
                    await AssertErrorAsync(showRequired, error, "请确认活动性质");
                    continue;
                }

                if (index == 6)
                {
                    Assert.Equal("特殊资源", labelText?.Trim());
                    var radios = await content.QuerySelectorAllAsync("label.el-radio.el-radio-button--default");
                    Assert.Equal(2, radios.Length);
                    await AssertErrorAsync(showRequired, error, "请确认特殊资源");
                    continue;
                }

                if (index == 7)
                {
                    Assert.Equal("活动形式", labelText?.Trim());
                    var input = await content.QuerySelectorAsync("div.el-input > input[type='textarea'][name='Description'][placeholder='请输入内容'].el-input__inner");
                    Assert.NotNull(input);
                    var inputValue = await input.EvaluateFunctionAsync<string>("x=>x.value");
                    Assert.Equal(string.Empty, inputValue);
                    await AssertErrorAsync(showRequired, error, "请确认活动形式");
                    continue;
                }
                if (index == 8)
                {
                    Assert.True(string.IsNullOrWhiteSpace(labelText));
                    submitButton = await content.QuerySelectorAsync("button.el-button.el-button--primary");
                    Assert.NotNull(submitButton);
                    resetButton = await content.QuerySelectorAsync("button.el-button.el-button--default");
                    Assert.NotNull(resetButton);
                    continue;
                }

                throw new Exception(index.ToString());
            }
        }

        private static async Task AssertErrorAsync(bool showRequired, ElementHandle error, string expectedRrrorText)
        {
            if (showRequired)
            {
                Assert.NotNull(error);
                var errorText = await error.EvaluateFunctionAsync<string>("x=>x.innerText");
                Assert.Equal(expectedRrrorText, errorText.Trim());
            }
        }
    }
}
