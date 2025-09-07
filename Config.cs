using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeathEffects
{
    public class Config : IRocketPluginConfiguration
    {
        public bool Enabled;
        public bool EffectsOnDeath;
        public bool ClearEffectsOnRespawn;
        public bool EffectsOnConnect;
        public bool EffectsOnDisconnect;
        public bool CommandEnabled;
        public string ChatMSGColor;
      
        [XmlArrayItem(ElementName = "Effect")]
        public List<ushort> Effects;

        [XmlIgnore]
        public System.Collections.ObjectModel.ReadOnlyCollection<ushort> _Effects;

        public void LoadDefaults()
        {
            Enabled = true;
            EffectsOnDeath = false;
            ClearEffectsOnRespawn = true;
            EffectsOnConnect = false;
            EffectsOnDisconnect = false;
            CommandEnabled = true;
            ChatMSGColor = "red";
            Effects = new List<ushort>()
            {
            120,
            133,
            127,
            119,
            128
        };
        }
    }
}