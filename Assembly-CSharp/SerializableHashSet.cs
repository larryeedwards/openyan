using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x0200057E RID: 1406
public class SerializableHashSet<T> : HashSet<T>, ISerializationCallbackReceiver, IXmlSerializable
{
	// Token: 0x0600222F RID: 8751 RVA: 0x0019B7FC File Offset: 0x00199BFC
	public SerializableHashSet()
	{
		this.elements = new List<T>();
	}

	// Token: 0x06002230 RID: 8752 RVA: 0x0019B810 File Offset: 0x00199C10
	public void OnBeforeSerialize()
	{
		this.elements.Clear();
		foreach (T item in this)
		{
			this.elements.Add(item);
		}
	}

	// Token: 0x06002231 RID: 8753 RVA: 0x0019B878 File Offset: 0x00199C78
	public void OnAfterDeserialize()
	{
		base.Clear();
		for (int i = 0; i < this.elements.Count; i++)
		{
			base.Add(this.elements[i]);
		}
	}

	// Token: 0x06002232 RID: 8754 RVA: 0x0019B8BA File Offset: 0x00199CBA
	public XmlSchema GetSchema()
	{
		return null;
	}

	// Token: 0x06002233 RID: 8755 RVA: 0x0019B8C0 File Offset: 0x00199CC0
	public void ReadXml(XmlReader reader)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		bool isEmptyElement = reader.IsEmptyElement;
		reader.Read();
		if (isEmptyElement)
		{
			return;
		}
		while (reader.NodeType != XmlNodeType.EndElement)
		{
			reader.ReadStartElement("Element");
			T item = (T)((object)xmlSerializer.Deserialize(reader));
			reader.ReadEndElement();
			base.Add(item);
			reader.MoveToContent();
		}
	}

	// Token: 0x06002234 RID: 8756 RVA: 0x0019B934 File Offset: 0x00199D34
	public void WriteXml(XmlWriter writer)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		foreach (T t in this)
		{
			writer.WriteStartElement("Element");
			xmlSerializer.Serialize(writer, t);
			writer.WriteEndElement();
		}
	}

	// Token: 0x04003773 RID: 14195
	[SerializeField]
	private List<T> elements;

	// Token: 0x04003774 RID: 14196
	private const string XML_Element = "Element";
}
