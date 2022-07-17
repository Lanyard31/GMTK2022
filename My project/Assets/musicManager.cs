using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{

    AudioManager _audio;

    // Start is called before the first frame update
    void Start()
    {
        _audio = FindObjectOfType<AudioManager>();
        _audio.Play("theme2");
        _audio.Play("Engine");
    }


}
