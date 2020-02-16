using System;
using UnityEngine;

// Token: 0x0200017A RID: 378
public class MusicNoteScript : MonoBehaviour
{
	// Token: 0x06000BEB RID: 3051 RVA: 0x0005DAF0 File Offset: 0x0005BEF0
	private void Update()
	{
		base.transform.localPosition += new Vector3(this.Speed * Time.deltaTime * -1f, 0f, 0f);
		if (!this.MusicMinigame.KeyDown)
		{
			this.GaveInput = false;
			if (this.InputManager.TappedUp)
			{
				this.GaveInput = true;
				this.Tapped = "up";
			}
			else if (this.InputManager.TappedDown)
			{
				this.GaveInput = true;
				this.Tapped = "down";
			}
			else if (this.InputManager.TappedRight)
			{
				this.GaveInput = true;
				this.Tapped = "right";
			}
			else if (this.InputManager.TappedLeft)
			{
				this.GaveInput = true;
				this.Tapped = "left";
			}
			if (Input.GetKeyDown(this.Direction) || (this.GaveInput && this.Tapped == this.Direction))
			{
				if (this.MusicMinigame.CurrentNote == this.ID)
				{
					if (base.transform.localPosition.x > -0.6f && base.transform.localPosition.x < -0.4f)
					{
						this.Rating = UnityEngine.Object.Instantiate<GameObject>(this.Perfect, base.transform.position, Quaternion.identity);
						this.Proceed = true;
						this.MusicMinigame.Health += 1f;
						this.MusicMinigame.CringeTimer = 0f;
						this.MusicMinigame.UpdateHealthBar();
					}
					else if (base.transform.localPosition.x > -0.4f && base.transform.localPosition.x < -0.2f)
					{
						this.Rating = UnityEngine.Object.Instantiate<GameObject>(this.Early, base.transform.position, Quaternion.identity);
						this.MusicMinigame.CringeTimer = 0f;
						this.Proceed = true;
					}
					else if (base.transform.localPosition.x > -0.8f && base.transform.localPosition.x < -0.4f)
					{
						this.Rating = UnityEngine.Object.Instantiate<GameObject>(this.Late, base.transform.position, Quaternion.identity);
						this.MusicMinigame.CringeTimer = 0f;
						this.Proceed = true;
					}
				}
			}
			else if (Input.anyKeyDown && base.transform.localPosition.x > -0.8f && base.transform.localPosition.x < -0.2f && !this.MusicMinigame.GameOver)
			{
				this.Rating = UnityEngine.Object.Instantiate<GameObject>(this.Wrong, base.transform.position, Quaternion.identity);
				this.Proceed = true;
				this.MusicMinigame.Cringe();
				if (!this.MusicMinigame.LockHealth)
				{
					this.MusicMinigame.Health -= 10f;
				}
				this.MusicMinigame.UpdateHealthBar();
			}
		}
		if (this.Proceed)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Ripple, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = base.transform.parent;
			gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
			this.Rating.transform.parent = base.transform.parent;
			this.Rating.transform.localPosition = new Vector3(-0.5f, 0.25f, 0f);
			this.Rating.transform.localScale = new Vector3(0.3f, 0.15f, 0.15f);
			this.MusicMinigame.CurrentNote++;
			this.MusicMinigame.KeyDown = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else if (base.transform.localPosition.x < -0.65f && this.MusicMinigame.CurrentNote == this.ID)
		{
			this.MusicMinigame.CurrentNote++;
		}
		if (base.transform.localPosition.x < -0.94f && !this.MusicMinigame.GameOver)
		{
			this.Rating = UnityEngine.Object.Instantiate<GameObject>(this.Miss, base.transform.position, Quaternion.identity);
			this.Rating.transform.parent = base.transform.parent;
			this.Rating.transform.localPosition = new Vector3(-0.94f, 0.25f, 0f);
			this.Rating.transform.localScale = new Vector3(0.3f, 0.15f, 0.15f);
			UnityEngine.Object.Destroy(base.gameObject);
			this.MusicMinigame.Cringe();
			if (!this.MusicMinigame.LockHealth)
			{
				this.MusicMinigame.Health -= 10f;
			}
			this.MusicMinigame.UpdateHealthBar();
		}
	}

	// Token: 0x0400099D RID: 2461
	public MusicMinigameScript MusicMinigame;

	// Token: 0x0400099E RID: 2462
	public InputManagerScript InputManager;

	// Token: 0x0400099F RID: 2463
	public GameObject Ripple;

	// Token: 0x040009A0 RID: 2464
	public GameObject Perfect;

	// Token: 0x040009A1 RID: 2465
	public GameObject Wrong;

	// Token: 0x040009A2 RID: 2466
	public GameObject Early;

	// Token: 0x040009A3 RID: 2467
	public GameObject Late;

	// Token: 0x040009A4 RID: 2468
	public GameObject Miss;

	// Token: 0x040009A5 RID: 2469
	public GameObject Rating;

	// Token: 0x040009A6 RID: 2470
	public string XboxDirection;

	// Token: 0x040009A7 RID: 2471
	public string Direction;

	// Token: 0x040009A8 RID: 2472
	public string Tapped;

	// Token: 0x040009A9 RID: 2473
	public bool GaveInput;

	// Token: 0x040009AA RID: 2474
	public bool Proceed;

	// Token: 0x040009AB RID: 2475
	public float Speed;

	// Token: 0x040009AC RID: 2476
	public int ID;
}
