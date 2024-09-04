using System.Collections;
using UnityEngine;

namespace TD
{
	public class Spawner : MonoBehaviour
	{
		public Path Path { private get; set; }

		private Coroutine m_waveRoutine;

		public void RunWave(WaveData waveData)
		{
			m_waveRoutine = StartCoroutine(GameWave(waveData));
		}

		public void Stop()
		{
			if(m_waveRoutine != null)
			{
				StopCoroutine(m_waveRoutine);
				m_waveRoutine = null;
			}
		}

		private IEnumerator GameWave(WaveData waveData)
		{
			for (int i = 0; i < waveData.m_Enemies.Count; i++)
			{
				EnemyData data = waveData.m_Enemies[i];

				GameObject enemyGO = Instantiate(data.m_Prefab, Path[0], Quaternion.LookRotation(Path[1] - Path[0]), transform);
				Enemy enemy = enemyGO.GetComponent<Enemy>();
				enemy.Initialize(data);

				yield return new WaitForSeconds(waveData.m_SpawnInterval);
			}
		}
	}
}
