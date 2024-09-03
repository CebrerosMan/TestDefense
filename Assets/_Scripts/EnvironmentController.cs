using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD
{
	public class EnvironmentController : MonoBehaviour
	{
		[SerializeField] private GameObject m_pathSegmentPrefab;
		[SerializeField] private GameObject m_pathCornerPrefab;
		private Path m_Path;

		private const float PATH_ELEVATION = -0.5f;

		public Path Path
		{
			set => m_Path = value;
		}


		void Start()
		{
			for (int i = 0; i + 1 < m_Path.Size; i++)
			{
				Vector3 p1 = m_Path[i];
				Vector3 p2 = m_Path[i + 1];
				Vector3 pathV = p2 - p1;

				Vector3 pos = Vector3.Lerp(p1, p2, 0.5f);
				pos.y = PATH_ELEVATION;

				GameObject pathSegment = Instantiate(m_pathSegmentPrefab, pos, Quaternion.LookRotation(pathV,Vector3.up),transform);

				Vector3 scale = pathSegment.transform.localScale;
				scale.z = pathV.magnitude;

				pathSegment.transform.localScale = scale;

				p1.y = PATH_ELEVATION;
				Instantiate(m_pathCornerPrefab, p1, Quaternion.identity, transform);
			}
		}
	}
}
