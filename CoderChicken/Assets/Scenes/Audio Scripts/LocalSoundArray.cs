using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LocalSoundArray : MonoBehaviour
{
    public Chickensound[] clucks;

    internal static LocalSoundArray instance;
    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        
       foreach (Chickensound c in clucks)
        {
            c.source = gameObject.AddComponent<AudioSource>();
            c.source.pitch = c.pitch;
            c.source.loop = c.loop;
            c.source.playOnAwake = c.playOnAwake;
            c.source.outputAudioMixerGroup = c.group;
        }


}

    public void Play(string name)
    {
            Chickensound chickensound = Array.Find(clucks, cluck => cluck.name == name);
        var i = Random.Range(0, chickensound.clip.Length);
        chickensound.source.clip = chickensound.clip[i];
        chickensound.source.Play();
        Debug.Log("Last sound played: " + chickensound.clip[i].name);
        
        


    }
}
