using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
public class InertiaTest : MonoBehaviour
{
    public Image drag;

    public RectTransform _dragRt;
    private bool _mouseDown;
    private Vector2 _mouseStartPos;

    //滑动结束时的瞬时速度
    Vector3 Speed = Vector3.zero;
    //速度衰減率
    public float decelerationRate = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        _dragRt = drag.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mouseDown = true;
            TouchBegin(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && _mouseDown)
        {
            TouchMove(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            TouchUp();
            _mouseDown = false;
            _mouseStartPos = Vector3.zero;
        }
        InertiaMove();
    }

    void TouchBegin(Vector2 screenPos)
    {
        Vector2 outPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, screenPos, Camera.main, out outPos);
        _mouseStartPos = outPos;
    }

    void TouchMove(Vector2 screenPos)
    {
        Vector2 outPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, screenPos, Camera.main, out outPos);
        Vector2 movePos = outPos - _mouseStartPos;
        _dragRt.anchoredPosition += movePos;

        CameraIN();
        _mouseStartPos = outPos;
    }
    private void CameraIN()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(_dragRt.transform.position);
        pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_dragRt.parent as RectTransform, pos, Camera.main, out Vector2 rectPos);
        _dragRt.localPosition = rectPos;
    }
    private void TouchUp()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, Input.mousePosition, Camera.main, out Vector2 outPos);
        Vector2 movePos = outPos - _mouseStartPos;
        Speed = movePos;
        Debug.Log("Current Speed" + Vector3.Magnitude(Speed));
    }
    private void InertiaMove()
    {
        if (Vector3.Magnitude(Speed) <= 0.1 || _mouseDown) return;

        Speed *= Mathf.Pow(decelerationRate, Time.deltaTime);
        _dragRt.anchoredPosition += (Vector2)Speed;

        CameraIN();
    }
}
