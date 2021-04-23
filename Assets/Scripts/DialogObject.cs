using System.Collections.Generic;

[System.Serializable]
public class DialogObject
{
	public string ID;
	public string Name;
	public string[] Messages;
	public List<DialogChoice> Choices;
}