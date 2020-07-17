/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS
{
	/// <summary>
	/// Command Echo Type
	/// </summary>
	public enum CommandEchoType
	{
		/// <summary>
		///
		/// </summary>
		NoEcho,
		/// <summary>
		/// default
		/// </summary>
		Echo
	}

	/// <summary>
	/// ATE[x]
	/// </summary>
	public sealed class CommandEchoCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private CommandEchoType _type = CommandEchoType.NoEcho;

		public CommandEchoCommand(Adaptor adaptor)
			: base(adaptor)
		{
		}

		#region Propertys

		/// <summary>
		///
		/// </summary>
		public CommandEchoType Type
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
					Adaptor.Put(_type == CommandEchoType.NoEcho ? "ATE0" : "ATE1", "\r");

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