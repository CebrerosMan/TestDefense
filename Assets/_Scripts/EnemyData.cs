using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "En_", menuName = "GameData/EnemyData")]
public class EnemyData : ScriptableObject
{
	public string m_DisplayName;
	public int m_Damage;
	public float m_Speed = 1f;
	public GameObject m_Prefab;
}
