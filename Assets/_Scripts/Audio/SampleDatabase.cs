using UnityEngine;

[CreateAssetMenu]

public class SampleDatabase : ScriptableObject
{
    public Sample[] samples;
 
    public Sample GetSample(int sampleID)
    {
        foreach (var sample in samples)
        {
            if (sample != null && sample.Id == sampleID) return sample;
        }
        return null;
    }
}