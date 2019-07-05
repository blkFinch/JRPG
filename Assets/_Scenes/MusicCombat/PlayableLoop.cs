using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPG/Loop"))]
public class PlayableLoop : ScriptableObject {

	[SerializeField]
	private string _name, _pathToMd;

	[SerializeField]
	private int bpm, _id;

	[SerializeField]
	AudioClip clip;

    public int Bpm
    {
        get
        {
            return bpm;
        }

        set
        {
            bpm = value;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }

    public int Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    public string PathToMd
    {
        get
        {
            return _pathToMd;
        }

        set
        {
            _pathToMd = value;
        }
    }

    public AudioClip Clip
    {
        get
        {
            return clip;
        }

        set
        {
            clip = value;
        }
    }
}
