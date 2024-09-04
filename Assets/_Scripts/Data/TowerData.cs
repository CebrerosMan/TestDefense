using UnityEngine;

namespace TD
{
	[CreateAssetMenu(fileName = "tw_",menuName = "Game Data/Tower")]
	public class TowerData : ScriptableObject
	{
		public int m_Damage = 1;
		public float m_Range = 1f;

		[Tooltip("Bullets per second")]
		public int m_FireRate = 1;

		public GameObject m_Prefab;
	}
}
