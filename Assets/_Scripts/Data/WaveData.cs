using System.Collections.Generic;
using UnityEngine;

namespace TD
{
	[CreateAssetMenu(fileName = "wv_", menuName = "Game Data/Wave")]
	public class WaveData : ScriptableObject
	{
		public float m_SpawnInterval;
		public List<EnemyData> m_Enemies;
	}
}