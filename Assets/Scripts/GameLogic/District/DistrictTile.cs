using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameLogic.District
{
    public enum DistrictType
    {
        Normal = 0,
        Fact,
        CounterIntel,
        Intelligence,
        School,
        HQ,
    }

    public enum DistrictState
    {
        Fine,
        Broken
    }

    [Serializable]
    public struct DistrictTypeImage
    {
        public DistrictType Type;
        public Image Image;
    }

    [ExecuteInEditMode]
    public class DistrictTile : MonoBehaviour
    {
        public DistrictType Type;
        public DistrictState State;

        public DistrictTypeImage[] DistrictTypeImageArray;

        public Image BrokenImage;

        private Dictionary<DistrictType, Image> __districtTypeDictionary;
        private Dictionary<DistrictType, Image> _districtTypeDictionary
        {
            get
            {
                if (__districtTypeDictionary == null)
                {
                    __districtTypeDictionary = new Dictionary<DistrictType, Image>();
                    foreach(DistrictTypeImage districtImage in DistrictTypeImageArray)
                    {
                        __districtTypeDictionary[districtImage.Type] = districtImage.Image;
                    }
                }
                return __districtTypeDictionary;
            }
        }

        private void Start()
        {
            ChangeDistrictType(Type);
            ChangeDistrictState(State);
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                ChangeDistrictType(Type);
                ChangeDistrictState(State);
            }
        }

        public void ChangeDistrictType(DistrictType type)
        {
            Type = type;
            foreach (DistrictTypeImage districtTypeImage in DistrictTypeImageArray)
            {
                districtTypeImage.Image.gameObject.SetActive(false);
            }

            if (_districtTypeDictionary.ContainsKey(type))
            {
                var districtStateImage = _districtTypeDictionary[type];
                districtStateImage.gameObject.SetActive(true);
            }
        }

        public void ChangeDistrictState(DistrictState state)
        {
            State = state;
            if(state == DistrictState.Broken)
            {
                BrokenImage.gameObject.SetActive(true);
            } else
            {
                BrokenImage.gameObject.SetActive(false);
            }

        }
    }
}
