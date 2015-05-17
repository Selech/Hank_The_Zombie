using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseRts : MonoBehaviour
{
	private const int LevelArea = 100;
	
	private const int ScrollArea = 25;
	private const int ScrollSpeed = 7;
	private const float DragSpeed = 1.2f;
	private float xDiff = 0;
	private float yDiff = 0;

	private bool readyForMoving = false;
	private Vector2[] touchPoints = new Vector2[2];
	private int indexPointer = 0;
	private float deltaMagnitudeDiff;
	private const int ZoomSpeed = 10;
	private const int ZoomMin = 25;
	private const int ZoomMax = 140;
	private bool Zooming;
	private int ZoomCur = 50;
	//private bool onDesktop = false;
	
	private const int PanSpeed = 7;
	private const int PanAngleMin = 50;
	private const int PanAngleMax = 80;

	// Zooming part
	private const float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	private const float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

	public Text txt;

	private Transform cameraTransform;

	public string cam;

	// Set Orientation
	void Start()
	{
		cameraTransform = Camera.main.transform;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	// Update is called once per frame
	void Update()
	{
		if (Popup.PopupOpen == false)
		{
			if (Input.touchCount == 2)
			{
				// Calculate delta in touch/mouse-positions 
				CalcDeltaTouch();
				
				// Check for Zoom
				CheckForZoom();
				
				// Init camera translation for this frame.
				CheckForMoving();
			}
			else
			{
				Zooming = false;
			}
		}

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
			CheckForMovingOnDesktop(); 
			CheckForZoomOnDesktop();
		#endif
	}

	void CheckForMovingOnDesktop()
	{
		if (Popup.PopupOpen == false) 
		{
			var translation = Vector3.zero;
			
			// Move camera with arrow keys
			translation += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			
			// Move camera with mouse
			if (Input.GetMouseButton(1)) // MMB
			{
				translation += new Vector3 (-(Input.GetAxis("Mouse X") * DragSpeed*20 * Time.deltaTime), 0,
				                            -(Input.GetAxis("Mouse Y") * DragSpeed*20 * Time.deltaTime));
			}
			
			// Finally move camera parallel to world axis
			GetComponent<Camera>().transform.position += translation;
		}
	}

	void CalcDeltaTouch()
	{
		// Store both touches.
		Touch touchZero = Input.GetTouch(0);
		Touch touchOne = Input.GetTouch(1);
		
		// Find the position in the previous frame of each touch.
		Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

		// Find the magnitude of the vector (the distance) between the touches in each frame.
		float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
		
		// Find the difference in the distances between each frame.
		deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

		// Update touch-Points
		touchPoints[indexPointer] = touchZeroPrevPos;
		indexPointer++;
		if(indexPointer > 1){ indexPointer = 0; readyForMoving = true; }
	}

	void CheckForMoving()
	{
		if (Zooming == false && readyForMoving)
		{
			var translation = Vector3.zero;
			
			// Move camera with arrow keys
			translation += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			
			// Move camera with mouse
			if (Input.GetMouseButton(1)) // MMB
			{
				xDiff = Mathf.Abs(touchPoints[0].x) - Mathf.Abs(touchPoints[1].x);
				yDiff = Mathf.Abs(touchPoints[0].y) - Mathf.Abs(touchPoints[1].y);

				// Hold button and drag camera around
				if (indexPointer==0)
				{
					if (Mathf.Abs(xDiff) < 50 && Mathf.Abs(yDiff) < 50)
					{
						translation -= new Vector3(-(xDiff * DragSpeed * Time.deltaTime), 0,
						                           -(yDiff * DragSpeed * Time.deltaTime));
					}
				}
			}
			
			// Finally move camera parallel to world axis
			GetComponent<Camera>().transform.position += translation;
		}
	}

	void CheckForZoomOnDesktop()
	{
		if(Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			if(Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				// Zooming in
				if (ZoomCur>ZoomMin) 
				{
					ZoomCur -= ZoomSpeed;
					if(cam == "orthographic")
					{
						Camera.main.orthographicSize -= 0.2f;
					}
					else
					{
						transform.position += cameraTransform .forward * (Time.deltaTime * ZoomSpeed);//deltaMagnitudeDiff);
						Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 2*Time.deltaTime);
					}
				}
			}
			else
			{
				// Zooming out
				if(ZoomCur<ZoomMax)
				{
					ZoomCur += ZoomSpeed;
					if(cam == "orthographic")
					{
						Camera.main.orthographicSize += 0.2f;
					}
					else
					{
						transform.position -= cameraTransform .forward * (Time.deltaTime * ZoomSpeed);//deltaMagnitudeDiff);
						Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 2*Time.deltaTime);
					}
				}
			}
		}
	}

	void CheckForZoom()
	{
		string pinchZoomingOut = "";
		
		if (Mathf.Abs(deltaMagnitudeDiff)>3)
		{
			Zooming = true;
			pinchZoomingOut = (deltaMagnitudeDiff > 0) ? "zoomOut" : "zoomIn";
			
			// Zooming in
			if ((Input.GetKey("t") || Zooming) && ZoomCur>ZoomMin && pinchZoomingOut=="zoomIn") {
				ZoomCur -= ZoomSpeed;
				if(cam == "orthographic")
				{
					Camera.main.orthographicSize -= 0.2f;
				}
				else
				{
					transform.position += cameraTransform .forward * (Time.deltaTime * ZoomSpeed);//deltaMagnitudeDiff);
					Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 2*Time.deltaTime);
				}
			}
			// Zooming out
			else if((Input.GetKey("g") || Zooming) && ZoomCur<ZoomMax && pinchZoomingOut=="zoomOut")
			{
				ZoomCur += ZoomSpeed;
				if(cam == "orthographic")
				{
					Camera.main.orthographicSize += 0.2f;
				}
				else
				{
					transform.position -= cameraTransform .forward * (Time.deltaTime * ZoomSpeed);//deltaMagnitudeDiff);
					Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 2*Time.deltaTime);
				}
			}
		}
		else if (Zooming && Mathf.Abs(deltaMagnitudeDiff)<0.5)
		{
			Zooming = false;
		}
	}
}