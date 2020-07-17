/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;

namespace sirius.GSM.IO
{
	/// <summary>
	///
	/// </summary>
	public abstract class Command : IDisposable
	{
		private const int _respond_waiting_thread_sleep_delay_ = 1000;

		protected Command(Adaptor adaptor)
		{
			_adaptor = adaptor;
		}

		#region Interface

		void IDisposable.Dispose()
		{
		}

		#endregion

		#region Propertys

		private readonly Adaptor _adaptor;
		private int _RespondTimeout = 50000000;
		private int _AttemptsToExecute = 3;

		/// <summary>
		///
		/// </summary>
		protected Adaptor Adaptor
		{
			get { return (_adaptor); }
		}

		/// <summary>
		/// Number of attempts to execute the Command
		/// </summary>
		public int AttemptsToExecute
		{
			get { return (_AttemptsToExecute); }
			set { _AttemptsToExecute = value; }
		}

		/// <summary>
		/// Adaptor response timeout on each attepmt in second
		/// </summary>
		public int RespondWaitingTimeout
		{
			get { return (_RespondTimeout / 10000000); }
			set { _RespondTimeout = value * 10000000; }
		}

		#endregion

		#region Functions

		/// <summary>
		///
		/// </summary>
		public virtual bool Execute()
		{
			return (true);
		}

		/// <summary>
		///
		/// </summary>
		public virtual bool IsSupported()
		{
			return (true);
		}

		/// <summary>
		/// It may throw TimeoutException. It also may throw CarrierException. 
		/// It also may throw CommandException
		/// </summary>
		protected Respond WaitForRespond()
		{
			string text = string.Empty;

			TimeSpan d = new TimeSpan(_RespondTimeout);
			DateTime t = DateTime.Now;

			while (DateTime.Now.Subtract(t).CompareTo(d) <= 0)
			{
				if (!_adaptor.IsDataReady)
					System.Threading.Thread.Sleep(_respond_waiting_thread_sleep_delay_);

				text += _adaptor.Get();
				Respond respond = Respond.Resolve(text);
				if (respond != null)
					return (respond);
			}
			throw new TimeoutException();
		}

		/// <summary>
		/// It may throw TimeoutException. It also may throw CarrierException. 
		/// It also may throw CommandException
		/// </summary>
		protected void WaitForRespond(RespondType respondType)
		{
			string text = string.Empty;

			TimeSpan d = new TimeSpan(_RespondTimeout);
			DateTime t = DateTime.Now;

			while (DateTime.Now.Subtract(t).CompareTo(d) <= 0)
			{
				if (!_adaptor.IsDataReady)
					System.Threading.Thread.Sleep(_respond_waiting_thread_sleep_delay_);

				text += _adaptor.Get();
				Respond respond = Respond.Resolve(text);
				if (respond != null)
					if (respond.Type == respondType)
						return;
					else
						throw new CommandException();
			}
			throw new TimeoutException();
		}

		#endregion
	}
}
