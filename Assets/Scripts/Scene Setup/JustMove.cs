using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustMove : MonoBehaviour
{
    private Vector3 vector = Vector3.right;
    public float speed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = vector * speed;
    }
}
