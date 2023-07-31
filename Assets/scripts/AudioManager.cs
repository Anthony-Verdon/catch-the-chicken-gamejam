using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    private static AudioManager instance;

    void Awake()
    {
        //if we want to not reload a song when we change of scene
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return ;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("menuAndTutorial");
    }

   public void Play(string name)
   {
        //we could replace it by a foreach loop, checking every element of the array
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();
        else
            Debug.LogWarning("Sound " + name + " not found !");
   }

   public void Stop(string name)
   {
        //we could replace it by a foreach loop, checking every element of the array
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Stop();
        else
            Debug.LogWarning("Sound " + name + " not found !");
   }
}
