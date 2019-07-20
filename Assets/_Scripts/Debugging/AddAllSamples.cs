using UnityEngine.UI;
using UnityEngine;

public class AddAllSamples : MonoBehaviour{
    public void addAllSamples(){
        foreach(var sample in GameManager.gm.sampleDatabse.samples){
            Hero.active.inventory.AddSampleFromID(sample.Id);
        }
    }
}