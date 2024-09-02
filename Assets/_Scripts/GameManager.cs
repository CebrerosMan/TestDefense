using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private Path m_path;
		[SerializeField]
		private Spawner m_spawner;
		[SerializeField]
		private GameObject m_castlePrefab;
		[SerializeField]
		private Transform m_camTransform;

		private Castle m_castle;

		private void Awake()
		{
			m_spawner.Path = m_path;

			GameObject castleGO = Instantiate(m_castlePrefab, m_path[^1], Quaternion.LookRotation(Vector3.left, Vector3.up));
			m_castle = castleGO.GetComponent<Castle>();
		}
	}
}
