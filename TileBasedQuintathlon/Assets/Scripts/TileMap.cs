
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TileMap : MonoBehaviour
{
    public int[,] tileIndicies; //the number associated with a tile, 0 for mountain, 1 for woods, 2 for paved road, 3 for pond, 4 for grass field

    //private Tile[,] tiles;
    private Tile[] tileOverlords;
    public int tileIndex;

    public GameObject[] prefabs; //mountain, woods, paved road, pond, grass field

    private GameObject tile;

    public int width, height;

    private AudioSource[] audioSources;

    public const float bpm = 90;
    public const float timeSignature = 4;
    public const float barsLength = 1;
    public const float loopPointMinutes = (barsLength * timeSignature) / bpm;
    public const float loopPointSeconds = loopPointMinutes * 60;
    private double startTime;
    private int nextSource;

    private int i, j; //save indicies into tileSounds array associated with the tile upon which the player is currently positioned

    void Start()
    {
        width = height = 10;
        tileIndicies = new int[width, height];
        //tiles = new Tile[width, height];
        tileOverlords = new Tile[prefabs.Length];
        PlaceTiles();
        i = j = -1; //this is the initial location of Player, outside the TileMap
        audioSources = GetComponents<AudioSource>();
        startTime = AudioSettings.dspTime;
        audioSources[0].Play();
        nextSource = 1;
    }

    public void PlaceTiles()
    {
        //Debug.Log("Placing tiles: " + width + " x " + height);

        // Place the "overlord" tiles since we don't need an audio source per tile instance on the map.
        for(int k = 0; k < prefabs.Length; k++)
        {
            tile = Instantiate(prefabs[k]);
            tile.GetComponent<SpriteRenderer>().enabled = false;
            tileOverlords[k] = tile.GetComponent<Tile>();
        }

        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                int index = Random.Range(0, prefabs.Length); //int Random.Range() will return an integer between 0 and prefabs.Length-1
                tile = Instantiate(prefabs[index], new Vector2(i, j), Quaternion.identity, transform);
                tile.GetComponents<AudioSource>().All(a => a.enabled = false);
                tileIndicies[i, j] = index;
            }
        }
    }

    private void Update()
    {
        // IMPORTANT!!!!!!!!!!!!!!
        // IMPORTANT!!!!!!!!!!!!!!
        // IMPORTANT!!!!!!!!!!!!!!
        // IMPORTANT!!!!!!!!!!!!!!
        // The entire premise of 2 audio sources is to switch between for minimal sound clipping/delays
        if (!audioSources[nextSource].isPlaying)
        {
            audioSources[nextSource].PlayScheduled(GetNextLoopTime());
            nextSource = 1 - nextSource;
        }
    }

    public void AdjustSound(int deltaX, int deltaY, Vector3 newPos)
    {
        if (i >= 0 && j >= 0 && i < width && j < height)
        {
            tileOverlords[tileIndex].StopAudioLoop();
        }


        //update indicies in response to WASD key presses
        i += deltaX;
        j += deltaY;

        if (i >= 0 && j >= 0 && i < width && j < height)
        {
            tileIndex = tileIndicies[i, j]; //used in Player to set action figure sprite for current tile
            tileOverlords[tileIndex].StartAudioLoop((float)((AudioSettings.dspTime - startTime) % loopPointSeconds));
        }
        else
        {
            tileIndex = 5; //last element in array of action figure sprites
        }

    }

    private double GetNextLoopTime()
    {
        return loopPointSeconds - (AudioSettings.dspTime - startTime) % loopPointSeconds + AudioSettings.dspTime;
    }
}
