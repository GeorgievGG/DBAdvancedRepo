using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using PhotoShare.Models;
using PhotoShare.Data;

namespace PhotoShare.Services.Services
{
    public class ColorService : IColorService
    { 
        public Color ById(int id)
        {
            return (Color)id;
        }

        public Color ByColorString(string color)
        {
            return (Color)Enum.Parse(typeof(Color), color);
        }
    }
}
