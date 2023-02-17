using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoadAttribute]
public static class tk2dEditorSkin
{
	static bool isProSkin;
	static bool initialized = false;

	static Dictionary<string, Texture2D> skinTextures = new Dictionary<string, Texture2D>();
	static Dictionary<string, Texture2D> platformTextures = new Dictionary<string, Texture2D>();

	// Sprite collection editor styles
	static tk2dEditorSkin()
	{
		initialized = false;
	}

	private static string Platform {
		get {
			return isProSkin ? "pro" : "free";
		}
	}

	public static Texture2D GetTexture(string name) {
		Texture2D tex = null;
		if (skinTextures.TryGetValue(name, out tex) && tex != null)
		{
			return tex;
		}

		tex = Resources.Load<Texture2D>("tk2dSkin/" + name);
		if (tex == null)
		{
			Debug.LogError("tk2d - Cant find skin texture " + name);
			return GetTexture("white");
		}

		return tex;
	}

	private static Texture2D GetPlatformTexture(string name) {
		if (platformTextures.ContainsKey(name)) {
			return platformTextures[name];
		}
		else {
			Texture2D tex = GetTexture(name + "@" + Platform);
			if (tex == null) {
				return null;
			}
			else {
				platformTextures.Add(name, tex);
				return tex;
			}
		}
	}

	public static GUIStyle SimpleButton(string textureInactive) {
		return SimpleButton(textureInactive, "");
	}

	public static GUIStyle SimpleButton(string textureInactive, string textureActive) {
		Init();
		GUIStyle style = GetStyle("SimpleButtonTemplate");
		style.normal.background = GetTexture(textureInactive);
		style.active.background = string.IsNullOrEmpty(textureActive) ? null : GetTexture(textureActive);
		return style;
	}

	public static GUIStyle SimpleCheckbox(string textureInactive, string textureActive) {
		Init();
		GUIStyle style = GetStyle("SimpleButtonTemplate");
		style.normal.background = GetTexture(textureInactive);
		style.onNormal.background = string.IsNullOrEmpty(textureActive) ? null : GetTexture(textureActive);
		return style;
	}

	public static void Done() 
	{
		initialized = false;

		styleDict.Clear();
		skinTextures.Clear();
		platformTextures.Clear();
	}

	public static GUIStyle GetStyle(string name) {
		Init();

		GUIStyle style = new GUIStyle();
		if (styleDict.TryGetValue(name, out style)) {
			return style;
		}
		else {
			style = new GUIStyle();
			Debug.LogError("Unable to find embedded gui style " + name);
			styleDict.Add(name, style);
			return style;
		}
	}

	private static GUIStyle CreateStyle(string name) {
		GUIStyle style = new GUIStyle();
		style.name = "tk2dExternal." + name;
		styleDict[name] = style;
		return style;
	}

	private static GUIStyle CreateStyle(string name, GUIStyle source) {
		GUIStyle style = new GUIStyle(source);
		style.name = "tk2dExternal." + name;
		styleDict[name] = style;
		return style;
	}

	private static void GenerateGUIStyles() {
		GUIStyle rotateHandle = CreateStyle("RotateHandle");
		rotateHandle.normal.background = GetTexture("rotatehandle");
		rotateHandle.hover.background = GetTexture("rotatehandledown");
		rotateHandle.fixedHeight = 17;
		rotateHandle.fixedWidth = 17;

		GUIStyle moveHandle = CreateStyle("MoveHandle");
		moveHandle.normal.background = GetTexture("movehandle");
		moveHandle.hover.background = GetTexture("movehandledown");
		moveHandle.fixedHeight = 17;
		moveHandle.fixedWidth = 17;

		GUIStyle whiteBox = CreateStyle("WhiteBox");
		whiteBox.normal.background = GetTexture("white");

		GUIStyle selection = CreateStyle("Selection");
		selection.normal.background = GetTexture("selection");
		selection.alignment = TextAnchor.MiddleCenter;
		selection.border = new RectOffset(10, 10, 0, 0);
		selection.overflow = new RectOffset(8, 8, 0, 2);

		GUIStyle animTrigger = CreateStyle("AnimTrigger");
		animTrigger.normal.background = GetTexture("animtrigger");

		GUIStyle animTriggerDown = CreateStyle("AnimTriggerDown");
		animTriggerDown.normal.background = GetTexture("animtriggerdown");

		GUIStyle inspectorBG = CreateStyle("InspectorBG");
		inspectorBG.normal.background = GetPlatformTexture("inspectorbg");
		inspectorBG.border = new RectOffset(10, 3, 3, 0);
		inspectorBG.padding = new RectOffset(0, 5, 0, 0);
		inspectorBG.overflow = new RectOffset(7, 0, 0, 0);

		GUIStyle inspectorHeaderBG = CreateStyle("InspectorHeaderBG");
		inspectorHeaderBG.normal.background = GetPlatformTexture("inspectorheader");
		inspectorHeaderBG.border = new RectOffset(9, 3, 3, 4);
		inspectorHeaderBG.padding = new RectOffset(0, 5, 0, 5);
		inspectorHeaderBG.overflow = new RectOffset(7, 0, 0, 0);

		GUIStyle toolbarSearch = CreateStyle("ToolbarSearch");
		toolbarSearch.normal.background = GetPlatformTexture("toolbarsearch");
		toolbarSearch.normal.textColor = new Color32(121, 121, 121, 255);
		toolbarSearch.border = new RectOffset(14, 0, 0, 0);
		toolbarSearch.padding = new RectOffset(17, 0, 0, 0);
		toolbarSearch.fixedHeight = 18;

		GUIStyle toolbarSearchClear = CreateStyle("ToolbarSearchClear");
		toolbarSearchClear.normal.background = GetPlatformTexture("toolbarsearch_clear");
		toolbarSearchClear.active.background = GetPlatformTexture("toolbarsearch_clear_active");
		toolbarSearchClear.fixedWidth = 13;
		toolbarSearchClear.fixedHeight = 18;

		GUIStyle toolbarSearchRightCap = CreateStyle("ToolbarSearchRightCap");
		toolbarSearchRightCap.normal.background = GetPlatformTexture("toolbar_rightcap");
		toolbarSearchRightCap.fixedWidth = 13;
		toolbarSearchRightCap.fixedHeight = 18;

		GUIStyle dropbox = CreateStyle("DropBox");
		dropbox.normal.background = GetPlatformTexture("dropbox");
		dropbox.normal.textColor = isProSkin ? new Color32( 175, 175, 175, 255 ) : new Color32( 29, 29, 29, 255 );
		dropbox.alignment = TextAnchor.MiddleCenter;
		dropbox.fixedWidth = 128;
		dropbox.fixedHeight = 128;

		GUIStyle bodyBackground = CreateStyle("BodyBackground");
		bodyBackground.normal.background = GetPlatformTexture("bodybg");

		GUIStyle animBG = CreateStyle("AnimBG");
		animBG.normal.background = GetPlatformTexture("animbg");
		animBG.border = new RectOffset( 0, 0, 30, 0 );
		animBG.overflow = new RectOffset( 0, 0, 0, 128 );

		GUIStyle listBoxBG = CreateStyle("ListBoxBG");
		listBoxBG.normal.background = GetPlatformTexture("listbox");
		listBoxBG.border = new RectOffset(1, 4, 0, 0);

		GUIStyle listBoxItem = CreateStyle("ListBoxItem");
		listBoxItem.normal.textColor = isProSkin ? new Color32( 179, 179, 179, 255 ) : new Color32( 0, 0, 0, 255 );
		listBoxItem.active.background = GetPlatformTexture("listboxitem");
		listBoxItem.active.textColor = Color.white;
		listBoxItem.onNormal.background = listBoxItem.active.background;
		listBoxItem.onNormal.textColor = listBoxItem.active.textColor;
		listBoxItem.padding = new RectOffset(0, 0, 2, 2);
		listBoxItem.margin = new RectOffset(0, 1, 0, 0);
		listBoxItem.contentOffset = new Vector2(8, 0);

		GUIStyle listBoxSectionHeader = CreateStyle("ListBoxSectionHeader");
		listBoxSectionHeader.normal.background = GetPlatformTexture("listboxsectionheader");
		listBoxSectionHeader.normal.textColor = isProSkin ? new Color32(175, 175, 175, 255) : new Color32(78, 78, 78, 255);
		listBoxSectionHeader.fontStyle = FontStyle.Bold;
		listBoxSectionHeader.border = new RectOffset(0, 0, 2, 2);
		listBoxSectionHeader.padding = new RectOffset(0, 0, 8, 8);
		listBoxSectionHeader.margin = new RectOffset(0, 1, 0, 4);
		listBoxSectionHeader.contentOffset = new Vector2(8, 0);

		GUIStyle tilemapToolbarBG = CreateStyle("TilemapToolbarBG");
		tilemapToolbarBG.normal.background = GetTexture("tilemap_toolbar_bg");
		tilemapToolbarBG.border = new RectOffset(8, 8, 8, 8);
		tilemapToolbarBG.padding = new RectOffset(6, 6, 6, 6);

		GUIStyle tilemapDeleteItem = CreateStyle("TilemapDeleteItem");
		tilemapDeleteItem.normal.background = GetTexture("btn_delete_item");
		tilemapDeleteItem.active.background = GetTexture("btn_delete_item_down");
		tilemapDeleteItem.fixedHeight = 17;
		tilemapDeleteItem.fixedWidth = 17;
		tilemapDeleteItem.margin.right = 2;

		GUIStyle tilemapToolbarButton = CreateStyle("TilemapToolbarButton", EditorStyles.miniButton); 
		tilemapToolbarButton.padding = new RectOffset(0, 0, 0, 2); tilemapToolbarButton.margin = new RectOffset(0, 0, 0, 0);
		tilemapToolbarButton.fixedWidth = 25; tilemapToolbarButton.fixedHeight = 23;

		GUIStyle tilemapToolbarButtonLeft = CreateStyle("TilemapToolbarButtonLeft", EditorStyles.miniButtonLeft);
		tilemapToolbarButtonLeft.padding = new RectOffset(0, 0, 0, 2); tilemapToolbarButtonLeft.margin = new RectOffset(0, 0, 0, 0);
		tilemapToolbarButtonLeft.fixedWidth = 25; tilemapToolbarButtonLeft.fixedHeight = 23;

		GUIStyle tilemapToolbarButtonRight = CreateStyle("TilemapToolbarButtonRight", EditorStyles.miniButtonRight);
		tilemapToolbarButtonRight.padding = new RectOffset(0, 0, 0, 2); tilemapToolbarButtonRight.margin = new RectOffset(0, 0, 0, 0);
		tilemapToolbarButtonRight.fixedWidth = 25; tilemapToolbarButtonRight.fixedHeight = 23;

		simpleButtonTemplate = CreateStyle("SimpleButtonTemplate");
		simpleButtonTemplate.fixedHeight = 17;
		simpleButtonTemplate.fixedWidth = 17;
		simpleButtonTemplate.margin.right = 2;

		GUIStyle outlineStyle = CreateStyle("Border");
		outlineStyle.normal.background = GetTexture("border");
		outlineStyle.border = new RectOffset(1, 1, 1, 1);
	}

	static GUIStyle simpleButtonTemplate;
	static private Dictionary<string, GUIStyle> styleDict = new Dictionary<string, GUIStyle>();

	private static void Init()
	{
		if (initialized)
		{
			return;
		}

		if (isProSkin != EditorGUIUtility.isProSkin)
		{
			Done();
			isProSkin = EditorGUIUtility.isProSkin;
		}

		GenerateGUIStyles();

		initialized = true;
	}
	
	public static GUIStyle SC_InspectorBG { get { return GetStyle("InspectorBG"); } }
	public static GUIStyle SC_InspectorHeaderBG { get { return GetStyle("InspectorHeaderBG"); } }
	public static GUIStyle SC_ListBoxBG { get { return GetStyle("ListBoxBG"); } }
	public static GUIStyle SC_ListBoxItem { get { return GetStyle("ListBoxItem"); } }
	public static GUIStyle SC_ListBoxSectionHeader { get { return GetStyle("ListBoxSectionHeader"); } }	
	public static GUIStyle SC_BodyBackground { get { return GetStyle("BodyBackground"); } }	
	public static GUIStyle SC_DropBox { get { return GetStyle("DropBox"); } }	
	
	public static GUIStyle ToolbarSearch { get { return GetStyle("ToolbarSearch"); } }
	public static GUIStyle ToolbarSearchClear { get { return GetStyle("ToolbarSearchClear"); } }
	public static GUIStyle ToolbarSearchRightCap { get { return GetStyle("ToolbarSearchRightCap"); } }

	public static GUIStyle Anim_BG { get { return GetStyle("AnimBG"); } }
	public static GUIStyle Anim_Trigger { get { return GetStyle("AnimTrigger"); } }
	public static GUIStyle Anim_TriggerSelected { get { return GetStyle("AnimTriggerDown"); } }

	public static GUIStyle MoveHandle { get { return GetStyle("MoveHandle"); } }
	public static GUIStyle RotateHandle { get { return GetStyle("RotateHandle"); } }
	
	public static GUIStyle WhiteBox { get { return GetStyle("WhiteBox"); } }
	public static GUIStyle Selection { get { return GetStyle("Selection"); } }
}

