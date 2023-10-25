using UnityEngine;

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
       
    }
    
    void Update()
    {
        
       
      
        if (Input.GetKeyDown(KeyCode.A) && (transform.position.x > -1))
        {
            transform.position += Vector3.left; //more accurate than transform.Translate(Vector3.left);
            map.AdjustSound(-1, 0, transform.position);
            spriteRenderer.sprite = sprites[map.tileIndex];
        }
        if (Input.GetKeyDown(KeyCode.W) && (transform.position.y < map.height))
        {
            transform.position += Vector3.up; //more accurate than transform.Translate(Vector3.up);
            map.AdjustSound(0, +1, transform.position);
            spriteRenderer.sprite = sprites[map.tileIndex];
        }
        if (Input.GetKeyDown(KeyCode.S) && (transform.position.y > -1))
        {
            transform.position += Vector3.down; //more accurate than transform.Translate(Vector3.down);
            map.AdjustSound(0, -1, transform.position);
            spriteRenderer.sprite = sprites[map.tileIndex];
        }
        if (Input.GetKeyDown(KeyCode.D) && (transform.position.x < map.width))
        {
            transform.position += Vector3.right;  //more accurate than transform.Translate(Vector3.right);
            map.AdjustSound(+1, 0, transform.position);
            spriteRenderer.sprite = sprites[map.tileIndex];
        }

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

}