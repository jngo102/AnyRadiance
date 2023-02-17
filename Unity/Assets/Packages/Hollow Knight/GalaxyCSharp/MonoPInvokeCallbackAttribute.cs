using System;

public class MonoPInvokeCallbackAttribute : Attribute
{
	private Type type;

	public MonoPInvokeCallbackAttribute(Type t)
	{
		type = t;
	}
}
