using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float Smoothing;

    private Vector2 _origin;
    private Vector2 _direction;
    private Vector2 _smoothDirection;
    private bool _touched;
    private int _pointerId;

    private void Awake()
    {
        _direction = Vector2.zero;
        _touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_touched)
        {
            _touched = true;
            _pointerId = eventData.pointerId;
            _origin = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId == _pointerId)
        {
            Vector2 currentPosition = eventData.position;
            Vector2 directionRaw = currentPosition - _origin;
            _direction = directionRaw.normalized;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == _pointerId)
        {
            _direction = Vector2.zero;
            _touched = false;
        }
    }

    public Vector2 GetDirection()
    {
        _smoothDirection = Vector2.MoveTowards(_smoothDirection, _direction, Smoothing);
        return _smoothDirection;
    }
}
