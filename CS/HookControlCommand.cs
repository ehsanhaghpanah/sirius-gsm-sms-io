/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS
{
	/// <summary>
	/// ATH
	/// </summary>
	public sealed class HookControlCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public HookControlCommand(Adaptor adaptor)
			: base(adaptor)
		{
		}

		#region Propertys

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
					Adaptor.Put("ATH", "\r");
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