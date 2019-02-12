using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Camera mainCam;
    public Camera UICam;

    //Cullen
    private const float UI_PERCENT_WIDTH = .25f;
    private const float TARGET_TOTAL_WIDTH = 1920f;
    private const float TARGET_WIDTH = TARGET_TOTAL_WIDTH  * (1f-UI_PERCENT_WIDTH);
    private const float TARGET_HEIGHT = 1080f;
    private const float ASPECT = TARGET_TOTAL_WIDTH / TARGET_HEIGHT; //aspect
    private const float CAM_ASPECT = TARGET_WIDTH / TARGET_HEIGHT;
    private static readonly float TARGET_SIZE = 45f;
    public static readonly Vector2 WORLD_MAX = new Vector2(TARGET_SIZE * CAM_ASPECT, TARGET_SIZE);
    //private const float V_WIDTH_PERCENT = .7f;     //percent of screen width to make viewport (EVERYTHING BASED ON THIS)


    //Cullen
    private float adjustRatio;                      //adjustment for zoom for scren that isn't target width
    private float minSize, maxSize;                 //orthographic size bounds for zoom
    private float scrollSen = 10f;                  //scroll sensitivity
    private Vector3 mouseClickPos;                  //for click & drag
    //private bool pressed = false;                   //for click & drag

    // Start is called before the first frame update
    void Start() {

        //Cullen
        //mainCam = GetComponent<Camera>();
        //UICam = GetComponentInChildren<Camera>();
        //cam = (Camera)GameObject.FindObjectOfType(typeof(Camera));

        //Cullen
        float pixelHeight = Screen.height;
        float pixelWidth = pixelHeight * CAM_ASPECT;
        mainCam.pixelRect = new Rect((Screen.width - (pixelHeight * ASPECT)) / 2f, 0, pixelWidth, pixelHeight);
        UICam.pixelRect = new Rect((Screen.width - (pixelHeight * ASPECT)) / 2f + pixelWidth, 0, pixelWidth * (TARGET_TOTAL_WIDTH/TARGET_WIDTH)* UI_PERCENT_WIDTH, pixelHeight);

        //Cullen
        if (mainCam.pixelWidth < pixelWidth) {
            //calculate by width instead of height
            pixelWidth = Screen.width * (TARGET_WIDTH / TARGET_TOTAL_WIDTH);
            pixelHeight = Screen.width/ASPECT;
            mainCam.pixelRect = new Rect(0, (Screen.height - (pixelHeight)) / 2f, pixelWidth, pixelHeight);
            UICam.pixelRect = new Rect((Screen.width - (pixelHeight * ASPECT)) / 2f + pixelWidth, (Screen.height - (pixelHeight)) / 2f,
                pixelWidth * UI_PERCENT_WIDTH, pixelHeight);
        }

        maxSize = mainCam.orthographicSize = TARGET_SIZE;
        minSize = .25f * maxSize;
    }

    // Update is called once per frame
    void LateUpdate() {
        //Cullen
        if (mainCam.pixelRect.Contains(Input.mousePosition)) {
            //Cullen
            //Get zoom in/out and multiply by sensitivity
            float zoom = -Input.GetAxis("Mouse ScrollWheel") * scrollSen;
            //calculate amount camera should move to create "zoom towards mouse" effect
            float moveMultiplier = (-zoom / mainCam.orthographicSize);

            //only do something if zoom will actually change (prevents the camera just moving towards/away from the mouse if already max/min zoom
            if (mainCam.orthographicSize + zoom >= minSize && mainCam.orthographicSize + zoom <= maxSize) {
                //move camera towards/away from mouse
                mainCam.transform.position += (mainCam.ScreenToWorldPoint(Input.mousePosition) - mainCam.transform.position) * moveMultiplier;
                clampCameraPos(); // adjust cam position
                //adjust and clamp camera zoom
                mainCam.orthographicSize = Mathf.Clamp(mainCam.orthographicSize + zoom, minSize, maxSize);
            } 

            //Cullen
            if (Input.GetMouseButtonDown(0)) {
                //set world point where dragging started
                mouseClickPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            } else if (Input.GetMouseButton(0) && (mainCam.ScreenToWorldPoint(Input.mousePosition) - mouseClickPos).magnitude > .25f) {
                //Calculate camera movment based on drag
                mainCam.transform.position = mainCam.transform.position - (mainCam.ScreenToWorldPoint(Input.mousePosition) - mouseClickPos);
                //keep camera in bounds
                clampCameraPos();
            }
        }
    }

    //Cullen
    private void clampCameraPos() {
        //keep border of viewport within calculated boundary based on current zoom
        //width of camera field = orthographicSize * ASPECT in each direction
        float x = Mathf.Clamp(mainCam.transform.position.x, -WORLD_MAX.x + mainCam.orthographicSize * CAM_ASPECT, WORLD_MAX.x - mainCam.orthographicSize * CAM_ASPECT);
        //height of camera field = orthographicSize in each direction
        float y = Mathf.Clamp(mainCam.transform.position.y, -WORLD_MAX.y + mainCam.orthographicSize, WORLD_MAX.y - mainCam.orthographicSize);

        mainCam.transform.position = new Vector3(x, y, -10);
    }

    //Cullen
    public bool inWorld(Vector3 position) {
        if (position.x < -WORLD_MAX.x || position.x > WORLD_MAX.x || position.y < -WORLD_MAX.y || position.y > WORLD_MAX.y) {
            return false;
        }
        return true;
    }
}
