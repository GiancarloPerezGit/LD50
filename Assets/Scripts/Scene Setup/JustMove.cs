using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustMove : MonoBehaviour
{
    public Vector3 vector;
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
