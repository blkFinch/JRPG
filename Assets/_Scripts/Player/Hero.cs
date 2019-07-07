using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero active;

    public HeroData data;
    public Inventory inventory;

    private void Awake() {
        if (active != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            active = this;
        }

        DontDestroyOnLoad(active.gameObject);
        init();
    }

    // GETTERS
    public string HeroName() {
        return data.Name;
    }


    /*
        INIT: initialize empty values in active hero before data is loaded/written

        there may be a more elegant solution for this but for now this is the space to 
        eliminate any null values because they will throw exceptions during deserialization
                -finch
     */
    public void init() {
        data.Name = "init";
        data.Level = 1;
        data.Atk = data.Def = data.Agi = data.Mag = 3;

        data.Hp = 35;
        data.CurrentHp = data.Hp;

        data.inventoryData = new InventoryData();
        inventory = new Inventory();
        inventory.items = new List<Item>();
        inventory.samples = new List<Sample>();
    }

    public void TakeDamage(int damage) {
        this.data.CurrentHp -= damage;
        if (this.data.CurrentHp <= 0)
        {
            Debug.Log(this.data.Name + "was knocked out!");
            GameManager.gm.LoadScene("GameOver");
        }
    }

    public void SerializeLocation() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        this.data.Loc_X = player.transform.position.x;
        this.data.Loc_Y = player.transform.position.y;
        this.data.Loc_Z = player.transform.position.z;
    }

    public void DeserializeLocation() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(this.data.Loc_X, this.data.Loc_Y, this.data.Loc_Z);
    }


}