using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public bool isGameOver = false;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI timerText;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
        DontDestroyOnLoad(gameObject); // Assure que le GameManager persiste entre les sc�nes.
    }

    void Update()
    {
        if (isGameOver)
        {
            // Affiche le texte pour recommencer
            restartText.gameObject.SetActive(true);

            // V�rifie si la touche "Enter" est press�e
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                isGameOver = false; // R�initialise l'�tat du jeu
                restartText.gameObject.SetActive(false); // Cache le texte apr�s le red�marrage
            }
        }
    }
}
