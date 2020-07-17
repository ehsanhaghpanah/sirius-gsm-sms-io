/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO
{
	/// <summary>
	/// It is thrown if an exception is occured in adaptor
	/// </summary>
	public sealed class AdaptorException : Exception
	{
		public AdaptorException()
		{
		}

		public AdaptorException(string message)
			: base(message)
		{
		}

		public AdaptorException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}