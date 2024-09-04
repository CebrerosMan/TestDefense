using UnityEngine;

namespace TD
{
	[CreateAssetMenu(fileName = "tw_",menuName = "Game Data/Tower")]
	public class TowerData : ScriptableObject
	{
		public int m_Damage = 1;
		public float m_Range = 1f;

		[Tooltip("Fire every x seconds")]
		public float m_FireRate = 1;

		public GameObject m_Prefab;
	}
}
