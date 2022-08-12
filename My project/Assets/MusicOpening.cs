using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOpening : MonoBehaviour
{
    public AudioSource one;
    public AudioSource two;
    public AudioSource three;
    public AudioSource four;

    bool readyTwo;
    bool readyThree;
    bool readyFour;


    // Start is called before the first frame update
    void Start()
    {
        one.Play();
        readyTwo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (one.isPlaying == false && four.isPlaying == false && readyTwo == true)
        {
            two.Play();
            readyTwo = false;
            readyThree = true;
        }

        if (two.isPlaying == false && readyThree == true)
        {
            three.Play();
            readyThree = false;
            readyFour = true;
        }


        if (three.isPlaying == false && readyFour == true)
        {
            four.Play();
            readyFour = false;
            readyTwo = true;
        }
    }
}
