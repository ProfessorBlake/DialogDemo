using System.Collections.Generic;

[System.Serializable]
public class DialogObject
{
	public string ID;
	public string Message;
	public List<DialogChoice> Choices;
}