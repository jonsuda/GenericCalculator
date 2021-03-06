﻿using System.ComponentModel.Composition;

namespace GenericCalculator.Operations
{
    [Export(typeof(IOperation<double>))]
    [ExportMetadata("Name", "Division")]
    [ExportMetadata("Symbol", '/')]
    public class DivisionDouble : IOperation<double>
    {
        public double Perform(double a, double b)
        {
            return a / b;
        }
    }
}
