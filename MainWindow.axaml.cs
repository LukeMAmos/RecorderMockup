using System;
using Avalonia.Controls;
using RecorderMockup.Viewer;

namespace RecorderMockup;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        float[] samples = new float[32];

        for(int i = 0; i < 32 ; i++)
        {
            //next single is from 0 to 1 , change to -1 to 1 
            samples[i] = Random.Shared.NextSingle() *2f -1f;
        }

        audioViewer.SetSamples(samples); 
    }
}