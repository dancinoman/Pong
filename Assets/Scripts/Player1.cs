using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player1 is typically the standard first player of the game which is the player
public class Player1 : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;
    public float yMin;
    public float yMax;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Binding movements with mouse control
        if (Input.GetMouseButton(0))
        {
            float verticalMove = Input.GetAxis("Mouse Y");
            float forceMovement = verticalMove * Time.deltaTime * speed;
            float rawMovement = transform.position.y + forceMovement;
            float clampedMovement = Mathf.Clamp(rawMovement, yMin, yMax);
            transform.position = new Vector2(transform.position.x, clampedMovement);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
            rb2d.velocity = Vector2.zero;
        }
    }
}
