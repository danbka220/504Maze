using UnityEngine;

public class Wall : MonoBehaviour
{
    private BoxCollider2D _collider;
    private RectTransform _rect;

    private void Start()
    {
        TryGetComponent(out _collider);
        TryGetComponent(out _rect);
    }

    private void LateUpdate()
    {
        if(_collider.size != _rect.rect.size)
            _collider.size = _rect.rect.size;
    }
}
