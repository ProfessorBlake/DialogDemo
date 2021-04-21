using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] private TextAsset dialogText;
    [SerializeField] private DialogList dialogList;

	private DialogObject currentDialog;

	private void Start()
	{
		dialogList = JsonUtility.FromJson<DialogList>(dialogText.text);

		ShowDialog("Scene1_Character1_Intro");
	}

	private void Update()
	{
		if(currentDialog.Choices.Count > 0 && Input.GetKeyDown(KeyCode.Alpha1)) //Press 1 and have at least 1 choice.
		{
			ShowDialog(currentDialog.Choices[0].TargetDialog);
		}
		else if(currentDialog.Choices.Count > 1 && Input.GetKeyDown(KeyCode.Alpha2)) //Press 2 and have at least 2 choices.
		{
			ShowDialog(currentDialog.Choices[1].TargetDialog);
		}
	}

	private void ShowDialog(string dialogID)
	{
		DialogObject newDialog = dialogList.Dialogs.Find(x => x.ID == dialogID);
		if (newDialog != null)
		{
			currentDialog = newDialog;
			Debug.Log(newDialog.Message);
			foreach(DialogChoice choice in newDialog.Choices)
			{
				Debug.Log(choice.Text + " : " + choice.TargetDialog);
			}
		}
		else 
		{
			Debug.LogError("Couldn't find a dialog ID: " + dialogID);
		}
		Debug.Log("---------------------");
	}
}
