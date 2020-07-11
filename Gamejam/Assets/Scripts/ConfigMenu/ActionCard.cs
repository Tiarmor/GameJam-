using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionCard : MonoBehaviour,IPointerDownHandler,IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{

    public GraphicRaycaster canvasUI;
    public EventSystem eventSystem;

    public string actionName;
    public int actionID;

    Vector3 originScale;
    Vector3 mousePos, originPos;
    public GameObject originParent, configMenu;    //记录原父物体，便于交换和取消拖拽

    ActionPreview preview;

    void Awake()
    {
        configMenu = GameObject.Find("Canvas").transform.Find("ConfigMenu").gameObject;
        preview = GameObject.Find("Canvas").transform.Find("ConfigMenu").Find("PreviewPanel").GetComponent<ActionPreview>();
        canvasUI= GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
        originScale = this.transform.localScale;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = this.transform.localScale + new Vector3(0.05f, 0.05f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = this.transform.localScale - new Vector3(0.05f, 0.05f, 0);
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        //在Preview中显示动作
        preview.SetPreviewAction(actionID);
    }


    public void OnDrag(PointerEventData eventData)
    {
        //随鼠标移动
        this.transform.position = Input.mousePosition - mousePos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mousePos = Input.mousePosition - this.transform.position;       
        //脱离父物体
        originParent = this.transform.parent.gameObject;
        if (originParent.GetComponent<ActionConfig>() != null)
        {
            originParent.GetComponent<ActionConfig>().SetAction("null", 0);
        }
        else if(originParent.name.Equals("CardList"))   //这个else只用在可重复使用卡片的情况下
        {
            GameObject temp = Instantiate<GameObject>(this.gameObject, originParent.transform);
            temp.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
            temp.transform.localScale = originScale;
        }

        originPos = this.transform.position;
        //拖拽时透明
        this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 180f / 255f);
        this.transform.SetParent(configMenu.transform);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 1);
        //如果没有对准panel则回原位，反之到新位置
        List<RaycastResult> list = CheckUIGameObject(Input.mousePosition);
        for(int i = 0; i < list.Count; i++)
        {
            if (SetCard(list[i].gameObject))
            {
                return;
            }
        }
        //拖到框外就清除
        Destroy(this.gameObject);  
        //回到原位置
        //SetCard(originParent);
    }

    //检测鼠标位置所有的UI物体
    public List<RaycastResult> CheckUIGameObject(Vector2 pos)
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.pressPosition = pos;
        eventData.position = pos;
        List<RaycastResult> list = new List<RaycastResult>();
        canvasUI.Raycast(eventData, list);
        return list;
    }


    //执行将卡片转移的操作
    public bool SetCard(GameObject cur)
    {
        if (cur.GetComponent<ActionConfig>() != null)
        {           
            //如果已被设置，交换二者位置
            if (!cur.GetComponent<ActionConfig>().curActionName.Equals("null"))
            {
                cur.GetComponentInChildren<ActionCard>().SetCard(originParent);
            }

            this.transform.SetParent(cur.transform);
            cur.GetComponent<ActionConfig>().SetAction(actionName, actionID);

            this.transform.localPosition = new Vector3(0, 0, 0);
            this.transform.SetAsFirstSibling();
            return true;
        }
        //如果只有拖回卡池才销毁，启用此处
        else if (cur.name.Equals("CardList"))
        {
            Destroy(this.gameObject);
            //this.transform.SetParent(cur.transform);
            return true;
        }
        return false;
    }

}
