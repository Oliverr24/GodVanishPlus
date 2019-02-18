using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace GodVanishPlus {
    public class VanishCommand : IRocketCommand {

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "vanish";

        public string Help => "Enable Vanish Mode!";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() {
            "godvanishplus.vanish"
        };

        public void Execute(IRocketPlayer caller, string[] command) {


            if (!caller.HasPermission("godvanishplus.vanish")) {
                UnturnedChat.Say(caller, "You do not have access to use this command!", Color.red);
                return;
            }


            if (caller is ConsolePlayer && command.Length < 1) {
                Rocket.Core.Logging.Logger.Log("This command cannot be called from console. Try /vanish <player>");
                return;
            }
            else if (caller is UnturnedPlayer && command.Length < 1) {
                UnturnedPlayer player = (UnturnedPlayer)caller;
                if (player.Features.VanishMode) {
                    UnturnedChat.Say(caller, GodVanishPlus.Instance.Configuration.Instance.vanishOffMsg, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.vanishColorOff, Color.red));
                    Rocket.Core.Logging.Logger.Log(caller.DisplayName + " has turned off Vanish Mode");
                    player.Features.VanishMode = false;
                    return;
                }
                else {
                    UnturnedChat.Say(caller, GodVanishPlus.Instance.Configuration.Instance.vanishOnMsg, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.vanishColorOn, Color.cyan));
                    Rocket.Core.Logging.Logger.Log(caller.DisplayName + " has turned on Vanish Mode");
                    player.Features.VanishMode = true;
                    return;
                }
            }
            else if (caller is ConsolePlayer && command.Length >= 1 || caller is UnturnedPlayer) {
                if (caller.HasPermission("nebulafalls.vanish.others")) {
                    UnturnedPlayer vanishPlayer = (UnturnedPlayer)UnturnedPlayer.FromName(command[0]);

                    if (vanishPlayer == null) {
                        UnturnedChat.Say(caller, "This player could not be found!", Color.red);
                        return;
                    }
                    else {
                        if (vanishPlayer.Features.VanishMode) {
                            vanishPlayer.Features.VanishMode = false;
                            UnturnedChat.Say(vanishPlayer, GodVanishPlus.Instance.Configuration.Instance.vanishOthersOff + caller.DisplayName, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.vanishOthersColor, Color.yellow));
                            UnturnedChat.Say(caller, GodVanishPlus.Instance.Configuration.Instance.takenOthersVanish + vanishPlayer.DisplayName, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.vanishOthersColor, Color.yellow));
                            Rocket.Core.Logging.Logger.Log(caller.DisplayName + " has turned off Vanish Mode for: " + vanishPlayer.DisplayName);
                            return;
                        }
                        else {
                            vanishPlayer.Features.VanishMode = true;
                            UnturnedChat.Say(vanishPlayer, GodVanishPlus.Instance.Configuration.Instance.vanishOthersOn + caller.DisplayName, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.vanishOthersColor, Color.yellow));
                            UnturnedChat.Say(caller, GodVanishPlus.Instance.Configuration.Instance.givenOthersVanish + vanishPlayer.DisplayName, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.vanishOthersColor, Color.yellow));
                            Rocket.Core.Logging.Logger.Log(caller.DisplayName + " has turned on Vanish Mode for: " + vanishPlayer.DisplayName);
                            return;
                        }
                    }
                }
                else {
                    UnturnedChat.Say(caller, "You do not have permission to give other players vanish!", Color.red);
                    return;
                }
            }
        }
    }
} 