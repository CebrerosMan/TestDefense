using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD
{
	public class Spawner : MonoBehaviour
	{
		private Path m_path;

		public Path Path
		{
			set => m_path = value;
		}
	}
}
