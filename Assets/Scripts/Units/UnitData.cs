using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "newUnit", menuName = "Unit/Unit Data")]
public class UnitData : ScriptableObject {
    public new string name;
    public float maxHP;
    public AttackModifier attack;
    public DefenseModifier defense;
    public MovementCost movement;
    public int attackRange;
    public int ammo;
    public Sprite sprite;
    public TileBase[] tilesBases;
}
