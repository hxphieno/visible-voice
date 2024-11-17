using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public TextAsset plotFile;
    public TMP_Text nameText;
    public TMP_Text contentText;

    public int dialogIndex = 1;
    public string[] dialogRows = null;

    public Button nextButton;

    public bool isActive=false;//用于激活与销毁
    // Start is called before the first frame update
    void Start() 
    {
        ReadText(plotFile);
        ShowDialogText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(string _name, string _content)
    {
        nameText.text = _name;
        contentText.text = _content;
    }
    public void ReadText(TextAsset _asset)
    {
        dialogRows = _asset.text.Split('\n').Skip(1).ToArray();
   
        Debug.Log(dialogRows[0]);
    }

    public void ShowDialogText (){
        foreach (var row in dialogRows) {
            string[] cells = row.Split(',');
            int a = int.Parse(cells[0]);
            if ((cells[1] == "#") && int.Parse(cells[0])==dialogIndex)
            {
                UpdateText(cells[2], cells[3]);
                dialogIndex = int.Parse(cells[4]);
                break;
            }
            else if (cells[1] == "&" && int.Parse(cells[0])==dialogIndex) {
                ///TODO:写一下选择逻辑
            }
            else if (cells[1] == "END" && int.Parse(cells[0]) == dialogIndex)
            {
                Destroy(gameObject);  
                Destroy(transform.parent.gameObject);
            }
        }
    }
    public void OnClickNext()
    {
 
            Debug.Log("Left");
            ShowDialogText();
        

    }
}
