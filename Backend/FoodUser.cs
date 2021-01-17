using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class FoodUser
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public long IdFood { get; set; }

        public virtual Food IdFoodNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
