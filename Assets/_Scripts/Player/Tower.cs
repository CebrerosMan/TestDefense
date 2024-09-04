using UnityEngine;

namespace TD
{
	public class Tower : MonoBehaviour
	{
		public TowerData m_Data;

		[SerializeField] private Detector m_detector;
		[SerializeField] public Transform m_mesh;
		[SerializeField] private Transform m_cannon;
		[SerializeField] private Animation m_anim;

		private float m_fireInterval;
		private float m_fireTimer;
		private bool m_initialized;
		private SphereCollider m_detectorCollider;

		public Transform Transform { get; private set; }

		public void Initialize(TowerData data)
		{
			m_Data = data;
			m_fireInterval = 1f / m_Data.m_FireRate;
			m_detectorCollider.radius = m_Data.m_Range;
			m_detectorCollider.enabled = true;

			m_initialized = true;
		}

		private void Awake()
		{
			Transform = transform;
			m_detectorCollider = m_detector.GetComponent<SphereCollider>();
			m_detectorCollider.enabled = false;
		}

		private void Update()
		{
			if (!m_initialized || !m_detector.Target)
				return;

			m_fireTimer += Time.deltaTime;

			Vector3 look = m_detector.Target.Transform.position - Transform.position;
			look.y = 0;

			m_mesh.localRotation = Quaternion.LookRotation(look);

			if(m_fireTimer > m_fireInterval)
			{
				m_fireTimer = 0;
				Fire();
			}
		}

		private void Fire()
		{
			m_anim.Stop();
			m_anim.Play();
			m_detector.Target.TakeDamage(m_Data.m_Damage);
		}
	}
}

