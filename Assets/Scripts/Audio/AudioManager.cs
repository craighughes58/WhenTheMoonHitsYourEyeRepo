using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private GameObject source2D;

    /// <summary>
    /// Sets instance reference, 
    /// also makes sure this manager isn't destroyed between scenes
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There was already a sound Manager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Plays a 2D sound
    /// Destroys object after clip is finished
    /// </summary>
    /// <param name="clip">The AudioClip to play</param>
    /// <param name="mixerGroup">What Mixer group this sound belongs in</param>
    /// <returns>The source spawned to play the sound.</returns>
    public AudioSource PlayClip2D(AudioClip clip, AudioMixerGroup mixerGroup = null)
    {
        if(clip == null)
        {
            Debug.LogWarning("NO CLIP WAS ASSIGNED!!");
            return null;
        }
        AudioSource source = Instantiate(source2D, Vector3.zero, Quaternion.identity)
                            .GetComponent<AudioSource>();
        SetupAudioSource(ref source, clip);
        return source;
    }

    /// <summary>
    /// Helper function to setup mixergroups and actually start playing the audio
    /// </summary>
    /// <param name="source">Source to modify, passed by reference</param>
    /// <param name="mixerGroup">Mixer group to assign the source to</param>
    void SetupAudioSource(ref AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
        //Giving some extra time so things don't clip
        Destroy(source.gameObject, source.clip.length + .25f);
    }

}