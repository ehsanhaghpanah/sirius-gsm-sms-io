/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS
{
	/// <summary>
	/// Response Format Type
	/// </summary>
	public enum ResponseFormatType
	{
		/// <summary>
		///
		/// </summary>
		Numeric,
		/// <summary>
		/// default
		/// </summary>
		Verbose
	}

	/// <summary>
	/// ATV[x], 
	/// Response Format Command
	/// </summary>
	public sealed class ResponseFormatCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private ResponseFormatType _type = ResponseFormatType.Verbose;

		public ResponseFormatCommand(Adaptor adaptor)
			: base(adaptor)
		{
		}

		#region Propertys

		/// <summary>
		///
		/// </summary>
		public ResponseFormatType Type
		{
			get { return (_type); }
			set { _type = value; }
		}

		#endregion

		#region Functions

		/// <summary>
		///
		/// </summary>
		public override bool Execute()
		{
			for (int attempt = 1; attempt <= AttemptsToExecute; attempt++)
			{
				try
				{
					Adaptor.Put(_type == ResponseFormatType.Numeric ? "ATV0" : "ATV1", "\r");

					WaitForRespond(RespondType.OK);
					return (true);
				}
				catch (Exception p)
				{
					logger.Error(p);
				}
			}
			return (false);
		}

		#endregion
	}
}