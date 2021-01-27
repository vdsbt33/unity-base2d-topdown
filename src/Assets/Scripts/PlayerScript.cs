using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode InteractKey;
    public KeyCode PauseKey;
    public KeyCode SelectKey;

    public bool isPaused = false;
    public Vector2 MovementSpeed = new Vector2(1f, 0.8f);
    public Collider2D Collider;
    

    #region Methods
    public void OnMove(Vector3 direction)
    {
        gameObject.transform.position += direction * Time.deltaTime;
    }

    public void OnPause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void OnInteract()
    {
        Debug.Log("OnInteract");
        Debug.Log($"LayerMask.NameToLayer('Interactible') = {1 << LayerMask.NameToLayer("Interactible")}");

        var startPoint = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        var castHits = Physics2D.Raycast(startPoint, new Vector2(0.1f, 0f), 0.1f, 1 << LayerMask.NameToLayer("Interactible"));
        Debug.DrawLine(startPoint, new Vector3(startPoint.x + 0.1f, startPoint.y, startPoint.z), Color.red, 120f);

        Debug.Log(castHits);
        Debug.Log(castHits.collider);
        if (castHits.collider != null)
        {
            Debug.Log("Hit collider:");
            Debug.Log(castHits.collider.gameObject.name);
        }
    }

    public void OnSelect()
    {
        Debug.Log("OnSelect");
    }
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var movementX = Input.GetKey(LeftKey) ? -1 : Input.GetKey(RightKey) ? 1 : 0;
        var movementY = Input.GetKey(UpKey) ? 1 : Input.GetKey(DownKey) ? -1 : 0;
        var movement = new Vector3(movementX * MovementSpeed.x, movementY * MovementSpeed.y, 0);
        //var movement = new Vector3(Input.GetAxis("Horizontal") * MovementSpeed, Input.GetAxis("Vertical") * MovementSpeed, 0);
        var isInteracting = Input.GetKeyDown(InteractKey);
        var isPausing = Input.GetKeyDown(PauseKey);
        var isSelecting = Input.GetKeyDown(SelectKey);

        if (isPausing)
        {
            OnPause();
        }

        if (isPaused)
        {
            return;
        }
        else if (movement != Vector3.zero)
        {
            OnMove(movement);
        }
        
        if (isInteracting)
        {
            OnInteract();
        }
        else if (isSelecting)
        {
            OnSelect();
        }
    }
    #endregion
}
