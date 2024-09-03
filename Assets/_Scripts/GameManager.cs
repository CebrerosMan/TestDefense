using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private Path m_path;
		[SerializeField] private Spawner m_spawner;
		[SerializeField] private GameObject m_castlePrefab;
		[SerializeField] private Transform m_camTransform;
		[SerializeField] private EnvironmentController m_environment;

		private Castle m_castle;

		private void Awake()
		{
			m_spawner.Path = m_path;
			m_environment.Path = m_path;

			GameObject castleGO = Instantiate(m_castlePrefab, m_path[^1], Quaternion.LookRotation(Vector3.left, Vector3.up));
			m_castle = castleGO.GetComponent<Castle>();

			Vector3 centroid = Vector3.zero;

			foreach(Vector3 point in m_path)
				centroid += point;

			centroid = centroid / m_path.Size;

			Vector3 camPos = m_camTransform.position;
			camPos.x = centroid.x;
			camPos.z = centroid.z;

			m_camTransform.position = camPos;
		}
	}
}
