using System;
using System.Collections.Generic;
using GlobalEnums;
using UnityEngine;

namespace TeamCherry
{
	
	[Serializable]
	public class SceneDefaultSettings : ScriptableObject
	{
		[SerializeField]
		public int selection;
	
		[SerializeField]
		public List<SceneManagerSettings> settingsList;
	
		public void OnEnable()
		{
			if (settingsList == null)
			{
				settingsList = new List<SceneManagerSettings>();
			}
		}
	
		public SceneManagerSettings GetMapZoneSettings(MapZone mapZone)
		{
			foreach (SceneManagerSettings settings in settingsList)
			{
				if (settings.mapZone == mapZone)
				{
					return settings;
				}
			}
			return null;
		}
	
		public SceneManagerSettings GetCurrentMapZoneSettings()
		{
			return GetMapZoneSettings((MapZone)selection);
		}
	
		public void SaveSettings(SceneManagerSettings sms)
		{
			SceneManagerSettings mapZoneSettings = GetMapZoneSettings(sms.mapZone);
			if (mapZoneSettings != null)
			{
				mapZoneSettings.defaultColor = new Color(sms.defaultColor.r, sms.defaultColor.g, sms.defaultColor.b, sms.defaultColor.a);
				mapZoneSettings.defaultIntensity = sms.defaultIntensity;
				mapZoneSettings.saturation = sms.saturation;
				mapZoneSettings.redChannel = new AnimationCurve(sms.redChannel.keys.Clone() as Keyframe[]);
				mapZoneSettings.greenChannel = new AnimationCurve(sms.greenChannel.keys.Clone() as Keyframe[]);
				mapZoneSettings.blueChannel = new AnimationCurve(sms.blueChannel.keys.Clone() as Keyframe[]);
				mapZoneSettings.heroLightColor = new Color(sms.heroLightColor.r, sms.heroLightColor.g, sms.heroLightColor.b, sms.heroLightColor.a);
			}
			else
			{
				settingsList.Add(new SceneManagerSettings(sms.mapZone, new Color(sms.defaultColor.r, sms.defaultColor.g, sms.defaultColor.b), sms.defaultIntensity, sms.saturation, new AnimationCurve(sms.redChannel.keys.Clone() as Keyframe[]), new AnimationCurve(sms.greenChannel.keys.Clone() as Keyframe[]), new AnimationCurve(sms.blueChannel.keys.Clone() as Keyframe[]), new Color(sms.heroLightColor.r, sms.heroLightColor.g, sms.heroLightColor.b, sms.heroLightColor.a)));
			}
		}
	
		public MapZone GetCurrentSelection()
		{
			return (MapZone)selection;
		}
	}
}