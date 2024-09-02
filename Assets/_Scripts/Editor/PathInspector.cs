using UnityEditor;
using UnityEngine;
using TD;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(Path))]
public class PathInspector : Editor
{
	private Path m_Target;
	private Transform m_Transform;

	public override VisualElement CreateInspectorGUI()
	{
		VisualElement root = new VisualElement();

		InspectorElement.FillDefaultInspector(root, serializedObject, this);

		m_Target = (Path)target;
		m_Transform = m_Target.transform;

		return root;
	}

	private void OnSceneGUI()
	{
		if (m_Target.m_Points == null)
			return;

		Vector3 worldPosition = m_Transform.position;

		for (int i = 0; i < m_Target.Size; i++)
		{
			Vector3 point = m_Target.m_Points[i];

			Vector3 handleStart = point + worldPosition;
			Vector3 handleEnd = Handles.PositionHandle(handleStart, Quaternion.identity);

			point += handleEnd - handleStart;

			m_Target.m_Points[i] = point;
		}
	}
}
