using System.Collections.Generic;
using UnityEngine;

namespace TD
{
	[CreateAssetMenu(fileName = "wv_", menuName = "GameData/Wave Data")]
	public class WaveData : ScriptableObject
	{
		public float m_SpawnInterval;
		public List<EnemyData> m_Enemies;
	}
}