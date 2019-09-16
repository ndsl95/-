using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ShowManager : MonoBehaviour,IPointerUpHandler, IPointerDownHandler
{
    public Button backBtn;
    public Button frontBtn;
    public Button nextBtn;
    public Image showImage;
    List<Sprite> curSpriteList;//当前展示的SpriteList
    [SerializeField]
    int curIndex;//当前展示的index
    int returnTag;//0表示直接返回main，1表示返回pptPanel
    public static  ShowManager instance;

    private void Awake ( )
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(delegate ( )
        {
            curIndex = 0;
            this.gameObject.SetActive(false);
        });

        frontBtn.onClick.AddListener(delegate ( )
        {
            FrontPanel();
        });

        nextBtn.onClick.AddListener(delegate ( )
        {
            NextPanel();
        });
    }

    //设置ShowImage需要显示哪个spriteList,tag用于表示showimage返回是返回到main还是pptPanel
    public void SetShowImage(List<Sprite> targetSprite,int tag)
    {
        returnTag = tag;
        curSpriteList = targetSprite;
        this.gameObject.SetActive(true);
        showImage.sprite = curSpriteList[0];
        curIndex = 0;
    }

    void IPointerDownHandler.OnPointerDown (PointerEventData eventData)
    {
        if (PPTManager.Instance != null)
        {
            PPTManager.Instance.playRandomPPT = false;
        }
    }

    public void NextPanel()
    {
        if (curIndex < curSpriteList.Count - 1)
        {
            curIndex++;
            showImage.sprite = curSpriteList[curIndex];
        }
    }


    public  void FrontPanel()
    {
        if (curIndex > 0)
        {
            curIndex--;
            showImage.sprite = curSpriteList[curIndex];
        }
    }

    void IPointerUpHandler.OnPointerUp (PointerEventData eventData)
    {
        if (eventData.position.x - eventData.pressPosition.x > 20)//右滑
        {
            FrontPanel();
        }
        else if (eventData.position.x - eventData.pressPosition.x < -20)//左滑动
        {
            NextPanel();
        }
    }
}
