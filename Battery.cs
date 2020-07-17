/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

namespace sirius.GSM.IO
{
	/// <summary>
	/// Battery Charging Status
	/// </summary>
	public enum BatteryChargingStatus
	{
		Charging = 0,
		NotCharging = 1,
		Unknown = 2
	}

	/// <summary>
	/// Battery
	/// </summary>
	public sealed class Battery
	{
		private readonly int _ChargeLevel;
		private readonly BatteryChargingStatus _ChargingStatus;

		internal Battery(int chargeLevel, BatteryChargingStatus chargingStatus)
		{
			_ChargeLevel = chargeLevel;
			_ChargingStatus = chargingStatus;
		}

		/// <summary>
		/// Charge Level
		/// </summary>
		public int ChargeLevel
		{
			get { return (_ChargeLevel); }
		}

		/// <summary>
		/// Battery Charging Status
		/// </summary>
		public BatteryChargingStatus ChargingStatus
		{
			get { return (_ChargingStatus); }
		}
	}
}
