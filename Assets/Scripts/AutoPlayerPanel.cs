using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlayerPanel : MonoBehaviour
{
    [Header("UI")]
    public Button backButton;
    public Button addButton;
    public Button subButton;
    public Text timeLabel;
    public Button playButton;

    [Header("Manager")]
    public PPTManager pptManager;
    int deltaTime;

    private void Awake ( )
    {
        backButton.onClick.AddListener(delegate ( ) { this.gameObject.SetActive(false); });
        addButton.onClick.AddListener(delegate ( ) { int time = int.Parse(timeLabel.text); time++; time = Mathf.Clamp(time , 1 , 10); timeLabel.text = time.ToString(); deltaTime = time; });
        subButton.onClick.AddListener(delegate ( ) { int time = int.Parse(timeLabel.text); time--; time = Mathf.Clamp(time , 1 , 10); timeLabel.text = time.ToString(); deltaTime = time; });
        playButton.onClick.AddListener(delegate ( ) { StartPlay(); });
    }
    // Start is called before the first frame update
    void Start ( )
    {
        deltaTime = int.Parse(timeLabel.text);
    }

    // Update is called once per frame
    void Update ( )
    {

    }

    void StartPlay ( )
    {
        pptManager.PlayRandomPPT(deltaTime);
        this.gameObject.SetActive(false);

    }
}
