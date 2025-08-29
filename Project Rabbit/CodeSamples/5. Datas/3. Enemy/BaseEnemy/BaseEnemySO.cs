using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BaseEnemySO", menuName = "Scriptable Objects/BaseEnemySO")]
public class BaseEnemySO : ScriptableObject, IStatProvider
{
    [Header("식별")]
    public int ID;
    public EnemyType Type;

    [Header("플레이어 감지 거리")]
    public float DetectingRange;

    [Header("근거리 설정")]
    public GameObject MeleeHitboxPrefab;
    public HitData MeleeHitData;

    [Header("원거리 설정")]
    public GameObject RangedProjectilePrefab;
    public HitData RangedHitData;

    [Header("기본 스탯")]
    public List<StatData> EnemyStats;
    public List<StatData> Stats => EnemyStats;  // IStatProvider 구현

    [Header("물리 설정")]
    public float gravityScale = 1f;
}
