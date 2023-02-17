using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	
	public class IStorage : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal IStorage(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(IStorage obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IStorage()
		{
			Dispose();
		}
	
		public virtual void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IStorage(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public virtual void FileWrite(string fileName, byte[] data, uint dataSize)
		{
			GalaxyInstancePINVOKE.IStorage_FileWrite(swigCPtr, fileName, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint FileRead(string fileName, byte[] data, uint dataSize)
		{
			uint result = GalaxyInstancePINVOKE.IStorage_FileRead(swigCPtr, fileName, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void FileDelete(string fileName)
		{
			GalaxyInstancePINVOKE.IStorage_FileDelete(swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual bool FileExists(string fileName)
		{
			bool result = GalaxyInstancePINVOKE.IStorage_FileExists(swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint GetFileSize(string fileName)
		{
			uint result = GalaxyInstancePINVOKE.IStorage_GetFileSize(swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint GetFileTimestamp(string fileName)
		{
			uint result = GalaxyInstancePINVOKE.IStorage_GetFileTimestamp(swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint GetFileCount()
		{
			uint result = GalaxyInstancePINVOKE.IStorage_GetFileCount(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual string GetFileNameByIndex(uint index)
		{
			string result = GalaxyInstancePINVOKE.IStorage_GetFileNameByIndex(swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetFileNameCopyByIndex(uint index, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStorage_GetFileNameCopyByIndex(swigCPtr, index, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}
	
		public virtual void FileShare(string fileName, IFileShareListener listener)
		{
			GalaxyInstancePINVOKE.IStorage_FileShare__SWIG_0(swigCPtr, fileName, IFileShareListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void FileShare(string fileName)
		{
			GalaxyInstancePINVOKE.IStorage_FileShare__SWIG_1(swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void DownloadSharedFile(ulong sharedFileID, ISharedFileDownloadListener listener)
		{
			GalaxyInstancePINVOKE.IStorage_DownloadSharedFile__SWIG_0(swigCPtr, sharedFileID, ISharedFileDownloadListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void DownloadSharedFile(ulong sharedFileID)
		{
			GalaxyInstancePINVOKE.IStorage_DownloadSharedFile__SWIG_1(swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual string GetSharedFileName(ulong sharedFileID)
		{
			string result = GalaxyInstancePINVOKE.IStorage_GetSharedFileName(swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetSharedFileNameCopy(ulong sharedFileID, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStorage_GetSharedFileNameCopy(swigCPtr, sharedFileID, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}
	
		public virtual uint GetSharedFileSize(ulong sharedFileID)
		{
			uint result = GalaxyInstancePINVOKE.IStorage_GetSharedFileSize(swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual GalaxyID GetSharedFileOwner(ulong sharedFileID)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IStorage_GetSharedFileOwner(swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID result = null;
			if (intPtr != IntPtr.Zero)
			{
				result = new GalaxyID(intPtr, cMemoryOwn: true);
			}
			return result;
		}
	
		public virtual uint SharedFileRead(ulong sharedFileID, byte[] data, uint dataSize, uint offset)
		{
			uint result = GalaxyInstancePINVOKE.IStorage_SharedFileRead__SWIG_0(swigCPtr, sharedFileID, data, dataSize, offset);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint SharedFileRead(ulong sharedFileID, byte[] data, uint dataSize)
		{
			uint result = GalaxyInstancePINVOKE.IStorage_SharedFileRead__SWIG_1(swigCPtr, sharedFileID, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void SharedFileClose(ulong sharedFileID)
		{
			GalaxyInstancePINVOKE.IStorage_SharedFileClose(swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint GetDownloadedSharedFileCount()
		{
			uint result = GalaxyInstancePINVOKE.IStorage_GetDownloadedSharedFileCount(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual ulong GetDownloadedSharedFileByIndex(uint index)
		{
			ulong result = GalaxyInstancePINVOKE.IStorage_GetDownloadedSharedFileByIndex(swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}