﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class DemoBlock
{
    /// <summary>
    /// 获得/设置 组件 Title 属性
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 组件说明信息
    /// </summary>
    [Parameter]
    public string Introduction { get; set; } = "未设置";

    /// <summary>
    /// 文件名 从ComponentLayout传递过来的razor文件名
    /// </summary>
    [CascadingParameter(Name = "RazorFileName")]
    public string? RazorFileName { get; set; }

    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 示例代码片段 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? Demo { get; set; }

    /// <summary>
    /// 获得/设置 是否显示代码块 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowCode { get; set; } = true;

    /// <summary>
    /// 获得/设置 Tooltip 提示信息文本
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<DemoBlock>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private DemoComponentConverter? ComponentConverter { get; set; }

    /// <summary>
    /// 获得/设置 友好链接锚点名称
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    private string BlockTitle => Name ?? Title;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Title ??= Localizer[nameof(Title)];
        TooltipText ??= Localizer[nameof(TooltipText)];
    }

    private RenderFragment RenderChildContent => builder =>
    {
        builder.AddContent(0, ChildContent);

        if (ComponentConverter.TryParse(Demo, out var t))
        {
            builder.OpenComponent(1, t);
            builder.CloseComponent();
        }
    };
}
