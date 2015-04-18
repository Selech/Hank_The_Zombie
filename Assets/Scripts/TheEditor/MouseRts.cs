using UnityEngine;
using System.Collections;

public class MouseRts : MonoBehaviour
{
	private const int LevelArea = 100;
	
	private const int ScrollArea = 25;
	private const int ScrollSpeed = 7;
	private const int DragSpeed = 7;
	
	private const int ZoomSpeed = 25;
	private const int ZoomMin = 25;
	private const int ZoomMax = 100;
	
	private const int PanSpeed = 7;
	private const int PanAngleMin = 50;
	private const int PanAngleMax = 80;

	// Zooming part
	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.


	// Update is called once per frame
	void Update()
	{
		if(Popup.PopupOpen == false)
		{
			// Check for Zoom
			CheckforZoom();

			// Init camera translation for this frame.
			var translation = Vector3.zero;
			
//			// Zoom in or out
//			var zoomDelta = Input.GetAxis("Mouse ScrollWheel")*ZoomSpeed*Time.deltaTime;
//			if (zoomDelta!=0)
//			{
//				translation -= Vector3.up * ZoomSpeed * zoomDelta;
//			}
//			
//			// Start panning camera if zooming in close to the ground or if just zooming out.
//			var pan = GetComponent<Camera>().transform.eulerAngles.x - zoomDelta * PanSpeed;
//			pan = Mathf.Clamp(pan, PanAngleMin, PanAngleMax);
//			if (zoomDelta < 0 || GetComponent<Camera>().transform.position.y < (ZoomMax / 2))
//			{
//				GetComponent<Camera>().transform.eulerAngles = new Vector3(pan, 0, 0);
//			}
			
			// Move camera with arrow keys
			translation += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			
			// Move camera with mouse
			if (Input.GetMouseButton(1)) // MMB
			{
				// Hold button and drag camera around
				translation -= new Vector3(Input.GetAxis("Mouse X") * DragSpeed * Time.deltaTime, 0,
				                           Input.GetAxis("Mouse Y") * DragSpeed * Time.deltaTime);
			}
			
			// Keep camera within level and zoom area
			var desiredPosition = GetComponent<Camera>().transform.position + translation;
			if (desiredPosition.x < -LevelArea || LevelArea < desiredPosition.x)
			{
				translation.x = 0;
			}
			if (desiredPosition.y < ZoomMin || ZoomMax < desiredPosition.y)
			{
				translation.y = 0;
			}
			if (desiredPosition.z < -LevelArea || LevelArea < desiredPosition.z)
			{
				translation.z = 0;
			}
			
			// Finally move camera parallel to world axis
			GetComponent<Camera>().transform.position += translation;
		}
	}

	void CheckforZoom()
	{
		// If there are two touches on the device...
		if (Input.touchCount == 2)
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
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			// If the camera is orthographic...
			if (GetComponent<Camera>().orthographic)
			{
				// ... change the orthographic size based on the change in distance between the touches.
				GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				
				// Make sure the orthographic size never drops below zero.
				GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 0.1f);
			}
			else
			{
				// Otherwise change the field of view based on the change in distance between the touches.
				GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
				
				// Clamp the field of view to make sure it's between 0 and 180.
				GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
			}
		}
	}
}
