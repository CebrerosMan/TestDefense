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
		if (!m_tower?.m_Data)
			return;

		Vector3 towerPos = m_tower.transform.position;
		float range = m_tower.m_Data.m_Range;

		Undo.RecordObject(m_tower.m_Data, "Range Updated");
		m_tower.m_Data.m_Range = Handles.RadiusHandle(Quaternion.identity, towerPos, range);
	}
}
