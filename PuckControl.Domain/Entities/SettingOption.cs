﻿
namespace PuckControl.Domain.Entities
{
    public class SettingOption : AbstractEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsSelected { get; set; }
    }
}
