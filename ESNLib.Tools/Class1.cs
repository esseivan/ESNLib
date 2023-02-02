using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    /// <summary>
    /// Define a number that is easily displayed in an engineer format (SI)
    /// </summary>
    public class EngineerNumber
    {
        /// <summary>
        /// Actual value of the number
        /// </summary>
        private double Value = default;

        /// <summary>
        /// Unit of the type, if any. (i.e. N for Newton)
        /// </summary>
        private string Unit = string.Empty;

        /// <summary>
        /// Add a space between the number and the unit when printed to string
        /// </summary>
        public bool SpaceBeforeUnit { get; set; } = false;

        /// <summary>
        /// How many digits to display
        /// </summary>
        public ushort PrecisionDigits { get; set; } = 3;

        /// <summary>
        /// Implicit casting to the class
        /// </summary>
        public static implicit operator EngineerNumber(float x)
        {
            EngineerNumber n = new EngineerNumber(x);
            return n;
        }

        /// <summary>
        /// Implicit casting to a double
        /// </summary>
        public static implicit operator double(EngineerNumber n)
        {
            return n.Value;
        }

        // public static Fraction operator +(EngineerNumber n)


        /// <summary>
        /// Create a
        /// </summary>
        /// <param name="Number">Number</param>
        public EngineerNumber(double Number)
        {
            this.Value = Number;
        }

        /// <summary>
        /// Create a
        /// </summary>
        /// <param name="Number">Number</param>
        /// <param name="Unit">Unit of the type, if any. (i.e. N for Newton)</param>
        public EngineerNumber(double Number, string Unit)
        {
            this.Value = Number;
            this.Unit = Unit;
        }

        /// <summary>
        /// Create a
        /// </summary>
        /// <param name="Number">Number</param>
        /// <param name="Unit">Unit of the type, if any. (i.e. N for Newton)</param>
        public EngineerNumber(double Number, string Unit, bool Spacing)
        {
            this.Value = Number;
            this.Unit = Unit;
            this.SpaceBeforeUnit = Spacing;
        }

        /// <summary>
        /// Create a
        /// </summary>
        /// <param name="Number">Number</param>
        /// <param name="Unit">Unit of the type, if any. (i.e. N for Newton)</param>
        public EngineerNumber(double Number, string Unit, bool Spacing, ushort Precision)
        {
            this.Value = Number;
            this.Unit = Unit;
            this.SpaceBeforeUnit = Spacing;
            this.PrecisionDigits = Precision;
        }

        public override string ToString()
        {
            return DecimalToEngineer(PrecisionDigits, SpaceBeforeUnit);
        }

        /// <summary>
        /// Convert decimal format to engineer format
        /// </summary>
        private string DecimalToEngineer(ushort Digits, bool Space = false)
        {
            if (double.IsNaN(Value))
            {
                return Value.ToString();
            }

            if (double.IsInfinity(Value))
            {
                return $"{Value}{(Space ? " " : "")}{Unit}";
            }

            string Output = string.Empty;

            bool isNegative;
            if (Value >= 0)
            {
                isNegative = false;
            }
            else
            {
                isNegative = true;
                Value = -Value;
            }

            short PowerValue = (short)Math.Floor(Math.Log10(Value) / 3);

            double NewValue = Value * Math.Pow(10, -PowerValue * 3);

            NewValue = Math.Round(NewValue, Digits);

            string[] Prefixes =
            {
                "y", // -8
                "z", // -7
                "a", // -6
                "f", // -5
                "p", // -4
                "n", // -3
                "μ", // -2
                "m", // -1
                "", //  0
                "k", //  1
                "M", //  2
                "G", //  3
                "T", //  4
                "P", //  5
                "E", //  6
                "Z", //  7
                "Y" //  8
            };

            if (PowerValue < -8 || PowerValue > 8)
                Output = $"{NewValue}e{PowerValue * 3}{(Space ? " " : "")}{Unit}";
            else
                Output = $"{NewValue}{(Space ? " " : "")}{Prefixes[PowerValue + 8]}{Unit}";

            if (isNegative)
                Output = "-" + Output;

            return Output;
        }

        /// <summary>
        /// Convert engineer format to decimal
        /// </summary>
        public static double EngineerToDecimal(string Text)
        {
            if (Text == string.Empty || Text == null)
            {
                return double.NaN;
            }

            short PowS = 0;

            char PowSString = Text.LastOrDefault();
            if (double.TryParse(Text, out double temp))
            {
                return temp;
            }

            if (!double.TryParse(Text.Remove(Text.Length - 1, 1), out double Value))
            {
                return double.NaN;
            }

            while (Value < 1)
            {
                Value *= 1000;
                PowS--;
            }

            while (Value >= 1000)
            {
                Value /= 1000;
                PowS++;
            }

            switch (PowSString)
            {
                case 'm':
                    PowS -= 1;
                    break;
                case 'k':
                    PowS += 1;
                    break;
                case 'M':
                    PowS += 2;
                    break;
                case 'G':
                    PowS += 3;
                    break;
                default:
                {
                    return double.NaN;
                }
            }

            Value *= Math.Pow(10, 3 * PowS);

            return Value;
        }
    }
}
