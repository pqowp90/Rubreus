using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy SO", menuName = " ", order = int.MaxValue)]
[System.Serializable]
public class EnemySO : ScriptableObject
{
    public List<EnemyInfo> enemies = new List<EnemyInfo>();
}
