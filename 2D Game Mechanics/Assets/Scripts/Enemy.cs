using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1;
    private Rigidbody2D _enemyRb;
    private GameObject _player;
    public GameObject enemyExplosion;

    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookDirection = (_player.transform.position - transform.position).normalized;

        _enemyRb.AddForce(lookDirection * Speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("OoB"))
        {
            Instantiate(enemyExplosion, transform.position, enemyExplosion.transform.rotation);
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}