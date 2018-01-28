using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameLogic.District
{
    public struct DistrictRule
    {
        public int PointsValue;
        public DistrictType Type;
    }

    public class DistrictRules : MonoBehaviour
    {
        public DistrictRule[] DistrictRulesArray = new DistrictRule[]
        {
            new DistrictRule()
            {
                Type = DistrictType.School,
                PointsValue = 2,
            },
            new DistrictRule()
            {
                Type = DistrictType.Intelligence,
                PointsValue = 3,
            },
            new DistrictRule()
            {
                Type = DistrictType.CounterIntel,
                PointsValue = 5,
            },
            new DistrictRule()
            {
                Type = DistrictType.Fact,
                PointsValue = 8,
            }
        };

        public List<DistrictTile> DistrictList = new List<DistrictTile>();
        private List<DistrictType> __completedType;

        private List<DistrictType> _completedType
        {
            get
            {
                if (__completedType == null)
                {
                    __completedType = new List<DistrictType>();
                    foreach(DistrictTile tile in DistrictList)
                    {
                        if (tile.Type != DistrictType.Normal)
                            __completedType.Add(tile.Type);
                    }
                }
                return __completedType;
            }
        }

        public bool SetDistrictPoints(int points)
        {
            foreach(DistrictRule districtRule in DistrictRulesArray)
            {
                var unlocked = points >= districtRule.PointsValue;
                if (unlocked)
                {
                    if (_completedType.Contains(districtRule.Type))
                        continue;

                    return UnlockDistrictType(districtRule.Type);
                }
            }
            return false;
        }

        public void RecoverDistrict()
        {
            DistrictList.Sort(new DistrictRecoverComparer());

            foreach (DistrictTile tile in DistrictList)
            {
                if (tile.State == DistrictState.Broken)
                {
                    tile.ChangeDistrictState(DistrictState.Fine);
                    break;
                }
            }
        }

        public void BreakDistrict()
        {
            DistrictList.Sort(new DistrictDestroyComparer());

            foreach (DistrictTile tile in DistrictList)
            {
                if (tile.State == DistrictState.Fine)
                {
                    tile.ChangeDistrictState(DistrictState.Broken);
                    break;
                }
            }
        }

        public bool HasFineDistricts()
        {
            foreach (DistrictTile tile in DistrictList)
            {
                if (tile.State == DistrictState.Fine)
                    return true;
            }

            return false;
        }

        private bool UnlockDistrictType(DistrictType type)
        {
            foreach(DistrictTile tile in DistrictList)
            {
                if (tile.Type == DistrictType.Normal && tile.State == DistrictState.Fine)
                {
                    tile.ChangeDistrictType(type);
                    _completedType.Add(type);
                    return true;
                }
            }
            return false;
        }


        public bool HasDistrictType(DistrictType type)
        {
            foreach (DistrictTile tile in DistrictList)
            {
                if (tile.Type == type && tile.State == DistrictState.Fine)
                    return true;
            }
            return false;
        }

        private class DistrictRecoverComparer : IComparer<DistrictTile>
        {
            public int Compare(DistrictTile left, DistrictTile right)
            {
                if (left == null)
                {
                    if (right == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (right == null)
                    {
                        return 1;
                    }
                    else
                    {
                        return right.Type - left.Type;
                    }
                }
            }
        }

        private class DistrictDestroyComparer : IComparer<DistrictTile>
        {
            public int Compare(DistrictTile left, DistrictTile right)
            {
                if (left == null)
                {
                    if (right == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (right == null)
                    {
                        return 1;
                    }
                    else
                    {
                        return left.Type - right.Type;
                    }
                }
            }
        }
    }
}
