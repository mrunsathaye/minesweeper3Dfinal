using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public int lives = 5;
    public GameObject[] hearts;
    bool flag = false;
    public int gameTries = 1;    
    private NavMeshAgent mesh;
    private Brick prevPosition;
    private Brick currentPosition;
    public int numBrickRevealed = 0;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<NavMeshAgent>();
        Debug.Log("This is a test");

    
    }

    void Awake() 
    {
        if (PlayerPrefs.HasKey ("Num Lives"))
        { 
            gameTries = PlayerPrefs.GetInt("Num Lives");
        }
        else
        {
            Save();
        }
        
    }
    void Save()
    {
        PlayerPrefs.SetInt("Num Lives", gameTries);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                GameObject objTouched = hit.transform.gameObject;
                Brick brick = objTouched.GetComponent<Brick>();
                if (brick != null) {
                    mesh.SetDestination(hit.transform.position);
                }
            }
        }

        DetectMine();
    }


    private void DetectMine()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.2f, out hit)) {
            GameObject objTouched = hit.transform.gameObject;
            Brick brick = objTouched.GetComponent<Brick>();
            if (brick != null) {
                brick.revealBrick();
                
                if (brick.revealed == true){
                    numBrickRevealed++;
                }

                if (brick != currentPosition) {
                    prevPosition = currentPosition;
                    if(!brick.mine)
                    {
                        currentPosition = brick;
                    }
                }

                if (brick.mine && prevPosition != null) {
                    mesh.SetDestination(prevPosition.transform.position);
                    flag = true;
                    //Debug.Log("Here!!! Now!!!" + lives);
                }

                else if(flag == true)
                {
                    lives--;
                    Destroy(hearts[lives].gameObject);

                    if( lives <= 0)
                    {
                        gameTries++;
                        Save();
                        Debug.Log("tried amount: "+ gameTries);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        //UnityEditor.EditorApplication.isPlaying = false;
                    }

                    // if (brick.boardRevealed == true){
                    //     UnityEditor.EditorApplication.isPlaying = false;
                    // }
                    flag = false;
                }

                
            }
        }
    }
}
