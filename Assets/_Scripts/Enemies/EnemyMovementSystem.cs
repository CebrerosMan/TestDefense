using System;
using System.Collections.Generic;
using UnityEngine;

namespace TD
{
	public class EnemyMovementSystem : MonoBehaviour
	{
		private const float SEGMENT_THRESHOLD = 0.01f;

		private Dictionary<Enemy, EnemyMovementState> m_movementData = new();
		private List<Enemy> m_removeEnemies = new();

		public Path Path { private get; set; }

		public void RegisterEnemy(Enemy enemy)
		{
			m_movementData.Add(enemy, new EnemyMovementState(1));
		}

		public void UnregisterEnemy(Enemy enemy)
		{
			m_movementData.Remove(enemy);
		}

		private void Update()
		{
			foreach (var kvp in m_movementData)
				if(!MoveEnemy(kvp.Key, kvp.Value, Time.deltaTime))
					m_removeEnemies.Add(kvp.Key);

			foreach (var enemy in m_removeEnemies)
				UnregisterEnemy(enemy);

			m_removeEnemies.Clear();
		}

		private bool MoveEnemy(Enemy enemy, EnemyMovementState state, float deltaTime)
		{
			int index = state.m_PathIndex;

			try
			{
				Vector3 target = Path[index];

				Vector3 newPos = Vector3.MoveTowards(enemy.Transform.position, target, deltaTime * enemy.Speed);

				enemy.Transform.position = newPos;

				if (Vector3.SqrMagnitude(target - newPos) < SEGMENT_THRESHOLD)
					state.m_PathIndex++;

				return true;
			}
			catch (ArgumentOutOfRangeException e)
			{
				Debug.LogError($"{e.Message}. Index: {index}");
				Debug.LogWarning($"Removing enemy: {enemy.name}:{enemy.GetInstanceID()}");

				return false;
			}
		}
	}
}
