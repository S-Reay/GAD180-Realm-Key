using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SP_AudioManager : MonoBehaviour
{
    public AudioSource[] sounds = new AudioSource[9];
    public AudioSource backgroundMusic;
    /*
     * 0 - Item Pickup
     * 1 - Key Pickup
     * 2 - Key Steal
     * 3 - Key Capture
     * 4 - Rigged Dice
     * 5 - Stun
     * 6 - Triple Dice
     * 7 - Round Start
     * 8 - Win game
     */

    public void PlaySound(int soundID)
    {
        sounds[soundID].Play();
    }

    public void PlayBG()
    {
        backgroundMusic.Play();
    }

}
