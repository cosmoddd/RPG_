using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {

    public static DialogSystem Instance { get; set; }
    public GameObject dialogPanel;
    public List<string> dialogLines = new List<string>();
    public string npcName;

    Button continueButton;
    Text dialogText, nameText;
    int dialogIndex;
    // SINGLETON ALERT!!!  SINGLETON ALERT!!!


    // Use this for initialization
    void Awake () {

        continueButton = dialogPanel.transform.Find("Continue").GetComponent<Button>();
        dialogText = dialogPanel.transform.Find("Text").GetComponent<Text>();
        nameText = dialogPanel.transform.Find("Name").GetChild(0).GetComponent<Text>();


        continueButton.onClick.AddListener(delegate { ContinueDialog(); });
        dialogPanel.SetActive(false);


        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
	}
	
    
    public void AddNewDialog(string[] lines, string npcName)
    {
        dialogIndex = 0;
        dialogLines = new List<string>(lines.Length);
        //dialogLines.AddRange(lines);
        foreach (string line in lines)
        {
            dialogLines.Add(line);
        }
        this.npcName = npcName;
        print(dialogLines.Count );
        CreateDialog();
    }

    public void CreateDialog()
    {
        dialogText.text = dialogLines[dialogIndex];
        nameText.text = npcName;
        dialogPanel.SetActive(true);
    }

    public void ContinueDialog()
    {
        if (dialogIndex < dialogLines.Count - 1)
        {
            dialogIndex++;
            dialogText.text = dialogLines[dialogIndex];

        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }
}
