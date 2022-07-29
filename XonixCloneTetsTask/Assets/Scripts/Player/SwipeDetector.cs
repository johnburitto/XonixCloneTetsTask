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
        if (_direction == SwipeDirection.Up)
        {
            _direction = SwipeDirection.None;

            return true;
        }

        return false;
    }

    public static bool SwipeDown()
    {
        if (_direction == SwipeDirection.Down)
        {
            _direction = SwipeDirection.None;

            return true;
        }

        return false;
    }

    public static bool SwipeLeft()
    {
        if (_direction == SwipeDirection.Left)
        {
            _direction = SwipeDirection.None;

            return true;
        }

        return false;
    }

    public static bool SwipeRight()
    {
        if (_direction == SwipeDirection.Right)
        {
            _direction = SwipeDirection.None;

            return true;
        }

        return false;
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
