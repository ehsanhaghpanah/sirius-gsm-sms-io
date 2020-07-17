/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS
{
	/// <summary>
	/// AT+CSQ
	/// Received Signal Strength Indicator (RSSI)
	/// </summary>
	public sealed class SignalQualityCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public SignalQualityCommand(Adaptor adaptor)
			: base(adaptor)
		{
		}

		#region Propertys

		/// <summary>
		///
		/// </summary>
		public int CurrentQuality
		{
			get { return (0); }
		}

		/// <summary>
		///
		/// </summary>
		public int MinimumStrength
		{
			get { return (0); }
		}

		/// <summary>
		///
		/// </summary>
		public int MaximumStrength
		{
			get { return (0); }
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
					Adaptor.Put("AT+CSQ", "\r");
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
