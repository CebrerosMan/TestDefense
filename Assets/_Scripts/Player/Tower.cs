using UnityEngine;

namespace TD
{
	public class Tower : MonoBehaviour
	{
		[SerializeField] public Detector m_detector;
		[SerializeField] private Transform m_mesh;
		[SerializeField] private Transform m_cannon;
		[SerializeField] private Animation m_anim;

		public TowerData Data { get; set; }
		public Transform Transform { get; private set; }
		public Detector Detector => m_detector;
		public Transform Mesh => m_mesh;
		public Transform Cannon => m_cannon;
		public Animation Animation => m_anim;

		public void Fire()
		{
			m_anim.Stop();
			m_anim.Play();
			m_detector.Target.TakeDamage(Data.m_Damage);
		}

		private void Awake()
		{
			Transform = transform;
		}
	}
}

