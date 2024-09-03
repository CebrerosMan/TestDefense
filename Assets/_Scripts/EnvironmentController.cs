using UnityEngine;

namespace TD
{
	public class EnvironmentController : MonoBehaviour
	{
		[SerializeField] private GameObject m_pathSegmentPrefab;
		[SerializeField] private GameObject m_pathCornerPrefab;

		private const float PATH_ELEVATION = 0;

		public Path Path { private get; set; }

		void Start()
		{
			for (int i = 0; i + 1 < Path.Size; i++)
			{
				Vector3 p1 = Path[i];
				Vector3 p2 = Path[i + 1];
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
