using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using Steamworks;
using SDG.Unturned;
using Rocket.Unturned;
using Rocket.API.Collections;
using Rocket.API;
using System.Collections.Generic;

namespace DeathEffects
{
    public class DeathEffects : RocketPlugin<Config>
    {
        public static DeathEffects Instance;

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList() {
                    {"player_not_found","Cannot find the player: {0}."},
                    {"Command_Disabled","The Command Is Currently Disabled!"},
                    {"DeathEffects_Spawned_Self","Successfully spawned Death Effects on you."},
                    {"DeathEffects_Spawned_Player","Spawned Death Effects on the player: {0}."}
                };
            }
        }

        protected override void Load()
        {
            Instance = this;
            if (!Configuration.Instance.Enabled)
            {
                Rocket.Core.Logging.Logger.Log("The plugin is disabled via the config file!");
                base.UnloadPlugin();
                return;
            }
            Rocket.Core.Logging.Logger.Log("Plugin Loaded!\r\nCreated by xXThe_HunterXx\r\nHave Fun! :)");

            Rocket.Core.Logging.Logger.Log("Checking Configuration...");

            if (Configuration.Instance.EffectsOnConnect)
            {
                Rocket.Core.Logging.Logger.Log("Effects on Connect Enabled!");
            }
            else
            {
                Rocket.Core.Logging.Logger.Log("Effects on Connect Disabled!");
            }

            if (Configuration.Instance.EffectsOnDisconnect)
            {
                Rocket.Core.Logging.Logger.Log("Effects on Disconnect Enabled!");
            }
            else
            {
                Rocket.Core.Logging.Logger.Log("Effects on Disconnect Disabled!");
            }


            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
            U.Events.OnPlayerConnected += OnPlayerConnected;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
        }

        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.Log("Plugin unloaded!");
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            U.Events.OnPlayerConnected -= OnPlayerDisconnected;
        }

        public void OnPlayerConnected(UnturnedPlayer player)
        {
            if (Configuration.Instance.EffectsOnConnect && player.IsAdmin || Configuration.Instance.EffectsOnConnect && player.HasPermission("DeathEffects"))
            {
                ushort[] Effects = Instance.Configuration.Instance.Effects.ToArray();
                for (int i = 0; i < Effects.Length; i++)
                    EffectManager.sendEffect(Effects[i], 30, player.Position);
            }
        }

        public void OnPlayerDisconnected(UnturnedPlayer player)
        {
            if (Configuration.Instance.EffectsOnDisconnect && player.IsAdmin || Configuration.Instance.EffectsOnDisconnect && player.HasPermission("DeathEffects"))
            {
                ushort[] Effects = Instance.Configuration.Instance.Effects.ToArray();
                for (int i = 0; i < Effects.Length; i++)
                    EffectManager.sendEffect(Effects[i], 30, player.Position);
            }
            else { return; }
        }

        private void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            if (Configuration.Instance.EffectsOnDeath && player.IsAdmin || Configuration.Instance.EffectsOnDeath && player.HasPermission("DeathEffects"))
            {
                ushort[] Effects = Instance.Configuration.Instance.Effects.ToArray();
                for (int i = 0; i < Effects.Length; i++)
                    EffectManager.sendEffect(Effects[i], 30, player.Position);
            }
        }

        public List<UnturnedPlayer> Players()
        {
            List<UnturnedPlayer> list = new List<UnturnedPlayer>();

            foreach (SteamPlayer sp in Provider.clients)
            {
                UnturnedPlayer p = UnturnedPlayer.FromSteamPlayer(sp);
                list.Add(p);
            }

            return list;
        }


    }
}
