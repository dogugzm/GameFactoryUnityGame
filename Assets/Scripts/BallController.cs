using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour

{
    [SerializeField]
    private float _moveSpeed = 1.0f;
    [SerializeField]
    private float _jumpSpeed = 5.0f;

    private bool _isGrounded;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Move(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Move(Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            Jump();
        }
    }

    private void Jump()
    {

        if (!_isGrounded)
        {
            return;
        }
        _rigidbody.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
    }

    private void Move(Vector3 _direction)
    {
        _rigidbody.AddForce(_direction*_moveSpeed, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision other)
    {
        _isGrounded = true;

        bool hasCollidedWithEnemy = other.collider.GetComponent<Enemy>();
     

        if (hasCollidedWithEnemy)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                bool isTopOnEnemy = enemy != null;

                if (isTopOnEnemy)
                {
                    enemy.Die();
                }
                else
                {
                    Die();
                }
            }

            Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.blue,5f);
        }
        

    }
    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Collectible collectible =  other.GetComponent<Collectible>();
        bool isCollectible = collectible != null;
        
        
        if (isCollectible)
        {
            collectible.Collect();
        }
    }

    public void Die()                        // enemy ile collison olunca 
    {
        StartCoroutine(ChangeScene());

        GetComponent<MeshRenderer>().enabled = false;    // direk destroy edemiyoruz bunun yerine basit çözüm olarak mesh i disable yaptık.
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.0f);                   // 1 saniye gecikme ile main scene ı yükle 

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 
}


