using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    new Camera camera;
    public float perspectiveZoomSpeed = 0.5f;  //����,�ܾƿ��Ҷ� �ӵ�(perspective��� ��)   
    public float orthoZoomSpeed = 0.5f;      //����,�ܾƿ��Ҷ� �ӵ�(OrthoGraphic��� ��)

    [SerializeField] private RectTransform _zoomTargetRt;

    private readonly float _ZOOM_IN_MAX = 16f;
    private readonly float _ZOOM_OUT_MAX = 1f;
    private readonly float _ZOOM_SPEED = 1.5f;

    private bool _isZooming = false;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        TouchZoom();

        //if (Input.touchCount == 2)
        //    ZoomAndPan();
        //else
        //    _isZooming = false;
    }

    private void ZoomAndPan()
    {
        if (_isZooming == false)
        {
            _isZooming = true;
        }
        Touch zeroTouch = Input.GetTouch(0);
        Touch oneTouch = Input.GetTouch(1);
        /* get zoomAmount */
        var prevTouchAPos = zeroTouch.position - zeroTouch.deltaPosition;
        var prevTouchBPos = oneTouch.position - oneTouch.deltaPosition;
        var curTouchAPos = zeroTouch.position;
        var curTouchBPos = oneTouch.position;
        var deltaDistance = Vector2.Distance(Normalize(curTouchAPos), Normalize(curTouchBPos)) - Vector2.Distance(Normalize(prevTouchAPos), Normalize(prevTouchBPos));
        var currentScale = _zoomTargetRt.localScale.x;
        var zoomAmount = deltaDistance * currentScale * _ZOOM_SPEED; // zoomAmount == deltaScale

        /* clamp & zoom */
        var zoomedScale = currentScale + zoomAmount;
        if (zoomedScale < _ZOOM_OUT_MAX)
        {
            zoomedScale = _ZOOM_OUT_MAX;
            zoomAmount = 0f;
        }
        if (_ZOOM_IN_MAX < zoomedScale)
        {
            zoomedScale = _ZOOM_IN_MAX;
            zoomAmount = 0f;
        }
        _zoomTargetRt.localScale = zoomedScale * Vector3.one;

        /* apply offset */
        // offset is a value against movement caused by scale up & down
        var pivotPos = _zoomTargetRt.anchoredPosition;
        var fromCenterToInputPos = new Vector2(
                Input.mousePosition.x - Screen.width * 0.5f,
                Input.mousePosition.y - Screen.height * 0.5f);
        var fromPivotToInputPos = fromCenterToInputPos - pivotPos;
        var offsetX = (fromPivotToInputPos.x / zoomedScale) * zoomAmount;
        var offsetY = (fromPivotToInputPos.y / zoomedScale) * zoomAmount;
        _zoomTargetRt.anchoredPosition -= new Vector2(offsetX, offsetY);

        /* get moveAmount */
        var deltaPosTouchA = zeroTouch.deltaPosition;
        var deltaPosTouchB = oneTouch.deltaPosition;
        var deltaPosTotal = (deltaPosTouchA + deltaPosTouchB) * 0.5f;
        var moveAmount = new Vector2(deltaPosTotal.x, deltaPosTotal.y);

        /* clamp & pan */
        var clampX = (Screen.width * zoomedScale - Screen.width) * 0.5f;
        var clampY = (Screen.height * zoomedScale - Screen.height) * 0.5f;
        var clampedPosX = Mathf.Clamp(_zoomTargetRt.localPosition.x + moveAmount.x, -clampX, clampX);
        var clampedPosY = Mathf.Clamp(_zoomTargetRt.localPosition.y + moveAmount.y, -clampY, clampY);
        _zoomTargetRt.anchoredPosition = new Vector3(clampedPosX, clampedPosY);
    }

    private Vector2 Normalize(Vector2 position)
    {
        var normlizedPos = new Vector2(
            (position.x - Screen.width * 0.5f) / (Screen.width * 0.5f),
            (position.y - Screen.height * 0.5f) / (Screen.height * 0.5f));
        return normlizedPos;
    }
    private void TouchZoom()
    {
        if (Input.touchCount == 2) //�հ��� 2���� ������ ��
        {
            Touch touchZero = Input.GetTouch(0); //ù��° �հ��� ��ġ�� ����
            Touch touchOne = Input.GetTouch(1); //�ι�° �հ��� ��ġ�� ����

            //��ġ�� ���� ���� ��ġ���� ���� ������
            //ó�� ��ġ�� ��ġ(touchZero.position)���� ���� �����ӿ����� ��ġ ��ġ�� �̹� �����ӿ��� ��ġ ��ġ�� ���̸� ��
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition�� �̵����� ������ �� ���
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // �� �����ӿ��� ��ġ ������ ���� �Ÿ� ����
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude�� �� ������ �Ÿ� ��(����)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // �Ÿ� ���� ����(�Ÿ��� �������� ũ��(���̳ʽ��� ������)�հ����� ���� ����_���� ����)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ���� ī�޶� OrthoGraphic��� ���
            if (camera.orthographic)
            {
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            {
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}
