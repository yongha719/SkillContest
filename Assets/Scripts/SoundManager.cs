using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private GameObject Source;

    [SerializeField]
    private AudioClip WarningSound;

    [SerializeField]
    private List<AudioClip> EnemyDieSounds = new List<AudioClip>();

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    public void PlayWarningSound()
    {
        Play(WarningSound);
    }


    void Play(AudioClip clip)
    {
        AudioSource source = Instantiate(Source, transform).GetComponent<AudioSource>();
        source.PlayOneShot(clip);
        Destroy(source.gameObject, clip.length);
    }
}
