using System.Collections.Generic;
using UnityEngine;

namespace TD
{
	public class Detector : MonoBehaviour
	{
		public List<Enemy> m_enemies = new();

		public Enemy Target { get; private set; }

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.TryGetComponent(out Enemy enemy))
				m_enemies.Add(enemy);

			Target = m_enemies[0];
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.TryGetComponent(out Enemy enemy))
			{
				if (enemy == Target)
					Target = null;

				m_enemies.Remove(enemy);
			}

			if (m_enemies.Count > 0)
				Target = m_enemies[0];
		}
	}
}
