﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.Container
{
    public class BTabPanelBase : ComponentBase, ITab
    {

        [CascadingParameter]
        private BTabs BTabs { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected string IsActive { get; set; }

        protected override void OnInit()
        {
            BTabs.AddTab(this);
        }

        public void Dispose()
        {
            BTabs.RemoveTab(this);
        }

        private void Activate()
        {
            BTabs.SetActivateTab(this);
        }
    }
}
