using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public GameObject Explosion;
    public GameObject PowerUpIndicator;
    public bool HasPowerup = false;
    private Rigidbody2D _playerRb;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(horizontalInput, verticalInput);
        _playerRb.AddForce(direction * Speed);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("OoB"))
        {
            Instantiate(Explosion, transform.position, Explosion.transform.rotation);
            gameObject.SetActive(false);
            SceneManager.LoadScene(0);
        }

        if(other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            PowerUpIndicator.gameObject.SetActive(true);
            HasPowerup = true;
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        PowerUpIndicator.gameObject.SetActive(false);
        HasPowerup = false;
    }
}