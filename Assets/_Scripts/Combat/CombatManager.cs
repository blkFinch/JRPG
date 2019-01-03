using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    //TODO: make this a scriptable called Enemy formation
    public Creature enemy;
    private bool playerTurn;
    private ArrayList turnOrder;
    private List<Creature> defeatedEnemies;
    private VictorySpoils spoils;

    private Hero _hero;

    // Use this for initialization
    void Start()
    {
        _hero = Hero.active;

        defeatedEnemies = new List<Creature>();

        SpawnEnemies();
        CalculateTurnOrder();
        MainCombatLoop();
    }

    private void SpawnEnemies()
    {
        //foreach enemy in EnemyFormation.enemies spawn obj 
        enemy.init();
        Debug.Log("Enemy Appear: " + enemy.CreatureName + " HP: " + enemy.Hp);
    }

    /*
		calculate turn order based on agi
		consider making two lists one of heros and one of enemies
		then sort by agi :
		characterList = characterList.OrderBy(x=>x.GetComponent<CharacterStats>().initiative).ToList();
	 */
    private void CalculateTurnOrder()
    {
        turnOrder = new ArrayList();

        //logic for calculating order of multiple heros/enemies will go here 

        if (_hero.data.Agi > enemy.Agi)
        {
            turnOrder.Add(_hero);
            turnOrder.Add(enemy);
        }
        else
        {
            turnOrder.Add(enemy);
            turnOrder.Add(_hero);
        }
        Debug.Log("turn order: " + turnOrder[0] + " " + turnOrder[1]);
    }


    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1); //just for pacing for now but could become ATB system

        int damage = CalculateDamage(enemy.Atk, _hero.data.Def);

        //TODO: calculate dodge

        Debug.Log(
            enemy + "attacked " + _hero.data.Name + " for " + damage
            );

        _hero.data.CurrentHp -= damage;

        RotateOrder();
    }

    //TODO: extract this method to a listener that takes a Creature target as argument
    public void PlayerAttack()
    {
        if (playerTurn)
        {
            int damage = CalculateDamage(_hero.data.Atk, enemy.Def);

            Debug.Log(
                _hero.data.Name + "attacked " + enemy + " for " + damage
                );

            enemy.CurrentHp -= damage;
            playerTurn = false;

            RotateOrder();
        }
    }

    public int CalculateDamage(int atk, int def)
    {
        int defended = 0;
        if (def > atk) { defended = def - atk; }
        return atk - defended;
    }

    private void CheckForKo()
    {
        if (_hero.data.CurrentHp <= 0)
        {
            Debug.Log(_hero.data.Name + "was knocked out!");
            GameManager.gm.LoadScene("GameOver");
        }

        //todo: foreach in EnemeyFormation.enemies

        if (enemy.CurrentHp <= 0)
        {
            Debug.Log(enemy.CreatureName + "was knocked out!");
            KoEnemy(enemy);
        }
    }

    private void KoEnemy(Creature enemy)
    {
        turnOrder.Remove(enemy);
        defeatedEnemies.Add(enemy);
    }

    private void CheckForVictory()
    {
        if (!turnOrder.Contains(enemy))
        { //this needs to be rewritten to allow formations
            Debug.Log("Victory!!");
            Victory();
        }
    }

    //TODO: add ui here to collect spoils
    private void Victory()
    {

        spoils = new VictorySpoils(defeatedEnemies);
        Debug.Log(spoils.toString());

        Hero.active.data.Xp += spoils.Xp;
        Hero.active.data.Gold += spoils.Gold;

        GameManager.gm.ReturnToWorld();
    }

    private void RotateOrder()
    {
        CheckForKo();
        CheckForVictory();

        var lastActive = turnOrder[0];
        turnOrder.Remove(lastActive);
        turnOrder.Add(lastActive);

        MainCombatLoop();
    }

    /*
        For now main combat loop:
        -MainCombatLoop -- checks which turn it is 
        -Enemy Turn || PlayerTurn  
        -Rotate Order -- calls CheckForKo 
        -MainCombatLoop
     */

    private void MainCombatLoop()
    {

        var activeAgent = turnOrder[0];
        playerTurn = activeAgent.GetType() == typeof(Hero);

        if (!playerTurn)
        {
            StartCoroutine(EnemyTurn());
        }
    }

}
