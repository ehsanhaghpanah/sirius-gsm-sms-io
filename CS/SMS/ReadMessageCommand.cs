/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS.SMS
{
	/// <summary>
	/// AT+CMGR, 
	/// Read Message Command
	/// </summary>
	public sealed class ReadMessageCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		private readonly int _Index;
		private ShortMessage _Message;
		private readonly MessageFormatType _MessageFormatType = MessageFormatType.Text;

		public ReadMessageCommand(Adaptor adaptor, int index)
			: base(adaptor)
		{
			_Index = index;
		}

		public ReadMessageCommand(Adaptor adaptor, int index, MessageFormatType messageFormatType)
			: base(adaptor)
		{
			_Index = index;
			_MessageFormatType = messageFormatType;
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
		public int Index
		{
			get { return (_Index); }
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

					if (_MessageFormatType == MessageFormatType.PDU)
					{
						string arg = respond.Args[0].ToString();
						if (arg.ToUpper().StartsWith("+CMGR: "))
						{
							_Message = new ShortMessage(0, respond.Args[1].ToString());
							return (true);
						}
						return (false);
					}
					else
					{
						string arg = respond.Args[0].ToString();
						if (arg.ToUpper().StartsWith("+CMGR: "))
						{
							arg = arg.Substring(6);
							string[] items = arg.Split(new[] { ',' });
							string address = items[1].Substring(1, items[1].Length - 2);
							_Message = new ShortMessage(address, respond.Args[1].ToString());

							return (true);
						}
						return (false);
					}
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
			RespondWaitingTimeout = 2 * RespondWaitingTimeout;

			for (int attempt = 1; attempt <= AttemptsToExecute; attempt++)
			{
				try
				{
					Adaptor.Put("AT+CMGR=" + _Index.ToString(), "\r");
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
