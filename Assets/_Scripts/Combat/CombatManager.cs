using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CombatManager : MonoBehaviour
{

    //ENEMY FORMATION
    //
    public EnemyFormation formation;
    Creature activeEnemy; 
    private GameObject enemySprite;
    public Canvas combatCanvas;


    //UTILITY
    //
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
        // enemy.init();
        // Debug.Log("Enemy Appear: " + enemy.CreatureName + " HP: " + enemy.Hp);

        foreach (var enemy in formation.Enemies)
        {

            enemySprite = new GameObject(enemy.name);
            enemySprite.AddComponent<Image>();
            enemySprite.GetComponent<Image>().sprite = enemy.Sprite;
            enemySprite.transform.SetParent(combatCanvas.transform);

            //for now just sets currentHp to max
            enemy.init();
        }

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
        bool heroAdded = false;
    
        IEnumerable<Creature> orderedAttackers = formation.Enemies.OrderByDescending( creature => creature.Agi);

        foreach(var creature in orderedAttackers){

            if ( !heroAdded && _hero.data.Agi > creature.Agi)
            {
                turnOrder.Add(_hero);
                heroAdded = true;
                Debug.Log("adding hero");
            }
            turnOrder.Add(creature);
            Debug.Log("adding creature " + creature.name);
        }

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
            this.activeEnemy = activeAgent as Creature;
            StartCoroutine(EnemyTurn());
        }
    }


    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1); //just for pacing for now but could become ATB system

        int damage = CalculateDamage(activeEnemy.Atk, _hero.data.Def);

        //TODO: calculate dodge

        Debug.Log(
            activeEnemy + "attacked " + _hero.data.Name + " for " + damage
            );

        //extract this out to hero class
        _hero.data.CurrentHp -= damage;

        RotateOrder();
    }

    //TODO: extract this method to a listener that takes a Creature target as argument
    public void PlayerAttack()
    {
        if (playerTurn)
        {
            foreach (var enemy in formation.Enemies)
            {
                
                int damage = CalculateDamage(_hero.data.Atk, enemy.Def);

                Debug.Log(
                    _hero.data.Name + "attacked " + enemy + " for " + damage
                    );

                enemy.CurrentHp -= damage;
                playerTurn = false;
            }

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
        foreach (var enemy in formation.Enemies)
        {
            if (enemy.CurrentHp <= 0)
            {
                Debug.Log(enemy.CreatureName + "was knocked out!");
                KoEnemy(enemy);
            }
        }
    }

    private void KoEnemy(Creature enemy)
    {
        turnOrder.Remove(enemy);
        defeatedEnemies.Add(enemy);
    }

    private void CheckForVictory()
    {
        foreach (var enemy in formation.Enemies)
        {
            
            if (!turnOrder.Contains(enemy))
            { //this needs to be rewritten to allow formations
                Debug.Log("Victory!!");
                Victory();
            }
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
}
