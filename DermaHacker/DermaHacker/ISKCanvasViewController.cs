using System;
using SkiaSharp;
using Xamarin.Forms;

namespace DermaHacker
{
    internal interface ISKCanvasViewController : IViewController, IVisualElementController, IElementController
    {
        event EventHandler SurfaceInvalidated;

        event EventHandler<GetPropertyValueEventArgs<SKSize>> GetCanvasSize;

        void OnPaintSurface(SKPaintSurfaceEventArgs e);

        void OnTouch(SKTouchEventArgs e);
    }
    internal class GetPropertyValueEventArgs<T> : EventArgs
    {
        public T Value { get; set; }
    }
    public class SKPaintSurfaceEventArgs : EventArgs
    {
        public SKPaintSurfaceEventArgs(SKSurface surface, SKImageInfo info)
        {
            this.Surface = surface;
            this.Info = info;
        }

        public SKSurface Surface { get; private set; }

        public SKImageInfo Info { get; private set; }
    }
    public class SKTouchEventArgs : EventArgs
    {
        public SKTouchEventArgs(long id, SKTouchAction type, SKPoint location, bool inContact)
            : this(id, type, SKMouseButton.Left, SKTouchDeviceType.Touch, location, inContact)
        {
        }

        public SKTouchEventArgs(
            long id,
            SKTouchAction type,
            SKMouseButton mouseButton,
            SKTouchDeviceType deviceType,
            SKPoint location,
            bool inContact)
        {
            this.Id = id;
            this.ActionType = type;
            this.DeviceType = deviceType;
            this.MouseButton = mouseButton;
            this.Location = location;
            this.InContact = inContact;
        }

        public bool Handled { get; set; }

        public long Id { get; private set; }

        public SKTouchAction ActionType { get; private set; }

        public SKTouchDeviceType DeviceType { get; private set; }

        public SKMouseButton MouseButton { get; private set; }

        public SKPoint Location { get; private set; }

        public bool InContact { get; private set; }

        public override string ToString()
        {
            return string.Format("{{ActionType={0}, DeviceType={1}, Handled={2}, Id={3}, InContact={4}, Location={5}, MouseButton={6}}}", (object)this.ActionType, (object)this.DeviceType, (object)this.Handled, (object)this.Id, (object)this.InContact, (object)this.Location, (object)this.MouseButton);
        }
    }
    public enum SKTouchAction
    {
        Entered,
        Pressed,
        Moved,
        Released,
        Cancelled,
        Exited,
    }
    public enum SKMouseButton
    {
        Unknown,
        Left,
        Middle,
        Right,
    }
    public enum SKTouchDeviceType
    {
        Touch,
        Mouse,
        Pen,
    }
}