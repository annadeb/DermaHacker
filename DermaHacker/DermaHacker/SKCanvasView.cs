using System;
using SkiaSharp;
using Xamarin.Forms;

namespace DermaHacker
{
    //[RenderWith(typeof(SKCanvasViewRenderer))]
    //public class SKCanvasView : View, ISKCanvasViewController, IViewController, IVisualElementController, IElementController
    //{
    //    public static readonly BindableProperty IgnorePixelScalingProperty = BindableProperty.Create(nameof(IgnorePixelScaling), typeof(bool), typeof(SKCanvasView), (object)false, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);
    //    public static readonly BindableProperty EnableTouchEventsProperty = BindableProperty.Create(nameof(EnableTouchEvents), typeof(bool), typeof(SKCanvasView), (object)false, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

    //    public event EventHandler<SKPaintSurfaceEventArgs> PaintSurface;

    //    public event EventHandler<SKTouchEventArgs> Touch;

    //    private event EventHandler SurfaceInvalidated;

    //    private event EventHandler<GetPropertyValueEventArgs<SKSize>> GetCanvasSize;

    //    public SKSize CanvasSize
    //    {
    //        get
    //        {
    //            GetPropertyValueEventArgs<SKSize> e = new GetPropertyValueEventArgs<SKSize>();
    //            EventHandler<GetPropertyValueEventArgs<SKSize>> getCanvasSize = this.GetCanvasSize;
    //            if (getCanvasSize != null)
    //                getCanvasSize((object)this, e);
    //            return e.Value;
    //        }
    //    }

    //    public bool IgnorePixelScaling
    //    {
    //        get
    //        {
    //            return (bool)this.GetValue(SKCanvasView.IgnorePixelScalingProperty);
    //        }
    //        set
    //        {
    //            this.SetValue(SKCanvasView.IgnorePixelScalingProperty, (object)value);
    //        }
    //    }

    //    public bool EnableTouchEvents
    //    {
    //        get
    //        {
    //            return (bool)this.GetValue(SKCanvasView.EnableTouchEventsProperty);
    //        }
    //        set
    //        {
    //            this.SetValue(SKCanvasView.EnableTouchEventsProperty, (object)value);
    //        }
    //    }

    //    public void InvalidateSurface()
    //    {
    //        EventHandler surfaceInvalidated = this.SurfaceInvalidated;
    //        if (surfaceInvalidated == null)
    //            return;
    //        surfaceInvalidated((object)this, EventArgs.Empty);
    //    }

    //    protected virtual void OnPaintSurface(SKPaintSurfaceEventArgs e)
    //    {
    //        EventHandler<SKPaintSurfaceEventArgs> paintSurface = this.PaintSurface;
    //        if (paintSurface == null)
    //            return;
    //        paintSurface((object)this, e);
    //    }

    //    protected virtual void OnTouch(SKTouchEventArgs e)
    //    {
    //        EventHandler<SKTouchEventArgs> touch = this.Touch;
    //        if (touch == null)
    //            return;
    //        touch((object)this, e);
    //    }

    //    event EventHandler ISKCanvasViewController.SurfaceInvalidated
    //    {
    //        add
    //        {
    //            this.SurfaceInvalidated += value;
    //        }
    //        remove
    //        {
    //            this.SurfaceInvalidated -= value;
    //        }
    //    }

    //    event EventHandler<GetPropertyValueEventArgs<SKSize>> ISKCanvasViewController.GetCanvasSize
    //    {
    //        add
    //        {
    //            this.GetCanvasSize += value;
    //        }
    //        remove
    //        {
    //            this.GetCanvasSize -= value;
    //        }
    //    }

    //    void ISKCanvasViewController.OnPaintSurface(SKPaintSurfaceEventArgs e)
    //    {
    //        this.OnPaintSurface(e);
    //    }

    //    void ISKCanvasViewController.OnTouch(SKTouchEventArgs e)
    //    {
    //        this.OnTouch(e);
    //    }

    //    protected override SizeRequest OnMeasure(
    //      double widthConstraint,
    //      double heightConstraint)
    //    {
    //        return new SizeRequest(new Size(40.0, 40.0));
    //    }
    //}
    //internal class SKCanvasViewRenderer
    //{
    //}
}
