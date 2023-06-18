using Photon.Pun;
using Photon.Realtime;

public class AvatarContainerChild : MonoBehaviourPunCallbacks
{
    public Player Owner => photonView.Owner;

    public override void OnEnable() {
        base.OnEnable();

        var container = FindObjectOfType<AvatarContainer>();
        if (container != null) {
            transform.SetParent(container.transform);
        }
    }
}