using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "SO/Level")]
public class LevelSO : ScriptableObject
{
    public int level;
    public float damage;
    public float health;
    public float speed;
    public float stunBar;
    public int foodAmountToLevelUp;
    public Vector3 charachterSize;
}
