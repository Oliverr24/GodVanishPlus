using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;


namespace GodVanishPlus {
    public class GodVanishPlus : RocketPlugin<Configuration> {

        public static GodVanishPlus Instance;

        public string directory = System.IO.Directory.GetCurrentDirectory() + "/..";

        protected override void Load() {
            Instance = this;

            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;

            if (File.Exists(directory + "/Staff-Kills.txt")) {
                Rocket.Core.Logging.Logger.Log("Staff-Kills.txt file has already been created!");
            } else {
                File.CreateText(directory + "/Staff-Kills.txt");
                Rocket.Core.Logging.Logger.Log("Staff-Kills.txt has been created!");
            }

            if (Configuration.Instance.Enabled == false) {
                Rocket.Core.Logging.Logger.Log("Plugin has been disabled in the configuration file. Unloaded!");
                base.UnloadPlugin();
            }

            Rocket.Core.Logging.Logger.Log("GodVanishPlus has loaded!");
        }

        protected override void Unload() {
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            Rocket.Core.Logging.Logger.Log("GodVanishPlus has unloaded!");
        }

        private void FixedUpdate() {

        }

        private void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer) {
            string path = directory + "/Staff-Kills.txt";
            List<string> Staff = new List<string>(Configuration.Instance.StaffGroups);

            if (Configuration.Instance.ChatAnnounce == true) {
                if (!(player is UnturnedPlayer)) {
                    return;
                } else {
                    UnturnedPlayer killer = (UnturnedPlayer)UnturnedPlayer.FromCSteamID(murderer);
                    foreach (RocketPermissionsGroup pGroup in R.Permissions.GetGroups(killer, true)) {
                        if (Configuration.Instance.StaffGroups.Contains(pGroup.Id)) {
                            if (killer.Features.GodMode) {
                                UnturnedChat.Say(player.DisplayName + " was killed by: " + killer.DisplayName + ". They were in: God Mode!");
                                File.AppendAllText(path, DateTime.Now.ToString() + "[##GOD ABUSE##]" + player.DisplayName + " was killed by " + killer.DisplayName + "." + System.Environment.NewLine);
                                return;
                            } else if (killer.Features.VanishMode) {
                                UnturnedChat.Say(player.DisplayName + " was killed by: " + killer.DisplayName + ". They were in: Vanish Mode!");
                                File.AppendAllText(path, DateTime.Now.ToString() + "[##VANISH ABUSE##]" + player.DisplayName + " was killed by " + killer.DisplayName + "." + System.Environment.NewLine);
                                return;
                            } else if (killer.Features.GodMode && killer.Features.VanishMode) {
                                UnturnedChat.Say(player.DisplayName + " was killed by: " + killer.DisplayName + ". They were in: God Mode and Vanish Mode!");
                                File.AppendAllText(path, DateTime.Now.ToString() + "[##GOD AND VANISH ABUSE##]" + player.DisplayName + " was killed by " + killer.DisplayName + "." + System.Environment.NewLine);
                                return;
                            } else {
                                return;
                            }
                        }
                    }
                }
            }

        }

    }
}
