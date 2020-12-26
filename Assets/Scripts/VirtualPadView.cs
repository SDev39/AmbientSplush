using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
using Es.InkPainter;

public class VirtualPadView : MonoBehaviour
{
    // Start is called before the first frame update

	[SerializeField]
	private InkShouter listener;

	[SerializeField]
	private Brush brush;

	[SerializeField]
	public GameObject targetGenerator;

	[SerializeField]
	public GameObject camera;

	[SerializeField]
	private RectTransform  canvasRectTransform;

	[SerializeField]
	private Image  stick;

	[SerializeField]
	private Image stickBackground;

	[SerializeField]
	private float stickRadiusMin;

	[SerializeField]
	private float stickRadiusMax;

	private bool isStickActive = false;

	private Vector3 zeroPoint = new Vector3 {
		x = 0.0f,
		y = 0.0f,
		z = 0.0f
	};

	public void SetEnable(bool state)
	{
		stick.enabled = state;
		stickBackground.enabled = state;
		isStickActive = false;
		this.enabled = state;

		// TODO : Let lister know stick is pressed.
	}

	void Awake()
	{
		// this.enabled = true;
		stick.enabled = false;
		stickBackground.enabled = false;
		listener.SetActive(false);
	}

	private Vector3 ScreenToCanvas(Vector3 screenPos)
	{
		// スクリーン座標を割合に変換
		float ratioX = screenPos.x / Screen.width;
		float ratioY = screenPos.y / Screen.height;

		// 割合をキャンバス座標に当てはめる.
		var canvasRect = canvasRectTransform.rect;
		float canvasX = ratioX * canvasRect.width;
		float canvasY = ratioY * canvasRect.height;

		// キャンバス座標は中心が0なので合わせる.
		canvasX -= canvasRect.width * 0.5f;
		canvasY -= canvasRect.height * 0.5f;

		return new Vector3()
		{
			x = canvasX,
			y = canvasY,
			z = 0.0f,
		};
	}

	private void UpdatePosition()
	{
		
		
		var screenTapPosition = TouchInputController.TapPosition;
		var canvasTapPosition = stickBackground.rectTransform.localPosition;
		var ray = Camera.main.ScreenPointToRay(screenTapPosition);
		RaycastHit hitInfo;
		var state = (TouchInputController.Tap || TouchInputController.Drag);
		state = state && !TouchInputController.TouchBlocked;

		TrackableHit hit;
		TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
		TrackableHitFlags.FeaturePointWithSurfaceNormal;

		//bool rayHit = Frame.Raycast(ray.origin, ray.direction, out hit, 100.0f, raycastFilter);
		bool rayHit = Physics.Raycast(ray, out hitInfo);
		if (!rayHit && state)
		{
			targetGenerator.GetComponent<InkPaintTargetGenerator>().CreateTarget();
			// Debug.Log("Ray not hit.");	
		}
		

		if (rayHit && state)
		{
			var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
			if (paintObject != null)
			{
				if (!paintObject.Paint(brush, hitInfo.point))
				{
					// Debug.Log("Painting from canvas was failed !!!");
				}
				else
				{
					// Debug.Log("Painting from canvas !!!");	
				}

				// Debug.Log(hitInfo.point);
			}

		}
		
		
		
		listener.SetActive(state && !Input.GetMouseButtonDown(0));
		if (TouchInputController.Tap)
		{
			if (!isStickActive) 
			{
				// isStickActive = true;
				// stick.enabled = true;
				// listener.enabled = true;
				// listener.SetActive(true);
				// stick.rectTransform.localPosition = ScreenToCanvas(TouchInputController.TapPosition);
				// stickBackground.enabled = true;
				// stickBackground.rectTransform.localPosition = ScreenToCanvas(TouchInputController.TapPosition);
			}
		}
		if (isStickActive)
		{
			if (TouchInputController.Drag)
			{
				// var screenDragPosition = TouchInputController.DragPosition;
				// var screenTapPosition = TouchInputController.TapPosition;
				// var canvasDragPosition = ScreenToCanvas(screenDragPosition);
				// var canvasTapPosition = stickBackground.rectTransform.localPosition;
				// Debug.DrawLine(canvasTapPosition, canvasDragPosition, Color.red);

				// var diffPosition = canvasDragPosition - canvasTapPosition;
				// var diffSqrMagnitude = diffPosition.sqrMagnitude;
				// var diffMagnitude = diffPosition.magnitude;

				// var stickSqrRadiusMax = stickRadiusMax * stickRadiusMax;
				// if (stickRadiusMax < diffMagnitude)
				// {
				// 	diffPosition.Normalize();
				// 	diffPosition *= stickRadiusMax;
				// 	diffSqrMagnitude = stickSqrRadiusMax;
				// }
				// // diffPositionとデバイスの垂直方向を見てインクの発射方向を決める

				// stick.rectTransform.localPosition = canvasTapPosition + diffPosition;

				// var xNormal = new Vector3(0, 0, 1);
				// var xAngleVec = new Vector3((screenDragPosition.x - screenTapPosition.x) * 100, 0, 0);
				// var yNormal = new Vector3(0, 0, 1);
				// var yAngleVec = new Vector3(0, (screenDragPosition.y - screenTapPosition.y) * 100, 0);
				// listener.transform.rotation = camera.transform.rotation;
				// listener.transform.Rotate(
				// 	Vector3.Angle(xAngleVec, xNormal),
				// 	Vector3.Angle(yAngleVec, yNormal),
				// 	0,
				// 	Space.World);
				


			}
			else
			{
				if (isStickActive)
				{
					// isStickActive = false;
					// stick.enabled = false;
					// stickBackground.enabled = false;
					// listener.enabled = false;
					// listener.SetActive(false);
				}
			}

		}
		else
		{
			return;
		}
	}
    
	
	public void Update()
	{
		
		UpdatePosition();
		
	}

}
