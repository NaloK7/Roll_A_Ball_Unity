using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    // public int speedBall;

    // Start is called before the first frame update
    void Start()
    {
        // speedBall = 2;
        maxCoins = FindObjectsOfType<RotateCoin>().Length; // compte le nombre de cylinder by rotate func
        CoinSounds = GetComponents<AudioSource>();
        oneCoinSound = CoinSounds[0];
        allCoinSound = CoinSounds[1];
        score = 0;
        hitTime = 0.15f;
        initColor = GetComponent<MeshRenderer>().material.color;
        isJumping = false;
        //Debug.Log(maxCoins);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))  // avancer
        {
            GetComponent<Rigidbody>().AddForce(Vector3.forward * 2);
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) // gauche
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * 2);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))  // reculer
        {
            GetComponent<Rigidbody>().AddForce(Vector3.back * 2);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))  // droite
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * 2);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)  // sauter
        {
            isJumping = !isJumping;
            GetComponent<Rigidbody>().AddForce(Vector3.up * 200);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // ajouter un composant audio dans les coins
        // GetComponent.audioSource.play()
        //Debug.Log("Je passe par l�");
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
                allCoinSound.Play();
                finish.text = "Finish";
                Time.timeScale = 0;
            }
            else
            {
                oneCoinSound.Play();
            }


        }
    }
    IEnumerator ChangeColor()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(hitTime);
        gameObject.GetComponent<MeshRenderer>().material.color = initColor;

    }
}
