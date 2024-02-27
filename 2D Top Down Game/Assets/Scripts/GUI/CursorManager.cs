using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();  
    }

    void Start()
    {
        Cursor.visible = false;

        if (Application.isPlaying)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Input.mousePosition;
        image.rectTransform.position = cursorPos;


        //REMOVE FOR REAL GAME
        //EDITOR ONLY WORKAROUND FOR CURSOR VISBILITY
        if(!Application.isPlaying) { return; }

        Cursor.visible = false;
        //END REMOVE SECTION :)
    }
}
