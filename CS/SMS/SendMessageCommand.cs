/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS.SMS
{
	/// <summary>
	/// AT+CMGS, 
	/// Send Message Command
	/// </summary>
	public sealed class SendMessageCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		
		private readonly ShortMessage _Message;
		private int _MessageReference;

		public SendMessageCommand(Adaptor adaptor, ShortMessage message)
			: base(adaptor)
		{
			_Message = message;
		}

		#region Propertys

		/// <summary>
		///
		/// </summary>
		public ShortMessage Message
		{
			get { return (_Message); }
		}

		/// <summary>
		///
		/// </summary>
		public int MessageReference
		{
			get { return (_MessageReference); }
		}

		#endregion

		#region Functions

		private bool Process(Respond respond)
		{
			if (respond.Type == RespondType.OK)
			{
				try
				{
					if (respond.Args.Count == 0)
						return (false);

					string arg = respond.Args[0].ToString();
					if (arg.ToUpper().StartsWith("+CMGS: "))
					{
						_MessageReference = int.Parse(arg.Substring(7));
						return (true);
					}
					return (false);
				}
				catch
				{
					return (false);
				}
			}
			return (false);
		}

		/// <summary>
		///
		/// </summary>
		public override bool Execute()
		{
			RespondWaitingTimeout = 5 * RespondWaitingTimeout;

			for (int attempt = 1; attempt <= AttemptsToExecute; attempt++)
			{
				try
				{
					if (_Message.Length == 0)
					{
						Adaptor.Put("AT+CMGS=\"" + _Message.Address + "\"", "\r");
						WaitForRespond(RespondType.Prompt);
						Adaptor.Put(_Message.Content + ((char)26).ToString());
					}
					else
					{
						Adaptor.Put("AT+CMGS=" + _Message.Length.ToString(), "\r");
						WaitForRespond(RespondType.Prompt);
						Adaptor.Put(_Message.Content + ((char)26).ToString());
					}

					Respond respond = WaitForRespond();
					if (respond != null)
						if (Process(respond))
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