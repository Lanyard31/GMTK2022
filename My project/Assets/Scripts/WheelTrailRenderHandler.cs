using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRenderHandler : MonoBehaviour
{

    Player player;
    TrailRenderer trailRenderer;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.emitting = false;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsTireScreeching(out float lateralVelocity, out bool isBraking))
            trailRenderer.emitting = true;
        else trailRenderer.emitting = false;
    }
}
