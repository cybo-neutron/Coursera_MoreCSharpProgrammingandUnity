using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ship
/// </summary>
public class Ship : MonoBehaviour
{
    // thrust and rotation support
    Rigidbody2D rb2D;
    Vector2 thrustDirection = new Vector2(1, 0);
    const float ThrustForce = 10;
    const float RotateDegreesPerSecond = 180;

    // screen wrapping support
    float colliderRadius;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		// saved for efficiency
        rb2D = GetComponent<Rigidbody2D>();
        colliderRadius = GetComponent<CircleCollider2D>().radius;
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
        // check for rotation input
        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0) {

            // calculate rotation amount and apply rotation
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0) {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // change thrust direction to match ship rotation
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }
	}

    /// <summary>
    /// FixedUpdate is called 50 times per second
    /// </summary>
    void FixedUpdate()
    {
        // thrust as appropriate
        if (Input.GetAxis("Thrust") != 0)
        {
            rb2D.AddForce(ThrustForce * thrustDirection,
                ForceMode2D.Force);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.transform.CompareTag("Asteroid"))
            {
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// Called when the game object becomes invisible to the camera
    /// </summary>
}





