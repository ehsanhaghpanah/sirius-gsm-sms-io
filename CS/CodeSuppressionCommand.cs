/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS
{
	/// <summary>
	/// Code Suppression Type
	/// </summary>
	public enum CodeSuppressionType
	{
		/// <summary>
		///
		/// </summary>
		TransmitCodes,
		/// <summary>
		///
		/// </summary>
		SuppressCodes
	}

	/// <summary>
	/// ATQ[x]
	/// Result Code Suppression
	/// </summary>
	public sealed class CodeSuppressionCommand : Command
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private CodeSuppressionType _type = CodeSuppressionType.TransmitCodes;

		public CodeSuppressionCommand(Adaptor adaptor)
			: base(adaptor)
		{
		}

		#region Propertys

		/// <summary>
		///
		/// </summary>
		public CodeSuppressionType Type
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
					Adaptor.Put(_type == CodeSuppressionType.TransmitCodes ? "ATQ0" : "ATQ1", "\r");

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