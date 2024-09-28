using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSkin : NetworkBehaviour
{
    public NetworkVariable<int> skinColorIndex = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public MeshRenderer meshRenderer;

    private PlayerSkinSelector skinColorSelector;

    public override void OnNetworkSpawn()
    {
        skinColorSelector = FindObjectOfType<PlayerSkinSelector>();
        skinColorSelector.OnColorChanged += HandleColorChange;

        skinColorIndex.OnValueChanged += OnSkinColorChanged;
        OnSkinColorChanged(0, skinColorIndex.Value);
    }

    private void OnSkinColorChanged(int oldIndex, int newIndex)
    {
        if (meshRenderer != null && skinColorSelector != null)
        {
            meshRenderer.material.color = skinColorSelector.GetColorByIndex(newIndex);
        }
    }

    private void HandleColorChange(int newColorIndex)
    {
        if (IsOwner)
        {
            skinColorIndex.Value = newColorIndex;
        }
    }

    private void OnDestroy()
    {
        skinColorSelector.OnColorChanged -= HandleColorChange;
    }
}
