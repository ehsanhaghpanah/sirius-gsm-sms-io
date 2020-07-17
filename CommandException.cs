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
	public sealed class CommandException : Exception
	{
		public CommandException()
		{
		}

		public CommandException(string message)
			: base(message)
		{
		}

		public CommandException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
