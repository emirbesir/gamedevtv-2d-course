using System.Collections;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] private float regularMoveSpeed = 5f;
    [SerializeField] private float boostedMoveSpeed = 10f;
    [SerializeField] private float turnSpeed = 200f;
    private float currentMoveSpeed;

    private void Start()
    {
        currentMoveSpeed = regularMoveSpeed;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * currentMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.up * currentMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, -turnSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Boost"))
        {
            currentMoveSpeed = boostedMoveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        currentMoveSpeed = regularMoveSpeed;
    }
}
