using System;
using task1.Droid;
using task1.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GradientColorStack), typeof(GradientColorStackRenderer))]  
namespace task1.Droid {
    [Obsolete]
    public class GradientColorStackRenderer : VisualElementRenderer<StackLayout>
{
    private Color StartColor
    {
        get;
        set;
    }
    private Color EndColor
    {
        get;
        set;
    }
    protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
    {
        var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Width,
        //var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
                this.StartColor.ToAndroid(),
        this.EndColor.ToAndroid(),
        Android.Graphics.Shader.TileMode.Clamp);//mirror
        var paint = new Android.Graphics.Paint()
        {
            Dither = true,
        };
        paint.SetShader(gradient);
        canvas.DrawPaint(paint);
        base.DispatchDraw(canvas);
    }
    protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
    {
        base.OnElementChanged(e);
        if (e.OldElement != null || Element == null)
        {
            return;
        }
        try
        {
            var stack = e.NewElement as GradientColorStack;
            this.StartColor = stack.StartColor;
            this.EndColor = stack.EndColor;
        }
        catch (Exception ex)
        {
        }
    }
}  
}  