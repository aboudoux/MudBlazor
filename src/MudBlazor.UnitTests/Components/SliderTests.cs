// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using NUnit.Framework;

namespace MudBlazor.UnitTests.Components;

[TestFixture]
public class SliderTests : BunitTest
{
    private int _valueState = 0;
    private int ValueState
    {
        get => _valueState;
        set => _valueState = value;
    }
    
    [Test]
    public void Slider_UseStateOnInput()
    {
        var compo = Context.RenderComponent<MudSlider<int>>(
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.Min), 1),
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.Step), 1),
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.Max), 10),
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.Mvu), true),
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.State), ValueState),
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.StateChanged), EventCallback.Factory.Create<int>(this, (a) => ValueState = a))
            );

        compo.Find("input").Input(5);
        ValueState.Should().Be(5);
        compo.RenderCount.Should().Be(2);
    }

    [Test]
    public void Slider_UseStateOnChange()
    {
        var compo = Context.RenderComponent<MudSlider<int>>(
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.Min), 1),
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.Step), 1),
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.Max), 10),
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.Mvu), true),
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.State), ValueState),
            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.StateChanged), EventCallback.Factory.Create<int>(this, (a) => ValueState = a)),

            ComponentParameterFactory.Parameter(nameof(MudSlider<int>.Immediate), false)
        );

        compo.Find("input").Change(5);
        ValueState.Should().Be(5);
        compo.RenderCount.Should().Be(2);
    }
}
