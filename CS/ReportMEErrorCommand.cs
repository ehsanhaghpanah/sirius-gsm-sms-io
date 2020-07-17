/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS
{
	/// <summary>
	/// AT+CMEE
	/// Report ME Error Type
	/// </summary>
	public enum ReportMEErrorType
	{
		/// <summary>
		/// AT+CMEE=1
		/// </summary>
		ReportOK,
		/// <summary>
		/// AT+CMEE=0, default
		/// </summary>
		ReportNo
	}

	/// <summary>
	/// AT+CMEE
	/// </summary>
	public sealed class ReportMEErrorCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private ReportMEErrorType _type = ReportMEErrorType.ReportOK;

		public ReportMEErrorCommand(Adaptor adaptor)
			: base(adaptor)
		{
		}

		#region Propertys

		/// <summary>
		///
		/// </summary>
		public ReportMEErrorType ReportType
		{
			get
			{
				for (int attempt = 1; attempt <= AttemptsToExecute; attempt++)
				{
					try
					{
						Adaptor.Put("AT+CMEE?", "\r");
						Respond respond = WaitForRespond();
						if (respond.Type == RespondType.OK)
						{
							if (respond.Text.ToUpper().StartsWith("\r\n+CMEE: 0"))
								return (ReportMEErrorType.ReportNo);

							if (respond.Text.ToUpper().StartsWith("\r\n+CMEE: 1"))
								return (ReportMEErrorType.ReportOK);

							throw new CommandException();
						}
						throw new CommandException();
					}
					catch (Exception p)
					{
						logger.Error(p);
					}
				}
				throw new CommandException("AT+CMEE? failed after attempts.");
			}
			set
			{
				for (int attempt = 1; attempt <= AttemptsToExecute; attempt++)
				{
					try
					{
						Adaptor.Put(value == ReportMEErrorType.ReportNo ? "AT+CMEE=0" : "AT+CMEE=1", "\r");

						WaitForRespond(RespondType.OK);
						_type = value;
						return;
					}
					catch (Exception p)
					{
						logger.Error(p);
					}
				}
				throw new CommandException("AT+CMEE=[?] failed after attempts.");
			}
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
					Adaptor.Put(_type == ReportMEErrorType.ReportNo ? "AT+CMEE=0" : "AT+CMEE=1", "\r");

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
