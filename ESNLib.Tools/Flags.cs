using System;
using System.Collections.Generic;

namespace ESNLib.Tools
{
    /// <summary>
    /// Easily use flags with a specific length of bits
    /// </summary>
    public class Flags
    {
        /// <summary>
        /// List that contains all flags data
        /// </summary>
        public List<int> FlagList { get; set; }
        /// <summary>
        /// Number of bit per element of the FlagList (Int32)
        /// </summary>
        public const int typeByteCount = 32;

        public Flags()
        {
            FlagList = new List<int>();
        }

        /// <summary>
        /// Check if FlagList is valid for write/read
        /// </summary>
        public bool CheckValid()
        {
            return FlagList != null;
        }

        #region GetSet

        /// <summary>
        /// Get a single bit at the specified index
        /// </summary>
        public bool GetBit(int index)
        {
            return GetBits(index, 1) > 0;
        }

        /// <summary>
        /// Get a range of bits
        /// </summary>
        public int GetBits(int startIndex, int count)
        {
            if (!CheckValid())
            {
                FlagList = new List<int>();
                return -1;
            }

            // Invalid count, set to maxCount
            if (count > typeByteCount)
            {
                count = typeByteCount;
            }

            int t_count = count,
                t_index = startIndex,
                t_count2 = 0;

            // Get index for the list
            int list_index = t_index / typeByteCount;
            t_index %= typeByteCount;

            // If overflow, do in 2 steps for 2 elements of the list
            if (t_index + t_count > typeByteCount)
            {
                t_count = typeByteCount - t_index;
                t_count2 = count - t_count;
            }
            
            // If index not existing, return -1
            if (FlagList.Count < list_index + 1)
            {
                return -1;
            }

            // Get the mask to retrieve only the wanted data
            long mask = GetMask(t_index, t_count);
            // Get the value of the first element of the list
            long t0 = FlagList[list_index] & mask;
            int t1 = (int)Math.Pow(2, t_index);
            long output = (t0 / t1);

            // If count 2nd element not 0
            if (t_count2 > 0)
            {
                // If element not existing, abort and return the output
                if (FlagList.Count < list_index + 2)
                {
                    return (int)output;
                }

                // Get the mask to retrieve only the wanted data
                mask = GetMask(0, t_count2);

                // Add the value of the first element of the list
                t0 = FlagList[list_index + 1] & mask;
                t1 = (int)Math.Pow(2, t_count);
                output += (t0 * t1);
            }

            return (int)output;
        }

        /// <summary>
        /// Set a specific bit
        /// </summary>
        public void SetBit(int index, bool value)
        {
            SetBits(index, 1, value ? 1 : 0);
        }

        /// <summary>
        /// Set a specific range from startIndex to startIndex + maxCount
        /// </summary>
        public void SetBits(int startIndex, long value)
        {
            SetBits(startIndex, typeByteCount, value);
        }

        /// <summary>
        /// Set a specific range
        /// </summary>
        public void SetBits(int startIndex, int count, long value)
        {
            if (!CheckValid())
            {
                FlagList = new List<int>();
            }

            // Invalid count, set to maxCount
            if (count > typeByteCount)
            {
                count = typeByteCount;
            }

            int t_count = count,
                t_index = startIndex,
                t_count2 = 0;

            // Get index for the list
            int list_index = t_index / typeByteCount;
            t_index = t_index % typeByteCount;

            // If overflow, do in 2 steps for 2 elements of the list
            if (t_index + t_count > typeByteCount)
            {
                t_count = typeByteCount - t_index;
                t_count2 = count - t_count;
            }

            // While not enough flags, add new
            while (FlagList.Count < list_index + 1)
            {
                FlagList.Add(0);
            }

            // Get the mask to retrieve only the wanted data
            long mask = GetMask(t_index, t_count);
            // Get the value for the first element of the list
            long t_value = (value * (long)Math.Pow(2, t_index)) & mask;
            // Apply the value to the element of the list (without touching others values)
            long wReg = FlagList[list_index];
            wReg &= ~mask;
            wReg |= t_value;
            FlagList[list_index] = (int)wReg;

            // If count 2nd element not 0
            if (t_count2 > 0)
            {
                // While not enough flags, add new
                while (FlagList.Count < list_index + 2)
                {
                    FlagList.Add(0);
                }

                // Get the mask to retrieve only the wanted data
                mask = GetMask(0, t_count2);
                // Get the value for the first element of the list
                t_value = (value / (long)Math.Pow(2, t_count)) & mask;
                // Apply the value to the element of the list (without touching others values)
                wReg = FlagList[list_index + 1];
                wReg &= ~mask;
                wReg |= t_value;
                FlagList[list_index + 1] = (int)wReg;
            }
        }

        /// <summary>
        /// Get the mask for the specified rage
        /// </summary>
        public long GetMask(int startIndex, int count)
        {
            long t1 = (long)Math.Pow(2, count) - 1;
            long t2 = (long)Math.Pow(2, startIndex);
            return (t1 * t2);
        }

        #endregion GetSet

        /// <summary>
        /// Display the data in binary from startIndex to startIndex + maxCount
        /// </summary>
        public string DisplayBinary(int startIndex)
        {
            return DisplayBinary(startIndex, typeByteCount - startIndex);
        }

        /// <summary>
        /// Display the data in binary
        /// </summary>
        public string DisplayBinary(int startIndex, int count)
        {
            return Convert.ToString(GetBits(startIndex, count), 2);
        }

        /// <summary>
        /// Display the data in binary from startIndex to startIndex + maxCount
        /// </summary>
        public string DisplayHex(int startIndex)
        {
            return DisplayHex(startIndex, typeByteCount - startIndex);
        }

        /// <summary>
        /// Display the data in binary
        /// </summary>
        public string DisplayHex(int startIndex, int count)
        {
            return Convert.ToString(GetBits(startIndex, count), 16);
        }
    }
}
