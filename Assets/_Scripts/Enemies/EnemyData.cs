using UnityEngine;


namespace TD
{
	[CreateAssetMenu(fileName = "en_", menuName = "GameData/Enemy Data")]
	public class EnemyData : ScriptableObject
	{
		public string m_DisplayName;
		public int m_Health = 1;
		public int m_Damage = 1;
		public float m_Speed = 1f;
		public GameObject m_Prefab;
	}
}
