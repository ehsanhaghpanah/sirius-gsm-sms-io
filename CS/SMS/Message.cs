/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO.CS.SMS
{
	/// <summary>
	/// Short Message
	/// </summary>
	public sealed class ShortMessage
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public ShortMessage(int index, string address, string content)
		{
			_Index = index;
			_Address = address;
			_Content = content;
		}

		public ShortMessage(string address, string content)
		{
			_Address = address;
			_Content = content;
		}

		public ShortMessage(int length, string pdu)
		{
			_Length = length;
			_Content = pdu;
		}

		public ShortMessage(int index, int length, string pdu)
		{
			_Index = index;
			_Length = length;
			_Content = pdu;
		}

		#region Propertys

		private readonly int _Index = -1;
		private readonly int _Length;
		private readonly string _Address;
		private readonly string _Content;

		/// <summary>
		///
		/// </summary>
		public int Index
		{
			get { return (_Index); }
		}

		/// <summary>
		///
		/// </summary>
		public int Length
		{
			get { return (_Length); }
		}

		/// <summary>
		///
		/// </summary>
		public string Address
		{
			get { return (_Address); }
		}

		/// <summary>
		///
		/// </summary>
		public string Content
		{
			get { return (_Content); }
		}

		#endregion

		#region Functions

		public static ShortMessage Resolve(string header, string footer, MessageFormatType formatType)
		{
			try
			{
				if (!header.ToUpper().StartsWith("+CMGL:"))
					throw new ArgumentException();

				header = header.Substring(6);
				string[] items = header.Split(new[] { ',' });
				if (items.Length == 0)
					throw new ArgumentException();

				if (formatType == MessageFormatType.Text)
					return (new ShortMessage(int.Parse(items[0].Trim()), items[2].Replace("\"", ""), footer));
				
				return (new ShortMessage(int.Parse(items[0].Trim()), int.Parse(items[3].Trim()), footer));
			}
			catch (Exception p)
			{
				logger.Error("Exception; {0} and Args; {1}, {2}, {3}", p, header, footer, formatType);
				return (null);
			}
		}

		#endregion
	}
}
