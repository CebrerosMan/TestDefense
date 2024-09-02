using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TD
{
	[ExecuteAlways]
	public class Path : MonoBehaviour
	{
		public List<Vector3> m_Points;

		private Transform m_transform;

		public int Size => m_Points.Count;

		public Vector3 this[System.Index index]
		{
			get => m_Points[index] + m_transform.position;
		}

		private void Awake()
		{
			m_transform = transform;
		}

#if UNITY_EDITOR
		private void OnEnable()
		{
			SceneView.duringSceneGui += DrawGUI;
		}

		private void OnDisable()
		{
			SceneView.duringSceneGui -= DrawGUI;
		}

		private void DrawGUI(SceneView sceneView)
		{
			Handles.color = Color.yellow;

			for (int i = 0; i < Size; i++)
				if (i < Size - 1)
					Handles.DrawLine(this[i], this[i + 1]);
		}
#endif
	}
}
