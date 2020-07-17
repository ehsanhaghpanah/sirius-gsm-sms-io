/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO
{
	/// <summary>
	/// It is thrown if carrier exception (No Carrier) is occured
	/// </summary>
	public sealed class CarrierException : Exception
	{
		public CarrierException()
		{
		}

		public CarrierException(string message)
			: base(message)
		{
		}

		public CarrierException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
