using UnityEngine;

public class DestroyByBoundery : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
