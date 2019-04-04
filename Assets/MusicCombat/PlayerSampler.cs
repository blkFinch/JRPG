using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class PlayerSampler : MonoBehaviour
{

    public PlayableLoop activeLoop;
    public bool queuedToPlay = false;

    public MainLoop master;

    private AudioSource source;

    //TODO: find a way to set this path or use text asset
    private string metadataPath;
    private List<string> loopData;


    //ICON SPEED = 1f
    // speed = distance/time => bpm = distance / clipLength (s)
    float SPAWN_X_POS = 10;
    float TARGET_X_POS = -6;
    float iconSpeed;

    //ICON ASSETS
    public GameObject greenIcon;
    public GameObject blueIcon;
    public GameObject yellowIcon;
    public GameObject redIcon;

    // Use this for initialization
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        master = FindObjectOfType<MainLoop>();

        InitLoop();
        master.notifyLastBarObservers += PlayClipOnTime;
    }

    private void InitLoop()
    {
        metadataPath = activeLoop.PathToMd;
        if (metadataPath != null)
        {
            MetadataParser mp = new MetadataParser(metadataPath);
            loopData = mp.LoopData;
        }
    }

    //SPAWNS ICONS IN TIME WITH BEAT
    // 
    IEnumerator SpawnBarAndPlay()
    {

        //Matches the speed of the icon to hit the mark  on time
        //TODO: consider changing so this spawns the beat further away to keep same time...
        iconSpeed = (SPAWN_X_POS - TARGET_X_POS) / master.bpm;

        bool loopStart = false;

        //meta data for loop
        bool dataToRead = true;
        int lines = loopData.Count;
        int readLine = 0;

        while (dataToRead)
        {
            int beats = 0;
            while (beats < 4)
            {
                ParseMdForBeat(readLine);

                readLine++;
                beats++;

                if (readLine >= lines) { dataToRead = false; }

                yield return new WaitForSeconds(master.barTime / 4);
            }
            if (!loopStart)
            {
                source.Play();
                loopStart = true;
            }
        }
    }

    //TODO: consider making this it's own class??
    private void ParseMdForBeat(int lineToRead)
    {
        string _line = loopData[lineToRead];

        //Parses song metadata file and spawns appropriate colored note
        // [Second Arg of SpawnIcon is vertical Shift]
        switch (_line)
        {
            case "1000":
                SpawnIcon(greenIcon, -1);
                break;
            case "0100":
                SpawnIcon(blueIcon, 0);
                break;
            case "0010":
                SpawnIcon(yellowIcon, 1);
                break;
            case "0001":
                SpawnIcon(redIcon, 2);
                break;
            case "0000":
                //rest
                break;

            default:
                //no note spawn
                break;

        }

    }

    //TODO: adapt this to take a float Y_value to spawn the notes higher or lower
    private void SpawnIcon(GameObject icon, float y_shift)
    {
        float y_pos = 0 + y_shift;
        GameObject _icon = Instantiate(icon);
        _icon.transform.position = new Vector3(SPAWN_X_POS, y_pos, 0);
        _icon.GetComponent<MovingIcon>().setSpeed(iconSpeed);
    }

    public void StartLoop()
    {
        source.clip = activeLoop.Clip;
        source.loop = true;
        queuedToPlay = true;
    }

    //Only called by delegate from master loop 
    //
    private void PlayClipOnTime()
    {
        if (queuedToPlay)
        {
            queuedToPlay = false;
            StartCoroutine(SpawnBarAndPlay());
        }
    }



    public void StopLoop()
    {
        source.loop = false;
    }
}
