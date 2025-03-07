﻿using System.Globalization;
using Microsoft.AspNetCore.Components;
using MudBlazor.Extensions;
using MudBlazor.Utilities;

namespace MudBlazor
{
    public partial class MudSlider<T> : MudComponentBase
    {
        protected string Classname =>
            new CssBuilder("mud-slider")
                .AddClass(Class)
                .Build();

        protected string _value;
        protected string _state;
        protected string _min = "0";
        protected string _max = "100";
        protected string _step = "1";

        /// <summary>
        /// The minimum allowed value of the slider. Should not be equal to max.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Slider.Validation)]
        public T Min
        {
            get => Converter.Get(_min);
            set => _min = Converter.Set(value);
        }

        /// <summary>
        /// The maximum allowed value of the slider. Should not be equal to min.
        /// </summary>
        /// 
        [Parameter]
        [Category(CategoryTypes.Slider.Validation)]
        public T Max
        {
            get => Converter.Get(_max);
            set => _max = Converter.Set(value);
        }

        /// <summary>
        /// How many steps the slider should take on each move.
        /// </summary>
        /// 
        [Parameter]
        [Category(CategoryTypes.Slider.Validation)]
        public T Step
        {
            get => Converter.Get(_step);
            set => _step = Converter.Set(value);
        }

        /// <summary>
        /// If true, the slider will be disabled.
        /// </summary>
        /// 
        [Parameter]
        [Category(CategoryTypes.Slider.Behavior)]
        public bool Disabled { get; set; }

        /// <summary>
        /// If true, the component use State / StateChanged instead of Value.
        /// </summary>
        /// 
        [Parameter]
        [Category(CategoryTypes.Slider.Behavior)]
        public bool Mvu { get; set; }

        /// <summary>
        /// Child content of component.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Slider.Behavior)]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        [Category(CategoryTypes.Slider.Behavior)]
        public Converter<T> Converter { get; set; } = new DefaultConverter<T>() { Culture = CultureInfo.InvariantCulture };

        [Parameter] public EventCallback<T> ValueChanged { get; set; }
        [Parameter] public EventCallback<T> StateChanged { get; set; }


        [Parameter]
        [Category(CategoryTypes.Slider.Data)]
        public T State
        {
            get => Converter.Get(_state);
            set
            {
                var d = Converter.Set(value);
                _state = d;
            }
        }

        [Parameter]
        [Category(CategoryTypes.Slider.Data)]
        public T Value
        {
            get => Converter.Get(_value);
            set
            {
                var d = Converter.Set(value);
                if (_value == d)
                    return;
                _value = d;
                ValueChanged.InvokeAsync(value);
            }
        }

        /// <summary>
        /// The color of the component. It supports the Primary, Secondary and Tertiary theme colors.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Slider.Appearance)]
        public Color Color { get; set; } = Color.Primary;

        protected string InputClassName => $"mud-slider-{Color.ToDescriptionString()}";

        protected string Text
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;
                _value = value;
                ValueChanged.InvokeAsync(Value);
            }
        }

        /// <summary>
        /// If true, the dragging the slider will update the Value immediately.
        /// If false, the Value is updated only on releasing the handle.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Slider.Behavior)]
        public bool Immediate { get; set; } = true;

        //protected static string ToFpS(double value)
        //{
        //    var s = ToS(value);
        //    if (!s.Contains('.'))
        //        return s + ".0";
        //    return s;
        //}

        private void OnStateChanged(bool fire, ChangeEventArgs args)
        {
            if (fire) StateChanged.InvokeAsync(Converter.Get((string)args.Value));
        }
    }
}
