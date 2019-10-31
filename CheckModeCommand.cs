using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace GodVanishPlus {
    public class CheckModeCommand : IRocketCommand {

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "checkmode";

        public string Help => "Check if someone is in God or Vanish!";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() {
            "godvanishplus.checkmode"
        };

        public void Execute(IRocketPlayer caller, string[] command) {

            if (!caller.HasPermission("godvanishplus.checkmode")) {
                UnturnedChat.Say(caller, "You do not have permission to use this command!", Color.red);
                return;
            }

            if (command.Length < 1) {
                UnturnedChat.Say(caller, "Incorrect usage! Use: /checkmode <player>");
                return;
            }
            else if (command.Length >= 1) {

                UnturnedPlayer target = (UnturnedPlayer)UnturnedPlayer.FromName(command[0]);

                if (target == null) {
                    UnturnedChat.Say(caller, command[0] + " was not found. Please try again!", Color.red);
                }

                bool isGod = target.Features.GodMode;
                bool isVanish = target.Features.VanishMode;
                bool isAdmin = target.IsAdmin;

                if (isGod && isVanish && isAdmin) {
                    UnturnedChat.Say(caller, target.DisplayName + GodVanishPlus.Instance.Configuration.Instance.checkModeMessageInMode + " Admin, God and Vanish.", UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.checkModeMessageColor, Color.cyan));
                    return;
                }
                else if (isGod && isVanish) {
                    UnturnedChat.Say(caller, target.DisplayName + GodVanishPlus.Instance.Configuration.Instance.checkModeMessageInMode + " God and Vanish.", UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.checkModeMessageColor, Color.cyan));
                    return;
                }
                else if (isGod && isAdmin) {
                    UnturnedChat.Say(caller, target.DisplayName + GodVanishPlus.Instance.Configuration.Instance.checkModeMessageInMode + " Admin and God.", UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.checkModeMessageColor, Color.cyan));
                    return;
                }
                else if (isVanish && isAdmin) {
                    UnturnedChat.Say(caller, target.DisplayName + GodVanishPlus.Instance.Configuration.Instance.checkModeMessageInMode + " Admin and Vanish.", UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.checkModeMessageColor, Color.cyan));
                    return;
                }
                else if (isGod) {
                    UnturnedChat.Say(caller, target.DisplayName + GodVanishPlus.Instance.Configuration.Instance.checkModeMessageInMode + "  God.", UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.checkModeMessageColor, Color.cyan));
                    return;
                }
                else if (isVanish) {
                    UnturnedChat.Say(caller, target.DisplayName + GodVanishPlus.Instance.Configuration.Instance.checkModeMessageInMode + " Vanish.", UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.checkModeMessageColor, Color.cyan));
                    return;
                }
                else if (isAdmin) {
                    UnturnedChat.Say(caller, target.DisplayName + GodVanishPlus.Instance.Configuration.Instance.checkModeMessageInMode + " Admin.", UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.checkModeMessageColor, Color.cyan));
                    return;
                }
                else {
                    UnturnedChat.Say(caller, target.DisplayName + GodVanishPlus.Instance.Configuration.Instance.checkModeMessageOutMode, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.checkModeMessageColor, Color.cyan));
                    return;
                }


            }

        }

    }
}
