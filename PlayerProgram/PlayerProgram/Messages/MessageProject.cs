﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MessageProject
{
    public abstract class Message
    {
        [XmlInclude(typeof(JoinGame))]
        [XmlInclude(typeof(RegisteredGames))]
        [XmlInclude(typeof(ConfirmGameRegistration))]
        //Function messageIntoXML and xmlIntoMessage are supposed to be cutted and pasted into player, master and server project files.
        public string messageIntoXML(Message mess)
        {
            XmlSerializer xmlSerial = new XmlSerializer(typeof(Message), new Type[] {typeof(JoinGame),typeof(ConfirmGameRegistration),typeof(ConfirmJoiningGame),
            typeof(GameMessage),typeof(GetGames),typeof(RegisteredGames),typeof(RegisterGame),typeof(RejectJoiningGame)});
            //XmlSerializer xmlSerial = new XmlSerializer(mess.GetType());

            StringWriter textWriter = new StringWriter();
            xmlSerial.Serialize(textWriter, mess);

            return textWriter.ToString();
        }

        public Message xmlIntoMessage(String mess)
        {
            XmlSerializer xmlSerial = new XmlSerializer(typeof(Message), new Type[] {typeof(JoinGame),typeof(ConfirmGameRegistration),typeof(ConfirmJoiningGame),
            typeof(GameMessage),typeof(GetGames),typeof(RegisteredGames),typeof(RegisterGame),typeof(RejectJoiningGame)});

            StringReader textReader = new StringReader(mess);
            Message message = (Message)xmlSerial.Deserialize(textReader);

            return message;
        }
    }

    public enum Role
    {
        member,
        leader
    }

    public enum Team
    {
        blue,
        red
    }

    public class GameInfo
    {
        public string name { get; set; }
        public int blueTeamPlayers { get; set; }
        public int redTeamPlayers { get; set; }

        public GameInfo() { }

        public GameInfo(String Name, int blue, int red)
        {
            name = Name;
            blueTeamPlayers = blue;
            redTeamPlayers = red;
        }
    }

    public class Player
    {
        public int playerID { get; set; }
        public Team team { get; set; }
        public Role role { get; set; }

        public Player() { }

        public Player(int id, Team tea, Role rol)
        {
            playerID = id;
            team = tea;
            role = rol;
        }
    }

    public class Board
    {
        public int width { get; set; }
        public int goalAreaHeight { get; set; }
        public int taskAreaHeight { get; set; }

        public Board() { }

        public Board(int wid, int goalHeight, int taskHeight)
        {
            width = wid;
            goalAreaHeight = goalHeight;
            taskAreaHeight = taskHeight;
        }
    }

    public class PlayerLocation
    {
        public int x { get; set; }
        public int y { get; set; }

        public PlayerLocation() { }

        public PlayerLocation(int xx, int yy)
        {
            x = xx;
            y = yy;
        }
    }

}
