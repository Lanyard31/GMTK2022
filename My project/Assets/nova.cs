using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nova : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ResetScene", 5.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("Main");
    }
}
