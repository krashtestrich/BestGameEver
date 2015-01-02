using System;
using System.Security.Cryptography;

namespace GameLogic.Helpers
{
    public class SecureRandom : RandomNumberGenerator
    {
        private static readonly RandomNumberGenerator Rng = new RNGCryptoServiceProvider();


        public int Next()
        {
            var data = new byte[sizeof(int)];
            Rng.GetBytes(data);
            return BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
        }

        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            return (int)Math.Floor((minValue + ((double)maxValue - minValue) * NextDouble()));
        }

        public static double NextDouble()
        {
            var data = new byte[sizeof(uint)];
            Rng.GetBytes(data);
            var randUint = BitConverter.ToUInt32(data, 0);
            return randUint / (uint.MaxValue + 1.0);
        }

        public override void GetBytes(byte[] data)
        {
            Rng.GetBytes(data);
        }

        public override void GetNonZeroBytes(byte[] data)
        {
            Rng.GetNonZeroBytes(data);
        }
    }
}
