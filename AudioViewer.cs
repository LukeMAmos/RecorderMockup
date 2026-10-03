using System; 
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives; 
using Avalonia.Input; 
using Avalonia.Media;


namespace RecorderMockup.Viewer
{
    public class AudioViewer : Control
    {
        
        public void SetSamples(float[] samples)
        {
            if(samples.Length < maxSamples) return; 
            Samples = samples; 
            InvalidateVisual(); 
        }

        public override void Render(DrawingContext context)
        {
            //Get the width and height used for calculating spacing
            double width = Bounds.Width; 
            double height = Bounds.Height;
            if(width <= 0 || height <= 0) return; 
            double centreLine = height / 2.0; 

            //Calculate spacing between the dots 
            double YSpacing = (((height - 2 * margin) / 2.0)/ maxDots); 
            double XSpacing = ((width - 2 * margin) / maxSamples); 

            //draw rounded rectangle background 
            context.DrawRectangle(backgroundColour, null , 
            new RoundedRect(new Rect(0,0 , width , height), cornerRadius)); 

            //32 Rows so 32 Samples, 
            if(Samples == null) return; 
            for(int samplePos = 0; samplePos < maxSamples ; samplePos++)
            {

                //For each sample position work out how many circles to draw, up or down from the centre line based on its sample value 
                float val = Samples[samplePos];  
                int nDots = (int)(Math.Abs(val) * 10.0);
                nDots = Math.Min(nDots , maxDots); //never draw more than 10 dots  
                
                int direction = (Math.Sign(val) == Math.Sign(1.0)) ? -1 : 1; 
                
                //Draw each of the dots individually, spacing out in both width and height 
                for(int dot = 0; dot < nDots ; dot++)
                {   

                    Point dotLocation = new Point(margin + samplePos * XSpacing + XSpacing / 2.0, centreLine + dot * YSpacing * direction);    

                    var dotGeometry = new EllipseGeometry(new Rect (
                        dotLocation.X - dotRadius,
                        dotLocation.Y - dotRadius,
                        dotRadius * 2,
                        dotRadius * 2
                        ));
                    
                    context.DrawGeometry(dotColourOn , null , dotGeometry); 

                }

            }
        }

        private float[]? Samples;
        private const int maxDots = 10; //this is maxdots in one direction on a single sample 
        private const int maxSamples = 32; 
        private const double dotRadius = 3.0; 
        private IBrush dotColourOn = new SolidColorBrush(Color.Parse("#D76AFB")); 
        private IBrush backgroundColour = new SolidColorBrush(Color.Parse("#121212"));
        private const double cornerRadius = 8.0; 
        private const double margin = 2.0; 
    }



}