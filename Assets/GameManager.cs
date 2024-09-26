using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
        //DontDestroyOnLoad(gameObject); // Assure que le GameManager persiste entre les scènes.
    }
}
