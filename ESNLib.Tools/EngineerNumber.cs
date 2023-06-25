using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

        private bool HasUnit => !string.IsNullOrEmpty(Unit);

        /// <summary>
        /// Add a space between the number and the unit when printed to string
        /// </summary>
        public bool SpaceBeforeUnit { get; set; } = false;

        /// <summary>
        /// How many significatives digits to display
        /// </summary>
        public ushort PrecisionDigits { get; set; } = 3;

        /// <summary>
        /// Zeros with no value displayed
        /// </summary>
        public bool TrailingZeros { get; set; } = false;

        private static readonly Dictionary<string, short> PrefixToValue = new Dictionary<
            string,
            short
        >()
        {
            { "y", -8 },
            { "z", -7 },
            { "a", -6 },
            { "f", -5 },
            { "p", -4 },
            { "n", -3 },
            { "μ", -2 },
            { "m", -1 },
            { "", 0 },
            { "k", 1 },
            { "M", 2 },
            { "G", 3 },
            { "T", 4 },
            { "P", 5 },
            { "E", 6 },
            { "Z", 7 },
            { "Y", 8 },
        };
        private static readonly Dictionary<short, string> ValueToPrefix = new Dictionary<
            short,
            string
        >()
        {
            { -8, "y" },
            { -7, "z" },
            { -6, "a" },
            { -5, "f" },
            { -4, "p" },
            { -3, "n" },
            { -2, "μ" },
            { -1, "m" },
            { 0, "" },
            { 1, "k" },
            { 2, "M" },
            { 3, "G" },
            { 4, "T" },
            { 5, "P" },
            { 6, "E" },
            { 7, "Z" },
            { 8, "Y" },
        };

        /// <summary>
        /// Implicit casting to the class
        /// </summary>
        public static implicit operator EngineerNumber(float x)
        {
            EngineerNumber n = new EngineerNumber(x);
            return n;
        }

        /// <summary>
        /// Implicit casting to the class
        /// </summary>
        public static implicit operator EngineerNumber(double x)
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

        /// <summary>
        /// +(n)
        /// </summary>
        public static EngineerNumber operator +(EngineerNumber n) => n;

        /// <summary>
        /// -(n)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static EngineerNumber operator -(EngineerNumber n) =>
            new EngineerNumber(-n.Value, n.Unit, n.SpaceBeforeUnit, n.PrecisionDigits);

        /// <summary>
        /// a+b
        /// </summary>
        public static EngineerNumber operator +(EngineerNumber a, EngineerNumber b)
        {
            // No unit verification
            return new EngineerNumber(
                a.Value + b.Value,
                a.Unit,
                a.SpaceBeforeUnit,
                a.PrecisionDigits
            );
        }

        /// <summary>
        /// a-b
        /// </summary>
        public static EngineerNumber operator -(EngineerNumber a, EngineerNumber b)
        {
            // No unit verification
            return new EngineerNumber(
                a.Value - b.Value,
                a.Unit,
                a.SpaceBeforeUnit,
                a.PrecisionDigits
            );
        }

        /// <summary>
        /// a*b
        /// </summary>
        public static EngineerNumber operator *(EngineerNumber a, EngineerNumber b)
        {
            // No unit operation
            return new EngineerNumber(a.Value * b.Value, "?", a.SpaceBeforeUnit, a.PrecisionDigits);
        }

        /// <summary>
        /// a/b
        /// </summary>
        public static EngineerNumber operator /(EngineerNumber a, EngineerNumber b)
        {
            // No unit operation
            return new EngineerNumber(a.Value / b.Value, "?", a.SpaceBeforeUnit, a.PrecisionDigits);
        }

        /// <summary>
        /// Create a <see cref="Double"/> as an engineer number
        /// </summary>
        /// <param name="Number">Number</param>
        public EngineerNumber(EngineerNumber Number)
        {
            this.Value = Number.Value;
            this.Unit = Number.Unit;
            this.SpaceBeforeUnit = Number.SpaceBeforeUnit;
            this.PrecisionDigits = Number.PrecisionDigits;
        }

        /// <summary>
        /// Create a <see cref="Double"/> as an engineer number
        /// </summary>
        /// <param name="Number">Number</param>
        public EngineerNumber(float Number)
        {
            this.Value = Number;
        }

        /// <summary>
        /// Create a <see cref="Double"/> as an engineer number
        /// </summary>
        /// <param name="Number">Number</param>
        public EngineerNumber(double Number)
        {
            this.Value = Number;
        }

        /// <summary>
        /// Create a <see cref="Double"/> as an engineer number
        /// </summary>
        /// <param name="Number">Number</param>
        /// <param name="Unit">Unit of the type, if any. (i.e. N for Newton)</param>
        public EngineerNumber(double Number, string Unit)
        {
            this.Value = Number;
            this.Unit = Unit;
        }

        /// <summary>
        /// Create a <see cref="Double"/> as an engineer number
        /// </summary>
        /// <param name="Number">Number</param>
        /// <param name="Unit">Unit of the type, if any. (i.e. N for Newton)</param>
        /// <param name="Spacing">Add a space between the number and the unit</param>
        public EngineerNumber(double Number, string Unit, bool Spacing)
        {
            this.Value = Number;
            this.Unit = Unit;
            this.SpaceBeforeUnit = Spacing;
        }

        /// <summary>
        /// Create a <see cref="Double"/> as an engineer number
        /// </summary>
        /// <param name="Number">Number</param>
        /// <param name="Unit">Unit of the type, if any. (i.e. N for Newton)</param>
        /// <param name="Spacing">Add a space between the number and the unit</param>
        /// <param name="Precision">The number of digit precision (significative digits)</param>
        public EngineerNumber(double Number, string Unit, bool Spacing, ushort Precision)
        {
            this.Value = Number;
            this.Unit = Unit;
            this.SpaceBeforeUnit = Spacing;
            this.PrecisionDigits = Precision;
        }

        /// <summary>
        /// Create a <see cref="Double"/> as an engineer number
        /// </summary>
        /// <param name="Number">Number</param>
        /// <param name="Unit">Unit of the type, if any. (i.e. N for Newton)</param>
        /// <param name="Spacing">Add a space between the number and the unit</param>
        /// <param name="Precision">The number of digit precision (significative digits)</param>
        /// <param name="TrailingZeros">Zeros with no value displayed</param>
        public EngineerNumber(
            double Number,
            string Unit,
            bool Spacing,
            ushort Precision,
            bool TrailingZeros
        )
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
                return HasUnit ? $"{Value}{(Space ? " " : "")}{Unit}" : Value.ToString();
            }

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
            short SubPowerValue = (short)Math.Floor(Math.Log10(NewValue));

            // Trick to get the number of significative digits requested.
            NewValue =
                Math.Round(NewValue * Math.Pow(10, -SubPowerValue), Digits - 1)
                * Math.Pow(10, SubPowerValue);

            string Output = TrailingZeros
                ? NewValue.ToString($"N{PrecisionDigits - 1}")
                : NewValue.ToString();
            if ((!HasUnit) || (PowerValue < -8 || PowerValue > 8))
            {
                if (PowerValue != 0)
                {
                    Output += $"e{PowerValue * 3}{((Space && HasUnit) ? " " : "")}{Unit}";
                }
            }
            else
            {
                Output += $"{(Space ? " " : "")}{ValueToPrefix[PowerValue]}{Unit}";
            }

            if (isNegative)
            {
                Output = "-" + Output;
            }

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
            Text = Text.Trim();

            // Try direct conversion

            Regex validateDoubleFormat = new Regex(
                @"(\d{1,}(?:[.]\d{0,})?e-?\d{1,})[ ]?(?:\w{1,})?"
            );
            var result = validateDoubleFormat.Match(Text);
            if (result.Success)
            {
                if (!double.TryParse(result.Groups[1].Value, out double temp))
                {
                    return double.NaN;
                }
                return temp;
            }

            short PowS = 0;

            Regex validateSiPrefix = new Regex(@"^(\d{1,}(?:[.]\d{0,})?)[ ]?(\w)?(?:\w{1,})?$");
            result = validateSiPrefix.Match(Text);

            if (!result.Success)
            {
                return double.NaN;
            }

            // Group[1] : number        Group[2] : SI prefix
            if (!double.TryParse(result.Groups[1].Value, out double Value))
            {
                return double.NaN;
            }

            string si = result.Groups[2].Value;
            if (!PrefixToValue.ContainsKey(si))
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

            short delta = PrefixToValue[si];
            PowS += delta;

            Value *= Math.Pow(10, 3 * PowS);

            return Value;
        }
    }
}
