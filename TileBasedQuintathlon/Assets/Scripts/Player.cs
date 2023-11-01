using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public TileMap map;
    private Animator animator;


    private void Start()
    {
        transform.position = new Vector3(-1, -1, 0);
        map.AdjustSound(0, 0, transform.position);  //NOTE: script execution order set to ensure TileMap Start() runs before this Player Start() method

        //sprites = new Sprite[6]; //initialized in the Inspector

        animator = GetComponent<Animator>();
    }

    private void Move(Vector2 input)
    {
        // Move position
        transform.position += new Vector3(Mathf.Floor(input.x), Mathf.Floor(input.y));
        map.AdjustSound(Mathf.FloorToInt(input.x), Mathf.FloorToInt(input.y), transform.position);

        // Set animation state
        animator.SetInteger("animation", map.tileIndex);
    }


    public void OnMove(InputAction.CallbackContext callback)
    {
        if (callback.phase == InputActionPhase.Performed)
        {
            Move(callback.ReadValue<Vector2>());
        }
    }
}