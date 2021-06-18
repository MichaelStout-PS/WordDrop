using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// This is an example audio manager system and can be altered with
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

   [SerializeField]private AudioSource _musicAudioSource;
   [SerializeField] private AudioSource _soundEffectAudioSource;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundEffect(AudioClip audioClip)
    {
        _soundEffectAudioSource.PlayOneShot(audioClip);
    }
}
