using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCam : MonoBehaviour
{
    public Vector3 offset; // variable apparaît dans unity

    public GameObject sphere;

    public void Start()
    {
        // offset = this.transform.position - sphere.transform.position;
    }

    public void Update()
    {

        // this.transform.position = sphere.transform.position + offset;
        
        
    }
}
