using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SwitchingCharacter : MonoBehaviour
{
    int selectedChar;
    int previousChoice;
    Vector3 saveSelected;

    public CinemachineFreeLook camChar;

    private GameObject selectedObj;

    // Start is called before the first frame update
    void Start()
    {
        saveSelected = transform.position;
        selectedChar = 0;
        previousChoice = selectedChar;
        SelectedCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManager.isPlaying)
        {
            previousChoice = selectedChar;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (selectedChar == 0)
                    selectedChar = 2;
                else selectedChar--;

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (selectedChar == 2)
                    selectedChar = 0;
                else selectedChar++;
            }

            if (previousChoice != selectedChar)
            {
                saveSelected = transform.GetChild(previousChoice).position;
                SelectedCharacter();
            }

            transform.position = selectedObj.transform.position;
        }
    }

    void SelectedCharacter()
    {
        int i = 0;
        foreach(Transform chara in transform)
        {
            if (i == selectedChar)
            {
                chara.gameObject.transform.position = saveSelected; 
                chara.gameObject.SetActive(true);
                camChar.Follow = chara;
                camChar.LookAt = chara;
                selectedObj = chara.gameObject;
                
            }
                
            else
                chara.gameObject.SetActive(false);
            i++;
        }
    }
}
