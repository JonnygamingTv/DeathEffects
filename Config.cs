using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeathEffects
{
    public class Config : IRocketPluginConfiguration
    {
        public bool Enabled;
        public bool EffectsOnDeath;
        public bool EffectsOnConnect;
        public bool EffectsOnDisconnect;
        public bool CommandEnabled;
        public string ChatMSGColor;
      
        [XmlArrayItem(ElementName = "Effect")]
        public List<ushort> Effects;
        
        public void LoadDefaults()
        {
            Enabled = true;
            EffectsOnDeath = true;
            ChatMSGColor = "red";
            EffectsOnConnect = true;
            CommandEnabled = true;
            EffectsOnDisconnect = true;
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