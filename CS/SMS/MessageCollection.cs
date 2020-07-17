/**
 * Copyright (C) Ehsan Haghpanah, 2010.
 * All rights reserved.
 * Ehsan Haghpanah, (github.com/ehsanhaghpanah)
 */

using System;
using System.Collections;

namespace sirius.GSM.IO.CS.SMS
{
	/// <summary>
	/// Message Collection
	/// </summary>
	public sealed class MessageCollection : IEnumerable
	{
		private readonly ArrayList _Array;

		public MessageCollection()
		{
			_Array = new ArrayList();
		}

		#region Interface

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (_Array.GetEnumerator());
		}

		#endregion

		#region Functions

		public void Add(ShortMessage item)
		{
			_Array.Add(item);
		}

		public void Remove(ShortMessage item)
		{
			_Array.Remove(item);
		}

		public int Count
		{
			get { return (_Array.Count); }
		}

		public ShortMessage this[int index]
		{
			get
			{
				if ((0 <= index) && (index <= (_Array.Count - 1)))
					return ((ShortMessage)_Array[index]);
				
				throw new ArgumentOutOfRangeException();
			}
		}

		public ShortMessage FirstItem()
		{
			return ((ShortMessage)_Array[0]);
		}

		public ShortMessage PreviousItem(ShortMessage item)
		{
			for (int index = 0; index <= (_Array.Count - 1); index++)
				if (_Array[index].Equals(item))
				{
					if (index == 0)
						return ((ShortMessage)_Array[index]);
					return ((ShortMessage)_Array[index - 1]);
				}

			throw new ArgumentException();
		}

		public ShortMessage NextItem(ShortMessage item)
		{
			for (int index = 0; index <= (_Array.Count - 1); index++)
				if (_Array[index].Equals(item))
				{
					if (index == _Array.Count - 1)
						return ((ShortMessage)_Array[index]);
					return ((ShortMessage)_Array[index + 1]);
				}

			throw new ArgumentException();
		}

		public ShortMessage LastItem()
		{
			return ((ShortMessage)_Array[_Array.Count - 1]);
		}

		#endregion
	}
}