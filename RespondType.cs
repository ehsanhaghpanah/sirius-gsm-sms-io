/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

namespace sirius.GSM.IO
{
	/// <summary>
	/// 
	/// </summary>
	public enum RespondType
	{
		/// <summary>
		/// 
		/// </summary>
		OK,
		/// <summary>
		/// Error, (GSM AT Command Set, ERROR)
		/// </summary>
		CommandError,
		/// <summary>
		/// Mobile Equipment or Network Error, (GSM AT Command Set, +CME ERROR)
		/// Mobile Equipment or Network Error, (GSM AT Command Set, +CMS ERROR)
		/// </summary>
		NetworkError,
		/// <summary>
		/// 
		/// </summary>
		Prompt,
		/// <summary>
		///
		/// </summary>
		NorSupported
	}
}
