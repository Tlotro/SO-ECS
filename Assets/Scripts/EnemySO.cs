using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemySO : ScriptableObject
{
    public float MaxHP = 10f;
    public float Speed = 3f;
    public int XPValue = 5;
    public float damage = 0;
    public Sprite EnemySprite;
}
