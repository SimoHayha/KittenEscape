using UnityEngine;
        _fireShadowEmitter = (DetonatorBurstEmitter)_fireShadow.AddComponent("DetonatorBurstEmitter");
        _fireShadow.transform.parent = this.transform;
        _fireShadow.transform.localRotation = Quaternion.identity;
        _fireShadowEmitter.material = fireShadowMaterial;
        _fireShadowEmitter.useWorldSpace = MyDetonator().useWorldSpace;
        _fireShadowEmitter.upwardsBias = MyDetonator().upwardsBias;
    }