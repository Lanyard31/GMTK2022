using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    Player player;


    private void Awake()
    {
        player = GetComponent<Player>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 InputVector = Vector2.zero;

        InputVector.x = Input.GetAxis("Horizontal");
        InputVector.y = Input.GetAxis("Vertical");

        player.SetInputVector(InputVector);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
            player.Drift();

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Submit"))
        {
            player.DriftOff();
        }

    }
}
