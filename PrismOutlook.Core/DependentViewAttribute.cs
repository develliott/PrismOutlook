﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismOutlook.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependentViewAttribute : Attribute
    {
        public string Region { get; set; }
        public Type Type { get; set; }

        public DependentViewAttribute(string region, Type type)
        {
            Region = region ?? throw new ArgumentNullException(nameof(region));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
