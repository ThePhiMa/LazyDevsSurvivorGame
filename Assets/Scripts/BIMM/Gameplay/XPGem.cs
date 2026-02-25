using UnityEngine;
using BIMM.Data;

namespace BIMM.Gameplay
{
    public class XPGem : MonoBehaviour
    {
        [SerializeField] private XPGemData _data;

        public float XPValue => _data.XPValue;
    }
}
