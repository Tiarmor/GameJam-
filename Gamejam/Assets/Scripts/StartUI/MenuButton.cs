using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler
{
    Text buttonText;
    Vector3 originTextPos;  //记录text原位置

    private void Awake()
    {
        buttonText = this.transform.Find("Text").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        originTextPos = buttonText.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = new Color(104f / 255f, 104f / 255f, 104f / 255f, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.transform.position = originTextPos + new Vector3(4, -4, 0);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.transform.position = originTextPos;
        switch (buttonText.text)
        {
            case ("Start"):
                this.transform.parent.GetComponent<MainMenu>().PlayGame();
                break;
            case ("Settings"):
                this.transform.parent.parent.Find("Options").gameObject.SetActive(true);
                this.transform.parent.gameObject.SetActive(false);
                break;
            case ("Exit"):
                this.transform.parent.GetComponent<MainMenu>().QuitGame();
                break;
        }
    
    }
}
