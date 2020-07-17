/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS.SMS
{
	/// <summary>
	/// AT+CMGD,
	/// Delete Message Command
	/// </summary>
	public sealed class DeleteMessageCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public DeleteMessageCommand(Adaptor adaptor, int index)
			: base(adaptor)
		{
			_Index = index;
		}

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
					Adaptor.Put("AT+CMGD=" + _Index, "\r");
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

		#region Propertys

		private readonly int _Index;

		/// <summary>
		///
		/// </summary>
		public int Index
		{
			get { return (_Index); }
		}

		#endregion
	}
}
