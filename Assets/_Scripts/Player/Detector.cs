using System.Collections.Generic;
using UnityEngine;

namespace TD
{
	public class Detector : MonoBehaviour
	{
		public List<Enemy> m_enemies = new();

		public Enemy Target { get; private set; }
		public SphereCollider Collider { get; private set; }

		private void Awake()
		{
			Collider = GetComponent<SphereCollider>();
			Collider.enabled = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.TryGetComponent(out Enemy enemy))
				RegisterEnemy(enemy);
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.TryGetComponent(out Enemy enemy))
				UnregisterEnemy(enemy);
		}

		private void RegisterEnemy(Enemy enemy)
		{
			m_enemies.Add(enemy);
			enemy.EnemyKilledEvent += OnEnemyKilled;

			Target = m_enemies[0];
		}

		private void UnregisterEnemy(Enemy enemy)
		{
			m_enemies.Remove(enemy);
			enemy.EnemyKilledEvent -= OnEnemyKilled;

			if (enemy == Target)
				Target = null;

			if (m_enemies.Count > 0)
				Target = m_enemies[0];
		}

		private void OnEnemyKilled(Enemy enemy)
		{
			UnregisterEnemy(enemy);
		}
	}
}
