﻿namespace EventPlanning.Models
{
    public class FieldModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int? EventId { get; set; }
    }
}