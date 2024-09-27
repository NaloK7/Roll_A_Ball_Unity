using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    AudioSource oneCoinSound;
    AudioSource allCoinSound;
    AudioSource[] CoinSounds;

    bool isJumping;
    public Color initColor;
    public float hitTime;
    public TMP_Text txt;
    public int score;
    public TMP_Text finish;
    public int maxCoins;
    public int speedBall;
    private float timer = 0f; 
    private bool timerStarted = false;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        maxCoins = FindObjectsOfType<RotateCoin>().Length; // compte le nombre de cylinder by rotate func
        CoinSounds = GetComponents<AudioSource>();
        oneCoinSound = CoinSounds[0];
        allCoinSound = CoinSounds[1];
        score = 0;
        hitTime = 0.15f;
        initColor = GetComponent<MeshRenderer>().material.color;
        speedBall = 15;
        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Démarre le timer lors du premier mouvement de la balle
        if (!timerStarted && (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow) ||
                              Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow) ||
                              Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ||
                              Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            timerStarted = true;
        }

        // Met à jour le timer si le jeu a commencé
        if (timerStarted)
        {
            timer += Time.deltaTime; // Ajoute le temps écoulé depuis le dernier frame
            gameManager.timerText.text = timer.ToString("F2") + " s";
        }
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))  // avancer
        {
            GetComponent<Rigidbody>().AddForce(Vector3.forward * speedBall);
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) // gauche
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * speedBall);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))  // reculer
        {
            GetComponent<Rigidbody>().AddForce(Vector3.back * speedBall);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))  // droite
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * speedBall);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)  // sauter
        {
            isJumping = !isJumping;
            GetComponent<Rigidbody>().AddForce(Vector3.up * 200);
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Floor")) // si rentre en collision avec le "Floor"
        {
            isJumping = false;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            StartCoroutine("ChangeColor");
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Coin"))
        {
            
            score++;
            txt.text = "Coins : " + score;
            if (score >= maxCoins)
            {
                timerStarted = false;
                allCoinSound.Play();
                Time.timeScale = 0;
                gameManager.isGameOver = true;
            }
            else
            {
                if (!oneCoinSound.isPlaying)
                {
                    StartCoroutine(PlaySoundForDuration(oneCoinSound, 0.6f));
                }
            }


        }
    }
    IEnumerator ChangeColor()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(hitTime);
        gameObject.GetComponent<MeshRenderer>().material.color = initColor;

    }

    IEnumerator PlaySoundForDuration(AudioSource audioSource, float duration)
    {
        audioSource.Play(); // Commence la lecture du son
        yield return new WaitForSeconds(duration); // Attend la durée spécifiée
        audioSource.Stop(); // Arrête la lecture du son
    }
}
