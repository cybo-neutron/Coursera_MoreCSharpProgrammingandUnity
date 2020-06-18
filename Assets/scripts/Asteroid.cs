using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float minImpulse;
    [SerializeField] float maxImpulse;

    [SerializeField] List<Sprite> spList;   

    Rigidbody2D rb;
    Vector2 forceDirection;
    float colliderRadius;

    float impulse;

    private void Awake()
    {
        Sprite randomsprite = spList[Random.Range(0, spList.Count)];
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = randomsprite;
    }
    void Start()
    {
        colliderRadius = GetComponent<CircleCollider2D>().radius;


        impulse = Random.Range(minImpulse, maxImpulse);
        forceDirection = new Vector2(Mathf.Cos(impulse), Mathf.Sin(impulse));
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(forceDirection * impulse, ForceMode2D.Impulse);



    }

    
    void FixedUpdate()
    {
        ScreenWrap();
    }

    void ScreenWrap()
    {
        if (transform.position.x - colliderRadius > ScreenUtils.ScreenRight)
            transform.position = new Vector3(ScreenUtils.ScreenLeft, transform.position.y, transform.position.z);
        if (transform.position.x + colliderRadius < ScreenUtils.ScreenLeft)
            transform.position = new Vector3(ScreenUtils.ScreenRight, transform.position.y, transform.position.z);
        if (transform.position.y - colliderRadius > ScreenUtils.ScreenTop)
            transform.position = new Vector3(transform.position.x, ScreenUtils.ScreenBottom, transform.position.z);
        if (transform.position.y + colliderRadius < ScreenUtils.ScreenBottom)
            transform.position = new Vector3(transform.position.x, ScreenUtils.ScreenTop, transform.position.z);

    }

   

}
