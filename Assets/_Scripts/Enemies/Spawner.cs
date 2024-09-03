using System.Collections;
using UnityEngine;

namespace TD
{
	public class Spawner : MonoBehaviour
	{
		private Path m_path;
		public Path Path
		{
			set => m_path = value;
		}

		public void RunWave(WaveData waveData)
		{
			StartCoroutine(GameWave(waveData));
		}

		private IEnumerator GameWave(WaveData waveData)
		{
			for(int i = 0; i<waveData.m_Enemies.Count; i++)
			{
				EnemyData data = waveData.m_Enemies[i];

				GameObject enemyGO = Instantiate(data.m_Prefab, m_path[0],Quaternion.identity,transform);
				Enemy enemy = enemyGO.GetComponent<Enemy>();
				enemy.Initialize(data);

				yield return new WaitForSeconds(waveData.m_SpawnInterval);
			}
		}
	}
}
