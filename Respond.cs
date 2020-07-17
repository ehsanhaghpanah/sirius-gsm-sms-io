/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;
using System.Collections;

namespace sirius.GSM.IO
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class Respond
	{
		public Respond(RespondType type)
		{
			_Type = type;
		}

		public Respond(RespondType type, string text)
		{
			_Type = type;
			_Text = text;
		}

		#region Propertys

		private readonly RespondType _Type;
		private readonly string _Text = string.Empty;

		/// <summary>
		///
		/// </summary>
		public RespondType Type
		{
			get { return (_Type); }
		}

		/// <summary>
		///
		/// </summary>
		public string Text
		{
			get { return (_Text); }
		}

		/// <summary>
		///
		/// </summary>
		public ArrayList Args
		{
			get
			{
				if (_Text == string.Empty)
					return (null);
				if (_Text.Length == 0)
					return (null);

				ArrayList al = new ArrayList();
				string[] items = _Text.Split(new[] { '\r', '\n' });
				foreach (string t in items)
					if (t.Length != 0)
						al.Add(t);

				return (al);
			}
		}

		#endregion

		#region Functions

		/// <summary>
		///
		/// </summary>
		public static Respond Resolve(string text)
		{
			{
				bool check = false;
				check = check || (text.ToUpper().IndexOf("\r\nOK", StringComparison.Ordinal) >= 0);
				check = check || (text.ToUpper().IndexOf("OK\r\n", StringComparison.Ordinal) >= 0);
				if (check)
					return (new Respond(RespondType.OK, text));
			}

			{
				bool check = false;
				check = check || (text.ToUpper().IndexOf("\r\nERROR", StringComparison.OrdinalIgnoreCase) >= 0);
				check = check || (text.ToUpper().IndexOf("ERROR\r\n", StringComparison.OrdinalIgnoreCase) >= 0);
				if (check)
					throw new CommandException();
			}

			{
				bool check = false;
				check = check || (text.ToUpper().IndexOf("+CME ERROR:", StringComparison.OrdinalIgnoreCase) >= 0);
				if (check)
					throw new NetworkException();
			}

			{
				bool check = false;
				check = check || (text.ToUpper().IndexOf("+CMS ERROR:", StringComparison.OrdinalIgnoreCase) >= 0);
				if (check)
					throw new AdaptorException();
			}

			{
				bool check = false;
				check = check || (text.ToUpper().IndexOf("\r\nNO CARRIER", StringComparison.OrdinalIgnoreCase) >= 0);
				check = check || (text.ToUpper().IndexOf("NO CARRIER\r\n", StringComparison.OrdinalIgnoreCase) >= 0);
				if (check)
					throw new CarrierException();
			}

			{
				bool check = false;
				check = check || (text.ToUpper().IndexOf("\r\n> ", StringComparison.OrdinalIgnoreCase) >= 0);
				if (check)
					return (new Respond(RespondType.Prompt));
			}

			return (null);
		}

		#endregion
	}
}
