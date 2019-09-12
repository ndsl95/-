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
    public Button exitBtn;
    public Button autoBtn;

    public GameObject tips;

    public ShowManager showManager;
    public FileManager fileManager;
    public PPTManager pptManager;
    public AutoPlayerPanel autoPanel;
   
    // Start is called before the first frame update
    void Start()
    {
        znBtn.onClick.AddListener(znBtnOnClick);
        zhBtn.onClick.AddListener(zhBtnOnClick);
        pptBtn.onClick.AddListener(pptBtnOnClick);
        autoBtn.onClick.AddListener(delegate ( ) { autoButtonOnclick(); });
        exitBtn.onClick.AddListener(delegate ( ) { Application.Quit(); });
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

    void autoButtonOnclick()    
    {
        StartCoroutine(autoButtonOnClickCoroutline());
    }

    IEnumerator autoButtonOnClickCoroutline()
    {
        fileManager.SearchPPTDirectory();
        yield return new WaitUntil(FileManager.Instance.GetFileReadState);
        pptManager.Init();
        autoPanel.gameObject.SetActive(true);

    }
}
