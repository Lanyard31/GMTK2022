using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingMusic : MonoBehaviour
{

    public AudioSource Begin;
    public AudioSource Middle;
    public AudioSource End;

    Player player;
    bool ReadyMiddle;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        ReadyMiddle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Begin.isPlaying == false && Middle.isPlaying == false && End.isPlaying == false && ReadyMiddle == true)
        {
            Middle.Play();
        }

        if (Middle.isPlaying == true)
            ReadyMiddle = false;

        if (Middle.isPlaying == false && player.dead == false && End.isPlaying == false)
            ReadyMiddle = true;

        /*
        if (player.dead == true && End.isPlaying == false)
        {
            ReadyMiddle = false;
            Begin.Stop();
            Middle.Stop();
            End.Play();
        }
        */
    }
}
