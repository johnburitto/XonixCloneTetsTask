using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 _dragDirection;
    private static SwipeDirection _direction = SwipeDirection.None;

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragDirection = (eventData.position - eventData.pressPosition).normalized;
        _direction = GetDirection();
    }

    private SwipeDirection GetDirection()
    {
        float positiveX = Mathf.Abs(_dragDirection.x);
        float positiveY = Mathf.Abs(_dragDirection.y);

        if (positiveX > positiveY)
        {
            return _dragDirection.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
        }
        else
        {
            return _dragDirection.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
        }
    }

    public static bool SwipeUp()
    {
        return _direction == SwipeDirection.Up;
    }

    public static bool SwipeDown()
    {
        return _direction == SwipeDirection.Down;
    }

    public static bool SwipeLeft()
    {
        return _direction == SwipeDirection.Left;
    }

    public static bool SwipeRight()
    {
        return _direction == SwipeDirection.Right;
    }
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right,
    None
}
