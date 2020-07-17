/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS.SMS
{
	/// <summary>
	/// Message State Type
	/// </summary>
	public enum MessageStateType
	{
		/// <summary>
		/// (+CMGF=1), REC UNREAD
		/// (+CMGF=0), 0
		/// </summary>
		RecordUnRead = 0,
		/// <summary>
		/// (+CMGF=1), REC READ
		/// (+CMGF=0), 1
		/// </summary>
		RecordRead = 1,
		/// <summary>
		/// (+CMGF=1), STO UNSENT
		/// (+CMGF=0), 2
		/// </summary>
		StoredUnSent = 2,
		/// <summary>
		/// (+CMGF=1), STO SENT
		/// (+CMGF=0), 3
		/// </summary>
		StoredSent = 3,
		/// <summary>
		/// (+CMGF=1), ALL
		/// (+CMGF=0), 4
		/// </summary>
		All = 4
	}

	/// <summary>
	/// AT+CMGL,
	/// List Message Command
	/// </summary>
	public sealed class ListMessageCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		
		public ListMessageCommand(Adaptor adaptor, MessageStateType messageStateType)
			: base(adaptor)
		{
			_MessageStateType = messageStateType;
			_MessageCollection = new MessageCollection();
		}

		public ListMessageCommand(Adaptor adaptor, MessageStateType messageStateType, MessageFormatType messageFormatType)
			: base(adaptor)
		{
			_MessageStateType = messageStateType;
			_MessageFormatType = messageFormatType;
			_MessageCollection = new MessageCollection();
		}

		#region Propertys

		private MessageStateType _MessageStateType;
		private readonly MessageFormatType _MessageFormatType = MessageFormatType.Text;
		private readonly MessageCollection _MessageCollection;

		/// <summary>
		///
		/// </summary>
		public MessageStateType MessageStateType
		{
			get { return (_MessageStateType); }
			set { _MessageStateType = value; }
		}

		/// <summary>
		///
		/// </summary>
		public MessageCollection MessageCollection
		{
			get { return (_MessageCollection); }
		}

		#endregion

		#region Functions

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
					if (_MessageFormatType == MessageFormatType.PDU)
						Adaptor.Put("AT+CMGL=" + ((int) _MessageStateType), "\r");
					else
						Adaptor.Put("AT+CMGL=\"" + MessageStateToText(_MessageStateType) + "\"", "\r");
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

		/// <summary>
		///
		/// </summary>
		private static string MessageStateToText(MessageStateType messageStateType)
		{
			switch (messageStateType)
			{
				case MessageStateType.RecordUnRead:
					{
						return ("REC UNREAD");
					}
				case MessageStateType.RecordRead:
					{
						return ("REC READ");
					}
				case MessageStateType.StoredUnSent:
					{
						return ("STO UNSENT");
					}
				case MessageStateType.StoredSent:
					{
						return ("STO SENT");
					}
				default:
					{
						return ("ALL");
					}
			}
		}

		private bool Process(Respond respond)
		{
			if (respond.Type == RespondType.OK)
			{
				try
				{
					if (respond.Args.Count == 0)
						return (false);

					if (respond.Args.Count == 1)
						return (true);

					if (((respond.Args.Count - 1) % 2) != 0)
						return (false);

					for (int i = 0; i < (respond.Args.Count - 1); i += 2)
					{
						ShortMessage item = ShortMessage.Resolve(respond.Args[i].ToString(), respond.Args[i + 1].ToString(), _MessageFormatType);
						_MessageCollection.Add(item);
					}

					return (true);
				}
				catch
				{
					return (false);
				}
			}
			return (false);
		}
	}
}
