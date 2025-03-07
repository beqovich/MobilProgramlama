﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    public float moveSpeed;
    public float rotateAmount;
    float rot;
    int score;
    public GameObject winText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos.x < 0)
            {
                rot = rotateAmount;
            }
            else
            {
                rot = -rotateAmount;
            }

            transform.Rotate(0, 0, rot);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            score++;

            if (score >= 5)
            {
                print("Level Complete");
                winText.SetActive(true);
            }

        }
        else if (collision.gameObject.tag == "Danger")
        {
            SceneManager.LoadScene("Game");
        }
    }


}
