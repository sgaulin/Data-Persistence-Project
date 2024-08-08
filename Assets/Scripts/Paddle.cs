using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.25f;

    private MainManager mainManager;
    
    // Start is called before the first frame update
    void Start()
    {
        mainManager = GameManager.FindObjectOfType<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mainManager.m_GameOver && (GameManager.Instance != null ? GameManager.Instance.isGameActive : true))
        {
            float input = Input.GetAxis("Horizontal");

            Vector3 pos = transform.position;
            pos.x += input * Speed * Time.deltaTime;

            if (pos.x > MaxMovement)
                pos.x = MaxMovement;
            else if (pos.x < -MaxMovement)
                pos.x = -MaxMovement;

            transform.position = pos;
        }
        else if (mainManager.m_GameOver)
        {
            //ResetPaddle();
        }
        
    }

    void ResetPaddle()
    {
        transform.localPosition = new Vector3(0,0.4f,0);
        transform.localScale = new Vector3(5.1f,0.15f,1);
        
    }


}
