using System.Collections.Generic;
using UnityEngine;

namespace TD
{
	public class TowerAttackSystem : MonoBehaviour
	{
		private Dictionary<Tower, TowerAttackState> m_towerAttackData = new();

		public void Register(Tower tower)
		{
			m_towerAttackData.Add(tower, new TowerAttackState(tower.Data.m_FireRate));
			tower.Detector.Collider.enabled = true;
			tower.Detector.Collider.radius = tower.Data.m_Range;
		}

		public void Unregister(Tower tower)
		{
			m_towerAttackData.Remove(tower);
			tower.Detector.Collider.enabled = false;
		}

		private void Update()
		{
			foreach(var kvp in m_towerAttackData)
				ProcessTowerAttack(kvp.Key, kvp.Value);
		}

		private void ProcessTowerAttack(Tower tower, TowerAttackState state)
		{
			if (!tower.Detector.Target)
				return;

			state.m_FireTimer += Time.deltaTime;

			Vector3 look = tower.Detector.Target.Transform.position - tower.Transform.position;
			look.y = 0;

			tower.Mesh.localRotation = Quaternion.LookRotation(look);

			if (state.m_FireTimer >= state.m_FireInterval)
			{
				state.m_FireTimer = 0;
				tower.Fire();
			}
		}
	}
}
