using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float ScrollSpeed;
    public float TileSizeZ;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * ScrollSpeed, TileSizeZ);
        transform.position = _startPosition + Vector3.forward * newPosition;
    }
}
