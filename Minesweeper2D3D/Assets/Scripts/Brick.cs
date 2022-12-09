using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Brick : MonoBehaviour
{    
    private static Dictionary<string, Sprite> imageTile;
    public bool mine = false;
    public float radius = 1.42f;
    public SpriteRenderer tile = null;
    public List<Brick> surrounding;
    public bool revealed = false;
    public bool brickTile=false;

    //public int numBrickRevealed = 0;
    // public int numMine = 0;
    //public bool boardRevealed = false;

    public static void BuildSpritesMap()
    {
        if (imageTile == null) {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/MinesweeperSprites");
            imageTile = new Dictionary<string, Sprite>();
            for (int i = 0; i < sprites.Length; i++) {
                imageTile.Add(sprites[i].name, (Sprite) sprites[i]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BuildSpritesMap();
       // var unitBricks = GameObject.FindGameObjectsWithTag("Brick");
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

    public void flag() {
        name = "TileFlag";
        Sprite sprite;
        if (imageTile.TryGetValue(name, out sprite)){
            tile.sprite = sprite;
        }
    }

    public void revealBrick()
    {
        // if (revealed) { 
        //     numBrickRevealed++;
        //     return;
        // }

        //revealed = true;

        string name;

        if (mine) 
        {
            name = "TileMine";
        } 
        else 
        {
            int num = 0;
            surrounding.ForEach(brick => {
                if (brick.mine) num += 1;
            });
            name = $"Tile{num}";
        }

        Sprite sprite;
        if (imageTile.TryGetValue(name, out sprite)){
            tile.sprite = sprite;
        }
    }

    public void boardCleared()
    {

        
        // surrounding.ForEach(brick => 
        // {
        //     if ( (brick.mine == false) && (brick.revealed == true) )
        //     {
        //         numBrickRevealed++;
        //     } 
        // });

        

        // for (int i=0; i<unitBricks.Length; i++){
        //     if ( (unitBricks[i].mine == false) && (unitBricks[i].revealed == true) ){
        //         numBrickRevealed++;
        //     }
        // }

        // if (numBrickRevealed == 400){
        //     boardRevealed = true;
        // }
        //Debug.Log("Here!!! Now!!!" + boardRevealed);
    }
}
