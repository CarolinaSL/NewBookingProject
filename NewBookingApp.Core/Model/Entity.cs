﻿namespace NewBookingApp.Core.Model
{
    public abstract class Entity
    {
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public long? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}