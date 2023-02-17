using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	[CallbackIdentity(4527)]
	public struct HTML_BrowserRestarted_t
	{
		public const int k_iCallback = 4527;
	
		public HHTMLBrowser unBrowserHandle;
	
		public HHTMLBrowser unOldBrowserHandle;
	}
}