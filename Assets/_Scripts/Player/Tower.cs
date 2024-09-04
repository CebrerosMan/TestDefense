using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TD
{
	public class Tower : MonoBehaviour
	{
		public TowerData m_Data;

		[SerializeField] private Detector m_detector;
		[SerializeField] private Transform m_cannon;

		private float m_fireInterval;
		private float m_fireTimer;
		private Transform m_transform;

		private void Awake()
		{
			m_transform = transform;
			m_detector.GetComponent<SphereCollider>().radius = m_Data.m_Range;
		}

		private void Start()
		{
			m_fireInterval = 1f / m_Data.m_FireRate;
		}

		private void Update()
		{
			if (!m_detector.Target)
			{
				m_fireTimer = 0;
				return;
			}

			m_fireTimer += Time.deltaTime;

			Vector3 look = m_detector.Target.Transform.position - m_transform.position;
			look.y = 0;

			m_cannon.localRotation = Quaternion.LookRotation(look);

			if(m_fireTimer > m_fireInterval)
			{
				m_fireTimer = 0;
				Fire();
			}
		}

		private void Fire()
		{
			m_detector.Target.TakeDamage(m_Data.m_Damage);
		}
	}
}

