using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPG/EnemyFormation"))]

public class EnemyFormation : ScriptableObject {
    [SerializeField]
    private Creature[] enemies;

    public Creature[] Enemies
    {
        get
        {
            return enemies;
        }

        set
        {
            enemies = value;
        }
    }
}