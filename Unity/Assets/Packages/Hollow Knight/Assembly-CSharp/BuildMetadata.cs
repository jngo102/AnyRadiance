using System;
using System.IO;
using UnityEngine;

[Serializable]
public class BuildMetadata
{
	[SerializeField]
	private string branchName;

	[SerializeField]
	private string revision;

	[SerializeField]
	private long commitTime;

	[SerializeField]
	private string machineName;

	[SerializeField]
	private long buildTime;

	private static bool didLoad;

	private static BuildMetadata embedded;

	public const string EmbeddedFileName = "BuildMetadata.json";

	public string BranchName => branchName;

	public string Revision => revision;

	public DateTime CommitTime => DateTime.FromBinary(commitTime);

	public string MachineName => machineName;

	public DateTime BuildTime => DateTime.FromBinary(buildTime);

	public static BuildMetadata Embedded
	{
		get
		{
			if (!didLoad)
			{
				didLoad = true;
				try
				{
					BuildMetadata objectToOverwrite = new BuildMetadata();
					JsonUtility.FromJsonOverwrite(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "BuildMetadata.json")), objectToOverwrite);
					embedded = objectToOverwrite;
				}
				catch (FileNotFoundException)
				{
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
			return embedded;
		}
	}

	public static BuildMetadata Create(string branchName, string revision, DateTime commitTime, string machineName, DateTime buildTime)
	{
		return new BuildMetadata
		{
			branchName = branchName,
			revision = revision,
			commitTime = commitTime.ToBinary(),
			machineName = machineName,
			buildTime = buildTime.ToBinary()
		};
	}
}
