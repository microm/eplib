using System;
using System.Collections.Generic;

namespace Tool.TSystem.Assist
{
	public class ReverseComparer<T> : IComparer<T> where T : IComparable
	{
		public int Compare(T lhs, T rhs)
		{
			return -lhs.CompareTo(rhs);
		}
	}
}