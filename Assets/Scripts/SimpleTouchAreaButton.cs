using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _touched;
    private int _pointerId;

    private void Awake()
    {
        _touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_touched)
        {
            _touched = true;
            _pointerId = eventData.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == _pointerId)
        {
            _touched = false;
        }
    }

    public bool CanFire()
    {
        return _touched;
    }
}
