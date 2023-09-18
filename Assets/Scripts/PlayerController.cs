using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;
    private Rigidbody rb;
    private int count;

    public AudioClip soundPickup;
    private AudioSource audioSource;

    public TextMeshProUGUI timeText;
    public float startTime = 95f;
    public float timeLeft;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {   
        timeLeft = startTime;
        count = 0;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        SetCountText();
        timeLeft = startTime;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Update(){
        timeLeft -= Time.deltaTime;
        winText.text = "Time Left: " + timeLeft.ToString("F2");
        if(timeLeft<=0f){
            SceneManager.LoadScene("GameOver");
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 18)
        {
             SceneManager.LoadScene("Win");
        }
    }



    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {   
            audioSource.PlayOneShot(soundPickup);
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

}