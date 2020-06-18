using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float thrust;
    Rigidbody2D rb;
    Vector2 bcSize;
    Vector2 forceDirection;

    // Start is called before the first frame update
    void Start()
    {
        bcSize = GetComponent<BoxCollider2D>().size;
        rb = GetComponent<Rigidbody2D>();
        forceDirection = new Vector2(transform.rotation.x,transform.rotation.y);
        rb.AddForce(thrust * forceDirection, ForceMode2D.Impulse);
        Destroy(this.gameObject, 2.5f);

    }

    // Update is called once per frame
    void Update()
    {
        ScreenWrap();
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Asteroid"))
        {

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    void ScreenWrap()
    {
        if (transform.position.x - bcSize.x > ScreenUtils.ScreenRight)
            transform.position = new Vector3(ScreenUtils.ScreenLeft, transform.position.y, transform.position.z);
        if (transform.position.x + bcSize.x < ScreenUtils.ScreenLeft)
            transform.position = new Vector3(ScreenUtils.ScreenRight, transform.position.y, transform.position.z);
        if (transform.position.y - bcSize.y > ScreenUtils.ScreenTop)
            transform.position = new Vector3(transform.position.x, ScreenUtils.ScreenBottom, transform.position.z);
        if (transform.position.y + bcSize.y < ScreenUtils.ScreenBottom)
            transform.position = new Vector3(transform.position.x, ScreenUtils.ScreenTop, transform.position.z);

    }
}
