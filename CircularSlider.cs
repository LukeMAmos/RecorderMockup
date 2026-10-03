using System; 
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives; 
using Avalonia.Input; 
using Avalonia.Media;
using HarfBuzzSharp;

//This code is for building a component instead of a skin for a component that already exists
namespace RecorderMockup.Controls
{
    public class CircularSlider : RangeBase
    {
        static CircularSlider()
            {
                AffectsRender<CircularSlider>(ValueProperty, MinimumProperty, MaximumProperty);
            }

        public override void Render(DrawingContext context)
        {
            //Same as paint from JUCE project 
            double width = Bounds.Width; 
            double height = Bounds.Height;
            if(width<=0.0f || height <= 0.0f) return; 

            //draw rounded rectangle background 
            context.DrawRectangle(backgroundColour, null , 
            new RoundedRect(new Rect(0,0 , width , height), cornerRadius)); 

            //Centre point 
            Point centre = new Point(width / 2, height / 2);

            double radius = (Math.Min(width, height)/2.0f) - 2 - dotRadius - 2; //10 here gives us a margin

            //Calculating the angle step between dots 
            float angleStep = 2.0f * (float)Math.PI / (float)numDots; 

            //for each of the dots, find its dot location and draw a circle at that position 

            for(int dot = 0; dot <numDots ; dot++)
            {
                //get dot location from the angle from step 
                float dotAngle = rotaryStartAngle + (dot * angleStep); 
                Point dotLocation = new Point(centre.X + radius * Math.Cos(dotAngle) , centre.Y + radius * Math.Sin(dotAngle));

                //using the dot location to draw each dot, could then implement different colours 
                var dotGeometry = new EllipseGeometry(new Rect (
                    dotLocation.X - dotRadius,
                    dotLocation.Y - dotRadius,
                    dotRadius * 2,
                    dotRadius * 2
                ));

                context.DrawGeometry(dotColourOn , null , dotGeometry); 
            }
        }

        private const float rotaryStartAngle = (float)Math.PI * 0.50f; //start right at bottom 
        private const double dotRadius = 9.0;  
        private IBrush dotColourOn = new SolidColorBrush(Color.Parse("#D76AFB")); 
        private IBrush backgroundColour = new SolidColorBrush(Color.Parse("#121212"));
        private const int numDots = 24; 
        private const double cornerRadius = 8.0; 
    }




}