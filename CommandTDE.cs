using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace DeathEffects
{
    public class CommandTriggerDeathEffects : IRocketCommand
    {

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (DeathEffects.Instance.Configuration.Instance.CommandEnabled)
            {
                if (command.Length == 0)
                {
                    UnturnedPlayer player = (UnturnedPlayer)caller;
                    ushort[] Effects = DeathEffects.Instance.Configuration.Instance.Effects.ToArray();
                    for (int i = 0; i < Effects.Length; i++)
                        EffectManager.sendEffect(Effects[i], 30, player.Position);
                    UnturnedChat.Say(caller, DeathEffects.Instance.Translate("DeathEffects_Spawned_Self"), UnturnedChat.GetColorFromName(DeathEffects.Instance.Configuration.Instance.ChatMSGColor, Color.red));
                }
                if (command.Length > 0)
                {
                    UnturnedPlayer target = UnturnedPlayer.FromName(command[0]);
                    UnturnedPlayer player = (UnturnedPlayer)caller;

                    if (target == null)
                    {
                        string GivenName = string.Join(" ", command);
                        UnturnedChat.Say(caller, DeathEffects.Instance.Translate("player_not_found", GivenName), UnturnedChat.GetColorFromName(DeathEffects.Instance.Configuration.Instance.ChatMSGColor, Color.red)); return;
                    }

                    ushort[] Effects2 = DeathEffects.Instance.Configuration.Instance.Effects.ToArray();
                    for (int i = 0; i < Effects2.Length; i++)
                        EffectManager.sendEffect(Effects2[i], 30, target.Position);
                    UnturnedChat.Say(caller, DeathEffects.Instance.Translate("DeathEffects_Spawned_Player", target.DisplayName), UnturnedChat.GetColorFromName(DeathEffects.Instance.Configuration.Instance.ChatMSGColor, Color.red)); return;                }
            }
            else
                UnturnedChat.Say(caller, DeathEffects.Instance.Translate("Command_Disabled"), UnturnedChat.GetColorFromName(DeathEffects.Instance.Configuration.Instance.ChatMSGColor, Color.red));
        }
        public string Name
        {
            get { return "TriggerDeathEffects"; }
        }
           public string Syntax
        {
            get { return " (nothing) | <player>"; }
        }

        public string Help
        {
            get { return "Triggers Death Effects on you or the given player."; }
        }

        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public List<string> Permissions
        {
            get { return new List<string>() {"TriggerDeathEffects"}; }
        }
        public List<string> Aliases
        {
            get { return new List<string>() {"tde"}; }
        }
    }
}
