using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class G3Drop : MonoBehaviour, IDropHandler
{
    public int correctNumber; 
    [SerializeField] private Game3 _game3;

    public void OnDrop(PointerEventData eventData)
    {
        _game3.ActivateDrag(false);
        GameObject dragged = eventData.pointerDrag;
        if (dragged == null) return;

        int draggedNumber = int.Parse(dragged.GetComponentInChildren<TMP_Text>().text);

       
        dragged.transform.position = transform.position;
        dragged.transform.SetParent(transform);

      
        dragged.GetComponent<G3Drag>().MarkAsDroppedCorrectly();

   
        if (draggedNumber == correctNumber)
        {
            _game3.AddScore();
        }

      
        StartCoroutine(NextTask(dragged));
    }

    IEnumerator NextTask(GameObject dragged)
    {
        yield return new WaitForSeconds(1);

       
        G3Drag dragScript = dragged.GetComponent<G3Drag>();
        dragged.transform.position = dragScript.StartPosition;
        dragged.transform.SetParent(dragScript.OriginalParent);
        dragScript.MarkAsDroppedCorrectly(false);

        _game3.GenerateNewTask();
        _game3.ActivateDrag(true);
    }
}
