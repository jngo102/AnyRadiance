namespace Galaxy.Api
{
	
	public enum LobbyMemberStateChange
	{
		LOBBY_MEMBER_STATE_CHANGED_ENTERED = 1,
		LOBBY_MEMBER_STATE_CHANGED_LEFT = 2,
		LOBBY_MEMBER_STATE_CHANGED_DISCONNECTED = 4,
		LOBBY_MEMBER_STATE_CHANGED_KICKED = 8,
		LOBBY_MEMBER_STATE_CHANGED_BANNED = 0x10
	}
}