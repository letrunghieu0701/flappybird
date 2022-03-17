using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoving : MonoBehaviour
{
    [SerializeField] float velocity = 0.2f;

    Rigidbody2D rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity_vec = new Vector2(-velocity, 0);
        rb.velocity = velocity_vec * Time.deltaTime;
        // rb.AddRelativeForce(velocity_vec * Time.deltaTime);
    }
}
