using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BundleSizeAnalysis
{
    enum ValueType
    {
        NONE,
        SIZE,
        TEXTURES,
        MESHES,
        ANIMATIONS,
        SOUNDS,
        SHADERS,
        LEVELS,
        OTHER,

        MAX_COUNT,
    }

    class SingleInfo
    {
        public ValueType Type { private set; get; }
        public float Value { private set; get; }
        public string Unit { private set; get; }

        public SingleInfo(ValueType type, string value, string unit)
        {
            this.Type = type;
            this.Value = float.Parse(value);
            this.Unit = unit;
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", this.Type, this.Value, this.Unit);
        }
    }

    class BundleInfo
    {
        public string Name { get; private set; }
        private SingleInfo[] infos = new SingleInfo[(int)ValueType.MAX_COUNT];
        public readonly List<string> Assets = new List<string>(20);

        public SingleInfo GetInfoByType(ValueType type)
        {
            return infos[(int)type];
        }

        public BundleInfo(string name, string size, string unit)
        {
            Name = name;
            infos[(int)ValueType.NONE] = new SingleInfo(ValueType.NONE, size, unit);
            infos[(int)ValueType.SIZE] = new SingleInfo(ValueType.SIZE, size, unit);
        }

        public BundleInfo(string name, string size, string unit, string texture, string textureunit, string meshes, string meshesunit, string aniamtions, string aniamtionsunit, string sounds, string soundsunit, string shaders, string shadersunit, string levels, string levelsunit, string other, string otherunit)
        {
            infos[(int)ValueType.NONE] = new SingleInfo(ValueType.NONE, name, unit);
            infos[(int)ValueType.SIZE] = new SingleInfo(ValueType.SIZE, name, unit);
            infos[(int)ValueType.TEXTURES] = new SingleInfo(ValueType.TEXTURES, texture, textureunit);
            infos[(int)ValueType.MESHES] = new SingleInfo(ValueType.MESHES, meshes, meshesunit);
            infos[(int)ValueType.ANIMATIONS] = new SingleInfo(ValueType.ANIMATIONS, aniamtions, aniamtionsunit);
            infos[(int)ValueType.SOUNDS] = new SingleInfo(ValueType.SOUNDS, sounds, soundsunit);
            infos[(int)ValueType.SHADERS] = new SingleInfo(ValueType.SHADERS, shaders, shadersunit);
            infos[(int)ValueType.LEVELS] = new SingleInfo(ValueType.LEVELS, levels, levelsunit);
            infos[(int)ValueType.OTHER] = new SingleInfo(ValueType.OTHER, other, otherunit);
        }

        public void SetValue(ValueType type, string value, string unit)
        {
            infos[(int)type] = new SingleInfo(type, value, unit);
        }

        public SingleInfo GetValue(ValueType type) 
        {
            return infos[(int)type];
        }

        public void AddAsset(string asset)
        {
            this.Assets.Add(asset);
        }
    }
}
