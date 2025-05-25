using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class G3Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup _canvasGroup;
    private Vector3 _startPosition;
    private Transform _originalParent;
    private Image _image;

    public Vector3 StartPosition => _startPosition;
    public Transform OriginalParent => _originalParent;

    private bool _isDraggable = true;
    private bool _wasDroppedCorrectly = false;

    void Start()
    {
        _image = GetComponent<Image>();
        _startPosition = transform.position;
        _originalParent = transform.parent;

        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_isDraggable) return;

        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDraggable) return;

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (!_wasDroppedCorrectly)
        {
            transform.position = _startPosition;
            transform.SetParent(_originalParent);
        }
    }

    public void SaveStartPosition()
    {
        _startPosition = transform.position;
        _originalParent = transform.parent;
    }

    public void SetActiv(bool isActiv)
    {
        _isDraggable = isActiv;
        _image.raycastTarget = isActiv;
    }

    public void MarkAsDroppedCorrectly(bool state = true)
    {
        _wasDroppedCorrectly = state;
    }
}
