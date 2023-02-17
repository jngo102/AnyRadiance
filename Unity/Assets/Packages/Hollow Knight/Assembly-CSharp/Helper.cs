using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static class Helper
{
	private static RaycastHit2D[] rayHitStore;

	public static int GetCollidingLayerMaskForLayer(int layer)
	{
		int num = 0;
		for (int i = 0; i < 32; i++)
		{
			if (!Physics2D.GetIgnoreLayerCollision(layer, i))
			{
				num |= 1 << i;
			}
		}
		return num;
	}

	public static float GetReflectedAngle(float angle, bool reflectHorizontal, bool reflectVertical)
	{
		if (reflectHorizontal)
		{
			angle = 180f - angle;
		}
		if (reflectVertical)
		{
			angle = 0f - angle;
		}
		while (angle > 360f)
		{
			angle -= 360f;
		}
		while (angle < -360f)
		{
			angle += 360f;
		}
		return angle;
	}

	public static Vector3 GetRandomVector3InRange(Vector3 min, Vector3 max)
	{
		float x = ((min.x != max.x) ? UnityEngine.Random.Range(min.x, max.x) : min.x);
		float y = ((min.y != max.y) ? UnityEngine.Random.Range(min.y, max.y) : min.y);
		float z = ((min.z != max.z) ? UnityEngine.Random.Range(min.z, max.z) : min.z);
		return new Vector3(x, y, z);
	}

	public static Vector2 GetRandomVector2InRange(Vector2 min, Vector2 max)
	{
		float x = ((min.x != max.x) ? UnityEngine.Random.Range(min.x, max.x) : min.x);
		float y = ((min.y != max.y) ? UnityEngine.Random.Range(min.y, max.y) : min.y);
		return new Vector2(x, y);
	}

	public static bool IsRayHittingNoTriggers(Vector2 origin, Vector2 direction, float length, int layerMask, out RaycastHit2D closestHit)
	{
		if (rayHitStore == null)
		{
			rayHitStore = new RaycastHit2D[10];
		}
		int num = Physics2D.RaycastNonAlloc(origin, direction, rayHitStore, length, layerMask);
		bool flag = false;
		closestHit = default(RaycastHit2D);
		for (int i = 0; i < num; i++)
		{
			RaycastHit2D raycastHit2D = rayHitStore[i];
			Collider2D collider = raycastHit2D.collider;
			if ((bool)collider && !collider.isTrigger)
			{
				if (!flag || raycastHit2D.distance < closestHit.distance)
				{
					closestHit = raycastHit2D;
				}
				flag = true;
			}
			rayHitStore[i] = default(RaycastHit2D);
		}
		return flag;
	}

	public static bool IsRayHittingNoTriggers(Vector2 origin, Vector2 direction, float length, int layerMask)
	{
		RaycastHit2D closestHit;
		return IsRayHittingNoTriggers(origin, direction, length, layerMask, out closestHit);
	}

	public static string CombinePaths(string path1, params string[] paths)
	{
		if (path1 == null)
		{
			throw new ArgumentNullException("path1");
		}
		if (paths == null)
		{
			throw new ArgumentNullException("paths");
		}
		return Enumerable.Aggregate(paths, path1, (string acc, string p) => Path.Combine(acc, p));
	}

	public static bool FileOrFolderExists(string path)
	{
		if (!File.Exists(path))
		{
			return Directory.Exists(path);
		}
		return true;
	}

	public static void DeleteFileOrFolder(string path)
	{
		if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
		{
			Directory.Delete(path, recursive: true);
		}
		else
		{
			File.Delete(path);
		}
	}

	public static void CopyFileOrFolder(string src, string dest)
	{
		if ((File.GetAttributes(src) & FileAttributes.Directory) == FileAttributes.Directory)
		{
			DirectoryInfo source = new DirectoryInfo(src);
			DirectoryInfo target = (Directory.Exists(dest) ? new DirectoryInfo(dest) : Directory.CreateDirectory(dest));
			DeepCopy(source, target);
		}
		else
		{
			File.Copy(src, dest);
		}
	}

	public static void DeepCopy(DirectoryInfo source, DirectoryInfo target)
	{
		DirectoryInfo[] directories = source.GetDirectories();
		foreach (DirectoryInfo directoryInfo in directories)
		{
			DeepCopy(directoryInfo, target.CreateSubdirectory(directoryInfo.Name));
		}
		FileInfo[] files = source.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			fileInfo.CopyTo(Path.Combine(target.FullName, fileInfo.Name));
		}
	}

	public static void MoveFileOrFolder(string src, string dest)
	{
		if ((File.GetAttributes(src) & FileAttributes.Directory) == FileAttributes.Directory)
		{
			Directory.Move(src, dest);
		}
		else
		{
			File.Copy(src, dest);
		}
	}

	public static bool CheckMatchingSearchFilter(string text, string filter)
	{
		text = text.ToLower();
		filter = filter.ToLower().Replace('_', ' ');
		return Enumerable.All(filter.Split(' '), (string f) => text.Contains(f));
	}

	public static string ParseSearchString(string original)
	{
		if (string.IsNullOrEmpty(original))
		{
			return null;
		}
		return original.Trim().ToLower().Replace(" ", "");
	}
}
