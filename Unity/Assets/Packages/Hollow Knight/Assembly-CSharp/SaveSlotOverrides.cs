using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class SaveSlotOverrides : MonoBehaviour
{
	[Tooltip("Insert an UNENCRYPTED save file into the slots that should be overridden.")]
	public TextAsset[] overrideFiles = new TextAsset[4];

	private void OnValidate()
	{
		if (overrideFiles.Length != 4)
		{
			TextAsset[] array = new TextAsset[4];
			for (int i = 0; i < Mathf.Min(array.Length, overrideFiles.Length); i++)
			{
				array[i] = overrideFiles[i];
			}
			overrideFiles = array;
		}
	}

	private void Awake()
	{
		try
		{
			for (int i = 0; i < overrideFiles.Length; i++)
			{
				if (overrideFiles[i] != null)
				{
					int slotIndex = i + 1;
					string text = overrideFiles[i].text;
					if (!Platform.Current.IsFileSystemProtected)
					{
						string graph = Encryption.Encrypt(text);
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, graph);
						Platform.Current.WriteSaveSlot(slotIndex, memoryStream.ToArray(), null);
						memoryStream.Close();
					}
					else
					{
						Platform.Current.WriteSaveSlot(slotIndex, Encoding.UTF8.GetBytes(text), null);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("GM Save - There was an error overriding save files!" + ex);
		}
	}
}
