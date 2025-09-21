using UnityEngine;

public class Delivery : MonoBehaviour
{
    private bool hasPackage = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Pickup"))
        {
            hasPackage = true;
            Destroy(col.gameObject);
        }
        if (col.CompareTag("Customer") && hasPackage)
        {
            hasPackage = false;
            Destroy(col.gameObject);
        }
    }
}
