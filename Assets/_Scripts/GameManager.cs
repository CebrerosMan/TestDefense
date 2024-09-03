using UnityEngine;
using UnityEngine.UI;

namespace TD
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private WaveData m_waveData;
		[SerializeField] private Path m_path;
		[SerializeField] private EnemyMovementSystem m_enemyMovement;
		[SerializeField] private Spawner m_spawner;
		[SerializeField] private GameObject m_castlePrefab;
		[SerializeField] private Transform m_camTransform;
		[SerializeField] private EnvironmentController m_environment;
		[SerializeField] private Button m_startButton;

		private Castle m_castle;
		private int m_enemyCount;

		private void Awake()
		{
			Enemy.EnemyInitializedEvent += RegisterEnemy;
			m_startButton.onClick.AddListener(StartGame);
			m_spawner.Path = m_path;
			m_environment.Path = m_path;
			m_enemyMovement.Path = m_path;

			//Spawn castle at the last point in the path, looking towards it
			GameObject castleGO = Instantiate(m_castlePrefab, m_path[^1], Quaternion.LookRotation(m_path[^2] - m_path[^1], Vector3.up));
			m_castle = castleGO.GetComponent<Castle>();

			//Center camera around the path
			Vector3 centroid = Vector3.zero;

			foreach(Vector3 point in m_path)
				centroid += point;

			centroid = centroid / m_path.Size;

			Vector3 camPos = m_camTransform.position;
			camPos.x = centroid.x;
			camPos.z = centroid.z;

			m_camTransform.position = camPos;
		}

		private void StartGame()
		{
			m_startButton.gameObject.SetActive(false);
			m_spawner.RunWave(m_waveData);

			m_enemyCount = m_waveData.m_Enemies.Count;
		}

		private void RegisterEnemy(Enemy enemy)
		{
			enemy.EnemyKilledEvent += OnEnemyKilled;
			m_enemyMovement.RegisterEnemy(enemy);
		}

		private void OnEnemyKilled(Enemy enemy)
		{
			enemy.EnemyKilledEvent -= OnEnemyKilled;
			m_enemyMovement.UnregisterEnemy(enemy);
			
			m_enemyCount--;
			if (m_enemyCount == 0)
				Win();	
		}

		private void Win()
		{
			print("Congratz");
		}
	}
}
