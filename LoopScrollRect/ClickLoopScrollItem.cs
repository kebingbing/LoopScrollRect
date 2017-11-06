using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

/*
 * 调用方法
 * RepeatButton.Get(go).onPress.AddListener(xxxx);
        RepeatButton.Get(go).onRelease.AddListener(xxxx);
 */

public class ClickLoopScrollItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{

    public bool invokeOnce = false;//是否只调用一次  
    private bool hadInvoke = false;//是否已经调用过  

    public float interval = 0.3f;//按下后超过这个时间则认定为"长按"  
    private bool isPointerDown = false;
    private float recordTime;
    public delegate void VoidDelegate(GameObject go);

    public VoidDelegate onClick;
    public VoidDelegate onPress;
    public VoidDelegate onRelease;

    public UnityEvent IntervalonClick = new UnityEvent(); //间隔点击  
    public float delaytimedelt = 1.5f;
    public float delaytime = 1.5f;
    public bool isfirstClick = true;
    static public ClickLoopScrollItem Get(GameObject go)
    {
        ClickLoopScrollItem listener = go.GetComponent<ClickLoopScrollItem>();
        if (listener == null) listener = go.AddComponent<ClickLoopScrollItem>();
        return listener;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (hadInvoke) return;
        if (isPointerDown)
        {
            if ((Time.time - recordTime) > interval && onPress != null)
            {

                onPress.Invoke(gameObject);
                hadInvoke = true;
            }
        }

       if (delaytimedelt > 0)
        {
            delaytimedelt -= Time.deltaTime;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        recordTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        if (!hadInvoke)
        {
            //Debugger.LogWarning("onClick.Invoke()111111111111111;");
            //onClick.Invoke();
        }
        else
        {
            Debugger.LogWarning("onRelease.Invoke()111111111111111;");
            onRelease.Invoke(gameObject);
        }
        
        hadInvoke = false;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hadInvoke && onClick != null)
        {
            onClick.Invoke(gameObject);
            Debugger.LogWarning("onClick.Invoke()111111111111111;");
        }

        //延迟点击
        if (Math.Abs(delaytime) > 0.0000001)
        {
  
            //时间到执行
            if (delaytimedelt < 0.0000001 && IntervalonClick != null)
            {
                IntervalonClick.Invoke();
                delaytimedelt = delaytime;

            }


        }



    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerDown = false;
        hadInvoke = false;
        //onRelease.Invoke();

        //Debugger.LogWarning("onRelease.Invoke()222;");
    }
}