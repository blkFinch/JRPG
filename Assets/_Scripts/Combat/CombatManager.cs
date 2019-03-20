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
    public Canvas combatCanvas;
    public GameObject enemyBody;
    private EnemyBody activeEnemy; 
    private EnemyBody target;
    private int targetIndex;


    //UTILITY
    //
    public List<GameObject> turnOrder;
    public List<Creature> defeatedEnemies;
    private VictorySpoils spoils;
    private bool playerTurn;
    private bool canAttack = false;
    private int turnIndex = 0;


    private Hero _hero;

    public CombatMenu combatMenu;


    // Use this for initialization
    void Start()
    {
        _hero = Hero.active;
        defeatedEnemies = new List<Creature>();
        turnOrder = new List<GameObject>();

        combatMenu = FindObjectOfType<CombatMenu>();
        InputManager.im.notifyActionButtonObservers += PlayerAttack;
        
        SpawnEnemies();
        MainCombatLoop();
        
        //starts okayer turn countdown
        StartPlayerWait();
    }

    private void SpawnEnemies()
    { 
        
        //this orders the attackers by agility so this is the CalculateTurnOrder()
        var orderedAttackers = formation.Enemies.OrderByDescending( enemy => enemy.Agi).ToArray();
        
        for(int i = 0; i < orderedAttackers.Length; i++){
            SpawnEnemy(orderedAttackers[i]); 
        }
    }

    private void SpawnEnemy(Creature enemy){
        GameObject awakenedEnemy = Instantiate(enemyBody) as GameObject;
        awakenedEnemy.GetComponent<EnemyBody>().init(enemy);

        //TODO: replace this with spawning a 3d animation
        awakenedEnemy.GetComponent<Image>().sprite = enemy.Sprite;
        awakenedEnemy.transform.SetParent(combatCanvas.transform);
        awakenedEnemy.gameObject.name = enemy.name;

        turnOrder.Add(awakenedEnemy);
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

        activeEnemy = turnOrder[0].GetComponent<EnemyBody>();
        if (!playerTurn)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    void StartPlayerWait(){
        StartCoroutine(PlayerWait());
    }

    //TODO: clean this up into a proper player turn
    IEnumerator PlayerWait(){
        int countDown = 50;
        while(countDown < 0){
            countDown -= _hero.data.Agi;
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Player turn!");
        playerTurn = true;

        combatMenu.PlayerTurn();
    }


    IEnumerator EnemyTurn()
    {
        activeEnemy.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1); //just for pacing for now but could become ATB system

        int damage = CalculateDamage(activeEnemy.atk, _hero.data.Def);

        // //TODO: calculate dodge

        Debug.Log(
            activeEnemy._name + "attacked " + _hero.data.Name + " for " + damage
            );

        // //extract this out to hero class
        _hero.TakeDamage(damage);

        RotateOrder();
    }

    public void PlayerAttack()
    {
        if (target && canAttack)
        {          
            canAttack = false;  
            int damage = CalculateDamage(_hero.data.Atk, target.def);

            Debug.Log(_hero.data.Name + "attacked " + target + " for " + damage);

            target.TakeDamage(damage);
            Debug.Log("current hp" + target.currentHp);
            

            RotateOrder();
            StartPlayerWait();
        }
        
    }


    public void Target(int index){
        targetIndex = index;
        target = turnOrder[index].GetComponent<EnemyBody>();

        ClearEnemyColor();
        target.GetComponent<Image>().color = Color.blue;

        StartCoroutine(PrepareAttack());

    }

    public void TargetLeft(){
        if(targetIndex > 0){
            targetIndex--;
            Target(targetIndex);
        }
    }

    public void TargetRight(){
        if(targetIndex < turnOrder.Count){
            targetIndex++;
            Target(targetIndex);
        }

    }

    IEnumerator PrepareAttack(){
        yield return new WaitForSeconds(1); //prevents enemy from being attacked upon targeting
        canAttack = true;
    }

    public int CalculateDamage(int atk, int def)
    {
        int defended = 0;
        if (def > atk) { defended = def - atk; }
        return atk - defended;
    }

    private void RotateOrder()
    {
        CheckForVictory();

        ClearEnemyColor();

        var lastActive = turnOrder[0];
        turnOrder.Remove(lastActive);
        turnOrder.Add(lastActive);

        MainCombatLoop();
    }

    private void ClearEnemyColor()
    {
        for (int f = 0; f < turnOrder.Count; f++)
        {
            turnOrder[f].GetComponent<Image>().color = Color.white;
        }
    }

    private void CheckForVictory()
    {
        if (!turnOrder.Any()){
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

}
