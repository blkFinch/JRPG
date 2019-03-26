using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerSampler : MonoBehaviour
{

    public AudioClip clip;
    public bool queuedToPlay = false;

    public MainLoop master;

    private AudioSource source;

    //TODO: find a way to set this path or use text asset
    public string metadataPath = "/Assets/MusicCombat/bass_loop.sd";
    private List<string> loopData;


    //ICON SPEED = 1f
    // speed = distance/time => bpm = distance / clipLength (s)
    public GameObject icon;
    float SPAWN_X_POS = 10;
    float TARGET_X_POS = -6;
    float iconSpeed;



    // Use this for initialization
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        master = FindObjectOfType<MainLoop>();
        master.notifyLastBarObservers += PlayClipOnTime;

        var mdFile = File.ReadAllLines(metadataPath);
        loopData = new List<string>(mdFile);
    }

    //SPAWNS ICONS IN TIME WITH BEAT
    // 
    IEnumerator spawnBarAndPlay()
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
                if (loopData[readLine] == "1")
                {
                    SpawnIcon();
                }
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

    private void SpawnIcon()
    {
        GameObject _icon = Instantiate(icon);
        _icon.transform.position = new Vector3(SPAWN_X_POS, 0, 0);
        _icon.GetComponent<MovingIcon>().setSpeed(iconSpeed);
    }

    public void StartLoop()
    {
        source.clip = clip;
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
            StartCoroutine(spawnBarAndPlay());
        }
    }



    public void StopLoop()
    {
        source.loop = false;
    }
}
