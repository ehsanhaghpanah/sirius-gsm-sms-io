/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS.SMS
{
	/// <summary>
	/// Message Format Type
	/// </summary>
	public enum MessageFormatType
	{
		/// <summary>
		/// AT+CMGF=0
		/// </summary>
		PDU,
		/// <summary>
		/// AT+CMGF=1
		/// </summary>
		Text
	}

	/// <summary>
	/// AT+CMGF,
	/// Message Format Command
	/// </summary>
	public sealed class MessageFormatCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private MessageFormatType _type = MessageFormatType.Text;

		public MessageFormatCommand(Adaptor adaptor)
			: base(adaptor)
		{
		}

		#region Propertys

		/// <summary>
		///
		/// </summary>
		public MessageFormatType FormatType
		{
			get
			{
				for (int attempt = 1; attempt <= AttemptsToExecute; attempt++)
				{
					try
					{
						Adaptor.Put("AT+CMGF?", "\r");
						Respond respond = WaitForRespond();
						if (respond.Type == RespondType.OK)
						{
							if (respond.Text.ToUpper().StartsWith("\r\n+CMGF: 0"))
								return (MessageFormatType.PDU);

							if (respond.Text.ToUpper().StartsWith("\r\n+CMGF: 1"))
								return (MessageFormatType.Text);

							throw new CommandException();
						}
						throw new CommandException();
					}
					catch (Exception p)
					{
						logger.Error(p);
					}
				}
				throw new CommandException("AT+CMGF? failed after attempts.");
			}
			set
			{
				for (int attempt = 1; attempt <= AttemptsToExecute; attempt++)
				{
					try
					{
						Adaptor.Put(value == MessageFormatType.Text ? "AT+CMGF=1" : "AT+CMGF=0", "\r");

						WaitForRespond(RespondType.OK);
						_type = value;
						return;
					}
					catch (Exception p)
					{
						logger.Error(p);
					}
				}
				throw new CommandException("AT+CMGF=[?] failed after attempts.");
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
					Adaptor.Put(_type == MessageFormatType.Text ? "AT+CMGF=1" : "AT+CMGF=0", "\r");

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