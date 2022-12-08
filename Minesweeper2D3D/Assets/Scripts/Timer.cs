using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    CharacterController penguin;//= new CharacterController();
    [SerializeField] GameObject characterOld;
    public TextMeshProUGUI displayText;

    // Start is called before the first frame update
    void Awake()
    {
       penguin = characterOld.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       displayText.text = "Trial: "+ penguin.gameTries.ToString();
    }
}
