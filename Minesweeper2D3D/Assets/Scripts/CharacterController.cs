using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public int blood = 5;
    public GameObject[] hearts;

    bool flag = false;

    private NavMeshAgent mMeshAgent;

    private Brick mPreviousBrick;

    private Brick mCurrentBrick;

    // Start is called before the first frame update
    void Start()
    {
        mMeshAgent = GetComponent<NavMeshAgent>();
        Debug.Log("This is a test");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                GameObject hitObject = hit.transform.gameObject;
                Brick brick = hitObject.GetComponent<Brick>();
                if (brick != null) {
                    mMeshAgent.SetDestination(hit.transform.position);
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
            GameObject hitObject = hit.transform.gameObject;
            Brick brick = hitObject.GetComponent<Brick>();
            if (brick != null) {
                brick.ShowSecret();
                
                if (brick != mCurrentBrick) {
                    mPreviousBrick = mCurrentBrick;
                    if(!brick.mine)
                    {
                        mCurrentBrick = brick;
                    }
                }

                if (brick.mine && mPreviousBrick != null) {
                    mMeshAgent.SetDestination(mPreviousBrick.transform.position);
                    flag = true;
                    Debug.Log("Here!!! Now!!!" + blood);

                }

                else if(flag == true)
                {
                    blood--;

                    Destroy(hearts[blood].gameObject);


                    if(blood <= 0)
                    {
                        Debug.Log("should quit!!!");
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        //UnityEditor.EditorApplication.isPlaying = false;
                    }

                    flag = false;
                }

                
            }
        }
    }
}
