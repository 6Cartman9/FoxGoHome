using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsset : ScriptableObject
{
    [Header("������� ���")]
    public Color color = Color.white;
    public RuntimeAnimatorController animations;

    [Header("���������")]
    public float m_MoveSpeed = 1;
    public int hp = 1;
    public int damage = 1;


}
