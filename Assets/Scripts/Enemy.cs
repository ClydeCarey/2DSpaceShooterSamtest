﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    private float _speed = 4.0f;

    private Player _player;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The animator is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
                
        if(transform.position.y <-5.0f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7, 0);
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)   
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player !=null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            //_speed = 0;
            transform.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 2.0f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }

            _anim.SetTrigger("OnEnemyDeath");
            //_speed = 0;
            transform.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject,2.0f);
        }
    }
    
}
