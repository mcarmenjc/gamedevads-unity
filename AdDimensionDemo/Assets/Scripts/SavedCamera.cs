using System;
using UnityEngine;

namespace AdDimension.Helpers
{
	public class SavedCamera
	{
		private static Vector3 _position;
		public static Vector3 Position {
			get { return _position; }
			set { _position = value; }
		}

		private static Vector3 _direction;
		public static Vector3 Direction {
			get { return _direction; }
			set { _direction = value; }
		}
	}
}

