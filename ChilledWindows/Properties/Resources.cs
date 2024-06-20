using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ChilledWindows.Properties
{
	// Token: 0x02000004 RID: 4
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002841 File Offset: 0x00000A41
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002849 File Offset: 0x00000A49
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("ChilledWindows.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002875 File Offset: 0x00000A75
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000287C File Offset: 0x00000A7C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002884 File Offset: 0x00000A84
		internal static byte[] Chilled_Windows
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("Chilled_Windows", Resources.resourceCulture);
			}
		}

		// Token: 0x0400001D RID: 29
		private static ResourceManager resourceMan;

		// Token: 0x0400001E RID: 30
		private static CultureInfo resourceCulture;
	}
}
