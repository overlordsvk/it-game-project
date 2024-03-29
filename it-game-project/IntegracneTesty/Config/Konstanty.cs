﻿using Game.DAL.Entities;
using Game.DAL.Entity.Entities;
using Game.DAL.Enums;
using System;
using System.Collections.Generic;

namespace IntegracneTesty.Config
{
    public class Konstanty
    {
        public static readonly Guid guidItemSekera = Guid.Parse("629fd444-0146-4b94-a928-e62a4ab51f42");
        public static readonly Guid guidItemMec = Guid.Parse("84124fe7-e59b-411d-a76e-73b81bf233c2");
        public static readonly Guid guidAccountPeter = Guid.Parse("5092aae4-3f07-4a0e-afbb-bc094a3cc73e");
        public static readonly Guid guidAccountIvan = Guid.Parse("b49c2b2f-99b0-4f21-8655-70b0d2c7c5d2");
        public static readonly Guid guidAccountVedro = Guid.Parse("3454b2db-4bb1-4ffa-a3e9-be8c9617252d");
        public static readonly Guid guidGroupPrvaSkupina = Guid.Parse("e9c469c0-60e0-437e-ad8f-c9a742e551b0");
        public static readonly Guid guidFightSlayerWalker = Guid.Parse("5faff0b1-78ac-4ac1-be7c-dd7a83250a5c");
        public static readonly Guid guidgPostPrvejSkupiny = Guid.Parse("ad311795-bf7b-4240-a643-4760fee1106c");
        public static readonly Guid guidChatSlayerWalker = Guid.Parse("325ded93-ea55-42a8-a585-2e5495fe5df1");
        public static readonly Guid guidMessageSlayerWalker = Guid.Parse("12eb575d-be07-43db-be9e-8c7add0e4bb6");
        public static readonly Guid guidMessageWalkerSlayer = Guid.Parse("65016352-7a07-442a-b439-5f4516e90c70");
        public static readonly Guid guidItemLuk = Guid.Parse("b7701e3c-e06c-4f0d-8aef-4b23901b5d95");
        public static readonly Guid _guid4 = Guid.Parse("bdf0f556-5168-4e1f-8697-94c6f54a09a5");
        public static readonly Guid _guid5 = Guid.Parse("c528f80c-874e-472d-9b54-92606c44be50");
        public static readonly Guid _guid6 = Guid.Parse("4bfc8e4b-85d4-47f5-ae87-080a80f750c5");
        public static readonly Guid _guid16 = Guid.Parse("25ce42c3-20e7-4d5c-b9cc-56ab5a64c9f6");
        public static readonly Guid _guid17 = Guid.Parse("931f192b-452d-469d-aaf4-af9b18886769");
        public static readonly Guid _guid18 = Guid.Parse("5fd2c0ea-f31d-482b-a1b7-4650495ad346");
        public static readonly Guid _guid19 = Guid.Parse("abb4f59a-8fed-4060-ac96-be827992054c");
        public static readonly Guid _guid20 = Guid.Parse("46f7bc5a-c043-4722-9438-6e6e7640fc71");

        public static readonly Item itemSekera = new Item
        {
            Id = guidItemSekera,
            Name = "Sekera",
            Attack = 20,
            Defense = 5,
            Weight = 12,
            ItemType = ItemType.Weapon,
            Equipped = true
        };

        public static readonly Item itemMec = new Item
        {
            Id = guidItemMec,
            Name = "Mec",
            Attack = 25,
            Defense = 10,
            Weight = 8,
            ItemType = ItemType.Weapon
        };

        public static readonly Item itemLuk = new Item
        {
            Id = guidItemLuk,
            Name = "Luk",
            Attack = 44,
            Defense = 3,
            Weight = 3,
            ItemType = ItemType.Weapon,
            Equipped = true
        };

        public static readonly Character characterSlayer = new Character
        {
            Name = "KingSlayer",
            Money = 666,
            Health = 98,
            Score = 12,
            Strength = 5,
            Perception = 8,
            Endurance = 2,
            Charisma = 8,
            Intelligence = 1,
            Agility = 5,
            Luck = 9,
            Items = new List<Item>
            {
                itemSekera
            }
        };

        public static readonly Character characterBela = new Character
        {
            Name = "BelaKing",
            Money = 666,
            Health = 98,
            Score = 12,
            Strength = 5,
            Perception = 8,
            Endurance = 2,
            Charisma = 8,
            Intelligence = 1,
            Agility = 5,
            Luck = 9,
            Items = new List<Item>
            {
                itemMec
            }
        };

        public static readonly Character characterWalker = new Character
        {
            Name = "White Walker",
            Money = 1200,
            Health = 50,
            Score = 89,
            Strength = 9,
            Perception = 2,
            Endurance = 5,
            Charisma = 0,
            Intelligence = 2,
            Agility = 6,
            Luck = 4,
            Items = new List<Item>
            {
                itemLuk
            }
        };

        public static readonly Account accountPeter = new Account
        {
            Id = guidAccountPeter,
            Username = "Pieter",
            Email = "pieter@gmail.com",
            PasswordSalt = "12345678",
            PasswordHash = "12345678",
            Roles = "Admin",
            Character = characterBela,
        };

        public static readonly Account accountIvan = new Account
        {
            Id = guidAccountIvan,
            Username = "Ivan",
            Email = "navi@ivan.com",
            PasswordSalt = "IvanJeBoh",
            PasswordHash = "IvanJeBoh",
            Roles = "",
            Character = characterSlayer
        };

        public static readonly Account accountVedro = new Account
        {
            Id = guidAccountVedro,
            Username = "Vedro",
            Email = "vedro@vemail.com",
            PasswordSalt = "QWE123975",
            PasswordHash = "QWE123975",
            Roles = "",
            Character = characterWalker
        };

        public static readonly Group groupPrvaSkupina = new Group
        {
            Id = guidGroupPrvaSkupina,
            Name = "Prva skupina",
            Description = "My sme prva skupina!",
            Members = new List<Character>
            {
                characterSlayer,
                characterWalker
            }
        };

        public static readonly Fight fightSlayerWalker = new Fight
        {
            Id = guidFightSlayerWalker,
            Attacker = characterSlayer,
            Defender = characterWalker,
            AttackerWeapon = itemSekera,
            DefenderWeapon = itemLuk,
            Timestamp = DateTime.Now,
            AttackSuccess = true
        };

        public static readonly GroupPost gPostPrvejSkupiny = new GroupPost
        {
            Id = guidgPostPrvejSkupiny,
            Author = characterSlayer,
            Group = groupPrvaSkupina,
            Text = "Hi",
            Timestamp = DateTime.Now
        };

        public static readonly Chat chatSlayerWalker = new Chat
        {
            Id = guidChatSlayerWalker,
            Receiver = characterSlayer,
            Sender = characterWalker,
            Subject = "Destruction",
        };

        public static readonly Message messageSlayerWalker = new Message
        {
            Id = guidMessageSlayerWalker,
            Author = characterSlayer,
            Chat = chatSlayerWalker,
            Text = "This is war",
            Timestamp = DateTime.Now,
        };

        public static readonly Message messageWalkerSlayer = new Message
        {
            Id = guidMessageWalkerSlayer,
            Author = characterWalker,
            Chat = chatSlayerWalker,
            Text = "ok",
            Timestamp = DateTime.Now,
        };
    }
}