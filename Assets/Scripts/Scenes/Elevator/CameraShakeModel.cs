
public class CameraShakeModel
{
    public readonly int durationCameraShakeOnElevatorStoppedMs;
    public readonly float amplitudeCameraShakeOnElevatorStoppedMs;
    public readonly int durationDelayBeforeShake;
    public readonly int durationDelayAfterShake;

    public CameraShakeModel(
        int durationCameraShakeOnElevatorStoppedMs,
        float amplitudeCameraShakeOnElevatorStoppedMs,
        int durationDelayBeforeShake,
        int durationDelayAfterShake
        )
    {
        this.durationCameraShakeOnElevatorStoppedMs = durationCameraShakeOnElevatorStoppedMs;
        this.amplitudeCameraShakeOnElevatorStoppedMs = amplitudeCameraShakeOnElevatorStoppedMs;
        this.durationDelayBeforeShake = durationDelayBeforeShake;
        this.durationDelayAfterShake = durationDelayAfterShake;
    }
}
