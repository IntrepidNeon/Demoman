using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipCollection : MonoBehaviour
{
    public List<AudioClip> battleCries = new List<AudioClip>();
    public List<AudioClip> breakSounds = new List<AudioClip>();
    public AudioClip shootSound;
    public AudioClip bounceSound;

    public static List<AudioClip> BattleCries;

    public static List<AudioClip> BreakSounds;

    public static AudioClip ShootSound;

    public static AudioClip BounceSound;

    private void Start()
    {
        BattleCries = battleCries;
        BreakSounds = breakSounds;
        ShootSound = shootSound;
        BounceSound = bounceSound;
    }
}
