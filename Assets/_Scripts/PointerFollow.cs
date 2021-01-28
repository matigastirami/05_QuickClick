using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFollow : MonoBehaviour
{

    private Camera _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        // It's needed to use z = 0 because if not, the camera won't be able to see the mouse trail
        mousePos = new Vector3(mousePos.x, mousePos.y);

        transform.position = mousePos;
    }
}
