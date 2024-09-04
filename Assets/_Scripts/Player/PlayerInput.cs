using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TD
{
	//In charge of tower placement
	public class PlayerInput : MonoBehaviour
	{
		public TowerData m_TowerData;
		[SerializeField] private Camera m_cam;

		private GameObject m_placedTower;
		private Plane m_floorPlane;
		private Transform m_camTransform;
		private List<MeshRenderer> m_meshRenderers = new();
		private List<Color> m_towerColors = new();
		private Collider[] m_colliderBuffer = new Collider[16];

		private const float RAYCAST_LENGTH = 100f;
		private const int PLACEMENT_MASK = 1 << 6 | 1 << 7;
		private const float PLACEMENT_RADIUS = 0.5f;

		private void Awake()
		{
			m_floorPlane = new Plane(Vector3.up, Vector3.zero);
			m_camTransform = m_cam.transform;
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				StartPlacement();
			else if (m_placedTower && Input.GetMouseButton(0))
				MovePlacement();
			else if (m_placedTower && Input.GetMouseButtonUp(0))
				FinishPlacement();
		}

		private void StartPlacement()
		{
			if (!TryHitFloor(Input.mousePosition, out Vector3 floorPoint))
				return;

			m_placedTower = Instantiate(m_TowerData.m_Prefab, floorPoint, Quaternion.identity);
			m_placedTower.GetComponentsInChildren(m_meshRenderers);

			m_towerColors.Clear();
			foreach (var towerRenderer in m_meshRenderers)
				m_towerColors.Add(towerRenderer.material.color);

		}

		private void MovePlacement()
		{
			if (!TryHitFloor(Input.mousePosition, out Vector3 floorPoint))
				return;

			m_placedTower.transform.position = floorPoint;

			Color c = IsValidPlacement(floorPoint) ? Color.green : Color.red;

			foreach (var towerRenderer in m_meshRenderers)
				towerRenderer.material.color = c;
		}

		private void FinishPlacement()
		{
			if (TryHitFloor(Input.mousePosition, out Vector3 floorPoint) && IsValidPlacement(floorPoint))
			{
				for (int i = 0; i < m_meshRenderers.Count; i++)
					m_meshRenderers[i].material.color = m_towerColors[i];

				m_placedTower.GetComponent<Tower>().Initialize(m_TowerData);
			}
			else
				Destroy(m_placedTower);

			m_placedTower = null;
		}

		private bool TryHitFloor(Vector3 mousePosition, out Vector3 floorPoint)
		{
			floorPoint = default;

			Vector3 worldMousePos = m_cam.ScreenToWorldPoint(mousePosition);
			Ray ray = new Ray(worldMousePos, m_camTransform.forward * RAYCAST_LENGTH);

			if (m_floorPlane.Raycast(ray, out float enter))
			{
				floorPoint = ray.GetPoint(enter);
				return true;
			}

			return false;
		}

		private bool IsValidPlacement(Vector3 position)
		{
			int resultCount = Physics.OverlapSphereNonAlloc(position, PLACEMENT_RADIUS, m_colliderBuffer, PLACEMENT_MASK);

			for (int i = resultCount - 1; i >= 0; i--)
				if (m_colliderBuffer[i].gameObject == m_placedTower)
					resultCount--;

			return resultCount == 0;
		}
	}
}
