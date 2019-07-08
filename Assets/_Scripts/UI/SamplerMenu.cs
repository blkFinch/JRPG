using UnityEngine;
using UnityEngine.UI;
public class SamplerMenu : Menu<SamplerMenu>
{

    [SerializeField]
    private Button sampleBtn;

    [SerializeField]
    private float spacingY = 10;

    [SerializeField]
    private Vector3 initialPos = new Vector3(100, 400, 0);

    // Use this for initialization
    void Start() {
        if (Hero.active.inventory.samples.Count > 0)
        {
            Debug.Log("Spawnign sample buttons");
            SpawnButtons();
        }
    }

    private void SpawnButtons() {
        for (int i = 0; i < Hero.active.inventory.samples.Count; i++)
        {
            Sample sample = Hero.active.inventory.samples[i];
            float space = spacingY * i;
            Vector3 pos = new Vector3(initialPos.x, initialPos.y + space, 0);
            SpawnSampleButton(sample, pos, i);
        }
    }

    private void SpawnSampleButton(Sample sample, Vector3 pos, int count) {
        Button _btn = Instantiate(sampleBtn, pos, Quaternion.identity);
        _btn.transform.SetParent(this.transform);
        _btn.GetComponentInChildren<Text>().text = sample.name;
        _btn.GetComponent<SampleButton>().sample = sample;

        //sets first button to selected
        if (count == 0) { 
			GetComponent<SelectOnInput>().selectedObject = _btn.gameObject;
			Debug.Log("SOI set to : " +  _btn);
		}
    }

}
