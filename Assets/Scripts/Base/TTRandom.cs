using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;

namespace TT
{
	public class TTRandom
	{
		private static long seed = 0;
		private static long id = 0;

		public static float Range (float v1, float v2, int i) {
			long _seed = id + i*31;

			long v = (_seed * 1103515245U + 12345U) & 0x7fffffffU;

			return (((float)v/0x7fffffffU) * (v2-v1) + v1);
		}

		public static int Range (int v1, int v2, int i) {
			return (int) Range ((float)v1, (float)v2, i);
		}

		public static float Range (float v1, float v2) {
			seed = (seed * 1103515245U + 12345U) & 0x7fffffffU;

			return (((float)seed/0x7fffffffU) * (v2-v1) + v1);
		}

		public static int Range (int v1, int v2) {
			return (int) Range ((float)v1, (float)v2);
		}

		public static long Range (long v1, long v2) {
			return (long) Range ((float)v1, (float)v2);
		}

		public static long Seed {
			get { 
				return seed; 
			}
			set { 
				seed = value; 
			}
		}

		private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

		public static int BetterRandom(int minimumValue, int maximumValue)
		{
			byte[] randomNumber = new byte[1];
 
            _generator.GetBytes(randomNumber);
 
            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);
 
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);
 
            int range = maximumValue - minimumValue + 1;
 
            double randomValueInRange = Math.Floor(multiplier * range);
 
            return (int)(minimumValue + randomValueInRange);
		}
	}
}

