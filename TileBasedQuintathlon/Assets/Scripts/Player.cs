using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public TileMap map;

    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    AudioSource audioSource;


    private void Start()
    {
        transform.position = new Vector3(-1, -1, 0);
        map.AdjustSound(0, 0, transform.position);  //NOTE: script execution order set to ensure TileMap Start() runs before this Player Start() method
        audioSource = GetComponent<AudioSource>();

        //sprites = new Sprite[6]; //initialized in the Inspector

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    private void Move(Vector2 input)
    {
        transform.position += new Vector3(Mathf.Floor(input.x), Mathf.Floor(input.y));
        map.AdjustSound(Mathf.FloorToInt(input.x), Mathf.FloorToInt(input.y), transform.position);
        spriteRenderer.sprite = sprites[map.tileIndex];
    }


    public void OnMove(InputAction.CallbackContext callback)
    {
        if (callback.phase == InputActionPhase.Performed)
        {
            Move(callback.ReadValue<Vector2>());
        }
    }
}