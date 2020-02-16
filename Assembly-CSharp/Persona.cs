using System;
using UnityEngine;

// Token: 0x020003BB RID: 955
[Serializable]
public class Persona
{
	// Token: 0x06001958 RID: 6488 RVA: 0x000EC7ED File Offset: 0x000EABED
	public Persona(PersonaType type)
	{
		this.type = type;
	}

	// Token: 0x170003AF RID: 943
	// (get) Token: 0x06001959 RID: 6489 RVA: 0x000EC7FC File Offset: 0x000EABFC
	public PersonaType Type
	{
		get
		{
			return this.type;
		}
	}

	// Token: 0x04001DF9 RID: 7673
	[SerializeField]
	private PersonaType type;

	// Token: 0x04001DFA RID: 7674
	public static readonly PersonaTypeAndStringDictionary PersonaNames = new PersonaTypeAndStringDictionary
	{
		{
			PersonaType.None,
			"None"
		},
		{
			PersonaType.Loner,
			"Loner"
		},
		{
			PersonaType.TeachersPet,
			"Teacher's Pet"
		},
		{
			PersonaType.Heroic,
			"Heroic"
		},
		{
			PersonaType.Coward,
			"Coward"
		},
		{
			PersonaType.Evil,
			"Evil"
		},
		{
			PersonaType.SocialButterfly,
			"Social Butterfly"
		},
		{
			PersonaType.Lovestruck,
			"Lovestruck"
		},
		{
			PersonaType.Dangerous,
			"Dangerous"
		},
		{
			PersonaType.Strict,
			"Strict"
		},
		{
			PersonaType.PhoneAddict,
			"Phone Addict"
		},
		{
			PersonaType.Fragile,
			"Fragile"
		},
		{
			PersonaType.Spiteful,
			"Spiteful"
		},
		{
			PersonaType.Sleuth,
			"Sleuth"
		},
		{
			PersonaType.Vengeful,
			"Vengeful"
		},
		{
			PersonaType.Protective,
			"Protective"
		},
		{
			PersonaType.Violent,
			"Violent"
		},
		{
			PersonaType.Nemesis,
			"?????"
		}
	};
}
