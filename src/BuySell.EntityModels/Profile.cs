﻿using System.ComponentModel.DataAnnotations;

namespace BuySell.EntityModels
{
    public class Profile:BaseCoreEntity
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string ProfileType { get; set; }

        public int ItemSoldPurchased { get; set; }
        public string Address { get; set; }

        public byte[] Content { get; set; }

        public string ImagePath { get; set; }
    }
}