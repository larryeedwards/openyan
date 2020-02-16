using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000308 RID: 776
	public class PostProcessingContext
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x000ADA02 File Offset: 0x000ABE02
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x000ADA0A File Offset: 0x000ABE0A
		public bool interrupted { get; private set; }

		// Token: 0x06001691 RID: 5777 RVA: 0x000ADA13 File Offset: 0x000ABE13
		public void Interrupt()
		{
			this.interrupted = true;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000ADA1C File Offset: 0x000ABE1C
		public PostProcessingContext Reset()
		{
			this.profile = null;
			this.camera = null;
			this.materialFactory = null;
			this.renderTextureFactory = null;
			this.interrupted = false;
			return this;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x000ADA42 File Offset: 0x000ABE42
		public bool isGBufferAvailable
		{
			get
			{
				return this.camera.actualRenderingPath == RenderingPath.DeferredShading;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x000ADA52 File Offset: 0x000ABE52
		public bool isHdr
		{
			get
			{
				return this.camera.allowHDR;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x000ADA5F File Offset: 0x000ABE5F
		public int width
		{
			get
			{
				return this.camera.pixelWidth;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x000ADA6C File Offset: 0x000ABE6C
		public int height
		{
			get
			{
				return this.camera.pixelHeight;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x000ADA79 File Offset: 0x000ABE79
		public Rect viewport
		{
			get
			{
				return this.camera.rect;
			}
		}

		// Token: 0x04001436 RID: 5174
		public PostProcessingProfile profile;

		// Token: 0x04001437 RID: 5175
		public Camera camera;

		// Token: 0x04001438 RID: 5176
		public MaterialFactory materialFactory;

		// Token: 0x04001439 RID: 5177
		public RenderTextureFactory renderTextureFactory;
	}
}
