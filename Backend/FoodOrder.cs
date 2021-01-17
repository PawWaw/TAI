using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class FoodOrder
    {
        public long Id { get; set; }
        public long FoodId { get; set; }
        public long OrderId { get; set; }

        public virtual Food Food { get; set; }
        public virtual Order Order { get; set; }
    }
}
