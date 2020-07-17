/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;
using System.IO.Ports;

namespace sirius.GSM.IO
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class Adaptor : IDisposable
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		private const int _attach_thread_sleep_delay_ = 500;
		private const int _detach_thread_sleep_delay_ = 500;
		private const int _get_thread_sleep_delay_ = 250;
		private const int _put_thread_sleep_delay_ = 250;

		/// <summary>
		///
		/// </summary>
		public Adaptor(string portName, int baudRate)
		{
			_Port = new SerialPort();
			{
				_Port.PortName = portName;
				_Port.BaudRate = baudRate;
				_Port.Parity = Parity.None;
				_Port.Handshake = Handshake.RequestToSend;
			}
		}

		~Adaptor()
		{
			//
			// it is better to call modem's API to clean up the memory
			//
		}

		#region Interface

		private bool _disposed;

		/// <summary>
		///
		/// </summary>
		void IDisposable.Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///
		/// </summary>
		private void Dispose(bool disposing)
		{
			if (!_disposed)
				if (disposing)
					_Port.Dispose();

			_disposed = true;
		}

		#endregion

		#region Propertys

		/// <summary>
		/// Is Port Ready
		/// </summary>
		public bool IsPortReady
		{
			get { return (_Port.IsOpen); }
		}

		/// <summary>
		/// Is Data Ready
		/// </summary>
		public bool IsDataReady
		{
			get { return (_Port.BytesToRead > 0); }
		}

		#endregion

		#region Functions

		private readonly SerialPort _Port;

		/// <summary>
		/// Attaches the adaptor to the network
		/// </summary>
		public void Attach()
		{
			try
			{
				if (!_Port.IsOpen)
				{
					_Port.Open();
					System.Threading.Thread.Sleep(_attach_thread_sleep_delay_);

					_Port.DiscardInBuffer();
					_Port.DiscardOutBuffer();
				}
			}
			catch (Exception p)
			{
				logger.Error(p);
				throw new AdaptorException("Adaptor Attach Exception.", p);
			}
		}

		/// <summary>
		/// Detaches the adaptor of the network
		/// </summary>
		public void Detach()
		{
			try
			{
				if (_Port.IsOpen)
				{
					System.Threading.Thread.Sleep(_detach_thread_sleep_delay_);
					_Port.Close();
				}
			}
			catch (Exception p)
			{
				logger.Error(p);
				throw new AdaptorException("Adaptor Detach Exception.", p);
			}
		}

		/// <summary>
		///
		/// </summary>
		public string Get()
		{
			try
			{
				System.Threading.Thread.Sleep(_get_thread_sleep_delay_);
				return (_Port.ReadExisting());
			}
			catch (Exception p)
			{
				logger.Error(p);
				throw new AdaptorException("Adaptor Get Exception.", p);
			}
		}

		/// <summary>
		///
		/// </summary>
		public void Put(string data)
		{
			try
			{
				System.Threading.Thread.Sleep(_put_thread_sleep_delay_);
				_Port.Write(data);
				System.Threading.Thread.Sleep(_put_thread_sleep_delay_);
			}
			catch (Exception p)
			{
				logger.Error("Exception; {0} and Args; {1}", p, data);
				throw new AdaptorException("Adaptor Put Exception.", p);
			}
		}

		/// <summary>
		///
		/// </summary>
		public void Put(string data, string terminator)
		{
			try
			{
				System.Threading.Thread.Sleep(_put_thread_sleep_delay_);
				_Port.Write(data);
				System.Threading.Thread.Sleep(_put_thread_sleep_delay_);
				_Port.Write(terminator);
				System.Threading.Thread.Sleep(_put_thread_sleep_delay_);
			}
			catch (Exception p)
			{
				logger.Error("Exception; {0} and Args; {1}, {2}", p, data, terminator);
				throw new AdaptorException("Adaptor Put Exception.", p);
			}
		}

		#endregion
	}
}
