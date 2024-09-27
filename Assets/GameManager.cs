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
        
        DontDestroyOnLoad(gameObject); // Assure que le GameManager persiste entre les scènes.
    }

    void Update()
    {
        if (isGameOver)
        {
            // Affiche le texte pour recommencer
            restartText.gameObject.SetActive(true);

            // Vérifie si la touche "Enter" est pressée
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                isGameOver = false; // Réinitialise l'état du jeu
                restartText.gameObject.SetActive(false); // Cache le texte après le redémarrage
            }
        }
    }
}
