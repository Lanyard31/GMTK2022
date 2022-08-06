using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineFix : MonoBehaviour
{


    public LineRenderer lr;
    public string SortingLayer;
    public int OrderInLayer;

    void Awake()
    {
        lr = this.GetComponent<LineRenderer>();
    }

    void Start()
    {
        lr.sortingLayerName = SortingLayer;
        lr.sortingOrder = OrderInLayer;
    }

}


