using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsset : ScriptableObject
{
    [Header("Внешний вид")]
    public Color color = Color.white;
    public RuntimeAnimatorController animations;

    [Header("Параметры")]
    public float m_MoveSpeed = 1;
    public int hp = 1;
    public int damage = 1;


}
