using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    [SerializeField] private TextAsset dialogText;
    [SerializeField] private DialogList dialogList;
	[SerializeField] private string startingDialogID;
	
	[SerializeField] private TMP_Text messageText;
	[SerializeField] private TMP_Text nameText;
	[SerializeField] private Button choice1;
	[SerializeField] private Button choice2;

	private DialogObject currentDialog;
	private int currentMessage = 0;

	private void Start()
	{
		dialogList = JsonUtility.FromJson<DialogList>(dialogText.text);

		ShowDialog(startingDialogID);
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
		else if(currentMessage < currentDialog.Messages.Length-1 && Input.GetKeyDown(KeyCode.Space)) //Advance message
		{
			currentMessage++;
			UpdateMessage();
		}
	}

	private void ShowDialog(string dialogID)
	{
		if(dialogID.StartsWith("GotoScene"))
		{
			int targetScene = int.Parse(dialogID.Substring(9));
			SceneManager.LoadScene(targetScene);
			return;
		}

		DialogObject newDialog = dialogList.Dialogs.Find(x => x.ID == dialogID);
		if (newDialog != null)
		{
			currentMessage = 0;
			currentDialog = newDialog;
			nameText.text = currentDialog.Name;
			messageText.text = currentDialog.Messages[currentMessage];
			choice1.gameObject.SetActive(false);
			choice2.gameObject.SetActive(false);
			if(currentDialog.Messages.Length == 1)
			{
				UpdateMessage();
			}
		}
		else 
		{
			Debug.LogError("Couldn't find a dialog ID: " + dialogID);
		}
	}

	private void UpdateMessage()
	{
		messageText.text = currentDialog.Messages[currentMessage];
		if(currentMessage == currentDialog.Messages.Length - 1) //Last mesage
		{
			if (currentDialog.Choices.Count > 0)
			{
				choice1.gameObject.SetActive(true);
				choice1.GetComponentInChildren<Text>().text = currentDialog.Choices[0].Text;
			}
			if (currentDialog.Choices.Count > 1)
			{
				choice2.gameObject.SetActive(true);
				choice2.GetComponentInChildren<Text>().text = currentDialog.Choices[1].Text;
			}
		}
	}

	public void ClickChoice(int choice)
	{
		ShowDialog(currentDialog.Choices[choice].TargetDialog);
	}
}
