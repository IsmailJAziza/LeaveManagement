﻿namespace LeaveManagement.Data.DataModel
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumbersOfDays { get; set; }

        public IEnumerable<LeaveAllocation>? LeaveAllocations { get; set; } = new List<LeaveAllocation>();
    }
}
