using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class FoodRate
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long FoodId { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }

        public virtual Food Food { get; set; }
        public virtual User User { get; set; }
    }
}
