﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace AlgorithmLibrary.MaurerPrimes
{
	public class CryptoRNG : IDisposable
	{
		private bool IsDisposed = false;
		private byte[] rngBytes8 = new byte[8];
		private byte[] rngBytes4 = new byte[4];
		private RNGCryptoServiceProvider rngCsp;

		public CryptoRNG()
		{
			disposeCheck();
			rngCsp = new RNGCryptoServiceProvider();
		}

		public void Dispose()
		{
			if (!IsDisposed)
			{
				IsDisposed = true;
				if (rngCsp != null)
				{
					rngCsp.Dispose();
					rngCsp = null;
				}

				if (rngBytes8 != null)
				{
					rngBytes8 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
					rngBytes8 = null;
				}

				if (rngBytes4 != null)
				{
					rngBytes4 = new byte[] { 0, 0, 0, 0 };
					rngBytes4 = null;
				}
			}
		}

		private void disposeCheck()
		{
			if (IsDisposed)
			{
				throw new ObjectDisposedException("CryptoRNG");
			}
		}

		public void NextBytes(byte[] buffer)
		{
			disposeCheck();
			rngCsp.GetBytes(buffer);
		}

		public int Next(int maxValue)
		{
			return Math.Abs(Next() % maxValue);
		}

		public int Next()
		{
			disposeCheck();
			rngCsp.GetBytes(rngBytes4);
			return Math.Abs(BitConverter.ToInt32(rngBytes4, 0));
		}

		public double NextDouble()
		{
			disposeCheck();
			rngCsp.GetBytes(rngBytes8);
			return Math.Abs(BitConverter.ToInt64(rngBytes8, 0) * (1.0 / long.MaxValue));
		}
	}
}
