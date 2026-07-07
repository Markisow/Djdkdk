using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200006F RID: 111
[RequireComponent(typeof(AudioSource))]
public class SynchronizedAudio : NetworkBehaviour
{
	// Token: 0x0600038D RID: 909 RVA: 0x0000B396 File Offset: 0x00009596
	private void Awake()
	{
		this.audioSource = base.GetComponent<AudioSource>();
		this.initialVolume = this.audioSource.volume;
		this.initialPitch = this.audioSource.pitch;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x0000B3C6 File Offset: 0x000095C6
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		this.InitializeNetworkVariables(NetworkingUtils.CompressFloatToByte(this.initialVolume, 0f, 1f), NetworkingUtils.CompressFloatToByte(this.initialPitch, 0f, 1f));
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00025510 File Offset: 0x00023710
	public override void OnNetworkSpawn()
	{
		NetworkVariable<byte> volume = this.Volume;
		volume.OnValueChanged = (NetworkVariable<byte>.OnValueChangedDelegate)Delegate.Combine(volume.OnValueChanged, new NetworkVariable<byte>.OnValueChangedDelegate(this.OnVolumeChanged));
		NetworkVariable<byte> pitch = this.Pitch;
		pitch.OnValueChanged = (NetworkVariable<byte>.OnValueChangedDelegate)Delegate.Combine(pitch.OnValueChanged, new NetworkVariable<byte>.OnValueChangedDelegate(this.OnPitchChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x06000390 RID: 912 RVA: 0x0000B3FF File Offset: 0x000095FF
	protected override void OnNetworkPostSpawn()
	{
		if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsConnectedClient)
		{
			this.ProcessInitialNetworkVariableValues();
		}
		base.OnNetworkPostSpawn();
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0000B425 File Offset: 0x00009625
	protected override void OnNetworkSessionSynchronized()
	{
		this.ProcessInitialNetworkVariableValues();
		base.OnNetworkSessionSynchronized();
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00025574 File Offset: 0x00023774
	public override void OnNetworkDespawn()
	{
		NetworkVariable<byte> volume = this.Volume;
		volume.OnValueChanged = (NetworkVariable<byte>.OnValueChangedDelegate)Delegate.Remove(volume.OnValueChanged, new NetworkVariable<byte>.OnValueChangedDelegate(this.OnVolumeChanged));
		NetworkVariable<byte> pitch = this.Pitch;
		pitch.OnValueChanged = (NetworkVariable<byte>.OnValueChangedDelegate)Delegate.Remove(pitch.OnValueChanged, new NetworkVariable<byte>.OnValueChangedDelegate(this.OnPitchChanged));
		if (this.stopOnDespawn && this.audioSource != null)
		{
			this.audioSource.Stop();
			if (this.audioSourceSequence != null)
			{
				this.audioSourceSequence.Kill(false);
			}
		}
		base.OnNetworkDespawn();
	}

	// Token: 0x06000393 RID: 915 RVA: 0x0000B433 File Offset: 0x00009633
	public override void OnDestroy()
	{
		this.DOKill(false);
	}

	// Token: 0x06000394 RID: 916 RVA: 0x0000B43D File Offset: 0x0000963D
	public void InitializeNetworkVariables(byte volume = 0, byte pitch = 0)
	{
		if (this.isNetworkVariablesInitialized)
		{
			return;
		}
		this.isNetworkVariablesInitialized = true;
		this.Volume = new NetworkVariable<byte>(volume, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.Pitch = new NetworkVariable<byte>(pitch, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
	}

	// Token: 0x06000395 RID: 917 RVA: 0x0000B46B File Offset: 0x0000966B
	private void ProcessInitialNetworkVariableValues()
	{
		this.OnVolumeChanged(0, this.Volume.Value);
		this.OnPitchChanged(0, this.Pitch.Value);
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0002560C File Offset: 0x0002380C
	private void Update()
	{
		if (!base.IsSpawned)
		{
			return;
		}
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (this.isPlaying && !this.audioSource.isPlaying)
		{
			this.isPlaying = false;
		}
		if (this.isPlaying)
		{
			this.time += Time.deltaTime;
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x0000B491 File Offset: 0x00009691
	private void OnVolumeChanged(byte oldVolume, byte newVolume)
	{
		this.audioSource.volume = NetworkingUtils.DecompressByteToFloat(newVolume, 0f, 1f);
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0000B4AE File Offset: 0x000096AE
	private void OnPitchChanged(byte oldPitch, byte newPitch)
	{
		this.audioSource.pitch = NetworkingUtils.DecompressByteToFloat(newPitch, 0f, 1f);
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0000B4CB File Offset: 0x000096CB
	public void Server_SetVolume(float volume)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Volume.Value = NetworkingUtils.CompressFloatToByte(volume, 0f, 1f);
	}

	// Token: 0x0600039A RID: 922 RVA: 0x0000B4F5 File Offset: 0x000096F5
	public void Server_SetPitch(float pitch)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Pitch.Value = NetworkingUtils.CompressFloatToByte(pitch, 0f, 1f);
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00025668 File Offset: 0x00023868
	public void Server_Play(float volume = -1f, float pitch = -1f, bool isOneShot = false, int clipIndex = -1, float time = 0f, bool randomClip = false, bool randomTime = false, bool fadeIn = false, float fadeInDuration = 0f, bool fadeOut = false, float fadeOutDuration = 0f, float duration = -1f)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		volume = ((volume == -1f) ? this.initialVolume : volume);
		pitch = ((pitch == -1f) ? this.initialPitch : pitch);
		if (randomClip && this.audioClips.Count > 0)
		{
			clipIndex = UnityEngine.Random.Range(0, this.audioClips.Count);
		}
		float num = (clipIndex == -1) ? this.audioSource.clip.length : this.audioClips[clipIndex].length;
		if (randomTime)
		{
			time = UnityEngine.Random.Range(0f, Mathf.Max(num - duration - fadeInDuration - fadeOutDuration, 0f));
		}
		this.duration = ((duration == -1f) ? num : duration);
		if (!isOneShot)
		{
			this.Volume.Value = NetworkingUtils.CompressFloatToByte(volume, 0f, 1f);
			this.Pitch.Value = NetworkingUtils.CompressFloatToByte(pitch, 0f, 1f);
			this.clipIndex = clipIndex;
			this.time = time;
			this.fadeIn = fadeIn;
			this.fadeInDuration = fadeInDuration;
			this.fadeOut = fadeOut;
			this.fadeOutDuration = fadeOutDuration;
			this.isPlaying = true;
		}
		this.Server_PlayRpc(volume, pitch, isOneShot, clipIndex, time, fadeIn, fadeInDuration, fadeOut, fadeOutDuration, duration, base.RpcTarget.Everyone);
	}

	// Token: 0x0600039C RID: 924 RVA: 0x000257C8 File Offset: 0x000239C8
	[Rpc(SendTo.SpecifiedInParams, InvokePermission = RpcInvokePermission.Server, DeferLocal = true)]
	private void Server_PlayRpc(float volume, float pitch, bool isOneShot, int clipIndex, float time, bool fadeIn, float fadeInDuration, bool fadeOut, float fadeOutDuration, float duration, RpcParams rpcParams = default(RpcParams))
	{
		NetworkManager networkManager = base.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			uint rpcMethodId = 408477299U;
			RpcAttribute.RpcAttributeParams attributeParams = new RpcAttribute.RpcAttributeParams
			{
				InvokePermission = RpcInvokePermission.Server,
				DeferLocal = true
			};
			FastBufferWriter writer = base.__beginSendRpc(rpcMethodId, rpcParams, attributeParams, SendTo.SpecifiedInParams, RpcDelivery.Reliable);
			writer.WriteValueSafe<float>(volume, default(FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe<float>(pitch, default(FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe<bool>(isOneShot, default(FastBufferWriter.ForPrimitives));
			BytePacker.WriteValueBitPacked(writer, clipIndex);
			writer.WriteValueSafe<float>(time, default(FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe<bool>(fadeIn, default(FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe<float>(fadeInDuration, default(FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe<bool>(fadeOut, default(FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe<float>(fadeOutDuration, default(FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe<float>(duration, default(FastBufferWriter.ForPrimitives));
			base.__endSendRpc(ref writer, 408477299U, rpcParams, attributeParams, SendTo.SpecifiedInParams, RpcDelivery.Reliable);
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			return;
		}
		this.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
		this.audioSource.volume = volume;
		this.audioSource.pitch = pitch;
		if (clipIndex != -1 && this.audioClips.Count > 0)
		{
			this.audioSource.clip = this.audioClips[clipIndex];
		}
		if (isOneShot)
		{
			this.audioSource.PlayOneShot(this.audioSource.clip);
			return;
		}
		this.audioSourceSequence = DOTween.Sequence(this);
		if (fadeIn)
		{
			this.audioSource.volume = 0f;
			this.audioSourceSequence.Append(DOTween.To(() => this.audioSource.volume, delegate(float x)
			{
				this.audioSource.volume = x;
			}, volume, fadeInDuration)).SetEase(Ease.Linear);
		}
		this.audioSourceSequence.AppendInterval(duration - fadeInDuration - fadeOutDuration - time);
		if (fadeOut)
		{
			this.audioSourceSequence.Append(DOTween.To(() => this.audioSource.volume, delegate(float x)
			{
				this.audioSource.volume = x;
			}, 0f, fadeOutDuration).OnComplete(delegate
			{
				this.audioSource.Stop();
			})).SetEase(Ease.Linear);
		}
		this.audioSource.Play();
		this.audioSource.time = time;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00025ACC File Offset: 0x00023CCC
	public void Server_ForceSynchronizeClientId(ulong clientId)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!this.isPlaying)
		{
			return;
		}
		this.Server_PlayRpc(NetworkingUtils.DecompressByteToFloat(this.Volume.Value, 0f, 1f), NetworkingUtils.DecompressByteToFloat(this.Pitch.Value, 0f, 1f), false, this.clipIndex, this.time, false, this.fadeInDuration, this.fadeOut, this.fadeOutDuration, this.duration, base.RpcTarget.Single(clientId, RpcTargetUse.Temp));
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00025B64 File Offset: 0x00023D64
	protected override void __initializeVariables()
	{
		bool flag = this.Volume == null;
		if (flag)
		{
			throw new Exception("SynchronizedAudio.Volume cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.Volume.Initialize(this);
		base.__nameNetworkVariable(this.Volume, "Volume");
		this.NetworkVariableFields.Add(this.Volume);
		flag = (this.Pitch == null);
		if (flag)
		{
			throw new Exception("SynchronizedAudio.Pitch cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.Pitch.Initialize(this);
		base.__nameNetworkVariable(this.Pitch, "Pitch");
		this.NetworkVariableFields.Add(this.Pitch);
		base.__initializeVariables();
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x0000B561 File Offset: 0x00009761
	protected override void __initializeRpcs()
	{
		base.__registerRpc(408477299U, new NetworkBehaviour.RpcReceiveHandler(SynchronizedAudio.__rpc_handler_408477299), "Server_PlayRpc", RpcInvokePermission.Server);
		base.__initializeRpcs();
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00025C14 File Offset: 0x00023E14
	private static void __rpc_handler_408477299(NetworkBehaviour target, FastBufferReader reader, __RpcParams rpcParams)
	{
		NetworkManager networkManager = target.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			return;
		}
		float volume;
		reader.ReadValueSafe<float>(out volume, default(FastBufferWriter.ForPrimitives));
		float pitch;
		reader.ReadValueSafe<float>(out pitch, default(FastBufferWriter.ForPrimitives));
		bool isOneShot;
		reader.ReadValueSafe<bool>(out isOneShot, default(FastBufferWriter.ForPrimitives));
		int num;
		ByteUnpacker.ReadValueBitPacked(reader, out num);
		float num2;
		reader.ReadValueSafe<float>(out num2, default(FastBufferWriter.ForPrimitives));
		bool flag;
		reader.ReadValueSafe<bool>(out flag, default(FastBufferWriter.ForPrimitives));
		float num3;
		reader.ReadValueSafe<float>(out num3, default(FastBufferWriter.ForPrimitives));
		bool flag2;
		reader.ReadValueSafe<bool>(out flag2, default(FastBufferWriter.ForPrimitives));
		float num4;
		reader.ReadValueSafe<float>(out num4, default(FastBufferWriter.ForPrimitives));
		float num5;
		reader.ReadValueSafe<float>(out num5, default(FastBufferWriter.ForPrimitives));
		RpcParams ext = rpcParams.Ext;
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Execute;
		((SynchronizedAudio)target).Server_PlayRpc(volume, pitch, isOneShot, num, num2, flag, num3, flag2, num4, num5, ext);
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x0000B58C File Offset: 0x0000978C
	protected internal override string __getTypeName()
	{
		return "SynchronizedAudio";
	}

	// Token: 0x04000288 RID: 648
	[Header("Settings")]
	[SerializeField]
	private bool stopOnDespawn = true;

	// Token: 0x04000289 RID: 649
	[Header("References")]
	[SerializeField]
	private List<AudioClip> audioClips = new List<AudioClip>();

	// Token: 0x0400028A RID: 650
	private NetworkVariable<byte> Volume;

	// Token: 0x0400028B RID: 651
	private NetworkVariable<byte> Pitch;

	// Token: 0x0400028C RID: 652
	private bool isNetworkVariablesInitialized;

	// Token: 0x0400028D RID: 653
	private AudioSource audioSource;

	// Token: 0x0400028E RID: 654
	private int clipIndex;

	// Token: 0x0400028F RID: 655
	private float time;

	// Token: 0x04000290 RID: 656
	private bool fadeIn;

	// Token: 0x04000291 RID: 657
	private float fadeInDuration;

	// Token: 0x04000292 RID: 658
	private bool fadeOut;

	// Token: 0x04000293 RID: 659
	private float fadeOutDuration;

	// Token: 0x04000294 RID: 660
	private float duration;

	// Token: 0x04000295 RID: 661
	private bool isPlaying;

	// Token: 0x04000296 RID: 662
	private float initialVolume;

	// Token: 0x04000297 RID: 663
	private float initialPitch;

	// Token: 0x04000298 RID: 664
	private Sequence audioSourceSequence;
}
