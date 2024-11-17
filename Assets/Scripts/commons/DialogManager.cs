using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class DialogManager : MonoBehaviour
{
    public TextAsset plotFile;
    public TMP_Text nameText;
    public TMP_Text contentText;

    public int dialogIndex = 1;
    public string[] dialogRows = null;

    public Button nextButton;
    private Tweener textTweener;
    public GameObject dialogPanel;
    public FirstPersonController playerController;
  
    public bool isActive=false;//���ڼ���������
    // Start is called before the first frame update
    void Start() 
    {
        ReadText(plotFile);

        dialogPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            ///���鵯�������������ƶ�
            Cursor.lockState = CursorLockMode.None;
            playerController.enabled = false;

            dialogPanel.SetActive(true);
            ShowDialogText();
            isActive = false;//ֻ����һ�ξͺã���Ȼ��˲��������о���
        }
    }

    public void UpdateText(string _name, string _content)
    {
        Debug.Log("UpdateText!");
        nameText.text = _name;
        contentText.text = "";
        textTweener=DOTween.To(() => "", x => contentText.text = x,_content, _content.Length*0.02f)
               .SetEase(Ease.Linear);
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
                ///TODO:дһ��ѡ���߼�
            }
            else if (cells[1] == "END" && int.Parse(cells[0]) == dialogIndex)
            {
                Destroy(gameObject);
                Destroy(dialogPanel);
                ///�ָ����������ƶ�������
                playerController.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void OnClickNext()
    {

        if (textTweener != null && textTweener.IsActive() && !textTweener.IsComplete())
        {
            textTweener.Complete(); 
        }
        else ShowDialogText();
        

    }
}
