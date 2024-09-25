using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0), 0.2f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // ajouter un composant audio dans les coins
        // GetComponent.audioSource.play()
        Destroy(this.gameObject);

    }

}
