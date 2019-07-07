using UnityEngine;


public enum Instrument
{
    Bass, Drums, Lead, Pad, Other
}

[CreateAssetMenu(menuName = ("RPG/Sample"))]
public class Sample : ScriptableObject{
    public AudioClip clip;
    public string sampleName;

    public Instrument instrument;

    [SerializeField]
    [Header("Never set id to 0! IDS must be unique")]
    private int id;

    public int Id {
        get {
            return id;
        }
    }
}