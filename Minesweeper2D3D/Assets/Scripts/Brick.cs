using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Brick : MonoBehaviour
{    
    private static Dictionary<string, Sprite> mTileImages;

    public bool mine = false;

    public float radius = 1.42f;

    public SpriteRenderer tile = null;

    public List<Brick> surrounding;

    private bool revealed = false;

    public static void BuildSpritesMap()
    {
        if (mTileImages == null) {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/MinesweeperSprites");
            mTileImages = new Dictionary<string, Sprite>();
            for (int i = 0; i < sprites.Length; i++) {
                mTileImages.Add(sprites[i].name, (Sprite) sprites[i]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BuildSpritesMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnValidate()
    {
        FindNeighbors();
    }

    private void FindNeighbors()
    {
        var allBricks = GameObject.FindGameObjectsWithTag("Brick");

        surrounding = new List<Brick>();

        for (int i = 0; i < allBricks.Length; i++) {
            var brick = allBricks[i];
            var distance = Vector3.Distance(transform.position, brick.transform.position);
            if (0 < distance && distance <= radius) {
                surrounding.Add(brick.GetComponent<Brick>());
            }
        }

        Debug.Log($"{surrounding.Count} neighbors");
    }

    public void revealBrick()
    {
        if (revealed) return;

        revealed = true;

        string name;

        if (mine) {
            name = "TileMine";
        } else {
            int num = 0;
            surrounding.ForEach(brick => {
                if (brick.mine) num += 1;
            });
            name = $"Tile{num}";
        }

        Sprite sprite;
        if (mTileImages.TryGetValue(name, out sprite))
            tile.sprite = sprite;
    }
}
