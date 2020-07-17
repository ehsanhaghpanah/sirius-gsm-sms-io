/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class NetworkException : Exception
	{
		public NetworkException()
		{
		}

		public NetworkException(string message)
			: base(message)
		{
		}

		public NetworkException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}