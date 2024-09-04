using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TD
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private WaveData m_waveData;
		[SerializeField] private Path m_path;
		[SerializeField] private EnemyMovementSystem m_enemyMovement;
		[SerializeField] private TowerAttackSystem m_towerAttack;
		[SerializeField] private Spawner m_spawner;
		[SerializeField] private GameObject m_castlePrefab;
		[SerializeField] private Transform m_camTransform;
		[SerializeField] private EnvironmentController m_environment;
		[SerializeField] private Button m_startButton;
		[SerializeField] private Button m_restartButton;
		[SerializeField] private TextMeshProUGUI m_gameText;

		private Castle m_castle;
		private int m_enemyCount;

		private void Awake()
		{
			Enemy.EnemyInitializedEvent += RegisterEnemy;

			m_startButton.onClick.AddListener(StartGame);
			m_restartButton.onClick.AddListener(RestartGame);
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

		private void OnDestroy()
		{
			Enemy.EnemyInitializedEvent -= RegisterEnemy;
		}

		private void StartGame()
		{
			m_startButton.gameObject.SetActive(false);
			m_spawner.RunWave(m_waveData);

			m_enemyCount = m_waveData.m_Enemies.Count;
		}

		private void RestartGame()
		{
			SceneManager.LoadScene(0);
		}

		private void RegisterEnemy(Enemy enemy)
		{
			enemy.EnemyKilledEvent += OnEnemyKilled;
			enemy.EnemyAttackedEvent += OnEnemyAttack;

			m_enemyMovement.RegisterEnemy(enemy);
		}

		private void OnEnemyKilled(Enemy enemy)
		{
			DestroyEnemy(enemy);
			CheckWinCondition();	
		}

		private void OnEnemyAttack(Enemy enemy)
		{
			DestroyEnemy(enemy);
			m_castle.Health -= enemy.Data.m_Damage;

			CheckWinCondition();
		}

		private void CheckWinCondition()
		{
			if(m_castle.Health <= 0)
				Lose();
			else if(m_enemyCount == 0)
				Win();
		}

		private void DestroyEnemy(Enemy enemy)
		{
			m_enemyMovement.UnregisterEnemy(enemy);
			m_enemyCount--;

			Destroy(enemy.gameObject);
		}

		private void Win()
		{
			StopGame("You Win!");
		}

		private void Lose()
		{
			StopGame("You Lose...");
		}

		private void StopGame(string message)
		{
			m_gameText.text = message;
			m_enemyMovement.enabled = false;
			m_towerAttack.enabled = false;
			m_spawner.Stop();
			m_restartButton.gameObject.SetActive(true);

			var towers = FindObjectsByType<Tower>(FindObjectsSortMode.None);

			foreach (var tower in towers)
				tower.enabled = false;
		}
	}
}
