using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool isDragging = false;
    private Vector2 mousePosition;
    private Vector2 dragStartPosition;

    [SerializeField] private float maxDistance = 5f;  
    [SerializeField] private float maxForce = 10f;
    [SerializeField] private float minForce;

    [SerializeField] private AudioClip _clip;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
    }

    void Update()
    {
     
        if (Input.GetMouseButtonDown(0)) 
        {
          
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<Collider2D>().OverlapPoint(mouseWorldPosition))
            {
                isDragging = true;
                dragStartPosition = mouseWorldPosition;  
                
            }
        }

        if (isDragging)
        {
         
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - dragStartPosition;
            float distance = Mathf.Min(direction.magnitude, maxDistance); 
            rb2d.position = dragStartPosition + direction.normalized * distance;

           
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 directionshot = (mousePosition - dragStartPosition).normalized;
                float pullAmount = Mathf.Clamp((mousePosition - dragStartPosition).magnitude, 0f, maxDistance);
                float t = pullAmount / maxDistance;

      
                
                float finalForceMagnitude = Mathf.Lerp(minForce, maxForce, t);

                Vector2 force = directionshot * finalForceMagnitude * -1f;
                rb2d.AddForce(force, ForceMode2D.Impulse);
                rb2d.gravityScale = 1f;
                isDragging = false;

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.Instance.PlaySFX(_clip);
    }

    public void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 0f;
    }
}
