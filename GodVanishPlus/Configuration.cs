using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodVanishPlus {
    public class Configuration : IRocketPluginConfiguration {

        public bool Enabled;
        public bool ChatAnnounce;
        public string ChatColor;
        public string vanishColorOn;
        public string godColorOn;
        public string vanishColorOff;
        public string godColorOff;
        public string godOnMsg;
        public string godOffMsg;
        public string vanishOnMsg;
        public string vanishOffMsg;
        public string godOthersOn;
        public string godOthersOff;
        public string givenOthersGod;
        public string takenOthersGod;
        public string godOthersColor;
        public string vanishOthersOn;
        public string vanishOthersOff;
        public string givenOthersVanish;
        public string takenOthersVanish;
        public string vanishOthersColor;
        public string checkModeMessageInMode;
        public string checkModeMessageOutMode;
        public string checkModeMessageColor;
        public List<string> StaffGroups;



        public void LoadDefaults() {
            Enabled = true;
            ChatAnnounce = true;
            ChatColor = "yellow";
            godColorOn = "Cyan";
            vanishColorOn = "Cyan";
            godColorOff = "Cyan";
            vanishColorOff = "Cyan";
            godOnMsg = "God mode has been enabled!";
            godOffMsg = "God mode has been disabled!";
            vanishOnMsg = "Vanish mode has been enabled!";
            vanishOffMsg = "Vanish mode has been disable!";
            godOthersOn = "You have been given god mode by: ";
            godOthersOff = "God mode has been revoked by: ";
            givenOthersGod = "You have given god mode to: ";
            takenOthersGod = "You have revoked god mode from: ";
            godOthersColor = "Yellow";
            vanishOthersOn = "You have been vanished by: ";
            vanishOthersOff = "Vanish has been revoked by: ";
            givenOthersVanish = "You have given vanish mode to: ";
            takenOthersVanish = "You have revoked vanish from: ";
            vanishOthersColor = "Yellow";
            checkModeMessageInMode = " is in staff mode. They will not kill! Modes:";
            checkModeMessageOutMode = " is playing legit!";
            checkModeMessageColor = "Yellow";
            StaffGroups = new List<string> {
                "mod",
                "admin"
            };
        }

    }
}
