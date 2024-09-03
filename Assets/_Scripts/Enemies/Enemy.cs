using System;
using UnityEngine;

namespace TD
{
	public class Enemy : MonoBehaviour
	{
		public static event Action<Enemy> EnemyInitializedEvent;

		public event Action<Enemy> EnemyKilledEvent;

		private Transform m_transform;
		private int m_health;
		private int m_maxHealth;
		private int m_damage;

		public Transform Transform => m_transform;
		public float Speed { get; private set; }

		public void Initialize(EnemyData data)
		{
			m_transform = transform;
			m_health = data.m_Health;
			m_maxHealth = data.m_Health;
			m_damage = data.m_Damage;
			Speed = data.m_Speed;

			EnemyInitializedEvent?.Invoke(this);
		}

		public void TakeDamage(int damage)
		{
			m_health -= damage;

			if (m_health < 0)
				EnemyKilledEvent?.Invoke(this);
		}
	}
}
