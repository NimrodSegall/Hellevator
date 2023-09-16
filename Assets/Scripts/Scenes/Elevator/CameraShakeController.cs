using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraShakeController : Controller<CameraShakeModel>
{
    [Inject(Id = MainGameWindowInstallerIds.VirtualCamera)]
    private readonly CinemachineVirtualCamera _virtualCamera;

    private CinemachineBasicMultiChannelPerlin _virtualCameraPerlinNoise;

    public CameraShakeController(CameraShakeModel model) : base(model)
    {

    }

    public override void InitializeWithExistingModel()
    {
        base.InitializeWithExistingModel();
        _virtualCameraPerlinNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public async UniTask ShakeElevator()
    {
        await Shake();
    }

    private async UniTask Shake()
    {
        await UniTask.Delay(_model.durationDelayBeforeShake);
        _virtualCameraPerlinNoise.m_AmplitudeGain = _model.amplitudeCameraShakeOnElevatorStoppedMs;
        await UniTask.Delay(_model.durationCameraShakeOnElevatorStoppedMs);
        _virtualCameraPerlinNoise.m_AmplitudeGain = 0;
        await UniTask.Delay(_model.durationDelayAfterShake);
    }
}
