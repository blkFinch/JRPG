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
}