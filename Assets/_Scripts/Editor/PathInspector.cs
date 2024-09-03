using UnityEditor;
using UnityEngine;
using TD;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(Path))]
public class PathInspector : Editor
{
	private Path m_Target;

	public override VisualElement CreateInspectorGUI()
	{
		VisualElement root = new VisualElement();

		InspectorElement.FillDefaultInspector(root, serializedObject, this);

		m_Target = (Path)target;

		return root;
	}

	private void OnSceneGUI()
	{
		if (m_Target?.m_Points == null)
			return;

		Vector3 worldPosition = m_Target.transform.position;

		for (int i = 0; i < m_Target.Size; i++)
		{
			Vector3 point = m_Target.m_Points[i];

			Vector3 handleStart = point + worldPosition;
			Vector3 handleEnd = Handles.PositionHandle(handleStart, Quaternion.identity);

			point += handleEnd - handleStart;

			Undo.RecordObject(m_Target, "Point Changed");
			m_Target.m_Points[i] = point;
		}
	}
}
