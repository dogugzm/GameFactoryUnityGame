using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _patrolRange;

    private Vector3 _initialPosition;
    private Vector3 _minPatrolPosition;
    private Vector3 _maxPatrolPosition;

    private Vector3 _destinationPoint;
    

    private void Awake()
    {
        _initialPosition = transform.position;
        _minPatrolPosition = _initialPosition + Vector3.left * _patrolRange;            // yön olarak sola gitmesini gösteriyo vektörel şekilde (fiziksel vektör olarak tam oturtamadım)
        _maxPatrolPosition = _initialPosition + Vector3.right * _patrolRange;

        SetDestination(_maxPatrolPosition);      // başlar başlamaz sağa doğru gitsin
    }

    private void SetDestination(Vector3 destination)
    {
        _destinationPoint = destination;
    }


    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(Vector3.Distance(transform.position , _maxPatrolPosition)) < 0.1f)                 // eğer sağa gidebielceğim max noktaya yaklaşırsam sola dön
        {
            SetDestination(_minPatrolPosition);
        }
        else if (Math.Abs(Vector3.Distance(transform.position , _minPatrolPosition)) < 0.1f)
        {
            SetDestination(_maxPatrolPosition);
        }

        transform.position = Vector3.MoveTowards(transform.position, _destinationPoint, Time.deltaTime * _moveSpeed);      // iki nokta arasında belli hızda gidip gelmemi sağlıyor
         
    }

    /**private void OnCollisionEnter(Collision other)
    {
        BallController ballController = other.collider.GetComponent<BallController>();                          // player ın colliderina erişiyorum.
        bool isPlayer = ballController != null;            // player olduğuna emin oluyorum

        if (isPlayer)                 // sadece player için çalışıacak fonksiyon 
        {
            ballController.Die();
        }
    }**/

    public void  Die()
    {
        Destroy(gameObject);
    }
}
