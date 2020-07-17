/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS.SMS
{
	/// <summary>
	/// AT+CSCA, 
	/// Service Centre Address Command
	/// </summary>
	public sealed class ServiceCentreAddress : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		private string _Address;

		public ServiceCentreAddress(Adaptor adaptor)
			: base(adaptor)
		{
		}

		#region Propertys

		public string Address
		{
			get { return (_Address); }
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
					if (arg.ToUpper().StartsWith("+CSCA: "))
					{
						arg = arg.Substring(6).Trim();
						string[] items = arg.Split(new[] { ',' });
						_Address = items[0].Trim(new[] { '\"' });

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
			RespondWaitingTimeout = 2 * RespondWaitingTimeout;

			for (int attempt = 1; attempt <= AttemptsToExecute; attempt++)
			{
				try
				{
					Adaptor.Put("AT+CSCA?", "\r");
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
