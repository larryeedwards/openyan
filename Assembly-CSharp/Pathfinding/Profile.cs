using System;
using System.Diagnostics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000123 RID: 291
	public class Profile
	{
		// Token: 0x06000A5A RID: 2650 RVA: 0x0004FCC9 File Offset: 0x0004E0C9
		public Profile(string name)
		{
			this.name = name;
			this.watch = new Stopwatch();
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0004FCEE File Offset: 0x0004E0EE
		public int ControlValue()
		{
			return this.control;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0004FCF6 File Offset: 0x0004E0F6
		public static void WriteCSV(string path, params Profile[] profiles)
		{
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0004FCF8 File Offset: 0x0004E0F8
		public void Run(Action action)
		{
			action();
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0004FD00 File Offset: 0x0004E100
		[Conditional("PROFILE")]
		public void Start()
		{
			this.watch.Start();
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0004FD0D File Offset: 0x0004E10D
		[Conditional("PROFILE")]
		public void Stop()
		{
			this.counter++;
			this.watch.Stop();
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0004FD28 File Offset: 0x0004E128
		[Conditional("PROFILE")]
		public void Log()
		{
			UnityEngine.Debug.Log(this.ToString());
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0004FD35 File Offset: 0x0004E135
		[Conditional("PROFILE")]
		public void ConsoleLog()
		{
			Console.WriteLine(this.ToString());
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0004FD44 File Offset: 0x0004E144
		[Conditional("PROFILE")]
		public void Stop(int control)
		{
			this.counter++;
			this.watch.Stop();
			if (this.control == 1073741824)
			{
				this.control = control;
			}
			else if (this.control != control)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Control numbers do not match ",
					this.control,
					" != ",
					control
				}));
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0004FDCC File Offset: 0x0004E1CC
		[Conditional("PROFILE")]
		public void Control(Profile other)
		{
			if (this.ControlValue() != other.ControlValue())
			{
				throw new Exception(string.Concat(new object[]
				{
					"Control numbers do not match (",
					this.name,
					" ",
					other.name,
					") ",
					this.ControlValue(),
					" != ",
					other.ControlValue()
				}));
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0004FE4C File Offset: 0x0004E24C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.name,
				" #",
				this.counter,
				" ",
				this.watch.Elapsed.TotalMilliseconds.ToString("0.0 ms"),
				" avg: ",
				(this.watch.Elapsed.TotalMilliseconds / (double)this.counter).ToString("0.00 ms")
			});
		}

		// Token: 0x04000718 RID: 1816
		private const bool PROFILE_MEM = false;

		// Token: 0x04000719 RID: 1817
		public readonly string name;

		// Token: 0x0400071A RID: 1818
		private readonly Stopwatch watch;

		// Token: 0x0400071B RID: 1819
		private int counter;

		// Token: 0x0400071C RID: 1820
		private long mem;

		// Token: 0x0400071D RID: 1821
		private long smem;

		// Token: 0x0400071E RID: 1822
		private int control = 1073741824;

		// Token: 0x0400071F RID: 1823
		private const bool dontCountFirst = false;
	}
}
