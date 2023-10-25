
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TileMap: MonoBehaviour
{
    private AudioSource[,] tileSounds; //each tile has its own AudioSource

    public int[,] tileIndicies; //the number associated with a tile, 0 for mountain, 1 for woods, 2 for paved road, 3 for pond, 4 for grass field

    public int tileIndex;

    public GameObject[] prefabs; //mountain, woods, paved road, pond, grass field

    private GameObject tile;

    public int width, height;

    private AudioSource outsideTileMapAudioSource; //holds the "prelude to a TBQ" song that is heard when player is outside the TileMap

    private int i, j; //save indicies into tileSounds array associated with the tile upon which the player is currently positioned

    void Start()
    {
        outsideTileMapAudioSource = GetComponent<AudioSource>();

        width = height = 10;

        tileSounds = new AudioSource[width, height];

        tileIndicies = new int[width, height];

        PlaceTiles();

        i = j = -1; //this is the initial location of Player, outside the TileMap
    }

    public void PlaceTiles()
    {
        //Debug.Log("Placing tiles: " + width + " x " + height);

        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                int index = Random.Range(0, prefabs.Length); //int Random.Range() will return an integer between 0 and prefabs.Length-1
                tile = Instantiate(prefabs[index]);
                tile.transform.SetParent(transform);
                tile.transform.position = new Vector3(i, j, 0);
                tileSounds[i, j] = tile.GetComponent<AudioSource>();
                tileIndicies[i, j] = index;
            }
        }
    }

    public void AdjustSound(int deltaX, int deltaY, Vector3 newPos)
    {
        if (i >= 0 && j >= 0 && i < width && j < height)
        {
           //player was previously inside TileMap, so stop the sound from it
           tileSounds[i, j].Stop();
        }

        //update indicies in response to WASD key presses
        i += deltaX;
        j += deltaY;

        if (i < 0 || j < 0 || i > width - 1 || j > height - 1)
        {
            //player is currently outside the TileMap, but don't restart "prelude to a TBQ" if it is already playing
            if (!outsideTileMapAudioSource.isPlaying)
                outsideTileMapAudioSource.Play();
            tileIndex = 5; //last element in array of action figure sprites
            return;
        }

        //player is inside the TileMap, so stop "prelude to a TBQ" if it is playing
        if (outsideTileMapAudioSource.isPlaying)
            outsideTileMapAudioSource.Stop();

        //player is inside the TileMap, so start the sound from it
        tileSounds[i, j].Play();

        tileIndex = tileIndicies[i, j]; //used in Player to set action figure sprite for current tile
    }
}
