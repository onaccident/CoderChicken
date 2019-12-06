using UnityEngine;
using System.Collections;

[System.Serializable]
public class ArrayLayout  {

	[System.Serializable]
	public struct RowData{
		public AudioManager[] row;
	}

	public RowData[][] rows = new RowData[7][]; //Grid of 7x7
}

public class AudioManager
{

}