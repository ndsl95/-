using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Image mainImage;
    public Button znBtn;//兆能
    public Button zhBtn;//兆和
    public Button pptBtn;

    public GameObject tips;

    public ShowManager showManager;
    public FileManager fileManager;
    public PPTManager pptManager;
   
    // Start is called before the first frame update
    void Start()
    {
        znBtn.onClick.AddListener(znBtnOnClick);
        zhBtn.onClick.AddListener(zhBtnOnClick);
        pptBtn.onClick.AddListener(pptBtnOnClick);
    }

    private void Update ( )
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void znBtnOnClick()
    {
        showManager.SetShowImage(SaveData.instance.ZnLists , 0);
    }

    void zhBtnOnClick()
    {
        showManager.SetShowImage(SaveData.instance.ZhLists,0);
    }

    void pptBtnOnClick()
    {
        tips.gameObject.SetActive(true);
        fileManager.SearchPPTDirectory();
        pptManager.Init();
        tips.gameObject.SetActive(false);
    }
}
