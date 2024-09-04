using TD;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Tower))]
public class TowerInspector : Editor
{
	private Tower m_tower;

	public override VisualElement CreateInspectorGUI()
	{
		m_tower = (Tower)target;

		return base.CreateInspectorGUI();
	}

	private void OnSceneGUI()
	{
		if (!m_tower?.Data)
			return;

		Vector3 towerPos = m_tower.transform.position;
		float range = m_tower.Data.m_Range;

		Vector3 radiusV = Vector3.back * range;
		Vector3 circumferencePoint = towerPos + radiusV;

		Handles.color = Color.green;
		Handles.DrawWireDisc(towerPos, Vector3.up, range);
		Handles.Label(towerPos + radiusV * 0.5f, range.ToString());
	}
}
