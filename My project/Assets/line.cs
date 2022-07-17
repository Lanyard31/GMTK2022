using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{

    private LineRenderer lineRenderer;

    // Assign this in Inspector
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        InitLine();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPositions(new Vector3[2] { this.transform.position, target.transform.position });
    }

    void InitLine()
    {
        // Get the LineRenderer assigned to the line object.
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startColor = new Color(1, 1, 1, 1);
        lineRenderer.endColor = new Color(0, 0, 0, 255);
        lineRenderer.positionCount = 2;
    }

}
