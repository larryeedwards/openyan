using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000563 RID: 1379
public class ArrayWrapper<T> : IEnumerable
{
	// Token: 0x060021DE RID: 8670 RVA: 0x0019AB9C File Offset: 0x00198F9C
	public ArrayWrapper(int size)
	{
		this.elements = new T[size];
	}

	// Token: 0x060021DF RID: 8671 RVA: 0x0019ABB0 File Offset: 0x00198FB0
	public ArrayWrapper(T[] elements)
	{
		this.elements = elements;
	}

	// Token: 0x170004A7 RID: 1191
	public T this[int i]
	{
		get
		{
			return this.elements[i];
		}
		set
		{
			this.elements[i] = value;
		}
	}

	// Token: 0x170004A8 RID: 1192
	// (get) Token: 0x060021E2 RID: 8674 RVA: 0x0019ABDC File Offset: 0x00198FDC
	public int Length
	{
		get
		{
			return this.elements.Length;
		}
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x0019ABE6 File Offset: 0x00198FE6
	public T[] Get()
	{
		return this.elements;
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x0019ABEE File Offset: 0x00198FEE
	public IEnumerator GetEnumerator()
	{
		return this.elements.GetEnumerator();
	}

	// Token: 0x04003754 RID: 14164
	[SerializeField]
	private T[] elements;
}
