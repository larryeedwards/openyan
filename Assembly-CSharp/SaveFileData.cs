using System;
using System.Xml.Serialization;

// Token: 0x020004F1 RID: 1265
[XmlRoot]
[Serializable]
public class SaveFileData
{
	// Token: 0x04002B26 RID: 11046
	public ApplicationSaveData applicationData = new ApplicationSaveData();

	// Token: 0x04002B27 RID: 11047
	public ClassSaveData classData = new ClassSaveData();

	// Token: 0x04002B28 RID: 11048
	public ClubSaveData clubData = new ClubSaveData();

	// Token: 0x04002B29 RID: 11049
	public CollectibleSaveData collectibleData = new CollectibleSaveData();

	// Token: 0x04002B2A RID: 11050
	public ConversationSaveData conversationData = new ConversationSaveData();

	// Token: 0x04002B2B RID: 11051
	public DateSaveData dateData = new DateSaveData();

	// Token: 0x04002B2C RID: 11052
	public DatingSaveData datingData = new DatingSaveData();

	// Token: 0x04002B2D RID: 11053
	public EventSaveData eventData = new EventSaveData();

	// Token: 0x04002B2E RID: 11054
	public GameSaveData gameData = new GameSaveData();

	// Token: 0x04002B2F RID: 11055
	public HomeSaveData homeData = new HomeSaveData();

	// Token: 0x04002B30 RID: 11056
	public MissionModeSaveData missionModeData = new MissionModeSaveData();

	// Token: 0x04002B31 RID: 11057
	public OptionSaveData optionData = new OptionSaveData();

	// Token: 0x04002B32 RID: 11058
	public PlayerSaveData playerData = new PlayerSaveData();

	// Token: 0x04002B33 RID: 11059
	public PoseModeSaveData poseModeData = new PoseModeSaveData();

	// Token: 0x04002B34 RID: 11060
	public SaveFileSaveData saveFileData = new SaveFileSaveData();

	// Token: 0x04002B35 RID: 11061
	public SchemeSaveData schemeData = new SchemeSaveData();

	// Token: 0x04002B36 RID: 11062
	public SchoolSaveData schoolData = new SchoolSaveData();

	// Token: 0x04002B37 RID: 11063
	public SenpaiSaveData senpaiData = new SenpaiSaveData();

	// Token: 0x04002B38 RID: 11064
	public StudentSaveData studentData = new StudentSaveData();

	// Token: 0x04002B39 RID: 11065
	public TaskSaveData taskData = new TaskSaveData();

	// Token: 0x04002B3A RID: 11066
	public YanvaniaSaveData yanvaniaData = new YanvaniaSaveData();
}
