using System;
using UnityEngine;

namespace TD
{
	public class Enemy : MonoBehaviour
	{
		public static event Action<Enemy> EnemyInitializedEvent;

		public event Action<Enemy> EnemyKilledEvent;
		public event Action<Enemy> EnemyAttackedEvent;

		private Transform m_transform;
		private int m_health;

		public Transform Transform => m_transform;
		public EnemyData Data { get; private set; }

		public void Initialize(EnemyData data)
		{
			Data = data;
			m_transform = transform;
			m_health = data.m_Health;

			EnemyInitializedEvent?.Invoke(this);
		}

		public void TakeDamage(int damage)
		{
			m_health -= damage;

			if (m_health <= 0)
				EnemyKilledEvent?.Invoke(this);
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.TryGetComponent(out Castle castle))
				EnemyAttackedEvent?.Invoke(this);
		}
	}
}
