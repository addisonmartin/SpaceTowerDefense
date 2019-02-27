using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CULLEN
public class CameraController : MonoBehaviour {

    //Cullen
    public Camera mainCam;
    public Camera UICam;
    public Canvas hudCanvas;

    //Cullen
    public static readonly float UI_PERCENT_WIDTH = .25f;                                        //percent of screen width to dedicate to ui
    public static readonly float TARGET_TOTAL_WIDTH = 1920f;                                     //target width of main camera and ui camera together
    public static readonly float TARGET_WIDTH = TARGET_TOTAL_WIDTH * (1f - UI_PERCENT_WIDTH);    //target width of just main camera
    public static readonly float TARGET_HEIGHT = 1080f;
    public static readonly float ASPECT = TARGET_TOTAL_WIDTH / TARGET_HEIGHT;    //aspect of whole screen
    public static readonly float CAM_ASPECT = TARGET_WIDTH / TARGET_HEIGHT;      //aspect of main camera
    private static readonly float TARGET_SIZE = 45f;                    //orthographic zoom level
    public static readonly Vector2 WORLD_MAX = new Vector2(TARGET_SIZE * CAM_ASPECT, TARGET_SIZE);  //Vector2 holding the maximum world coordinates (use negatives to get min coords)

    //Cullen
    private float adjustRatio;                      //adjustment for zoom for scren that isn't target width
    private float minSize, maxSize;                 //orthographic size bounds for zoom
    private float scrollSen = 10f;                  //scroll sensitivity
    private Vector3 mouseClickPos;                  //for click & drag

    void Start() {

        //Cullen
        //Scale cameras to fit screen height
        float pixelHeight = Screen.height;
        //calculate appropriate width
        float pixelWidth = pixelHeight * CAM_ASPECT;

        //main camera rectangle position "centered" horizontally (including UI)
        mainCam.pixelRect = new Rect((Screen.width - (pixelHeight * ASPECT)) / 2f, 0, pixelWidth, pixelHeight);

        //ui camera rectangle position positioned horizontally after the main camera rectangle
        //pixelWidth = (1-UI_PERCENT_WIDTH) * totalWidth, totalWidth = pixelWidth + uiWidth, uiWidth = totalWidth - pixelWidth
        UICam.pixelRect = new Rect((Screen.width - (pixelHeight * ASPECT)) / 2f + pixelWidth, 0, pixelWidth / (1f - UI_PERCENT_WIDTH) - pixelWidth, pixelHeight);

        //ui camera canvas scale factor adjust to the ratio of the screen to the target
        UICam.GetComponentInChildren<UnityEngine.UI.CanvasScaler>().scaleFactor = (pixelWidth / TARGET_WIDTH);

        //Cullen
        //If cameras were clipped, rescale cameras to fit screen width instead
        if (mainCam.pixelWidth < Mathf.Floor(pixelWidth)) {
            //width of main camera is percentage of screen (to fit ui in with it)
            pixelWidth = Screen.width * (TARGET_WIDTH / TARGET_TOTAL_WIDTH);
            //calculate appropriate height
            pixelHeight = Screen.width / ASPECT;

            //main camera rectangle position centered vertically
            mainCam.pixelRect = new Rect(0, (Screen.height - (pixelHeight)) / 2f, pixelWidth, pixelHeight);

            //ui camera rectangle position centered vertically and positioned horizontally after the main camera rectangle
            UICam.pixelRect = new Rect(pixelWidth, (Screen.height - (pixelHeight)) / 2f,
                Screen.width * UI_PERCENT_WIDTH, pixelHeight);

            //ui camera canvas scale factor adjust to the ratio of the screen to the target
            UICam.GetComponentInChildren<UnityEngine.UI.CanvasScaler>().scaleFactor = (pixelHeight / TARGET_HEIGHT);
        }

        //set values for zoom
        maxSize = mainCam.orthographicSize = TARGET_SIZE;
        minSize = .25f * maxSize;
    }

    // Update is called once per frame
    void LateUpdate() {
        //Cullen
        if (mainCam.pixelRect.Contains(Input.mousePosition)) {
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
            } else if (Input.GetMouseButton(0) && (mainCam.ScreenToWorldPoint(Input.mousePosition) - mouseClickPos).sqrMagnitude > .0625f) {
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
