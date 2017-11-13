﻿using Microsoft.Msagl.Drawing;
using RandomColorGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetGraph
{
    public static class Colorizer
    {
        public static List<Color> UsedColors { get; set; } = new List<Color>();

        public static Color GetRandomColor()
        {
            Random r = new Random();
            //var color = new Color((byte)r.Next(128, 255), (byte)r.Next(128, 255), (byte)r.Next(128, 255));
            var random = RandomColor.GetColor(ColorScheme.Random, Luminosity.Light);
            var color = new Color(random.R, random.G, random.B);

            if (!UsedColors.Contains(color))
                return color;
            else
            {
                GetRandomColor();
            }
            return Color.White;
        }
    }
}
